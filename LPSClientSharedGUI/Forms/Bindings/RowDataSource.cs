using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Gtk;

namespace LPS.Client
{
	public class RowDataSource
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
			set { row = value; AssignRow(); }
		}
		
		private void AssignRow()
		{
			foreach(RowColumnBinding binding in column_bindings.Values)
				binding.Row = row;
		}
		
		public BindingGroup GetGroupForColumn(DataColumn column)
		{
			RowColumnBinding row_col_binding;
			if(column_bindings.TryGetValue(column, out row_col_binding))
				return row_col_binding.Bindings;
			row_col_binding = new RowColumnBinding(column, row);
			BindingGroup grp = new BindingGroup(column.DataType);
			grp.Add(row_col_binding);
			return grp;
		}
	}
}
