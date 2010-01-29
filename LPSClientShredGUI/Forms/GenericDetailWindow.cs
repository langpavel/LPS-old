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

		private Widget CreateWidget(DataColumn db_col, ColumnInfo colinfo, uint top)
		{
			string caption = colinfo.Caption;
			Widget result;
			if(db_col.DataType == typeof(bool))
			{
				result = new CheckButton(caption);
			}
			else if(db_col.DataType == typeof(DateTime))
			{
				Label label = new Label(caption);
				label.UseUnderline = false;
				content.Attach(label,0,1,top,top+1,AttachOptions.Shrink, AttachOptions.Shrink,3,0);
				
				Entry dateEntry = new Entry();
				dateEntry.ButtonPressEvent += delegate {
					Console.WriteLine("Blablablablalba");
//					Gtk.Calendar calend = new Calendar();
//					calend.ShowAll();
					
				};
				result = dateEntry;
				
			}
			else
			{
				Label label = new Label(caption);
				label.UseUnderline = false;
				content.Attach(label,0,1,top,top+1,AttachOptions.Shrink, AttachOptions.Shrink,3,0);

				result = new Entry();
			}
			content.Attach(result,1,2,top,top+1,AttachOptions.Expand | AttachOptions.Fill, AttachOptions.Expand | AttachOptions.Fill,3,0);
			WidgetRowBinding b = this.BindWidget(db_col, result, colinfo);
			this.OwnedComponents.Add(b);
			return result;
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
					CreateWidget(col, colinfo, top);
					top++;
				}
			}
			content.NRows = top;
			content.ShowAll();
		}

		protected override void OnNewRow (DataRow row)
		{
			row["id"] = Connection.NextSeqValue(this.ListInfo.Id + "_id_seq");
		}

	}
}
