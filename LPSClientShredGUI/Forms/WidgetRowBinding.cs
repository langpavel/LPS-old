using System;
using System.Data;
using Gtk;

namespace LPSClient
{
	public abstract class WidgetRowBinding: IDisposable
	{
		public Widget Widget { get; set; }
		public DataRow Row { get; set; }
		public DataColumn Column { get; set; }

		public WidgetRowBinding()
		{
		}
		
		public abstract void Bind();
		public abstract void Unbind();
		
		public virtual void Dispose()
		{
			Unbind();
		}
	}
	
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
			Bind();
		}
	
		public override void Bind ()
		{
			this.Entry.Activated += delegate(object sender, EventArgs e) {
				Console.WriteLine("Activated {0}", ((Entry)sender).Text);
			};
			this.Entry.Changed += delegate(object sender, EventArgs e) {
				Console.WriteLine("Changed done {0}", ((Entry)sender).Text);
			};
			this.Entry.Text = Convert.ToString(Row[Column]);
		}
		
		public override void Unbind ()
		{
			
		}


	}
}
