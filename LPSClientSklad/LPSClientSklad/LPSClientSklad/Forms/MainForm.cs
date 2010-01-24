using System;
using System.Data;
using Gtk;
using LPSClient;

namespace LPSClient.Sklad
{

	public class MainForm : XmlWindowBase
	{
		[Glade.Widget] TreeView viewModules;
		[Glade.Widget] Notebook nbData;
		
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
			
			this.Window.DeleteEvent += delegate(object o, DeleteEventArgs args) {
				Application.Quit();
			};
			
			InitModules();
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
		
		private void InitModules()
		{
			TreeStore store = new TreeStore(typeof(Gdk.Pixbuf), typeof(string), typeof(string), typeof(object));
			viewModules.Model = store;
			string data = Connection.GetTextResource("modules.xml");
			ModulesTreeInfo info = ModulesTreeInfo.LoadFromString(data);
			AddModulesNodes(info, store, TreeIter.Zero);
			
			//CellRendererPixbuf iconRenderer = new CellRendererPixbuf();
			//viewModules.AppendColumn(new TreeViewColumn("", iconRenderer, "stock", 1));
			viewModules.AppendColumn(new TreeViewColumn("Modul", new CellRendererText(), "text", 1));
			viewModules.HeadersVisible = false;
			//viewModules.RowActivated += ViewModulesRowActivated;
		
			viewModules.ExpandAll();
		}

		public void ViewModulesRowActivated (object o, RowActivatedArgs args)
		{
			Console.WriteLine("Module clicked");
			TreeView view = o as TreeView;
			TreeStore store = view.Model as TreeStore;
			TreeIter iter;
			if(store.GetIter(out iter, args.Path))
			{
				ModulesTreeInfo info = store.GetValue(iter, 3) as ModulesTreeInfo;
				Console.WriteLine(info != null);
				NewModuleTab(info);
			}
		}
		
		#region Action handlers
		public void AppQuit(object sender, EventArgs args)
		{
			Application.Quit();
		}
		
		public void OpenItem(object sender, EventArgs args)
		{
		}

		public void AddItem(object sender, EventArgs args)
		{
		}
		#endregion
		
		public void PasswordChange(object sender, EventArgs args)
		{
			PasswdChDialog dialog = FormFactory.Create<PasswdChDialog>("chpasswd");
			dialog.Execute();
			dialog.Destroy();
		}
		
		public void RowActivated(object sender, RowActivatedArgs args)
		{
			try
			{
				TreeView view = (TreeView)sender;
				DataTableListStoreBinding binding = DataTableListStoreBinding.Get(view);
				DataRow row = binding.GetRow(args.Path);
				string form_id = (string)view.Data["FORM_ID"];
				Console.WriteLine("LIST: {0} ROW ID: {1}", form_id, row[0]);
				FormInfo fi = FormFactory.Instance.GetFormInfo(form_id);
				AutobindWindow w = fi.CreateObject() as AutobindWindow;
				w.Load(Convert.ToInt64(row[0]));
				w.ShowAll();
			}
			catch(Exception err)
			{
				Console.WriteLine(err.ToString());
			}
		}

		public void NewModuleTab(ModulesTreeInfo info)
		{
			TreeView tw = new TreeView();
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
			
			DataSet ds = Connection.GetDataSetSimple(info.ListSql);
			
			tw.Data["MODULE_INFO"] = info;
			
			//oldest:
			//DataTableTreeModel.AssignNew(tw, ds.Tables[0], false);

			//newer:
			DataTableListStoreBinding binding = new DataTableListStoreBinding(tw, ds.Tables[0]);
			binding.Bind();
			
			//newest but bad as oldest ;-[
			//DataTableStore.AssignNew(tw, ds.Tables[0], false);
			
			btnCloseTab.Clicked += delegate {
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
}
