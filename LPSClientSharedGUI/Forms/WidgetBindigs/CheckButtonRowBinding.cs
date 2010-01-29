using System;
using System.Data;
using Gtk;

namespace LPS.Client
{

	public class CheckButtonRowBinding: WidgetRowBinding
	{
		public CheckButton CheckBox
		{
			get { return this.Widget as CheckButton; }
			set { this.Widget = value; }
		}

		public CheckButtonRowBinding()
		{
		}
		
		public CheckButtonRowBinding(CheckButton chk_button, DataColumn column, DataRow row)
		{
			this.CheckBox = chk_button;
			this.Column = column;
			this.Row = row;
			UptadeCheckBoxValue(row[Column]);
			Bind();
		}
	
		protected void UptadeCheckBoxValue(object value)
		{
			if(value == null || value is DBNull)
			{
				CheckBox.Active = false;
				CheckBox.Inconsistent = true;
			}
			else if(Convert.ToBoolean(value))
			{
				CheckBox.Inconsistent = false;
				CheckBox.Active = true;
			}
			else
			{
				CheckBox.Inconsistent = false;
				CheckBox.Active = false;
			}
		}
		
		public override void Bind ()
		{
			CheckBox.Toggled += HandleCheckBoxToggled;
			this.Row.Table.ColumnChanged += HandleRowTableColumnChanged;
		}

		public override void Unbind ()
		{
			try
			{
				if(this.CheckBox != null)
				{
					this.CheckBox.Toggled -= HandleCheckBoxToggled;
					this.CheckBox = null;
				}
				if(this.Row != null && this.Row.Table != null)
				{
					this.Row.Table.ColumnChanged -= HandleRowTableColumnChanged;
					this.Row = null;
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		void HandleCheckBoxToggled (object sender, EventArgs e)
		{
			object o = Convert.ChangeType(((CheckButton)sender).Active, Column.DataType);
			Console.WriteLine("{0} <==\t'{1}'", Column.ColumnName, o);
			Row[Column] = o;
		}
		
		void HandleRowTableColumnChanged (object sender, DataColumnChangeEventArgs e)
		{
			if(e.Column == this.Column && e.Row == this.Row)
				UptadeCheckBoxValue(e.ProposedValue);
		}
		
	}

}
