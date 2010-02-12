using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace LPS.Client
{
	public class ChangesUpdater : IDisposable
	{
		private int sink;
		private int security;
		private LPSClientShared.LPSServer.Server server;
		private bool terminate;
		private Dictionary<string, List<DataSet>> datasets;

		public ChangesUpdater(string url, string login, string password, int sink, int security)
		{
			this.datasets = new Dictionary<string, List<DataSet>>();
			this.terminate = false;
			this.server = new LPSClientShared.LPSServer.Server(url);
			this.server.CookieContainer = new System.Net.CookieContainer();
			this.server.Login(login, password);
			this.sink = sink;
			this.security = security;
			lock(typeof(GLib.Thread))
			{
				if(!GLib.Thread.Supported)
					GLib.Thread.Init();
			}
			CheckUpdatesAsync();
		}

		/// <summary>
		/// Add dataset to collection by table name and assign delegate
		/// that atomatically remove dataset when it dataset.disposed
		/// </summary>
		public void AddDataSet(string tablename, DataSet ds)
		{
			lock(this)
			{
				List<DataSet> dslist;
				if(datasets.TryGetValue(tablename, out dslist))
				{
					dslist.Add(ds);
				}
				else
				{
					dslist = new List<DataSet>();
					datasets[tablename] = dslist;
					dslist.Add(ds);
				}
				ds.Disposed += delegate {
					lock(this)
					{
						dslist.Remove(ds);
					}
				};
			}
		}

		private void CheckUpdatesAsync()
		{
			//Console.WriteLine("{0} - Thd{1}: CheckUpdatesAsync", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
			lock(this)
			{
				if(this.terminate)
					return;
				server.BeginGetChanges(sink, security, CheckUpdatesAsyncEnd, null);
			}
		}

		private LPSClientShared.LPSServer.ChangeInfo[] l_changes;
		private void CheckUpdatesAsyncEnd(IAsyncResult aresult)
		{
			//Console.WriteLine("{0} - Thd{1}: CheckUpdatesAsyncEnd", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
			LPSClientShared.LPSServer.ServerCallResult result;
			lock(this)
			{
				result = server.EndGetChanges(aresult);
				if(this.terminate)
					return;
			}
			if(result.Changes != null && result.Changes.Length > 0)
			{
				//Console.WriteLine("{0} - Thd{1}: CheckUpdatesAsyncEnd - changes found!", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
				lock(this)
					l_changes = result.Changes;
				GLib.Timeout.Add(0, CheckUpdatesSync);
			}
			else
			{
				//Console.WriteLine("{0} - Thd{1}: CheckUpdatesAsyncEnd - changes not found, waiting 1s", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
				Thread.Sleep(3000);
				lock(this)
				{
					if(this.terminate)
						return;
				}
				CheckUpdatesAsync();
			}
		}

		private bool CheckUpdatesSync()
		{
			Console.WriteLine("{0} - Thd{1}: CheckUpdatesAsyncEnd", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
			LPSClientShared.LPSServer.ChangeInfo[] changes;
			lock(this)
			{
				if(this.terminate)
					return false;
				changes = l_changes;
				l_changes = null;
			}
			if(changes == null)
			{
				Console.WriteLine("{0} - Thd{1}: CheckUpdatesAsyncEnd changes == null!!!", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
				CheckUpdatesAsync();
				return false;
			}
			else
			{
				DoUpdates(changes);
				CheckUpdatesAsync();
				return false;
			}
		}

		public void DoUpdates(LPSClientShared.LPSServer.ChangeInfo[] changes)
		{
			Console.WriteLine("{0} - Thd{1}: DoUpdates", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
			lock(this)
			{
				foreach(LPSClientShared.LPSServer.ChangeInfo change in changes)
				{
					Console.WriteLine(" * Modify DT: {0}: table {1} {2}",
						change.ModifyDateTime,
						change.TableName,
						change.HasDeletedRows?"was updated and some rows deleted":"was updated");
					List<DataSet> dslist;
					if(datasets.TryGetValue(change.TableName, out dslist))
					{
						foreach(DataSet ds in dslist)
						{
							DateTime last_dt = change.ModifyDateTime;
							using(DataSet updates = GetDataSetUpdates(ds, ref last_dt))
							{
								Console.WriteLine("Updating {0} rows in {1}",
									updates.Tables[0].Rows.Count,
									change.TableName);
								UpdateTableRows(ds.Tables[0], updates.Tables[0]);
								ds.ExtendedProperties["DATETIME"] = last_dt;
							}
						}
					}
				}
			}
		}

		private void UpdateTableRows(DataTable dest, DataTable src)
		{
			dest.BeginLoadData();
			try
			{
				foreach(DataRow r in src.Rows)
				{
					dest.LoadDataRow(r.ItemArray, LoadOption.PreserveChanges);
				}
			}
			finally
			{
				dest.EndLoadData();
			}
		}

		private DataSet GetDataSetUpdates(DataSet ds, ref DateTime last_dt)
		{
			string list = (string)ds.ExtendedProperties["LIST"];
			string addsql = (string)ds.ExtendedProperties["ADDSQL"];
			object[] parameters = (object[])ds.ExtendedProperties["PARAMS"];
			DateTime dt_last = (DateTime)ds.ExtendedProperties["DATETIME"];
			if(String.IsNullOrEmpty(addsql))
			{
				if(parameters.Length == 0)
				{
					parameters = new object[] { "ts_last", dt_last };
					addsql = "(ts >= :ts_last)";
				}
				else
				{
					addsql = "";
					for(int i=0; i<parameters.Length; i+=2)
						addsql += String.Format("({0}=:{0}) and ", (string)parameters[i]);
					addsql += "(ts >= :ts_last)";
					ArrayList param2 = new ArrayList(parameters);
					param2.Add("ts_last");
					param2.Add(dt_last);
					parameters = param2.ToArray();
				}
			}
			else
			{
				addsql += " and (ts >= :ts_last)";
				ArrayList param2 = new ArrayList(parameters);
				param2.Add("ts_last");
				param2.Add(dt_last);
				parameters = param2.ToArray();
			}

			DataSet result;
			LPSClientShared.LPSServer.ServerCallResult callrslt;
			callrslt = server.GetDataSetByName(-1, 0, list, addsql, parameters, out result);
			last_dt = callrslt.DateTime;
			if(callrslt != null && callrslt.Exception != null)
				throw ServerException.Create(callrslt.Exception);
			return result;
		}

		public virtual void Dispose()
		{
			lock(this)
			{
				terminate = true;
				this.server.Dispose();
			}
		}
	}
}
