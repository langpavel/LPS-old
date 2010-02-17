using System;
using System.Data;
using Gtk;
using LPS;
using LPS.Client;
using System.Collections.Generic;

namespace LPS.Client.Sklad
{

	public class MainForm : XmlWindowBase
	{
		[Glade.Widget] TreeView viewModules;
		[Glade.Widget] ScrolledWindow scrollwindowColumns;
		[Glade.Widget] Notebook nbData;
		[Glade.Widget] Entry edtFilter;
		//[Glade.Widget] Widget tabSloupce;
		private NodeView viewColumns;
		
		public string ServerUrl { get; set; }
		public string UserLogin { get; set; }

		public MainForm ()
		{
		}
		
		public override void OnCreate()
		{
			this.ServerUrl = MainApp.ServerUrl;
			this.UserLogin = MainApp.UserLogin;
			
			this.Window.Title = "LPS Sklad: " + Connection.GetUserName() + " (" + this.UserLogin + ")@" + this.ServerUrl;
			
			this.Window.DeleteEvent += delegate {
				this.OnClose();
				Application.Quit();
			};
			
			nbData.SwitchPage += HandleNbDataSwitchPage;
			
			InitModules(false);
			
			viewColumns = new NodeView();
			viewColumns.Show();
			viewColumns.SearchColumn = 1;
			viewColumns.EnableSearch = true;
			ConfigurableColumn.CreateNodeViewColumns(viewColumns);
			scrollwindowColumns.Add(viewColumns);

			RestoreTabs();
		}

		private void RestoreTabs()
		{
			string[] opened_tabs = Connection.Configuration.GetConfiguration<string>("main", "opened_tabs", "")
				.Split(new string[] {"::"}, StringSplitOptions.RemoveEmptyEntries);
			if(opened_tabs != null && opened_tabs.Length != 0)
			{
				foreach(string tab_name in opened_tabs)
				{
					try
					{
						this.ShowModuleTab(Connection.Resources.GetModulesInfo(tab_name));
					}
					catch(Exception err)
					{
						Log.Error(err);
					}
				}
			}

		}

		protected virtual void OnClose()
		{
			try
			{
				Log.Debug("OnClose");
				SaveCurrentTabs();
			}
			catch(Exception err)
			{
				Log.Error(err);
			}
		}

		private void SaveCurrentTabs()
		{
			List<string> tabs = new List<string>();
			for(int i=0; i<nbData.NPages; i++)
				tabs.Add(((ListPage)nbData.GetNthPage(i)).Module.Id);
			Connection.Configuration.SaveConfiguration("main", "opened_tabs", String.Join("::", tabs.ToArray()));
		}

		public ListPage GetCurrentPage()
		{
			return nbData.GetNthPage(nbData.Page) as ListPage;
		}
		
		private void HandleNbDataSwitchPage (object o, SwitchPageArgs args)
		{
			ListPage current = GetCurrentPage();
			if(current != null)
			{
				this.edtFilter.Text = current.TableView.Filter;
				this.viewColumns.NodeStore = current.TableView.Binding.Mapping.ColumnsStore;
			}
			else
			{
				this.edtFilter.Text = "";
				this.viewColumns.NodeStore = null;
			}
		}

		private void AddModulesNodes(ModulesTreeInfo info, TreeStore store, TreeIter parent)
		{
			foreach(ModulesTreeInfo node in info.Items)
			{
				string desc = node.Description;
				desc = (desc == null)? "" : desc.Trim();
				TreeIter current;
				if(parent.Equals(TreeIter.Zero))
					current = store.AppendValues(null, node.Text, desc, node);
				else
					current = store.AppendValues(parent, null, node.Text, desc, node);
				AddModulesNodes(node, store, current);
			}
		}
		
		private void InitModules(bool refreshing)
		{
			TreeStore store;
			if(!refreshing)
			{
				store = new TreeStore(typeof(Gdk.Pixbuf), typeof(string), typeof(string), typeof(object));
				viewModules.Model = store;
				//CellRendererPixbuf iconRenderer = new CellRendererPixbuf();
				//viewModules.AppendColumn(new TreeViewColumn("", iconRenderer, "stock", 1));
				viewModules.AppendColumn(new TreeViewColumn("Modul", new CellRendererText2(), "text", 1));
				viewModules.HeadersVisible = false;
				//viewModules.RowActivated += ViewModulesRowActivated;
			}
			else
			{
				store = viewModules.Model as TreeStore;
				store.Clear();
			}
			AddModulesNodes(Connection.Resources.GetModulesInfo("root"), store, TreeIter.Zero);
		
			viewModules.ExpandAll();
		}

		public void ViewModulesRowActivated (object o, RowActivatedArgs args)
		{
			TreeView view = o as TreeView;
			TreeStore store = view.Model as TreeStore;
			TreeIter iter;
			if(store.GetIter(out iter, args.Path))
			{
				ModulesTreeInfo info = store.GetValue(iter, 3) as ModulesTreeInfo;
				if(info != null)
					ShowModuleTab(info);
			}
		}
		
		#region Action handlers
		public void AppQuit(object sender, EventArgs args)
		{
			this.OnClose();
			Connection.Dispose();
			Application.Quit();
		}

		public void AddItem(object sender, EventArgs args)
		{
			ListPage page = GetCurrentPage();
			if(page == null)
				return;
			page.TableView.OpenNewDetail();
		}

		public void OpenItem(object sender, EventArgs args)
		{
			ListPage page = GetCurrentPage();
			if(page == null)
				return;
			page.TableView.OpenDetail();
		}

		public void DeleteItem(object sender, EventArgs args)
		{
			ListPage page = GetCurrentPage();
			if(page == null)
				return;
			page.TableView.OpenDetailAndDelete();
		}

		public void OnFilterChanged(object sender, EventArgs args)
		{
			ListPage current = GetCurrentPage();
			if(current == null)
				return;
			current.TableView.Filter = edtFilter.Text;
		}
		
		public void PasswordChange(object sender, EventArgs args)
		{
			PasswdChDialog dialog = FormFactory.Create<PasswdChDialog>("chpasswd");
			dialog.Execute();
			dialog.Destroy();
		}
		#endregion
		
		public void ShowModuleTab(ModulesTreeInfo info)
		{
			ListPage page = null;
			for(int i = 0; i < nbData.NPages; i++)
			{
				page = nbData.GetNthPage(i) as ListPage;
				if(page == null)
					continue;
				if(page.Module == info)
					break;
				page = null;
			}
			if(page == null)
				page = new ListPage(nbData, info);
			
			page.SetCurrent();
		}
	
		public void RefreshModules(object o, EventArgs args)
		{
			InitModules(true);
		}
	}
}
