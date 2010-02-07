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
			_resource_manager = new ResourceManager(this);
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
			get
			{
				return this.GetCachedDataSet("users").Tables[0];
			}
		}

		private ResourceManager _resource_manager;
		public ResourceManager Resources
		{
			get
			{
				return _resource_manager;
			}
		}
		
		private static System.Net.CookieContainer CookieContainer;
		private LPSClientShared.LPSServer.Server Server;
		private int sink = -1;
		private int security = 0;
		
		public void Dispose()
		{
			FlushCache();
			this.Server.Dispose();
			this.Server = null;
		}

		public bool Ping()
		{
			try
			{
				return Server.Ping();
			}
			catch
			{
				return false;
			}
		}
		
		public long Login(string login, string password)
		{
			this.UserId = Server.Login(login, password);
			return this.UserId;
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
			Server.Logout();
		}
		
		public int ExecuteNonquerySimple(string sql)
		{
			return Server.SimpleExecuteNonquery(sql);
		}

		private void CheckServerResult(LPSClientShared.LPSServer.ServerCallResult result)
		{
			if(result == null)
				return;
			if(result.Exception != null)
				throw ServerException.Create(result.Exception);
		}
		
		public int ExecuteNonquery(string sql, Dictionary<string, object> parameters)
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
		
		public int ExecuteNonquery(string sql, params object[] parameters)
		{
			int affected;
			CheckServerResult(Server.ExecuteNonquery(sink, security, sql, parameters, out affected));
			return affected;
		}
		
		public object ExecuteScalarSimple(string sql)
		{
			return Server.SimpleExecuteScalar(sql);
		}

		public object ExecuteScalar(string sql, Dictionary<string, object> parameters)
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
		
		public object ExecuteScalar(string sql, params object[] parameters)
		{
			object result;
			CheckServerResult(Server.ExecuteScalar(sink, security, sql, parameters, out result));
			return result;
		}
		
		public Int64 NextSeqValue(string generator)
		{
			return Server.NextSeqValue(generator);
		}

		public string GetTextResource(string path)
		{
			return Server.GetTextResource(path);
		}
		
		[Obsolete]
		public DataSet GetDataSetSimple(string sql)
		{
			DataSet result = Server.GetDataSetSimple(sql);
			result.ExtendedProperties["sql"] = sql;
			result.ExtendedProperties["parameters"] = new object[] { };
			return result;
		}
		
		[Obsolete]
		public DataSet GetDataSet(string sql, params object[] parameters)
		{
			DataSet result;
			CheckServerResult(Server.GetDataSet(sink, security, sql, parameters, out result));
			result.ExtendedProperties["sql"] = sql;
			result.ExtendedProperties["parameters"] = parameters;
			return result;
		}

		[Obsolete]
		public int SaveDataSet(DataSet changes, bool updateUserInfo, string selectSql, object[] parameters)
		{
			int affected;
			CheckServerResult(Server.SaveDataSet(sink, security, changes, updateUserInfo, selectSql, parameters, out affected));
			return affected;
		}
		
		public DataSet GetDataSetByTableName(string name, params object[] parameters)
		{
			DataSet result;
			CheckServerResult(Server.GetDataSetByTableName(sink, security, name, parameters, out result));
			result.ExtendedProperties.Add("TABLE", name);
			return result;
		}
		
		public int SaveDataSet(DataSet dataset, bool updateUserInfo)
		{
			if(!dataset.HasChanges())
				return 0;
			int affected;
			if(dataset.ExtendedProperties.ContainsKey("TABLE"))
			{
				string tablename = (string)dataset.ExtendedProperties["TABLE"];
				using(DataSet changes = dataset.GetChanges())
					CheckServerResult(Server.SaveDataSetByTableName(sink, security, tablename, changes, true, true, out affected));
				if(affected > 0) 
					dataset.AcceptChanges();
				return affected;
			}
			else
			{
				Console.WriteLine("DEPRECATED SAVE!");
				string selectSql = (string)dataset.ExtendedProperties["sql"];
				object[] parameters = (object[])dataset.ExtendedProperties["parameters"];
				using(DataSet changes = dataset.GetChanges())
					CheckServerResult(Server.SaveDataSet(sink, security, changes, updateUserInfo, selectSql, parameters, out affected));
				dataset.AcceptChanges();
				return affected;
			}
		}
		
		public int SaveDataSet(DataSet dataset)
		{
			return SaveDataSet(dataset, true);
		}

		[Obsolete]
		public DataSet GetSameDataSet(DataSet dataset)
		{
			string selectSql = (string)dataset.ExtendedProperties["sql"];
			object[] parameters = (object[])dataset.ExtendedProperties["parameters"];
			return this.GetDataSet(selectSql, parameters);
		}
		
		public void FlushCache()
		{
			List<DataSet> copy = new List<DataSet>(cached_datasets.Values);
			foreach(DataSet ds in copy)
			{
				ds.Dispose();
			}
			cached_datasets.Clear();
		}
		
		private Dictionary<string, DataSet> cached_datasets;
		public DataSet GetCachedDataSet(string tableName)
		{
			DataSet result;
			cached_datasets.TryGetValue(tableName, out result);
			if(result == null)
			{
				result = this.GetDataSetByTableName(tableName);
				cached_datasets[tableName] = result;
				result.Disposed += delegate(object sender, EventArgs e) {
					cached_datasets.Remove(tableName);
				};
			}
			return result;
		}
		
	}
}
