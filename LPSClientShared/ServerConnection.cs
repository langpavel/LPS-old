using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace LPS.Client
{
	public class ServerConnection: IResourceStore, IDisposable
	{
		static ServerConnection()
		{
			CookieContainer = new System.Net.CookieContainer();
		}
		
		public ServerConnection(string url)
		{
			using(Log.Scope("ServerConnection constructor"))
			{
				Log.Debug("connecting to {0}", url);
				try
				{
					if(_instance != null)
					{
						_instance.Dispose();
						_instance = null;
					}
				}
				catch { }
				if(url == null)
					throw new ArgumentNullException("url");
				_url = url;
				this.Server = new LPSClientShared.LPSServer.Server(url);
				this.Server.CookieContainer = CookieContainer;
				this.cached_datasets = new Dictionary<string, DataSet>();
				_instance = this;
				resource_manager = new ResourceManager(this);
				configuration_store = new ConfigurationStore();
			}
		}

		private static string _url;
		[ThreadStatic]
		private static ServerConnection _instance;
		public static ServerConnection Instance
		{
			get
			{
				return _instance ?? (_instance = new ServerConnection(_url));
			}
		}
		
		public long UserId { get; set; }
		public DataTable Users 
		{ 
			get { return this.GetCachedDataSet("sys_user").Tables[0]; }
		}

		private ResourceManager resource_manager;
		public ResourceManager Resources
		{
			get	{ return resource_manager; }
		}

		private ConfigurationStore configuration_store;
		public ConfigurationStore Configuration
		{
			get { return configuration_store; }
		}

		private static System.Net.CookieContainer CookieContainer;
		private LPSClientShared.LPSServer.Server Server;
		private int sink = -1;
		private int security = 0;
		
		public void Dispose()
		{
			using(Log.Scope("ServerConnection.Dispose"))
			{
				try
				{
					FlushCache();
					this.Configuration.Dispose();
					this.configuration_store = null;
					this.Resources.Dispose();
					this.resource_manager = null;
					this.Logout();
				}
				catch(Exception err)
				{
					Log.Error(err);
				}
				this.Server.Dispose();
				this.Server = null;
			}
		}

		public bool Ping()
		{
			using(Log.Scope("Ping"))
			{
				try
				{
					return Server.Ping();
				}
				catch(Exception err)
				{
					Log.Error(err);
					return false;
				}
			}
		}

		private ChangesUpdater updater;
		public long Login(string login, string password)
		{
			using(Log.Scope())
			{
				Log.Debug("Login {0}", login);
				this.UserId = Server.Login(login, password);
				Server.RegisterListener(out this.sink, out this.security);
				updater = new ChangesUpdater(Server.Url, login, password, sink, security);
				return this.UserId;
			}
		}
		
		public string GetUserName(long id)
		{
			DataRow r = Users.Rows.Find(id);
			return String.Format("{0} {1}", r["surname"], r["first_name"]);
		}

		public string GetUserName()
		{
			return GetUserName(this.UserId);
		}

		public string GetLoggedUser()
		{
			return Server.GetLoggedUser();
		}
		
		public bool ChangePassword(string old_password, string new_password)
		{
			return Server.ChangePassword(old_password, new_password);
		}
		
		public void Logout()
		{
			using(Log.Scope())
			{
				Log.Debug("logout");
				if(this.UserId <= 0)
					return;
				if(this.sink >= 0)
				{
					updater.Dispose();
					updater = null;
					Server.UnregisterListener(this.sink, this.security);
					this.sink = -1;
				}
				Server.Logout();
				this.UserId = 0;
			}
		}
		
		public int ExecuteNonquerySimple(string sql)
		{
			using(Log.Scope())
			{
				return Server.SimpleExecuteNonquery(sql);
			}
		}

		private void CheckServerResult(LPSClientShared.LPSServer.ServerCallResult result)
		{
			using(Log.Scope())
			{
				if(result == null)
					return;
				if(result.Changes != null && result.Changes.Length > 0 && updater != null)
					updater.DoUpdates(result.Changes);
				if(result.Exception != null)
					throw ServerException.Create(result.Exception);
			}
		}
		
		public int ExecuteNonquery(string sql, Dictionary<string, object> parameters)
		{
			using(Log.Scope())
			{
				ArrayList p = new ArrayList(parameters.Count * 2);
				foreach(KeyValuePair<string, object> kv in parameters)
				{
					p.Add(kv.Key);
					p.Add(kv.Value);
				}

				int affected;
				CheckServerResult(Server.ExecuteNonquery(sink, security, sql, p.ToArray(), out affected));
				return affected;
			}
		}
		
		public int ExecuteNonquery(string sql, params object[] parameters)
		{
			using(Log.Scope())
			{
				int affected;
				CheckServerResult(Server.ExecuteNonquery(sink, security, sql, parameters, out affected));
				return affected;
			}
		}
		
		public object ExecuteScalarSimple(string sql)
		{
			using(Log.Scope())
			{
				return Server.SimpleExecuteScalar(sql);
			}
		}

		public object ExecuteScalar(string sql, Dictionary<string, object> parameters)
		{
			using(Log.Scope())
			{
				ArrayList p = new ArrayList(parameters.Count * 2);
				foreach(KeyValuePair<string, object> kv in parameters)
				{
					p.Add(kv.Key);
					p.Add(kv.Value);
				}
				object result;
				CheckServerResult(Server.ExecuteScalar(sink, security, sql, p.ToArray(), out result));
				return result;
			}
		}
		
		public object ExecuteScalar(string sql, params object[] parameters)
		{
			using(Log.Scope())
			{
				object result;
				CheckServerResult(Server.ExecuteScalar(sink, security, sql, parameters, out result));
				return result;
			}
		}
		
		public Int64 NextSeqValue(string generator)
		{
			using(Log.Scope())
			{
				Int64 result = Server.NextSeqValue(generator);
				Log.Debug("NextSeqVal {0}={1}", generator, result);
				return result;
			}
		}

		public string GetTextResource(string path)
		{
			using(Log.Scope())
			{
				return Server.GetTextResource(path);
			}
		}
		
		public DataSet GetDataSetBySql(string sql)
		{
			using(Log.Scope())
			{
				Log.Debug("sql={0}", sql);
				DataSet ds;
				object[] parameters = new object[] {};
				CheckServerResult(Server.GetDataSetBySql(this.sink, this.security, sql, parameters, out ds));
				ds.ExtendedProperties["SQL"] = sql;
				ds.ExtendedProperties["PARAMS"] = parameters;
				return ds;
			}
		}

		public DataSet GetDataSetByName(string name)
		{
			return GetDataSetByName(name, "");
		}

		public DataSet GetDataSetByName(string name, string addsql, params object[] parameters)
		{
			using(Log.Scope())
			{
				Log.Debug("name={0}, addsql={1}, parameters={2}", name, addsql, parameters);
				if(parameters.Length % 2 == 1)
					throw new ArgumentException("Musí být sudý počet parametrů", "parameters");
	
				IListInfo info = Resources.GetListInfo(name);
				DataSet result;
				LPSClientShared.LPSServer.ServerCallResult callrslt;
				callrslt = Server.GetDataSetByName(sink, security, name, addsql, parameters, out result);
				CheckServerResult(callrslt);
				result.ExtendedProperties.Add("ADDSQL", addsql);
				result.ExtendedProperties.Add("PARAMS", parameters);
				result.ExtendedProperties.Add("DATETIME", callrslt.DateTime);
				result.ExtendedProperties.Add("LIST", name);
				result.ExtendedProperties.Add("TABLE", info.TableName);
				this.updater.AddDataSet(info.TableName, result);
				return result;
			}
		}

		public void DisposeDataSet(DataSet ds)
		{
			DisposeDataSet(ds, false);
		}

		public void DisposeDataSet(DataSet ds, bool force)
		{
			if(ds != null)
			{
				using(Log.Scope())
				{
					if(ds.ExtendedProperties.ContainsKey("CACHE_CNT"))
					{
						int cnt = (int)ds.ExtendedProperties["CACHE_CNT"];
						cnt = cnt - 1;
						if(!force && cnt > 0)
						{
							ds.ExtendedProperties["CACHE_CNT"] = cnt;
							return;
						}
						else
							cached_datasets[(string)ds.ExtendedProperties["LIST"]] = null;
					}
	
					this.updater.RemoveDataSet(ds);
					ds.Dispose();
				}
			}
		}

		public int SaveDataSet(DataSet dataset, bool updateUserInfo)
		{
			using(Log.Scope())
			{
				if(dataset == null)
					throw new ArgumentNullException("dataset");
				if(!dataset.HasChanges())
					return 0;
				int affected;
				if(dataset.ExtendedProperties.ContainsKey("TABLE"))
				{
					string tablename = (string)dataset.ExtendedProperties["TABLE"];
					using(DataSet changes = dataset.GetChanges())
					{
						CheckServerResult(Server.SaveDataSet(sink, security, tablename, changes, true, true, out affected));
					}
					if(affected > 0) 
						dataset.AcceptChanges();
					return affected;
				}
				else
					throw new ArgumentException("Dataset neobsahuje hodnotu TABLE");
			}
		}
		
		public int SaveDataSet(DataSet dataset)
		{
			if(dataset == null)
				throw new ArgumentNullException("dataset");
			return SaveDataSet(dataset, true);
		}

		public void FlushCache()
		{
			using(Log.Scope())
			{
				List<DataSet> copy = new List<DataSet>(cached_datasets.Values);
				foreach(DataSet ds in copy)
				{
					this.DisposeDataSet(ds);
				}
				cached_datasets.Clear();
			}
		}
		
		private Dictionary<string, DataSet> cached_datasets;
		public DataSet GetCachedDataSet(string tableName)
		{
			using(Log.Scope())
			{
				DataSet result;
				if(cached_datasets.TryGetValue(tableName, out result) && result != null)
				{
					result.ExtendedProperties["CACHE_CNT"] = ((int)result.ExtendedProperties["CACHE_CNT"])+1;
					result.ExtendedProperties["CACHE_DT"] = DateTime.Now;
					return result;
				}
				if(result == null)
				{
					result = this.GetDataSetByName(tableName);
					cached_datasets[tableName] = result;
					result.ExtendedProperties.Add("CACHE_CNT", 1);
					result.ExtendedProperties.Add("CACHE_DT", DateTime.Now);
				}
				return result;
			}
		}

		public void CheckChanges()
		{
			using(Log.Scope())
				CheckServerResult(Server.GetChanges(this.sink, this.security));
		}

	}
}
