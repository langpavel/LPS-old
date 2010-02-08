using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Gtk;

namespace LPS.Client
{
	public class RowDataSource : IDisposable
	{
		private Dictionary<DataColumn, RowColumnBinding> column_bindings;
		private DataRow row;
		
		public RowDataSource()
		{
			column_bindings = new Dictionary<DataColumn, RowColumnBinding>();
		}
		
		public DataRow Row
		{
			get { return row; }
			set 
			{ 
				Unbind(); 
				row = value; 
				DoRowChanged(new DataRowChangeEventArgs(row, DataRowAction.ChangeCurrentAndOriginal)); 
				Bind(); 
			}
		}
		
		public event DataRowChangeEventHandler RowChanged;
		
		private void DoRowChanged(DataRowChangeEventArgs e)
		{
			if(RowChanged != null)
				RowChanged(this, e);
		}
		
		void HandleRowChanged (object sender, DataRowChangeEventArgs e)
		{
			if(e.Row == this.row)
				DoRowChanged(e);
		}
		
		private void Bind()
		{
			foreach(RowColumnBinding binding in column_bindings.Values)
				binding.Row = row;
			if(row == null)
				return;
			row.Table.RowChanged += HandleRowChanged;
		}
		
		private void Unbind()
		{
			if(row == null)
				return;
			row.Table.RowChanged -= HandleRowChanged;
		}

		public BindingGroup GetGroupForColumn(DataColumn column)
		{
			RowColumnBinding row_col_binding;
			if(column_bindings.TryGetValue(column, out row_col_binding))
				return row_col_binding.Bindings;
			row_col_binding = new RowColumnBinding(column, row);
			BindingGroup grp = new BindingGroup(column.DataType);
			grp.Add(row_col_binding);
			column_bindings[column] = row_col_binding;
			return grp;
		}
		
		public string FormatRowToText(string text)
		{
			if(row == null)
				return "Nepřiřazen záznam";
			string result;
			DataRowVersion ver;
			if(row.RowState == DataRowState.Deleted)
			{
				ver = DataRowVersion.Original;
				result = "Odstraněno: " + text;
			}
			else
			{
				ver = DataRowVersion.Default;
				result = text;
			}
				
			foreach(DataColumn col in row.Table.Columns)
			{
				object val = row[col, ver];
				string str;
				if(val == null || val == DBNull.Value)
					str = "";
				else
					str = val.ToString();
				result = result.Replace("{"+col.ColumnName+"}", str);
			}
			return result.Trim();
		}
		
		public virtual void Dispose()
		{
			foreach(RowColumnBinding rcb in column_bindings.Values)
			{
				rcb.Bindings.Dispose();
			}
			column_bindings.Clear();
		}
	}
}
