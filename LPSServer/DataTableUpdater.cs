using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Text;
using NpgsqlTypes;

namespace LPS.Server
{
	public class DataTableUpdater: IDisposable
	{
		private DateTime dt_now;
		private ConnectionInfo connection;
		private DataTable table;
		private string table_name;
		private DataSet schema_dataset;

		//private NpgsqlCommand select_cmd;
		private NpgsqlCommand insert_cmd;
		private NpgsqlCommand update_cmd;
		private NpgsqlCommand delete_cmd;
		
		public bool NotifyChangeSink { get; set; }
		public bool UpdateUserInfo { get; set; }
		public long UserId { get; set; }
		
		public DataTableUpdater(ConnectionInfo Connection, DataTable Table, string table_name)
		{
			this.dt_now = DateTime.Now;
			this.connection = Connection;
			this.table = Table;
			this.table_name = table_name;
			this.NotifyChangeSink = true;
			this.UpdateUserInfo = true;
			this.UserId = connection.UserId;
		}
		
		public void Dispose()
		{
			//if(select_cmd != null) { select_cmd.Dispose(); select_cmd = null; }
			if(insert_cmd != null) { insert_cmd.Dispose(); insert_cmd = null; }
			if(update_cmd != null) { update_cmd.Dispose(); update_cmd = null; }
			if(delete_cmd != null) { delete_cmd.Dispose(); delete_cmd = null; }
			if(schema_dataset != null) { schema_dataset.Dispose(); schema_dataset = null; }
		}
		
		private DataTable GetSchemaDataTable()
		{
			if(schema_dataset != null)
				return schema_dataset.Tables[0];
			schema_dataset = new DataSet();
			string sql = String.Format("select * from {0} where 1=0", table_name);
			using(NpgsqlCommand cmd = connection.CreateCommand(sql))
			using(NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
			{
				adapter.Fill(schema_dataset);
			}
			return schema_dataset.Tables[0];
		}

		private NpgsqlDbType GetNpgsqlType(Type type)
		{
			switch(type.Name)
			{
			case "String":
				return NpgsqlDbType.Varchar;
			case "Int32":
				return NpgsqlDbType.Integer;
			case "Int64":
				return NpgsqlDbType.Bigint;
			case "Boolean":
				return NpgsqlDbType.Boolean;
			case "Decimal":
				return NpgsqlDbType.Numeric;
			case "DateTime":
				return NpgsqlDbType.Timestamp;
			default:
				throw new Exception("Neznamy typ: " + type.FullName);
			}
		}
		
		private NpgsqlParameter[] CreateParameters(DataRowVersion version, DataRow row)
		{
			string prefix = "";
			if(version == DataRowVersion.Original)
				prefix = "o__";
			DataTable sch_tab = GetSchemaDataTable();
			List<NpgsqlParameter> result = new List<NpgsqlParameter>();
			foreach(DataColumn col in sch_tab.Columns)
			{
				if(row.Table.Columns.IndexOf(col.ColumnName) >= 0)
				{
					NpgsqlParameter param = new NpgsqlParameter(prefix + col.ColumnName, GetNpgsqlType(col.DataType));
					param.SourceColumn = col.ColumnName;
					param.SourceVersion = version;
					result.Add(param);
				}
			}
			return result.ToArray();
		}
		
		private NpgsqlCommand GetInsertCommand(DataRow row)
		{
			if(insert_cmd != null)
				return insert_cmd;
			insert_cmd = connection.CreateCommand();
			NpgsqlParameter[] insert_params = CreateParameters(DataRowVersion.Current, row);
			string[] col_names = new string[insert_params.Length];
			string[] param_names = new string[insert_params.Length];
			for(int i=0; i < insert_params.Length; i++)
			{
				NpgsqlParameter p = insert_params[i];
				col_names[i] = p.SourceColumn;
				if(p.SourceColumn == "ts")
				{
					param_names[i] = "now()";
				}
				else
				{
					param_names[i] = p.ParameterName;
					insert_cmd.Parameters.Add(p);
				}
			}
			insert_cmd.CommandText = String.Format("insert into {0} ({1}) values ({2})", 
				table_name, String.Join(", ", col_names), String.Join(", ", param_names));
			insert_cmd.Prepare();
			return insert_cmd;
		}

		private NpgsqlCommand GetUpdateCommand(DataRow row)
		{
			if(update_cmd != null)
				return update_cmd;
			update_cmd = connection.CreateCommand();
			NpgsqlParameter[] new_params = CreateParameters(DataRowVersion.Current, row);
			NpgsqlParameter[] orig_params = CreateParameters(DataRowVersion.Original, row);
			StringBuilder sb = new StringBuilder();
			sb.Append("update ").Append(table_name).Append(" set ");
			for(int i=0; i < new_params.Length; i++)
			{
				NpgsqlParameter p = new_params[i];
				if(p.SourceColumn == "ts")
				{
					sb.Append(p.SourceColumn).Append("=now()");
				}
				else
				{
					sb.Append(p.SourceColumn).Append("=").Append(p.ParameterName);
					update_cmd.Parameters.Add(p);
				}
				if(i != new_params.Length - 1)
					sb.Append(", ");
			}
			sb.Append(" where ");
			for(int i=0; i < orig_params.Length; i++)
			{
				NpgsqlParameter p = orig_params[i];

				if(p.SourceColumn == "ts" || 
				   p.SourceColumn == "id_user_create" || 
				   p.SourceColumn == "dt_create" || 
				   p.SourceColumn == "id_user_modify" || 
				   p.SourceColumn == "dt_modify")
					continue;
					
				if(i != 0)
					sb.Append(" and ");
				
				sb.AppendFormat("({0}={1} or {0} is null)", p.SourceColumn, p.ParameterName);
				update_cmd.Parameters.Add(p);
			}
			
			update_cmd.CommandText = sb.ToString();
			update_cmd.Prepare();
			return update_cmd;
		}

		private NpgsqlCommand GetDeleteCommand(DataRow row)
		{
			if(delete_cmd != null)
				return delete_cmd;
			delete_cmd = connection.CreateCommand();
			NpgsqlParameter[] orig_params = CreateParameters(DataRowVersion.Original, row);
			StringBuilder sb = new StringBuilder();
			sb.Append("delete from ").Append(table_name).Append(" where ");
			for(int i=0; i < orig_params.Length; i++)
			{
				NpgsqlParameter p = orig_params[i];
				if(p.SourceColumn == "ts" || 
				   p.SourceColumn == "id_user_create" || 
				   p.SourceColumn == "dt_create" || 
				   p.SourceColumn == "id_user_modify" || 
				   p.SourceColumn == "dt_modify")
					continue;
					
				if(i != 0)
					sb.Append(" and ");
				sb.AppendFormat("({0}={1} or {0} is null)", p.SourceColumn, p.ParameterName);
				delete_cmd.Parameters.Add(p);
			}
			
			update_cmd.CommandText = sb.ToString();
			update_cmd.Prepare();
			return update_cmd;
		}

		private int CheckAffected(int affected, DataRow row)
		{
			if(affected == 0)
			{
				string message = String.Format("SQL Příkaz postihl {0} řádků, to znamená že řádek mohl být již změněn někým jiným (tabulka {1})", affected, table_name);
				throw new DBConcurrencyException(message, null, new DataRow[] { row });
			}
			else if(affected > 1)
			{
				string message = String.Format("SQL Příkaz postihl {0} řádků, to znamená že je nejednoznačný (tabulka {1})", affected, table_name);
				throw new DBConcurrencyException(message, null, new DataRow[] { row });
			}
			return affected;
		}
		
		private int InsertRow(DataRow row)
		{
			NpgsqlCommand cmd = GetInsertCommand(row);
			foreach(NpgsqlParameter p in cmd.Parameters)
			{
				if(this.UpdateUserInfo)
				{
					if(p.SourceColumn == "id_user_create" || p.SourceColumn == "id_user_modify")
					{
						p.Value = this.UserId;
						continue;
					}
					if(p.SourceColumn == "dt_create" || p.SourceColumn == "dt_modify")
					{
						p.Value = this.dt_now;
						continue;
					}
				}
				p.Value = row[p.SourceColumn, p.SourceVersion] ?? DBNull.Value;
			}
			return CheckAffected(cmd.ExecuteNonQuery(), row);
		}
		
		private int UpdateRow(DataRow row)
		{
			NpgsqlCommand cmd = GetUpdateCommand(row);
			foreach(NpgsqlParameter p in cmd.Parameters)
			{
				if(this.UpdateUserInfo && p.SourceVersion == DataRowVersion.Current)
				{
					if(p.SourceColumn == "id_user_create" || p.SourceColumn == "id_user_modify")
					{
						p.Value = this.UserId;
						continue;
					}
					if(p.SourceColumn == "dt_create" || p.SourceColumn == "dt_modify")
					{
						p.Value = this.dt_now;
						continue;
					}
				}
				p.Value = row[p.SourceColumn, p.SourceVersion] ?? DBNull.Value;
			}
			return CheckAffected(cmd.ExecuteNonQuery(), row);
		}
		
		private int DeleteRow(DataRow row)
		{
			NpgsqlCommand cmd = GetDeleteCommand(row);
			foreach(NpgsqlParameter p in cmd.Parameters)
			{
				p.Value = row[p.SourceColumn, p.SourceVersion] ?? DBNull.Value;
			}
			return CheckAffected(cmd.ExecuteNonQuery(), row);
		}
		
		public void DoNotifyChange(char stav, DataRow row)
		{
			switch(stav)
			{
			case 'D':
				ServerChangeSink.AddNewData(table_name, Convert.ToInt64(row["id", DataRowVersion.Original]), stav, null);
				return;
			default:
				ServerChangeSink.AddNewData(table_name, Convert.ToInt64(row["id"]), stav, row);
				return;
			}
		}
		
		public int Run()
		{
			dt_now = DateTime.Now;
			int affected = 0;
			using(NpgsqlTransaction trans = connection.Connection.BeginTransaction())
			{
				try
				{
					foreach(DataRow row in table.Rows)
					{
						switch(row.RowState)
						{
						case DataRowState.Added:
							affected += InsertRow(row);
							continue;
						case DataRowState.Modified:
							affected += UpdateRow(row);
							continue;
						case DataRowState.Deleted:
							affected += DeleteRow(row);
							continue;
						}
					}
					trans.Commit();
					
					if(this.NotifyChangeSink)
					{
						foreach(DataRow row in table.Rows)
						{
							switch(row.RowState)
							{
							case DataRowState.Added:
								DoNotifyChange('I', row);
								continue;
							case DataRowState.Modified:
								DoNotifyChange('U', row);
								continue;
							case DataRowState.Deleted:
								DoNotifyChange('D', row);
								continue;
							}
						}
					}
					return affected;
				}
				catch
				{
					trans.Rollback();
					throw;
				}
			}
		}
	}
}
