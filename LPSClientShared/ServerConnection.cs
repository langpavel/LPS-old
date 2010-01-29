using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace LPSClient
{
	public class ServerConnection: LPSServer.IServer, IDisposable
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
				return this.GetCachedDataSet("select id, username, first_name, surname from users").Tables[0];
			}
		}
		
		private static System.Net.CookieContainer CookieContainer;
		private LPSClientShared.LPSServer.Server Server;
		
		public void Dispose()
		{
			FlushCache();
			this.Server.Dispose();
			this.Server = null;
		}
		
		#region IServer
		public string Ping(string data)
		{
			return Server.Ping(data);
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
			return Server.ExecuteNonquerySimple(sql);
		}

		public int ExecuteNonquery(string sql, Dictionary<string, object> parameters)
		{
			ArrayList p = new ArrayList(parameters.Count * 2);
			foreach(KeyValuePair<string, object> kv in parameters)
			{
				p.Add(kv.Key);
				p.Add(kv.Value);
			}
			return Server.ExecuteNonquery(sql, p.ToArray());
		}
		
		public int ExecuteNonquery(string sql, params object[] parameters)
		{
			return Server.ExecuteNonquery(sql, parameters);
		}
		
		public object ExecuteScalarSimple(string sql)
		{
			return Server.ExecuteScalarSimple(sql);
		}

		public object ExecuteScalar(string sql, Dictionary<string, object> parameters)
		{
			ArrayList p = new ArrayList(parameters.Count * 2);
			foreach(KeyValuePair<string, object> kv in parameters)
			{
				p.Add(kv.Key);
				p.Add(kv.Value);
			}
			return Server.ExecuteScalar(sql, p.ToArray());
		}
		
		public object ExecuteScalar(string sql, params object[] parameters)
		{
			return Server.ExecuteScalar(sql, parameters);
		}
		
		public Int64 NextSeqValue(string generator)
		{
			return Server.NextSeqValue(generator);
		}

		public string GetTextResource(string path)
		{
			return Server.GetTextResource(path);
		}
		
		public DataSet GetDataSetSimple(string sql)
		{
			DataSet result = Server.GetDataSetSimple(sql);
			result.ExtendedProperties["sql"] = sql;
			result.ExtendedProperties["parameters"] = new object[] { };
			return result;
		}
		
		public DataSet GetDataSet(string sql, params object[] parameters)
		{
			DataSet result = Server.GetDataSet(sql, parameters);
			result.ExtendedProperties["sql"] = sql;
			result.ExtendedProperties["parameters"] = parameters;
			return result;
		}
		
		public int SaveDataSet(DataSet changes, bool updateUserInfo, string selectSql, object[] parameters)
		{
			return Server.SaveDataSet(changes, updateUserInfo, selectSql, parameters);
		}
		
		public int SaveDataSet(DataSet dataset, bool updateUserInfo)
		{
			if(!dataset.HasChanges())
				return 0;
			string selectSql = (string)dataset.ExtendedProperties["sql"];
			object[] parameters = (object[])dataset.ExtendedProperties["parameters"];
			int result = Server.SaveDataSet(dataset.GetChanges(), updateUserInfo, selectSql, parameters);
			dataset.AcceptChanges();
			return result;
		}
		
		public int SaveDataSet(DataSet dataset)
		{
			return SaveDataSet(dataset, true);
		}

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
		public DataSet GetCachedDataSet(string sql)
		{
			DataSet result;
			cached_datasets.TryGetValue(sql, out result);
			if(result == null)
			{
				result = this.GetDataSetSimple(sql);
				cached_datasets[sql] = result;
				result.Disposed += delegate(object sender, EventArgs e) {
					cached_datasets.Remove(sql);
				};
			}
			return result;
		}
		
		public DataSet GetCachedDataSetCopy(string sql)
		{
			return GetCachedDataSet(sql).Copy();
		}

		#endregion
	}
}
