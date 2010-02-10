using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Xml;
using Npgsql;
using System.Configuration;

namespace LPS.Server
{
	
	[WebService(Namespace="http://lpsoft.org/server/",
	            Description="LPSoft server")]
	public class Server: System.Web.Services.WebService
	{
		#region Helper not exported methods
		private ConnectionInfo GetConnectionInfo()
		{
			ConnectionInfo ci = this.Session["CONN"] as ConnectionInfo;
			if(ci == null)
				throw new SoapException("Login is required", SoapException.ClientFaultCode);
			return ci;
		}
		
		public static NpgsqlParameter[] GetNpgsqlParameters(object[] p)
		{
			if(p.Length % 2 != 0)
				throw new SoapException("Nesprávný počet parametrů", SoapException.ClientFaultCode);
			
			List<NpgsqlParameter> result = new List<NpgsqlParameter>(p.Length >> 1);
			for(int i=0; i < p.Length; i += 2)
			{
				NpgsqlParameter param = new NpgsqlParameter(p[i] as string, p[i+1]);
				param.SourceColumn = p[i] as string;
				result.Add(param);
			}
			return result.ToArray();
		}
		#endregion
		
		#region Metody WebService bez session
		[WebMethod(EnableSession=false)]
		public bool Ping()
		{
			return true;
		}
		#endregion
		
		#region Obecné metody WebService se session
		[WebMethod(EnableSession=true)]
		public long Login(string login, string password)
		{
			ConnectionInfo ci = this.Session["CONN"] as ConnectionInfo;
			if(ci != null && ci.Verify(login, password))
				return ci.UserId;
			if(ci != null)
				ci.Dispose();
			this.Session.Clear();
			ci = ConnectionInfo.Create(login, password);
			this.Session["CONN"] = ci;
			return ci.UserId;
		}

		[WebMethod(EnableSession=true)]
		public string GetLoggedUser()
		{
			try
			{
				ConnectionInfo ci = GetConnectionInfo();
				return ci.UserName;
			}
			catch
			{
				return null;
			}
		}
		
		[WebMethod(EnableSession=true)]
		public bool ChangePassword(string old_password, string new_password)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.ChangePassword(old_password, new_password);
		}
		
		[WebMethod(EnableSession=true)]
		public int SimpleExecuteNonquery(string sql)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.ExecuteNonquery(sql);
		}

		[WebMethod(EnableSession=true)]
		public object SimpleExecuteScalar(string sql)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.ExecuteScalar(sql);
		}

		[WebMethod(EnableSession=true)]
		public Int64 NextSeqValue(string generator)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return Convert.ToInt64(ci.ExecuteScalar("select nextval(:gener)",
				new NpgsqlParameter("gener", generator)));
		}

		[WebMethod(EnableSession=true)]
		public DataSet GetDataSetSimple(string sql)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.GetDataSetSimple(sql);
		}

		[WebMethod(EnableSession=true)]
		public DataSet GetDataSetByTableNameSimple(string tableName, string addsql)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.GetDataSetByTableName(tableName, addsql);
		}
		
		[WebMethod(EnableSession=false)]
		public ServerCallResult GetChanges(int sink, int security)
		{
			try
			{
				return ServerChangeSink.GetResult(sink, security);
			}
			catch(Exception err)
			{
				return new ServerCallResult(err); 
			}
		}
		
		[WebMethod(EnableSession=true)]
		public ServerCallResult ExecuteNonquery(int sink, int security, string sql, object[] parameters, out int affected)
		{
			affected = 0;
			try
			{
				ConnectionInfo ci = GetConnectionInfo();
				affected = ci.ExecuteNonquery(sql, GetNpgsqlParameters(parameters));
				return ServerChangeSink.GetResult(sink, security);
			}
			catch(Exception err)
			{
				return new ServerCallResult(err); 
			}
		}
		

		[WebMethod(EnableSession=true)]
		public ServerCallResult ExecuteScalar(int sink, int security, string sql, object[] parameters, out object result)
		{
			result = null;
			try
			{
				ConnectionInfo ci = GetConnectionInfo();
				result = ci.ExecuteScalar(sql, GetNpgsqlParameters(parameters));
				return ServerChangeSink.GetResult(sink, security);
			}
			catch(Exception err)
			{
				return new ServerCallResult(err); 
			}
		}
		
		[WebMethod(EnableSession=true)]
		public ServerCallResult GetDataSet(int sink, int security, string sql, object[] parameters, out DataSet data)
		{
			data = null;
			try
			{
				ConnectionInfo ci = GetConnectionInfo();
				data = ci.GetDataSet(sql, GetNpgsqlParameters(parameters));
				return ServerChangeSink.GetResult(sink, security);
			}
			catch(Exception err)
			{
				return new ServerCallResult(err); 
			}
		}

		[WebMethod(EnableSession=true)]
		public ServerCallResult SaveDataSet(int sink, int security, DataSet changes, bool updateUserInfo, string selectSql, object[] parameters, out int affected)
		{
			affected = 0;
			try
			{
				ConnectionInfo ci = GetConnectionInfo();
				affected = ci.SaveDataSet(changes, updateUserInfo, selectSql, GetNpgsqlParameters(parameters));
				return ServerChangeSink.GetResult(sink, security);
			}
			catch(Exception err)
			{
				return new ServerCallResult(err); 
			}
		}
		
		[WebMethod(EnableSession=true)]
		public ServerCallResult SaveDataSetByTableName(int sink, int security, string name, DataSet changes, bool updateUserInfo, bool changesNotify, out int affected)
		{
			affected = 0;
			try
			{
				ConnectionInfo ci = GetConnectionInfo();
				affected = ci.SaveDataSetByTableName(name, changes, updateUserInfo, changesNotify);
				return ServerChangeSink.GetResult(sink, security);
			}
			catch(Exception err)
			{
				return new ServerCallResult(err); 
			}
		}
		#endregion

		[WebMethod(EnableSession=true)]
		public ServerCallResult RegisterListener(out int sink, out int security)
		{
			sink = -1;
			security = 0;
			try
			{
				ServerChangeListener listener = ServerChangeSink.RegisterNewListener();
				sink = listener.SinkIndex;
				security = listener.Security;
				return ServerChangeSink.GetResult(sink, security);
			}
			catch(Exception err)
			{
				return new ServerCallResult(err); 
			}
		}
		
		[WebMethod(EnableSession=true)]
		public ServerCallResult UnregisterListener(int sink, int security)
		{
			try
			{
				ServerChangeSink.RemoveListener(sink, security);
				return new ServerCallResult();
			}
			catch(Exception err)
			{
				return new ServerCallResult(err); 
			}
		}

		[WebMethod(EnableSession=true)]
		public ServerCallResult GetDataSetByTableName(int sink, int security, 
			string table, object[] parameters, out DataSet result)
		{
			result = null;
			try
			{
				ConnectionInfo ci = GetConnectionInfo();
				result = ci.GetDataSetByTableName(table, GetNpgsqlParameters(parameters));
				return ServerChangeSink.GetResult(sink, security);
			}
			catch(Exception err)
			{
				return new ServerCallResult(err); 
			}
		}
		
		[WebMethod(EnableSession=false, BufferResponse=false)]
		public string GetTextResource(string path)
		{
			return _GetTextResource(path);
		}
		
		public static string _GetTextResource(string path)
		{
			string resPath = "/var/www/LPS/resources";
			try
			{
				System.Configuration.Configuration rootWebConfig1 =
    				System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
    			System.Configuration.KeyValueConfigurationElement resourceDirectory = 
        			rootWebConfig1.AppSettings.Settings["ResourceDirectory"];
				resPath = resourceDirectory.Value;
			}
			catch 
			{
			}

			resPath = Path.Combine(resPath, path);
			try
			{				
				using(StreamReader reader = File.OpenText(resPath))
					return reader.ReadToEnd();
			}
			catch
			{
				return null;
			}
		}

		[WebMethod(EnableSession=true)]
		public void Logout()
		{
			ConnectionInfo ci = this.Session["CONN"] as ConnectionInfo;
			if(ci != null)
				ci.Dispose();
			this.Session.Clear();
		}
		
	}
}
