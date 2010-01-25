using System;
using System.Data;
using Gtk;

namespace LPSClient
{
	public class GenericDetailWindow : AutobindWindow
	{
		[Glade.Widget] Table content;
		
		public GenericDetailWindow ()
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();
		}
		
		public override void Load (long id)
		{
			this.Window.Title = "{kod} - {popis} - " + this.ListInfo.Text ?? "";
			Load("select * from "+this.ListInfo.Id+" where id=:id", id);
			content.NRows = (uint)this.Data.Tables[0].Columns.Count;
			uint top = 0;
			foreach(DataColumn col in this.Data.Tables[0].Columns)
			{
				ColumnInfo colinfo = ListInfo.GetColumnInfo(col.ColumnName);
				if(colinfo != null && colinfo.Editable)
				{
					string caption = colinfo.Caption;
					Label label = new Label(caption);
					label.UseUnderline = false;
					content.Attach(label,0,1,top,top+1,AttachOptions.Shrink, AttachOptions.Shrink,3,0);
					Entry entry = new Entry();
					WidgetRowBinding b = this.BindWidget(col, entry);
					this.OwnedComponents.Add(b);
					content.Attach(entry,1,2,top,top+1,AttachOptions.Shrink, AttachOptions.Expand | AttachOptions.Fill,3,0);
					top++;
				}
			}
			content.NRows = top;
			content.ShowAll();
		}

	}
}
