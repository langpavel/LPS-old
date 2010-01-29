using System;
using System.Data;
using Gtk;

namespace LPSClient
{

	public class EntryRowBinding: WidgetRowBinding
	{
		public Entry Entry
		{
			get { return this.Widget as Entry; }
			set { this.Widget = value; }
		}
		
		public EntryRowBinding()
		{
		}
		
		public EntryRowBinding(Entry entry, DataColumn column, DataRow row)
		{
			this.Entry = entry;
			this.Column = column;
			this.Row = row;
			UptadeEntryValue(row[Column]);
			Bind();
		}
	
		protected void UptadeEntryValue(object value)
		{
			string s = Convert.ToString(value);
			//Console.WriteLine("ENTRY <==\t'{0}'", s);
			this.Entry.Text = s;
		}
		
		public override void Bind ()
		{
			this.Entry.Changed += HandleEntryChanged;
			this.Row.Table.ColumnChanged += HandleRowTableColumnChanged;
		}

		public override void Unbind ()
		{
			try
			{
				if(this.Entry != null)
				{
					this.Entry.Changed -= HandleEntryChanged;
					this.Entry = null;
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

		void HandleEntryChanged (object sender, EventArgs e)
		{
			object o = Convert.ChangeType(((Entry)sender).Text, Column.DataType);
			Console.WriteLine("{0} <==\t'{1}'", Column.ColumnName, o);
			Row[Column] = o;
		}
		
		void HandleRowTableColumnChanged (object sender, DataColumnChangeEventArgs e)
		{
			if(e.Column == this.Column && e.Row == this.Row)
				UptadeEntryValue(e.ProposedValue);
		}
		
	}

}
