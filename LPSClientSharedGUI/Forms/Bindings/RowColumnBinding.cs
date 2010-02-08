using System;
using System.Data;

namespace LPS.Client
{
	public class RowColumnBinding : BindingBase, IDisposable
	{
		public RowColumnBinding()
		{
		}

		private DataRow row;
		public DataRow Row
		{
			get { return row; }
			set { Unbind(); row = value; DoValueChanged(); Bind(); }
		}
		
		private DataColumn column;
		public DataColumn Column
		{
			get { return column; }
			set { column = value; DoValueChanged(); }
		}
			
		protected void DoValueChanged()
		{
			if(row == null || column == null)
			{
				DoUpdateValue(null, null);
				return;
			}
			
			object orig = null;
			object val = null;

			if(row.HasVersion(DataRowVersion.Original))
				orig = row[column, DataRowVersion.Original];
			
			if(row.HasVersion(DataRowVersion.Default))
				val = row[column, DataRowVersion.Default];
			DoUpdateValue(orig, val);
		}
		
		protected override void DoUpdateValue (object orig_value, object new_value)
		{
			row[column] = Convert.ChangeType(new_value, column.DataType);
		}

		private void HandleColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			if(IsUpdating || e.Row != row)
				return;
			if(e.Column == null || e.Column == column)
			{
				DoValueChanged();
			}
		}

		private void Bind()
		{
			if(row == null)
				return;
			row.Table.ColumnChanged += HandleColumnChanged;
		}

		private void Unbind()
		{
			if(row == null)
				return;
			row.Table.ColumnChanged -= HandleColumnChanged;
		}
		
		public virtual void Dispose()
		{
			this.Row = null;
			this.Column = null;
		}
	}
}
