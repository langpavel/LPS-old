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
			CreateToolbarItems();
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
					if(binding != null)
						this.OwnedComponents.Add(binding);
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
			if(w is CheckButton)
				return BindCheckButton(col, (CheckButton) w);
			return null;
		}
		
		public WidgetRowBinding BindEntry(DataColumn col, Entry entry)
		{
			return new EntryRowBinding(entry, col, Row);
		}
		
		public WidgetRowBinding BindCheckButton(DataColumn col, CheckButton chkbutton)
		{
			return new CheckButtonRowBinding(chkbutton, col, Row);
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
			Data = Connection.GetDataSet(sql, "id", id);
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
		
		public Widget GetWidgetByName(string name)
		{
			DeepEnumerator enumerator = new DeepEnumerator(this.Window.GetEnumerator());
			while(enumerator.MoveNext())
			{
				Widget w = enumerator.Current as Widget;
				if(w == null)
					continue;
				if(w.Name == name)
					return w;
			}
			return null;
		}
		
		protected void CreateToolbarItems()
		{
			Toolbar tools = GetWidgetByName("toolbar") as Toolbar;
			if(tools == null)
				return;

			ToolButton btn = new ToolButton("gtk-save");
			btn.Label = "Uložit a zavřít";
			btn.Clicked += SaveClose;
			tools.Add(btn);
			btn.ShowAll();

			btn = new ToolButton("gtk-save");
			btn.Label = "Uložit";
			btn.Clicked += Save;
			tools.Add(btn);
			btn.ShowAll();
		}
		
		#region Generic event handlers
		public virtual void New(object o, EventArgs args)
		{
			this.ShowMessage(MessageType.Error, "Chyba", "Není podporováno");
		}

		public virtual void Save(object o, EventArgs args)
		{
			Connection.SaveDataSet(this.Data);
		}

		public virtual void SaveClose(object o, EventArgs args)
		{
			Connection.SaveDataSet(this.Data);
			this.Destroy();
		}

		public virtual void SaveAs(object o, EventArgs args)
		{
			this.ShowMessage(MessageType.Error, "Chyba", "Není podporováno");
		}
	
		public virtual void Close(object o, EventArgs args)
		{
			this.Destroy();
		}
		#endregion	
	}
}
