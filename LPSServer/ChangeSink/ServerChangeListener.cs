using System;
using System.Data;
using System.Collections.Generic;

namespace LPS.Server
{
	public class ServerChangeListener: IDisposable
	{
		private DataSet data;
		private Dictionary<string, DataTable> tables;
		internal DateTime last_access;
		
		public ServerChangeListener ()
		{
			data = null;
			tables = new Dictionary<string, DataTable>();
			last_access = DateTime.Now;
		}
		
		public int SinkIndex { get; set; }
		public int Security
		{
			get
			{
				return this.GetHashCode();
			}
		}
		
		private DataTable CreateTable(string table_name, DataRow row)
		{
			if(data == null)
				data = new DataSet("ds_changes");
			DataTable table = data.Tables.Add(table_name);
			foreach(DataColumn dc_src in row.Table.Columns)
			{
				table.Columns.Add(dc_src.ColumnName, dc_src.DataType);
			}
			table.Columns.Add("_S", typeof(char));
			table.PrimaryKey = new DataColumn[] { table.Columns[0] };
			return table;
		}
		
		private void AppendRow(DataTable table, long id, char type, DataRow src_row)
		{
			DataRow new_row = table.NewRow();
			int col_count = table.Columns.Count;
			new_row[col_count - 1] = type;
			if(type == 'D')
			{
				new_row["id"] = id;
			}
			else
			{
				for(int i = 0; i < col_count - 1; i++)
					new_row[i] = src_row[i];
			}
			table.Rows.Add(new_row);
		}
		
		private void UpdateRow(DataTable table, DataRow new_row, char type, DataRow src_row)
		{
			int col_count = table.Columns.Count;
			if(type != 'D')
			{
				for(int i = 0; i < col_count - 1; i++)
					new_row[i] = src_row[i];
			}
			new_row[col_count - 1] = type;
		}
		
		public void AddNewData(string table_name, long id, char type, DataRow row)
		{
			lock(this)
			{
				if(tables.ContainsKey(table_name))
				{
					DataTable table = tables[table_name];
					if(table == null)
					{
						table = CreateTable(table_name, row);
						AppendRow(table, id, type, row);
					}
					else
					{
						DataRow new_row = table.Rows.Find(id);
						if(new_row != null)
							UpdateRow(table, new_row, type, row);
						else
							AppendRow(table, id, type, row);
					}
				}
			}
		}
		
		public void ListenTo(string table_name)
		{
			lock(this)
			{
				if(!tables.ContainsKey(table_name))
					tables[table_name] = null;
			}
		}
		
		public void StopListenTo(string table_name)
		{
			DataTable table = null;
			lock(this)
			{
				if(tables.ContainsKey(table_name))
				{
					table = tables[table_name];
					if(table != null)
						table.DataSet.Tables.Remove(table);
					tables.Remove(table_name);
				}
			}
			if(table != null)
				table.Dispose();
		}
		
		public DataSet GetChangesAndClear()
		{
			lock(this)
			{
				last_access = DateTime.Now;
				DataSet result = data;
				data = null;
				tables = new Dictionary<string, DataTable>();
				return result;
			}
		}
		
		public void Dispose()
		{
			lock(this)
			{
				if(data != null)
					data.Dispose();
				data = null;
				tables = new Dictionary<string, DataTable>();
			}
		}
	}
}
