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
				if(!datasets.TryGetValue(tablename, out dslist))
				{
					dslist = new List<DataSet>();
					datasets[tablename] = dslist;
				}
				dslist.Add(ds);
				ds.ExtendedProperties["UPDATER_LIST"] = dslist;
				ds.ExtendedProperties["UPDATER_TABLE"] = tablename;
				Log.Debug("Updater registered {0}", tablename);
			}
		}

		public void RemoveDataSet(DataSet ds)
		{
			lock(this)
			{
				List<DataSet> dslist = ds.ExtendedProperties["UPDATER_LIST"] as List<DataSet>;
				string tablename = ds.ExtendedProperties["UPDATER_TABLE"] as string;
				if(dslist != null)
				{
					dslist.Remove(ds);
					Log.Debug("Updater unregistered {0}", tablename);
				}
			}
		}

		private void CheckUpdatesAsync()
		{
			//Log.Debug("{0} - Thd{1}: CheckUpdatesAsync", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
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
			//Log.Debug("{0} - Thd{1}: CheckUpdatesAsyncEnd", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
			LPSClientShared.LPSServer.ServerCallResult result;
			lock(this)
			{
				result = server.EndGetChanges(aresult);
				if(this.terminate)
					return;
			}
			if(result.Changes != null && result.Changes.Length > 0)
			{
				//Log.Debug("{0} - Thd{1}: CheckUpdatesAsyncEnd - changes found!", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
				lock(this)
					l_changes = result.Changes;
				GLib.Timeout.Add(0, CheckUpdatesSync);
			}
			else
			{
				//Log.Debug("{0} - Thd{1}: CheckUpdatesAsyncEnd - changes not found, waiting 1s", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
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
			using(Log.Scope("CheckUpdatesSync - synchronní kontrola změn"))
			{
				Log.Debug("{0} - Thd{1}: CheckUpdatesAsyncEnd", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
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
					Log.Debug("{0} - Thd{1}: CheckUpdatesAsyncEnd changes == null!!!", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
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
		}

		public void DoUpdates(LPSClientShared.LPSServer.ChangeInfo[] changes)
		{
			using(Log.Scope("{0} - Thd{1}: DoUpdates", DateTime.Now, Thread.CurrentThread.ManagedThreadId))
			lock(this)
			{
				foreach(LPSClientShared.LPSServer.ChangeInfo change in changes)
				{
					Log.Debug(" * Modify DT: {0}: table {1} {2}",
						change.ModifyDateTime,
						change.TableName,
						change.HasDeletedRows?"was updated and some rows deleted":"was updated");
					List<DataSet> dslist;
					if(datasets.TryGetValue(change.TableName, out dslist))
					{
						foreach(DataSet ds in dslist)
						{
							DateTime last_dt = change.ModifyDateTime; //(DateTime)ds.ExtendedProperties["DATETIME"];
							string tablename = change.TableName;
							using(DataSet alllist = GetDataSetAllChangesList(tablename, last_dt, change.HasDeletedRows))
							using(DataSet updates = GetDataSetUpdates(ds, ref last_dt))
							{
								UpdateTableRows(ds.Tables[0], updates.Tables[0], alllist.Tables[0]);
								ds.ExtendedProperties["DATETIME"] = last_dt;
							}
						}
					}
				}
			}
		}

		private void UpdateTableRows(DataTable dest, DataTable src, DataTable alllist)
		{
			List<long> for_removal = new List<long>();
			foreach(DataRow r in alllist.Rows)
				for_removal.Add(Convert.ToInt64(r[0]));
			dest.BeginLoadData();
			try
			{
				foreach(DataRow r in src.Rows)
				{
					for_removal.Remove(Convert.ToInt64(r[0]));
					dest.LoadDataRow(r.ItemArray, LoadOption.PreserveChanges);
				}
				foreach(long id in for_removal)
				{
					Log.Debug("remove id {0}", id);
					DataRow r = dest.Rows.Find(id);
					if(r != null)
						dest.Rows.Remove(r);
				}
			}
			finally
			{
				dest.EndLoadData();
			}
		}

		private DataSet GetDataSetAllChangesList(string table_name, DateTime dt_last, bool with_deleted)
		{
			LPSClientShared.LPSServer.ServerCallResult callrslt;
			DataSet ds;
			object[] parameters = new object[] { "ts_last", dt_last };
			string sql = String.Format("select id as id, false as deleted from {0} where (ts >= :ts_last)", table_name);
			if(with_deleted)
				sql += String.Format(" UNION select row_id as id, true as deleted from sys_deleted where table_name='{0}' and (ts >= :ts_last)", table_name);
			callrslt = server.GetDataSetBySql(-1, 0, sql, parameters, out ds);
			if(callrslt != null && callrslt.Exception != null)
				throw ServerException.Create(callrslt.Exception);
			return ds;
		}

		private DataSet GetDataSetUpdates(DataSet ds, ref DateTime last_dt)
		{
			string list = (string)ds.ExtendedProperties["LIST"];
			string addsql = (string)ds.ExtendedProperties["ADDSQL"];
			object[] parameters = (object[])ds.ExtendedProperties["PARAMS"];
			// workaround na nove recordy s volanim id=0
			if(parameters.Length > 1 && parameters[0].Equals("id") && ds.Tables[0].Rows.Count == 1
				&& ds.Tables[0].Rows[0].RowState != DataRowState.Deleted && ds.Tables[0].Rows[0].RowState != DataRowState.Detached)
				parameters[1] = ds.Tables[0].Rows[0][0];

			DateTime dt_last = last_dt;
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
