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
				result.Add(new NpgsqlParameter(p[i] as string, p[i+1]));
			return result.ToArray();
		}
		
		#region Metody WebService bez session
		[WebMethod(EnableSession=false)]
		public string Ping(string data)
		{
			return data;
		}
		#endregion
		
		#region Metody WebService se session
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
		public void Logout()
		{
			ConnectionInfo ci = this.Session["CONN"] as ConnectionInfo;
			if(ci != null)
				ci.Dispose();
			this.Session.Clear();
		}
		
		[WebMethod(EnableSession=true)]
		public int ExecuteNonquerySimple(string sql)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.ExecuteNonquery(sql);
		}

		[WebMethod(EnableSession=true)]
		public int ExecuteNonquery(string sql, params object[] parameters)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.ExecuteNonquery(sql, GetNpgsqlParameters(parameters));
		}
		
		[WebMethod(EnableSession=true)]
		public object ExecuteScalarSimple(string sql)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.ExecuteScalar(sql);
		}

		[WebMethod(EnableSession=true)]
		public object ExecuteScalar(string sql, params object[] parameters)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.ExecuteScalar(sql, GetNpgsqlParameters(parameters));
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
		public DataSet GetDataSet(string sql, object[] parameters)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.GetDataSet(sql, GetNpgsqlParameters(parameters));
		}

		[WebMethod(EnableSession=true)]
		public int SaveDataSet(DataSet changes, bool updateUserInfo, string selectSql, object[] parameters)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.SaveDataSet(changes, updateUserInfo, selectSql, GetNpgsqlParameters(parameters));
		}
 		
		#endregion
		
		[WebMethod(EnableSession=false, BufferResponse=false)]
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
	}
}
