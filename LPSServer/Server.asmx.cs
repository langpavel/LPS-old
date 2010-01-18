using System;
using System.Web;
using System.Web.Services;
using System.Data;
using Npgsql;

namespace LPSServer
{
	
	[WebService(Namespace="http://lpsoft.org/skladserver/",
	            Description="LPSoft Sklad web service")]
	public class Server : System.Web.Services.WebService
	{
		[WebMethod(EnableSession=false)]
		public bool Ping()
		{
			return true;
		}
		
		private ConnectionInfo GetConnectionInfo()
		{
			ConnectionInfo ci = this.Session["CONN"] as ConnectionInfo;
			if(ci == null)
				throw new LoginRequiredException();
			return ci;
		}
		
		[WebMethod(EnableSession=true)]
		public bool Login(string login, string password)
		{
			ConnectionInfo ci = this.Session["CONN"] as ConnectionInfo;
			if(ci != null && ci.Verify(login, password))
				return true;
			if(ci != null)
				ci.Dispose();
			this.Session.Clear();
			ci = ConnectionInfo.Create(login, password);
			this.Session["CONN"] = ci;
			return (ci != null);
		}

		[WebMethod(EnableSession=true)]
		public string GetLoggedUser()
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.UserName;
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
		public int ExecuteNonquery(string sql, params NpgsqlParameter[] parameters)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.ExecuteNonquery(sql, parameters);
		}
		
		[WebMethod(EnableSession=true)]
		public object ExecuteScalarSimple(string sql)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.ExecuteScalar(sql);
		}

		[WebMethod(EnableSession=true)]
		public object ExecuteScalar(string sql, params NpgsqlParameter[] parameters)
		{
			ConnectionInfo ci = GetConnectionInfo();
			return ci.ExecuteScalar(sql, parameters);
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
			
			using(NpgsqlCommand cmd = ci.CreateCommand(sql))
			{
				NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds;
			}
		}
		
		[WebMethod(EnableSession=true)]
		public DataSet GetDataSet(string sql, params NpgsqlParameter[] parameters)
		{
			ConnectionInfo ci = GetConnectionInfo();
			
			using(NpgsqlCommand cmd = ci.CreateCommand(sql, parameters))
			{
				NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds;
			}
		}

	}
}
