using System;
using System.Data;

namespace LPS.Client
{
	public class RowColumnBinding : BindingBase
	{
		public RowColumnBinding()
		{
		}

		public RowColumnBinding(DataColumn col)
		{
			this.column = col;
		}

		public RowColumnBinding(DataColumn col, DataRow row)
		{
			this.column = col;
			this.row = row;
			Bind();
		}

		public override bool IsMaster
		{
			get { return true; }
		}

		private DataRow row;
		public DataRow Row
		{
			get { return row; }
			set
			{
				Unbind();
				row = value;
				Bind();
			}
		}
		
		private DataColumn column;
		public DataColumn Column
		{
			get { return column; }
			set
			{
				Unbind();
				column = value;
				Bind();
			}
		}
			
		protected override void DoValueChanged()
		{
			if(row == null || column == null)
			{
				DoValueChanged(null, null, true, false);
				return;
			}

			object orig = null;
			object val = null;

			if(row.HasVersion(DataRowVersion.Original))
				orig = row[column, DataRowVersion.Original];
			
			if(row.HasVersion(DataRowVersion.Current))
				val = row[column, DataRowVersion.Current];
			else if(row.HasVersion(DataRowVersion.Proposed))
				val = row[column, DataRowVersion.Proposed];

			DoValueChanged(orig, val, column.ReadOnly, true);
		}
		
		protected override void DoUpdateValue (BindingInfo info)
		{
			if(row == null || column == null)
				return;
			
			Log.Debug("RowColumnBinding.DoUpdateValue: {0} <-- {1}", column.ColumnName, info.Value);
			
			if(info.ValueIsNull)
				row[column] = DBNull.Value;
			else
				row[column] = Convert.ChangeType(info.Value, column.DataType);
		}

		bool is_updating;
		private void HandleColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			if(is_updating || e.Row != row || IsUpdating)
				return;
			if(e.Column == null || e.Column == column)
			{
				is_updating = true;
				try
				{
					DoValueChanged();
				}
				finally
				{
					is_updating = false;
				}
			}
		}

		protected override void Bind()
		{
			if(row != null)
			{
				if(column != null && column.Table != row.Table)
					throw new InvalidOperationException("Řádek a sloupec nepatří do jedné tabulky");
				row.Table.ColumnChanged += HandleColumnChanged;
			}
			base.Bind();
		}

		protected override void Unbind()
		{
			if(row != null)
				row.Table.ColumnChanged -= HandleColumnChanged;
			base.Unbind();
		}
		
		public override void Dispose()
		{
			this.Row = null;
			this.Column = null;
			base.Dispose();
		}
	}
}
