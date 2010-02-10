using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Gtk;
using LPS;

namespace LPS.Client
{
	public abstract class AutobindWindow : XmlWindowBase
	{
		private Statusbar statusbar;
		public Statusbar Statusbar { get { return statusbar; } }
		public RowDataSource DataSource { get; set; }
		public string WindowTitle { get; set; }
		
		public override void OnCreate ()
		{
			base.OnCreate ();
			CreateToolbarItems();
			this.statusbar = this.GetWidgetByName("statusbar") as Statusbar;
			DataSource = new RowDataSource();
			DataSource.RowChanged += HandleRowChanged;
			this.OwnedComponents.Add(DataSource);
		}

		protected virtual void Autobind(DataRow row, TableInfo info)
		{
			foreach(Widget w in new DeepEnumerator(this.Window.GetEnumerator()))
			{
				string name = w.Name;
				if(name.StartsWith("edt_"))
					name = name.Substring(4);
				else if(name.StartsWith("db_"))
					name = name.Substring(3);
				else
					continue;
				
				try
				{
					DataTable table = Row.Table;
					DataColumn col = table.Columns[name];
					
					BindWidget(col, w, this.TableInfo.GetColumnInfo(name));
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			if(Statusbar != null)
				Statusbar.Push(1, "");

			binded = true;
		}

		public void BindWidget(DataColumn col, Widget w, ColumnInfo colinfo)
		{
			if(w is Entry)
				BindEntry(col, (Entry) w, colinfo);
			else if(w is CheckButton)
				BindCheckButton(col, (CheckButton) w, colinfo);
			else if(w is ComboBox)
				BindComboBox(col, (ComboBox) w, colinfo);
			else if(w is TextView)
				BindTextView(col, (TextView) w, colinfo);
		}
		
		public void BindEntry(DataColumn col, Entry entry, ColumnInfo colinfo)
		{
			DataSource.GetGroupForColumn(col).Add(new EntryBinding(entry));
		}
		
		public void BindCheckButton(DataColumn col, CheckButton chkbutton, ColumnInfo colinfo)
		{
			DataSource.GetGroupForColumn(col).Add(new CheckButtonBinding(chkbutton, !colinfo.Required));
		}
		
		public void BindComboBox(DataColumn col, ComboBox combo, ColumnInfo colinfo)
		{
			DataSource.GetGroupForColumn(col).Add(new ComboBoxBinding(colinfo, combo));
		}

		public void BindTextView(DataColumn col, TextView textview, ColumnInfo colinfo)
		{
			DataSource.GetGroupForColumn(col).Add(new TextViewBinding(textview));
		}

		bool binded;
		private DataRow _Row;
		public DataRow Row
		{
			get { return _Row; }
			set 
			{
				_Row = value;
				if(_Row != null && !binded)
					Autobind(_Row, this.TableInfo);
				DataSource.Row = _Row;
			}
		}
		
		public abstract void Load(long id);
		public virtual void New()
		{
			DataRow r = this.Data.Tables[0].NewRow();
			OnNewRow(r);
			this.Data.Tables[0].Rows.Add(r);
			this.Row = r;
		}

		public virtual bool DeleteQuery()
		{
			if(this.ShowQueryMessage(MessageType.Question, "Smazat", "Opravdu chcete smazat tento záznam?"))
			{
				this.Delete();
				this.DoSaveClose();
				this.Dispose();
				return true;
			}
			return false;
		}
		
		public virtual void Delete()
		{
			//throw new NotImplementedException("Tento dialog nepodporuje mazání");
			this.Row.Delete();
			this.Row = null;
		}
		
		public DataSet Data { get; protected set; }
		
		protected virtual void Load(string table_name, long id)
		{
			WindowTitle = TableInfo.DetailCaption ?? "{kod} - {popis}";
			Data = Connection.GetDataSetByName(table_name, "id", id);
			if(id <= 0)
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
			btn.Clicked += SaveCloseAction;
			tools.Add(btn);
			btn.ShowAll();

			btn = new ToolButton("gtk-save");
			btn.Label = "Uložit";
			btn.Clicked += SaveAction;
			tools.Add(btn);
			btn.ShowAll();

			btn = new ToolButton("gtk-delete");
			btn.Label = "Odstranit";
			btn.Clicked += DeleteAction;
			tools.Add(btn);
			btn.ShowAll();
		}

		public virtual void DoNew()
		{
			this.New();
		}
		
		public virtual void DoSaveClose()
		{
			DoSave();
			DoClose();
		}
		
		public virtual void DoSave()
		{
			Connection.SaveDataSet(this.Data);
		}
		
		public virtual void DoSaveAs()
		{
			throw new NotImplementedException("Ukládání kopie není podporováno u tohoto formuláře");
		}

		public virtual void DoClose()
		{
			this.Destroy();
		}
		
		public virtual void DoDelete()
		{
			this.DeleteQuery();
		}
		
		#region Generic event handlers
		/// <summary>Action event handler</summary>
		public void NewAction(object o, EventArgs args)
		{
			DoNew();
		}

		/// <summary>Action event handler</summary>
		public void SaveAction(object o, EventArgs args)
		{
			DoSave();
		}

		/// <summary>Action event handler</summary>
		public void SaveCloseAction(object o, EventArgs args)
		{
			DoSaveClose();
		}

		/// <summary>Action event handler</summary>
		public void SaveAsAction(object o, EventArgs args)
		{
			DoSaveAs();
		}
	
		/// <summary>Action event handler</summary>
		public void CloseAction(object o, EventArgs args)
		{
			DoClose();
		}

		/// <summary>Action event handler</summary>
		public void DeleteAction(object o, EventArgs args)
		{
			DoDelete();
		}
		#endregion	

		private void HandleRowChanged (object sender, DataRowChangeEventArgs e)
		{
			if(e.Row == this.Row)
			{
				this.Statusbar.Pop(1);
				this.Statusbar.Push(1, GetStatusbarText());
				this.Window.Title = DataSource.FormatRowToText(WindowTitle);
			}
		}
		
		protected virtual string GetStatusbarText()
		{
			if(Row == null)
				return "Nepřipojeno k řádku";
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
