using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Gtk;
using LPS;

namespace LPSClient
{
	public abstract class AutobindWindow : XmlWindowBase
	{
		public override void OnCreate ()
		{
			base.OnCreate ();
		}

		protected virtual void Autobind(DataRow row)
		{
			IEnumerator e = new DeepEnumerator(this.Window.GetEnumerator());
			while(e.MoveNext())
			{
				Widget w = e.Current as Widget;
				if(w == null)
					continue;
				string name = w.Name;
				if(name.StartsWith("edt_"))
					name = name.Substring(4);
				else if(name.StartsWith("db_"))
					name = name.Substring(3);
				else
					continue;
				Console.WriteLine("Try bind '{0}'", name);
				
				try
				{
					DataTable table = Row.Table;
					DataColumn col = table.Columns[name];
				
					WidgetRowBinding binding = BindWidget(col, w);
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
		}
		
		public WidgetRowBinding BindWidget(DataColumn col, Widget w)
		{
			if(w is Entry)
				return BindEntry(col, (Entry) w);
			return null;
		}
		
		public WidgetRowBinding BindEntry(DataColumn col, Entry entry)
		{
			return new EntryRowBinding(entry, col, Row);
		}

		protected virtual void Unbind(DataRow row)
		{
		}

		private DataRow _Row;
		public DataRow Row
		{
			get { return _Row; }
			set 
			{
				if(_Row != null)
					Unbind(_Row);
				_Row = value;
				if(_Row != null)
					Autobind(_Row);
			}
		}
		
		public abstract void Load(long id);
		
		public DataSet Data { get; protected set; }
		
		protected virtual void Load(string sql, long id)
		{
			Data = Connection.GetDataSet(sql, true, "id", id);
			this.Row = Data.Tables[0].Rows[0];
		}
		
		public override void Dispose ()
		{
			if(Data != null)
			{
				Connection.DisposeDataSet(Data);
				Data.Dispose();
				Data = null;
			}
			base.Dispose();
		}
		
		#region Generic event handlers
		public virtual void New(object o, EventArgs args)
		{
		}

		public virtual void Save(object o, EventArgs args)
		{
		}

		public virtual void SaveAs(object o, EventArgs args)
		{
		}
	
		public virtual void Close(object o, EventArgs args)
		{
			this.Destroy();
		}
		#endregion	
	}
}
