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
					_instance.Dispose();
				_instance = null;
			}
			catch { }
			if(url == null)
				throw new ArgumentNullException("url");
			_url = url;
			this.Server = new LPSClientShared.LPSServer.Server(url);
			this.Server.CookieContainer = CookieContainer;
			this.Server.Ping();
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
		
		private static System.Net.CookieContainer CookieContainer;
		private LPSClientShared.LPSServer.Server Server;
		
		public void Dispose()
		{
			this.Server.Dispose();
			this.Server = null;
		}
		
		#region IServer
		public bool Ping()
		{
			return Server.Ping();
		}
		
		public bool Login(string login, string password)
		{
			return Server.Login(login, password);
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
			return Server.GetDataSetSimple(sql);
		}
		
		public DataSet GetDataSet(string sql, bool for_edit, object[] parameters, out int server_id)
		{
			server_id = 0;
			DataSet ds = Server.GetDataSet(sql, for_edit, parameters, out server_id);
			ds.ExtendedProperties.Add("_SERVER_ID_", server_id);
			return ds;
		}
		
		public DataSet GetDataSet(string sql, bool for_edit, params object[] parameters)
		{
			int i;
			return this.GetDataSet(sql, for_edit, parameters, out i);
		}
		
		public DataSet GetDataSet(string sql, bool for_edit)
		{
			int i;
			return this.GetDataSet(sql, for_edit, new object[] { }, out i);
		}
		
		public DataSet GetDataSet(string sql, bool for_edit, Dictionary<string, object> parameters)
		{
			ArrayList p = new ArrayList(parameters.Count * 2);
			foreach(KeyValuePair<string, object> kv in parameters)
			{
				p.Add(kv.Key);
				p.Add(kv.Value);
			}
			int server_id;
			DataSet ds = this.GetDataSet(sql, for_edit, p.ToArray(), out server_id);
			return ds;
		}

		public DataSet GetDataSet(string sql, object[] parameters)
		{
			int i;
			DataSet result = Server.GetDataSet(sql, false, parameters, out i);
			return result;
		}
		
		public int SaveDataSet(DataSet changes_dataset, int server_id)
		{
			return Server.SaveDataSet(changes_dataset, server_id);
		}
		
		public int SaveDataSet(DataSet dataset)
		{
			if(!dataset.HasChanges())
				return 0;
			int server_id = (int)dataset.ExtendedProperties["_SERVER_ID_"];
			using(DataSet changes = dataset.GetChanges())
			{
				int result = this.SaveDataSet(changes, server_id);
				dataset.AcceptChanges();
				return result;
			}
		}
		
		public void DisposeDataSet(int server_id)
		{
			Server.DisposeDataSet(server_id);
		}

		public void DisposeDataSet(DataSet dataset)
		{
			try 
			{
				int server_id = (int)dataset.ExtendedProperties["_SERVER_ID_"];
				this.DisposeDataSet(server_id);
				dataset.Dispose();
			}
			catch {	}
		}

		#endregion
	}
}
