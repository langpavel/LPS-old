using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Npgsql;
using System.Web.Services.Protocols;
using System.Xml;
using System.Globalization;

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
				"select id from sys_user where username=:username and passwd=:passwd",
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
				"update sys_user set passwd=:newpsw where id=:id and username=:username and passwd=:oldpsw", out tr,
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

		#region CreateCommand
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
		
		public NpgsqlCommand CreateCommand(IListInfo table, string addsql)
		{
			string sql = table.ListSql;
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

		public NpgsqlCommand CreateCommand(IListInfo table, params NpgsqlParameter[] parameters)
		{
			NpgsqlCommand command = CreateCommand();
			string sql = table.ListSql;
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

		public NpgsqlCommand CreateCommand(IListInfo table, string addsql, params NpgsqlParameter[] parameters)
		{
			if(String.IsNullOrEmpty(addsql))
			   return CreateCommand(table, parameters);
			NpgsqlCommand result = CreateCommand(table, addsql);
			foreach(NpgsqlParameter parameter in parameters)
				result.Parameters.Add(parameter);
			return result;
		}
		#endregion

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

		public DataSet GetDataSetByName(string table, string addsql, params NpgsqlParameter[] parameters)
		{
			IListInfo info = this.Resources.GetListInfo(table);
			DataSet ds = new DataSet();
			using(NpgsqlTransaction trans = Connection.BeginTransaction())
			{
				using(NpgsqlCommand command = CreateCommand(info, addsql, parameters))
				using(NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
				{
					adapter.Fill(ds);
					foreach(DataTable dt in ds.Tables)
					{
						DataColumn pkcol = dt.Columns[0];
						if(pkcol.ColumnName == "id")
						{
							dt.PrimaryKey = new DataColumn[] { pkcol };
						}

						foreach(DataColumn col in dt.Columns)
						{
							ColumnInfo ci = info.GetColumnInfo(col.ColumnName);
							if(ci == null)
								continue;
							col.Caption = ci.Caption;
							//col.AllowDBNull = !ci.Required; // dela problemy pri vkladani radku do datatable
							if(ci.Default != null)
							{
								try
								{
									object val = Convert.ChangeType(ci.Default, col.DataType);
									col.DefaultValue = val;
								}
								catch { }
							}
							if(ci.Unique)
								col.Unique = true;
						}
					}
				}
				trans.Commit();
				return ds;
			}
		}

		/*
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
		*/
		
		public int SaveDataSet(string tablename, DataSet changes, bool updateUserInfo, bool changesNotify)
		{
			using(Log.Scope("SaveDataSet: table {0}", tablename))
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
					Log.Error(ex);
					throw;
				}
			}
		}

		public string GetGeneratorValue(string generator, DateTime sys_date)
		{
			using(Log.Scope("GetGeneratorValue({0},{1})", generator, sys_date))
			lock(this.GetType())
			{
				NpgsqlTransaction tr = Connection.BeginTransaction();
				try
				{
					DateTime dt_now = DateTime.Now;
					long gen_id; string gen_format; long gen_val_first; long gen_val_step;
					DateTime gen_dt; int gen_year, gen_month, gen_week, gen_day;
					using(NpgsqlCommand gen_cmd = this.CreateCommand(@"
						select
							sys_gen.id, sys_gen.format, sys_gen.value_first, sys_gen.value_step, sys_gen.user_lock,
							sys_gen_cyklus.sys_date, sys_gen_cyklus.year, sys_gen_cyklus.month, sys_gen_cyklus.week, sys_gen_cyklus.day
						from sys_gen
						inner join sys_gen_cyklus on (sys_gen.id_cyklus = sys_gen_cyklus.id)
						where sys_gen.kod=:kod"))
					{
						gen_cmd.Parameters.Add(new NpgsqlParameter("kod", generator));
						using(NpgsqlDataReader r = gen_cmd.ExecuteReader())
						{
							if(!r.Read())
								throw new ArgumentException("Neplatná hodnota 'KOD' generátoru", "generator");
							if(Convert.ToBoolean(r["user_lock"]))
								throw new ApplicationException("Generátor je zamčený");
							gen_id = Convert.ToInt64(r["id"]);
							gen_format = Convert.ToString(r["format"]);
							gen_val_first = Convert.ToInt64(r["value_first"]);
							gen_val_step = Convert.ToInt64(r["value_step"]);
							gen_dt = (Convert.ToBoolean(r["sys_date"])) ? sys_date : DateTime.Now;
							gen_year = (Convert.ToBoolean(r["year"])) ? gen_dt.Year : 0;
							gen_month = (Convert.ToBoolean(r["month"])) ? gen_dt.Month : 0;
							if(Convert.ToBoolean(r["week"]))
								gen_week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(gen_dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
							else
								gen_week = 0;
							gen_day = (Convert.ToBoolean(r["day"])) ? gen_dt.Year : 0;
						}
					}

					string result;
					using(NpgsqlCommand cmd_select = this.CreateCommand(String.Format("select id, value, user_lock from sys_gen_value where "+
						"id_gen={0} and year={1} and month={2} and week = {3} and day={4}",
						gen_id, gen_year, gen_month, gen_week, gen_day)))
					using(NpgsqlCommand cmd_update = this.CreateCommand())
					{
						using(NpgsqlDataReader r = cmd_select.ExecuteReader())
						{
							long val;
							if(r.Read())
							{
								if(Convert.ToBoolean(r["user_lock"]))
									throw new ApplicationException("Hodnota generátoru " + generator + " je uzamčená");
								long id = Convert.ToInt64(r["id"]);
								val = Convert.ToInt64(r["value"]);
								cmd_update.CommandText = String.Format("update sys_gen_value set value={0}, ts='{2:yyyy-MM-dd hh:mm:ss.ffffff}' where id={1}", val + gen_val_step, id, dt_now);
								if(cmd_update.ExecuteNonQuery() != 1)
									throw new ApplicationException("Aktualizace hodnoty generátoru "+generator+" se nezdařila");
							}
							else
							{
								val = gen_val_first;
								cmd_update.CommandText = String.Format(@"
									insert into sys_gen_value (id_gen, year, month, week, day, value, user_lock, ts)
									values ({0}, {1}, {2}, {3}, {4}, {5}, false, '{6:yyyy-MM-dd hh:mm:ss.ffffff}')", gen_id, gen_year, gen_month, gen_week, gen_day, gen_val_first + gen_val_step, dt_now);
								if(cmd_update.ExecuteNonQuery() != 1)
									throw new ApplicationException("Aktualizace hodnoty generátoru " + generator + " se nezdařila");
							}
							if(String.IsNullOrEmpty(gen_format))
								gen_format = "{X}";
							gen_format = gen_format.Replace("{X", "{0");
							gen_format = gen_format.Replace("{Y", "{1");
							gen_format = gen_format.Replace("{M", "{2");
							gen_format = gen_format.Replace("{W", "{3");
							gen_format = gen_format.Replace("{DT", "{5");
							gen_format = gen_format.Replace("{D", "{4");
							result = String.Format(gen_format, val, gen_year, gen_month, gen_week, gen_day, gen_dt);
						}
					}
					tr.Commit();
					ServerChangeSink.AddNewData("sys_gen_value", dt_now, false);
					Log.Debug("Generator value: '{0}'", result);
					return result;
				}
				catch(Exception err)
				{
					tr.Rollback();
					Log.Error(err);
					throw;
				}
				finally
				{
					tr.Dispose();
				}
			}
		}

		public DataSet GetRealSchemaInfo()
		{
			return GetDataSetSimple(
				@"SELECT cols.table_name, cols.ordinal_position,
					cols.column_name, cols.udt_name, cols.is_nullable,
					cols.character_maximum_length, cols.numeric_precision, cols.numeric_precision_radix, cols.numeric_scale,
					tc.constraint_type, tc.constraint_name, tc.table_name, kcu.column_name,
					ccu.table_name AS references_table,
					ccu.column_name AS references_field
				FROM information_schema.columns cols
				LEFT JOIN information_schema.key_column_usage kcu
					ON cols.column_name = kcu.column_name
					AND cols.table_schema = kcu.table_schema
					AND cols.table_name = kcu.table_name
				LEFT JOIN information_schema.table_constraints tc
					ON tc.constraint_catalog = kcu.constraint_catalog
					AND tc.constraint_schema = kcu.constraint_schema
					AND tc.constraint_name = kcu.constraint_name
				LEFT JOIN information_schema.referential_constraints rc
					ON tc.constraint_catalog = rc.constraint_catalog
					AND tc.constraint_schema = rc.constraint_schema
					AND tc.constraint_name = rc.constraint_name
				LEFT JOIN information_schema.constraint_column_usage ccu
					ON rc.unique_constraint_catalog = ccu.constraint_catalog
					AND rc.unique_constraint_schema = ccu.constraint_schema
					AND rc.unique_constraint_name = ccu.constraint_name
				WHERE cols.table_schema = 'public'
				ORDER BY cols.table_name, ordinal_position");

		}
	}
}
