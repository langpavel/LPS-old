using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Npgsql;
using System.Web.Services.Protocols;
using System.Xml;

namespace LPS.Server
{
	
	public class ConnectionInfo : IDisposable
	{
		public long UserId { get; set; }
		public string UserName { get; set; }
		public NpgsqlConnection Connection { get; set; }
		private string passwdhash;
		
		//private int ds_counter;

		#region Management
		private ConnectionInfo()
		{
			//StoredDataSets = new Dictionary<int, DataSetStoreItem>();
			NpgsqlEventLog.Level = LogLevel.None;
			//NpgsqlEventLog.LogName = "NpgsqlTests.LogFile";
		}

		/*
		public Dictionary<int, DataSetStoreItem> StoredDataSets { get; set; }
		public int StoreDataSet(DataSetStoreItem ds)
		{
			ds_counter++;
			StoredDataSets[ds_counter] = ds;
			return ds_counter;
		}
		
		public DataSetStoreItem RestoreDataSet(int index)
		{
			return StoredDataSets[index];
		}
		*/
		
		public static string GetSHA1String(string data, string salt)
		{
			SHA1 sha1 = new SHA1Managed();
			byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data + salt));
			return Convert.ToBase64String(hash);
		}
		
		public static ConnectionInfo Create(string login, string password)
		{
			NpgsqlConnection connection = new NpgsqlConnection();
			connection.ConnectionString =
				System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

			ConnectionInfo result = new ConnectionInfo();
			result.Connection = connection;
			result.UserName = login;
			result.passwdhash = GetSHA1String(password, login);
			result.Open();
			object id = result.ExecuteScalar(
				"select id from users where username=:username and passwd=:passwd",
				new NpgsqlParameter("username", login),
				new NpgsqlParameter("passwd", result.passwdhash));
			if(id == null || id is DBNull)
			{
				result.Dispose();
				throw new SoapException("Neplatné jméno nebo heslo", SoapException.ClientFaultCode);
			}
			result.UserId = Convert.ToInt64(id);
			return result;
		}
		
		public bool Verify(string login, string password)
		{
			return (login == UserName) && 
				(passwdhash == GetSHA1String(password, login));
		}
		
		public bool Verify(string password)
		{
			return
				(passwdhash == GetSHA1String(password, UserName));
		}
		
		public bool ChangePassword(string old_password, string new_password)
		{
			if(!Verify(old_password))
			{
				System.Threading.Thread.Sleep(250);
				return false;
			}
			string new_hash = GetSHA1String(new_password, UserName);
			NpgsqlTransaction tr;
			int rows = ExecuteNonqueryTr(
				"update users set passwd=:newpsw where id=:id and username=:username and passwd=:oldpsw", out tr,
				new NpgsqlParameter("id", UserId),
				new NpgsqlParameter("username", UserName),
				new NpgsqlParameter("oldpsw", passwdhash),
				new NpgsqlParameter("newpsw", new_hash));
			bool result = (rows == 1);
			if(result)
			{
				tr.Commit();
				passwdhash = new_hash;
				return true;
			}
			else
			{
				tr.Rollback();
				return false;
			}
		}
		
		public void Open()
		{
			if(Connection.State == ConnectionState.Closed)
			{
				Connection.Open();
				using(NpgsqlCommand cmd = this.Connection.CreateCommand())
				{
					cmd.CommandText = "SET client_encoding = 'UTF8'";
					cmd.ExecuteNonQuery();
				}
			}
		}
		
		public void Close()
		{
			if(Connection != null && Connection.State != ConnectionState.Closed)
				Connection.Close();
		}
			
		public void Dispose()
		{
			Close();
			if(Connection != null)
			{
				Connection.Dispose();
				Connection = null;
			}
		}
		#endregion

		#region SQL helpers
		
		public bool TestConnection()
		{
			return ("OK" == (string)ExecuteScalar("select 'OK';"));
		}
		
		public int ExecuteNonquery(string sql)
		{
			using(NpgsqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = sql;
				return cmd.ExecuteNonQuery();
			}
		}
		
		public int ExecuteNonquery(string sql, params NpgsqlParameter[] parameters)
		{
			using(NpgsqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = sql;
				foreach(NpgsqlParameter param in parameters)
					cmd.Parameters.Add(param);
				return cmd.ExecuteNonQuery();
			}
		}
		
		public int ExecuteNonqueryTr(string sql, out NpgsqlTransaction transaction, params NpgsqlParameter[] parameters)
		{
			transaction = this.Connection.BeginTransaction();
			try
			{
				using(NpgsqlCommand cmd = this.Connection.CreateCommand())
				{
					cmd.CommandText = sql;
					foreach(NpgsqlParameter param in parameters)
						cmd.Parameters.Add(param);
					return cmd.ExecuteNonQuery();
				}
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}
		
		public object ExecuteScalar(string sql)
		{
			using(NpgsqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = sql;
				return cmd.ExecuteScalar();
			}
		}
		
		public object ExecuteScalar(string sql, params NpgsqlParameter[] parameters)
		{
			using(NpgsqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = sql;
				foreach(NpgsqlParameter param in parameters)
					cmd.Parameters.Add(param);
				return cmd.ExecuteScalar();
			}
		}
		
		public NpgsqlCommand CreateCommand()
		{
			NpgsqlCommand cmd = this.Connection.CreateCommand();
			return cmd;
		}

		public NpgsqlCommand CreateCommand(string sql)
		{
			NpgsqlCommand cmd = this.Connection.CreateCommand();
			cmd.CommandText = sql;
			return cmd;
		}
		
		public NpgsqlCommand CreateCommand(string sql, params NpgsqlParameter[] parameters)
		{
			NpgsqlCommand cmd = this.Connection.CreateCommand();
			cmd.CommandText = sql;
			foreach(NpgsqlParameter param in parameters)
				cmd.Parameters.Add(param);
			return cmd;
		}
		
		public void UpdateUserInfo(DataTable table)
		{
			DateTime now = DateTime.Now;
			int id_user_create = table.Columns.IndexOf("id_user_create");
			int dt_create = table.Columns.IndexOf("dt_create");
			int id_user_modify = table.Columns.IndexOf("id_user_modify");
			int dt_modify = table.Columns.IndexOf("dt_modify");

			foreach(DataRow row in table.Rows)
			{
				switch(row.RowState)
				{
				case DataRowState.Added:
					if(id_user_create >= 0)
						row[id_user_create] = this.UserId;
					if(dt_create >= 0)
						row[dt_create] = now;
					if(id_user_create >= 0)
						row[id_user_modify] = this.UserId;
					if(dt_create >= 0)
						row[dt_modify] = now;
					break;
				case DataRowState.Modified:
					if(id_user_create >= 0)
						row[id_user_modify] = this.UserId;
					if(dt_create >= 0)
						row[dt_modify] = now;
					break;
				}
			}
		}
		#endregion

		#region DataSet handling
		public DataSet GetDataSetSimple(string sql)
		{
			DataSet ds = new DataSet();
			using(NpgsqlTransaction trans = Connection.BeginTransaction())
			{
				using(NpgsqlCommand command = CreateCommand(sql))
				using(NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
				{
					adapter.Fill(ds);
					foreach(DataTable table in ds.Tables)
					{
						DataColumn col = table.Columns[0];
						if(col.ColumnName == "id")
							table.PrimaryKey = new DataColumn[] { col };
					}
				}
				trans.Commit();
				return ds;
			}
		}
		
		public DataSet GetDataSet(string sql, NpgsqlParameter[] parameters)
		{
			DataSet ds = new DataSet();
			using(NpgsqlTransaction trans = Connection.BeginTransaction())
			{
				using(NpgsqlCommand command = CreateCommand(sql))
				using(NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
				{
					foreach(NpgsqlParameter parameter in parameters)
						command.Parameters.Add(parameter);
					adapter.Fill(ds);
					foreach(DataTable table in ds.Tables)
					{
						DataColumn col = table.Columns[0];
						if(col.ColumnName == "id")
							table.PrimaryKey = new DataColumn[] { col };
					}
				}
				trans.Commit();
				return ds;
			}
		}
		
		private void WriteValue(StringBuilder sb, object val)
		{
			if(val == null || val is DBNull)
				sb.Append("<FONT COLOR=\"#0000ff\">null</FONT>");
			else if (val is String)
				sb.Append("<CODE>'").Append(val).Append("'</CODE>");
			else
				sb.Append("<CODE>").Append(val).Append("</CODE>");
		}
		
		public int SaveDataSet(DataSet changes, bool updateUserInfo, string selectSql, NpgsqlParameter[] parameters)
		{
			using(NpgsqlTransaction trans = Connection.BeginTransaction())
			{
				try
				{
					int result = 0;
					using(NpgsqlCommand command = CreateCommand(selectSql))
					using(NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
					{
						foreach(NpgsqlParameter param in parameters)
							command.Parameters.Add(param);
						
						using(NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(adapter))
						{
							foreach(DataTable table in changes.Tables)
							{
								if(updateUserInfo)
									UpdateUserInfo(table);
								try
								{
									result += adapter.Update(table);
								}
								catch(DBConcurrencyException cerr)
								{
									StringBuilder sb = new StringBuilder();
									sb.AppendLine("<!--HTML--><HTML><HEAD><TITLE>Chyba - řádek byl změněn</TITLE></HEAD><BODY>");
									sb.AppendLine("<BIG><B>Řádek byl od doby posledního načtení změněn</B></BIG><BR />");
									sb.Append("<B>Zpráva chyby:</B> " + cerr.Message + "<BR />");
									sb.Append("<B>Select SQL:</B><BR /><CODE>" + selectSql + "</CODE><BR />");
									//sb.AppendLine("Select SQL: " + adapter.SelectCommand.CommandText);
									//sb.AppendLine("Update SQL: " + adapter.UpdateCommand.CommandText);
									//sb.AppendLine("Insert SQL: " + adapter.InsertCommand.CommandText);
									//sb.AppendLine("Delete SQL: " + adapter.DeleteCommand.CommandText);
									sb.AppendLine("<B>Řádek:</B><BR /><TABLE border=\"1\"><TR><TD><B>Sloupec</B></TD><TD><B>Původní hodnota</B></TD><TD><B>Nová hodnota</B></TD><TD><B>Typ</B></TD></TR>");
									foreach(DataColumn col in table.Columns)
									{
										object orig = cerr.Row[col, DataRowVersion.Original];
										object current = cerr.Row[col, DataRowVersion.Current];
										bool is_changed = (orig != null) ? (!orig.Equals(current)) : ((current != null) ? (!current.Equals(orig)) : (current != orig));
										sb.Append("<TR><TD>");
										if(is_changed) sb.Append("<B>");
										sb.Append(col.ColumnName);
										if(is_changed) sb.Append("</B>");
										sb.Append("</TD><TD>");
										WriteValue(sb, orig);
										sb.Append("</TD><TD>");
										WriteValue(sb, current);
										sb.Append("</TD><TD>");
										sb.Append(col.DataType.Name);
										sb.AppendLine("</TD></TR>");
									}
									sb.Append("</TABLE></BODY></HTML>");
									throw new SoapException(sb.ToString(), SoapException.ServerFaultCode);
								}
							}
						}
					}
					trans.Commit();
					return result;
				}
				catch(SoapException)
				{
					trans.Rollback();
					throw;
				}
				catch(NpgsqlException err)
				{
					trans.Rollback();
					throw RaiseNpgsqlException(err);
				}
				catch(Exception ex)
				{
					trans.Rollback();
					throw new SoapException(String.Format("Save error: select SQL:\n{0}\nERR: {1}", selectSql, ex),
						SoapException.ServerFaultCode);
				}
			}
		}
		
		#endregion
	
		private SoapException RaiseNpgsqlException(NpgsqlException err)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("SQL chyba ").AppendLine(err.Code);
			sb.AppendLine("SQL:");
			sb.AppendLine(err.ErrorSql);
			sb.AppendLine(err.BaseMessage);
			sb.AppendLine(err.Detail);
			sb.AppendLine(err.HelpLink);
			sb.AppendLine(err.Hint);
			sb.AppendLine(err.Where);
			return new SoapException(sb.ToString(),
				SoapException.ServerFaultCode);
		}
		
	}
}
