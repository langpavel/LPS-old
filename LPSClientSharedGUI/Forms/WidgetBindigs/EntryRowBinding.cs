using System;
using System.Data;
using Gtk;

namespace LPS.Client
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

		private bool is_updating;
		void HandleEntryChanged (object sender, EventArgs e)
		{
			Entry entry = (Entry)sender;
			try
			{
				is_updating = true;
				object o = Convert.ChangeType(entry.Text, Column.DataType);
				//Console.WriteLine("{0} <==\t'{1}'", Column.ColumnName, o);
				Row[Column] = o;
				if(Row.RowState == DataRowState.Added || !o.Equals(Convert.ChangeType(Row[Column, DataRowVersion.Original], Column.DataType)))
					entry.ModifyBase(StateType.Normal, new Gdk.Color(255,255,196));
				else
					entry.ModifyBase(StateType.Normal, new Gdk.Color(255,255,255));
			}
			catch
			{
				entry.ModifyBase(StateType.Normal, new Gdk.Color(255,196,196));
			}
			finally
			{
				is_updating = false;
			}
		}
		
		void HandleRowTableColumnChanged (object sender, DataColumnChangeEventArgs e)
		{
			if(e.Column == this.Column && e.Row == this.Row && !is_updating)
				UptadeEntryValue(e.ProposedValue);
		}
		
	}

}
