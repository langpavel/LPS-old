using System;
using System.Data;
using Gtk;
using LPSClient;

namespace LPSClient.Sklad
{

	public class MainForm : XmlWindowBase
	{
		[Glade.Widget] TreeView viewModules;
		[Glade.Widget] TreeView viewColumns;
		[Glade.Widget] Notebook nbData;
		[Glade.Widget] ToggleButton chkFiltrovat;
		[Glade.Widget] Entry edtFilter;
		
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
				Application.Quit();
			};
			
			InitModules(false);
			
			DataTableListStoreBinding.CreateColumnTreeViewColumns(viewColumns);
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
			string data = Connection.GetTextResource("modules.xml");
			ModulesTreeInfo info = ModulesTreeInfo.LoadFromString(data);
			AddModulesNodes(info, store, TreeIter.Zero);
		
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
			Application.Quit();
		}

		public TreeView GetCurrentView()
		{
			try
			{
				ScrolledWindow page = nbData.GetNthPage(nbData.Page) as ScrolledWindow;
				return page.Child as TreeView;
			}
			catch
			{
				return null;
			}
		}
		
		public void OpenItem(object sender, EventArgs args)
		{
			TreeView tw = GetCurrentView();
			if(tw == null)
				return;
			foreach(TreePath path in tw.Selection.GetSelectedRows())
			{
				OpenDetailForm(tw, path);
			}
		}

		public void AddItem(object sender, EventArgs args)
		{
		}
		
		public void OnFilterToggled(object sender, EventArgs args)
		{
			if(chkFiltrovat.Active)
				ApplyFilter(edtFilter.Text);
			else
				ApplyFilter(null);
		}
		
		public void OnFilterChanged(object sender, EventArgs args)
		{
			if(chkFiltrovat.Active)
				ApplyFilter(edtFilter.Text);
		}
		
		#endregion
		
		public void ApplyFilter(string filter)
		{
			TreeView tw = GetCurrentView();
			if(tw == null)
				return;
			ModulesTreeInfo info = tw.Data["INFO"] as ModulesTreeInfo;
			DataTableListStoreBinding b = info.Data["BINDING"] as DataTableListStoreBinding;
			b.ApplyFilter(filter);
		}
		
		public void PasswordChange(object sender, EventArgs args)
		{
			PasswdChDialog dialog = FormFactory.Create<PasswdChDialog>("chpasswd");
			dialog.Execute();
			dialog.Destroy();
		}
		
		public void OpenDetailForm(TreeView view, TreePath path)
		{
			DataTableListStoreBinding binding = DataTableListStoreBinding.Get(view);
			DataRow row = binding.GetRow(path);
			ModulesTreeInfo info = view.Data["INFO"] as ModulesTreeInfo;
			FormInfo fi = FormFactory.Instance.GetFormInfo(info.DetailName);
			AutobindWindow w = fi.CreateObject() as AutobindWindow;
			w.ListInfo = info;
			w.Load(Convert.ToInt64(row[0]));
			w.ShowAll();
		}
		
		public void RowActivated(object sender, RowActivatedArgs args)
		{
			try
			{
				TreeView view = (TreeView)sender;
				OpenDetailForm(view, args.Path);
			}
			catch(Exception err)
			{
				Console.WriteLine(err.ToString());
			}
		}

		public void ShowModuleTab(ModulesTreeInfo info)
		{
			if(String.IsNullOrEmpty(info.ListSql))
				return;
			TreeView tw = info.Data["VIEW"] as TreeView;
			if(tw != null)
			{
				nbData.Page = nbData.PageNum(tw.Parent);
				RefreshData(info);
			}
			else
			{
				tw = new TreeView();
				tw.RowActivated += RowActivated;
				HBox header = new HBox();
				header.Add(new Label(info.Text));
				Image img = new Image("gtk-close", IconSize.Menu);
				Button btnCloseTab = new Button(img);
				btnCloseTab.BorderWidth = 0;
				btnCloseTab.Relief = ReliefStyle.None;
				btnCloseTab.WidthRequest = 19;
				btnCloseTab.HeightRequest = 19;
				header.Add(btnCloseTab);
				header.ShowAll();
				
				ScrolledWindow page = new ScrolledWindow();
				page.Add(tw);
				page.ShowAll();
					
				int pgIdx = nbData.AppendPage(page, header);
				nbData.SetTabReorderable(page, true);
				//nbData.SetTabDetachable(page, true);
				
				DataSet ds = Connection.GetDataSetSimple(info.ListSql);
				
				DataTableListStoreBinding binding = new DataTableListStoreBinding(tw, ds.Tables[0], info);
				binding.Bind();
				binding.Sorting = "id";
				viewColumns.Model = binding.ColumnList;

				info.Data["VIEW"] = tw;
				info.Data["DATASET"] = ds;
				info.Data["BINDING"] = binding;
				tw.Data["INFO"] = info;
				
				btnCloseTab.Clicked += delegate {
					info.Data.Remove("VIEW");
					info.Data.Remove("DATASET");
					info.Data.Remove("BINDING");
					nbData.Remove(header);
					nbData.Remove(page);
					tw.Dispose();
					page.Dispose();
					header.Dispose();
					ds.Dispose();
					//binding.Dispose();
					//store.Dispose();
				};
	
				nbData.Page = pgIdx;
			}
		}			
		
		public void RefreshData(ModulesTreeInfo info)
		{
			TreeView tw = info.Data["VIEW"] as TreeView;
			//DataTableListStoreBinding binding = info.Data["BINDING"] as DataTableListStoreBinding;
			DataSet ds = info.Data["DATASET"] as DataSet;
			DataTableListStoreBinding binding = info.Data["BINDING"] as DataTableListStoreBinding;
			if(ds == null)
				return;
			DataSet newDs = Connection.GetSameDataSet(ds);
			TreePath[] selectedPaths = tw.Selection.GetSelectedRows();
			double hpos = ((tw.Parent as ScrolledWindow).HScrollbar as Scrollbar).Value;
			double vpos = ((tw.Parent as ScrolledWindow).VScrollbar as Scrollbar).Value;
			binding.Unbind();
			ds.Dispose();
			info.Data["DATASET"] = newDs;
			binding.DataTable = newDs.Tables[0];
			binding.Bind();
			if(selectedPaths.Length > 0)
			{
				try {
					tw.Selection.SelectPath(selectedPaths[0]);
					((tw.Parent as ScrolledWindow).HScrollbar as Scrollbar).Value = hpos;
					((tw.Parent as ScrolledWindow).VScrollbar as Scrollbar).Value = vpos;
				} catch { }
			}
		}
		
		public void RefreshModules(object o, EventArgs args)
		{
			InitModules(true);
		}
	}
}
