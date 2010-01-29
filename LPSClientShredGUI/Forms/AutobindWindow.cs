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
		private Statusbar statusbar;
		public Statusbar Statusbar { get { return statusbar; } }
		
		public override void OnCreate ()
		{
			base.OnCreate ();
			CreateToolbarItems();
			this.statusbar = this.GetWidgetByName("statusbar") as Statusbar;
		}

		private TextRowBinding titlebinding;
		protected virtual void Autobind(DataRow row, ModulesTreeInfo info)
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
				//Console.WriteLine("Try bind '{0}'", name);
				
				try
				{
					DataTable table = Row.Table;
					DataColumn col = table.Columns[name];
				
					WidgetRowBinding binding = BindWidget(col, w, ListInfo.GetColumnInfo(name));
					if(binding != null)
						this.OwnedComponents.Add(binding);
					
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			if(Statusbar != null)
				BindStatusbar();
				
			TextRowBinding titlebinding = new TextRowBinding(row, this.Window.Title);
			titlebinding.TextUpdating += HandleTitleTextUpdating;
			titlebinding.UpdateText();
		}

		void HandleTitleTextUpdating (object sender, TextUpdatingArgs args)
		{
			this.Window.Title = args.Text;
		}
		
		public WidgetRowBinding BindWidget(DataColumn col, Widget w, ColumnInfo colinfo)
		{
			if(w is Entry)
				return BindEntry(col, (Entry) w, colinfo);
			if(w is CheckButton)
				return BindCheckButton(col, (CheckButton) w, colinfo);
			if(w is ComboBox)
				return BindComboBox(col, (ComboBox) w, colinfo);
			return null;
		}
		
		public WidgetRowBinding BindEntry(DataColumn col, Entry entry, ColumnInfo colinfo)
		{
			return new EntryRowBinding(entry, col, Row);
		}
		
		public WidgetRowBinding BindCheckButton(DataColumn col, CheckButton chkbutton, ColumnInfo colinfo)
		{
			return new CheckButtonRowBinding(chkbutton, col, Row);
		}
		
		public WidgetRowBinding BindComboBox(DataColumn col, ComboBox combo, ColumnInfo colinfo)
		{
			return new ComboBoxRowBinding(combo, col, Row, colinfo);
		}

		protected virtual void Unbind(DataRow row)
		{
			if(Statusbar != null)
				UnbindStatusbar();
			if(titlebinding != null)
			{
				titlebinding.TextUpdating -= HandleTitleTextUpdating;
				titlebinding.Dispose();
				titlebinding = null;
			}
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
					Autobind(_Row, this.ListInfo);
			}
		}
		
		public abstract void Load(long id);
		public virtual void New()
		{
			Load(0);
		}
		
		public DataSet Data { get; protected set; }
		
		protected virtual void Load(string sql, long id)
		{
			Data = Connection.GetDataSet(sql, "id", id);
			if(id == 0)
			{
				this.Row = Data.Tables[0].NewRow();
				OnNewRow(this.Row);
				Data.Tables[0].Rows.Add(this.Row);
			}
			else
			{
				this.Row = Data.Tables[0].Rows[0];
			}
		}
		
		protected virtual void OnNewRow(DataRow row)
		{
		}
		
		public override void Dispose ()
		{
			if(Data != null)
			{
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

		protected virtual void BindStatusbar()
		{
			Row.Table.RowChanged += UpdateStatusbar;
			this.Statusbar.Push(1, GetStatusbarText());
		}

		protected virtual void UnbindStatusbar()
		{
			Row.Table.RowChanged -= UpdateStatusbar;
			this.Statusbar.Pop(1);
		}
		
		private void UpdateStatusbar (object sender, DataRowChangeEventArgs e)
		{
			if(e.Row == this.Row)
			{
				this.Statusbar.Pop(1);
				this.Statusbar.Push(1, GetStatusbarText());
			}
		}
		
		protected virtual string GetStatusbarText()
		{
			string stav;
			switch(Row.RowState)
			{
			case DataRowState.Added:
				stav = "Nový";
				break;
			case DataRowState.Deleted:
				stav = "Smazáno";
				break;
			case DataRowState.Modified: 
				stav = "Změněno";
				break;
			case DataRowState.Unchanged:
				stav = "Nezměněno";
				break;
			default:
				stav = Row.RowState.ToString();
				break;
			}
			return String.Format("Id: {0}; Stav: {1}", Row["id"], stav);
		}

	}
}
