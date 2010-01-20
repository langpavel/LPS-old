using System;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using Npgsql;

namespace LPSServer
{
	
	[WebService(Namespace="http://lpsoft.org/skladserver/",
	            Description="LPSoft Sklad web service")]
	public class Server: System.Web.Services.WebService, IServer
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
		
		private NpgsqlParameter[] GetNpgsqlParameters(object[] p)
		{
			List<NpgsqlParameter> result = new List<NpgsqlParameter>(p.Length);
			for(int i=0; i<p.Length; i+=2)
				result.Add(new NpgsqlParameter(p[i] as string, p[i+1]));
			return result.ToArray();			
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

		[WebMethod(EnableSession=false, BufferResponse=false)]
		public string GetTextResource(string path)
		{
			string resPath = Assembly.GetExecutingAssembly().GetName().FullName;
			resPath = Path.GetDirectoryName(resPath);
			resPath = Path.Combine(resPath, "resources");
			path = Path.Combine(resPath, path);
			try
			{				
				using(StreamReader reader = File.OpenText(path))
					return reader.ReadToEnd();
			}
			catch
			{
				return null;
			}
		}
		
		[WebMethod(EnableSession=true)]
		public DataSet GetDataSetSimple(string sql)
		{
			ConnectionInfo ci = GetConnectionInfo();
			DataSetStoreItem dstore = new DataSetStoreItem();
			dstore.DataSet = new DataSet();
			dstore.Command = ci.CreateCommand(sql);
			dstore.DataAdapter = new NpgsqlDataAdapter(dstore.Command);
			dstore.DataAdapter.Fill(dstore.DataSet);
			foreach(DataTable dt in dstore.DataSet.Tables)
			{
				dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
			}
			dstore.DisposeWithoutDataSet();
			return dstore.DataSet;
		}

		[WebMethod(EnableSession=true)]
		public DataSet GetDataSet(string sql, bool for_edit, object[] parameters, out int server_id)
		{
			server_id = 0;
			ConnectionInfo ci = GetConnectionInfo();
			
			DataSetStoreItem dstore = new DataSetStoreItem();
			dstore.DataSet = new DataSet();
			dstore.Command = ci.CreateCommand(sql, GetNpgsqlParameters(parameters));
			dstore.DataAdapter = new NpgsqlDataAdapter(dstore.Command);
			if(for_edit)
			{
				dstore.CommandBuilder = new NpgsqlCommandBuilder(dstore.DataAdapter);
				server_id = ci.StoreDataSet(dstore);
			}
			dstore.DataAdapter.Fill(dstore.DataSet);
			foreach(DataTable dt in dstore.DataSet.Tables)
			{
				dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
			}
			if(!for_edit)
				dstore.DisposeWithoutDataSet();
			return dstore.DataSet;
		}

		[WebMethod(EnableSession=true)]
		public int SaveDataSet(DataSet changes, int srv_id)
		{
			ConnectionInfo ci = GetConnectionInfo();
			DataSetStoreItem dstore = ci.RestoreDataSet(srv_id);
			//return dstore.DataAdapter.Update(changes);
			int result = 0;
			foreach(DataTable ch_table in changes.Tables)
			{
				/*
				DataTable dt = dstore.DataSet.Tables[ch_table.TableName, ch_table.Namespace];
				try
				{
					dt.Merge(ch_table, true, MissingSchemaAction.Error);
				}
				catch(Exception ex)
				{
					throw new Exception("changes:" + ch_table.Rows.Count, ex);
				}
				*/
				try
				{
					result += dstore.DataAdapter.Update(ch_table);
					//dt.AcceptChanges();
				}
				catch
				{
					//dt.RejectChanges();
					throw;
				}
			}
			return result;
		}

		[WebMethod(EnableSession=true)]
		public void DisposeDataSet(int server_id)
		{
			ConnectionInfo ci = GetConnectionInfo();
			ci.DisposeDataSet(server_id);
		}
		
	}
}
