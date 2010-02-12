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
				DoValueChanged(null, null);
				return;
			}

			object orig = null;
			object val = null;

			if(row.HasVersion(DataRowVersion.Original))
				orig = row[column, DataRowVersion.Original];
			
			if(row.HasVersion(DataRowVersion.Default))
				val = row[column, DataRowVersion.Default];

			DoValueChanged(orig, val);
		}
		
		protected override void DoUpdateValue (object orig_value, object new_value)
		{
			if(row == null || column == null)
				return;
			
			Console.WriteLine(String.Format("{0} <-- {1}", column.ColumnName, new_value));
			
			if(new_value == null || new_value == DBNull.Value)
				row[column] = DBNull.Value;
			else
				row[column] = Convert.ChangeType(new_value, column.DataType);
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
