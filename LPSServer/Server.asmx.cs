using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using Npgsql;

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

		[WebMethod(EnableSession=false)]
		public string[] ShowServiceConfiguration()
		{
			List<string> settings = new List<string>();
			NameValueCollection AppSettings = WebConfigurationManager.AppSettings;
			if(AppSettings == null)
				return new string[] {"AppSettings is null"};
		    foreach(string key in AppSettings.AllKeys)
				settings.Add(String.Format("{0}='{1}'", key, AppSettings.Get(key)));
			return settings.ToArray();
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
		public string GetGeneratorValue(string generator, DateTime sys_date)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.GetGeneratorValue(generator, sys_date);
		}

		[WebMethod(EnableSession=true)]
		public DataSet GetDataSetSimple(string sql)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.GetDataSetSimple(sql);
		}

		[WebMethod(EnableSession=true)]
		public DataSet GetDataSetByNameSimple(string name, string addsql)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.GetDataSetByName(name, addsql);
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
		public ServerCallResult GetDataSetBySql(int sink, int security, string sql, object[] parameters, out DataSet data)
		{
			data = null;
			try
			{
				ConnectionInfo ci = GetConnectionInfo();
				DateTime dt_now = DateTime.Now;
				data = ci.GetDataSet(sql, GetNpgsqlParameters(parameters));
				return ServerChangeSink.GetResult(sink, security, dt_now);
			}
			catch(Exception err)
			{
				return new ServerCallResult(err); 
			}
		}

		[WebMethod(EnableSession=true)]
		public ServerCallResult SaveDataSet(int sink, int security, string table_name, DataSet changes, bool updateUserInfo, bool changesNotify, out int affected)
		{
			affected = 0;
			try
			{
				Console.WriteLine(table_name);
				ConnectionInfo ci = GetConnectionInfo();
				affected = ci.SaveDataSet(table_name, changes, updateUserInfo, changesNotify);
				return ServerChangeSink.GetResult(sink, security);
			}
			catch(Exception err)
			{
				Console.WriteLine(err);
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
		public ServerCallResult GetDataSetByName(int sink, int security,
			string table, string addsql, object[] parameters, out DataSet result)
		{
			result = null;
			try
			{
				ConnectionInfo ci = GetConnectionInfo();
				DateTime dt_now = DateTime.Now;
				result = ci.GetDataSetByName(table, addsql, GetNpgsqlParameters(parameters));
				return ServerChangeSink.GetResult(sink, security, dt_now);
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
			path = path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

			string resPath = WebConfigurationManager.AppSettings["ResourceDirectory"];

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
