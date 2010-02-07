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
	
	public class ConnectionInfo : IDisposable, IResourceStore
	{
		/// pro overeni hesla pri zmene
		private string passwdhash;
		
		public long UserId { get; set; }
		public string UserName { get; set; }

		public NpgsqlConnection Connection { get; set; }
		public ResourceManager Resources { get; set; }
		
		#region Management
		private ConnectionInfo()
		{
			Resources = new ResourceManager(this);
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
			this.Resources.Dispose();
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
		
		public int SaveDataSetAndNotify()
		{
			return 0;
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
				catch
				{
					trans.Rollback();
					throw;
				}
			}
		}
		
		#endregion
	
		public string GetTextResource(string path)
		{
			return Server._GetTextResource(path);
		}
		
		public NpgsqlCommand CreateCommand(TableInfo table, string addsql)
		{
			string sql = table.ListSql ?? table.EditSql;
			if(addsql == null)
				addsql = "";
			string sqllower = sql.ToLower();
			int idx = sqllower.IndexOf("{where}");
			bool isadd = String.IsNullOrEmpty(addsql.Trim());
			string op;
			if(idx >= 0)
			{
				op = isadd ? "" : " WHERE ";
				return CreateCommand(sql.Substring(0, idx) + op + addsql + sql.Substring(idx + "{where}".Length));
			}
			idx = sqllower.IndexOf("{and}");
			if(idx >= 0)
			{
				op = isadd ? "" : " AND ";
				return CreateCommand(sql.Substring(0, idx) + op + addsql + sql.Substring(idx + "{and}".Length));
			}
			op = isadd ? "" : " WHERE ";
			return CreateCommand(sql + op + addsql);
		}

		public NpgsqlCommand CreateCommand(TableInfo table, NpgsqlParameter[] parameters)
		{
			NpgsqlCommand command = CreateCommand();
			string sql = table.ListSql ?? table.EditSql;
			string sqllower = sql.ToLower();
			StringBuilder paramstr = new StringBuilder();
			bool first = true;
			foreach(NpgsqlParameter parameter in parameters)
			{
				if(first) first = false; else paramstr.Append(" AND ");
				object val = parameter.Value;
				if(val == null || val is DBNull)
					paramstr.AppendFormat("({0} IS NULL)", parameter.SourceColumn);
				else
					paramstr.AppendFormat("({0} = {1})", parameter.SourceColumn, parameter.ParameterName);
				command.Parameters.Add(parameter);
			}
			int idx = sqllower.IndexOf("{where}");
			string op;
			if(idx >= 0)
			{
				op = first ? "" : " WHERE ";
				command.CommandText = sql.Substring(0, idx) + op + paramstr.ToString() + sql.Substring(idx + "{where}".Length);
				return command;
			}
			idx = sqllower.IndexOf("{and}");
			if(idx >= 0)
			{
				op = first ? "" : " AND ";
				command.CommandText = sql.Substring(0, idx) + op + paramstr.ToString() + sql.Substring(idx + "{and}".Length);
				return command;
			}
			op = first ? "" : " WHERE ";
			command.CommandText = sql + op + paramstr.ToString();
			return command;
		}
		
		public DataSet GetDataSetByTableName(string table, NpgsqlParameter[] parameters)
		{
			TableInfo tableinfo = this.Resources.GetTableInfo(table);
			DataSet ds = new DataSet();
			using(NpgsqlTransaction trans = Connection.BeginTransaction())
			{
				using(NpgsqlCommand command = CreateCommand(tableinfo, parameters))
				using(NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
				{
					adapter.Fill(ds);
					foreach(DataTable dt in ds.Tables)
					{
						DataColumn col = dt.Columns[0];
						if(col.ColumnName == "id")
						{
							dt.PrimaryKey = new DataColumn[] { col };
						}
					}
					ds.ExtendedProperties.Add("TABLE", table);
					ds.ExtendedProperties.Add("SQL", command.CommandText);
				}
				trans.Commit();
				return ds;
			}
		}
		
		public DataSet GetDataSetByTableName(string table, string addsql)
		{
			TableInfo tableinfo = this.Resources.GetTableInfo(table);
			DataSet ds = new DataSet();
			using(NpgsqlTransaction trans = Connection.BeginTransaction())
			{
				using(NpgsqlCommand command = CreateCommand(tableinfo, addsql))
				using(NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
				{
					adapter.Fill(ds);
					foreach(DataTable dt in ds.Tables)
					{
						DataColumn col = dt.Columns[0];
						if(col.ColumnName == "id")
						{
							dt.PrimaryKey = new DataColumn[] { col };
						}
					}
					ds.ExtendedProperties.Add("TABLE", table);
					ds.ExtendedProperties.Add("SQL", command.CommandText);
				}
				trans.Commit();
				return ds;
			}
		}
		
		public int SaveDataSetByTableName(string tablename, DataSet changes, bool updateUserInfo, bool changesNotify)
		{
			try
			{
				using(DataTableUpdater updater = new DataTableUpdater(this, changes.Tables[0], tablename))
				{
					updater.UpdateUserInfo = updateUserInfo;
					updater.NotifyChangeSink = changesNotify;
					return updater.Run();
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw;
			}
		}
	}
}
