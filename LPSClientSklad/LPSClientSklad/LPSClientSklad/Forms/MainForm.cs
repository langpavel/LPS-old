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
		
		public ServerConnection Connection { get { return ServerConnection.Instance; } }
		public string ServerUrl { get; set; }
		public string UserLogin { get; set; }

		public MainForm ()
		{
		}
		
		public override void OnCreate()
		{
			this.ServerUrl = MainApp.ServerUrl;
			this.UserLogin = MainApp.UserLogin;
			
			this.Window.Title = "LPS Sklad: " + this.UserLogin + " @ " + this.ServerUrl;
			
			this.Window.DeleteEvent += delegate(object o, DeleteEventArgs args) {
				Application.Quit();
			};
			
			InitModules();
		}

		private void InitModules()
		{
			Type t_str = typeof(string);
			TreeStore tree = new TreeStore(t_str, typeof(Gdk.Pixbuf), t_str, t_str);
			viewModules.Model = tree;
			
			//Gdk.Pixbuf i_folder = new Gdk.Pixbuf(null, "Images.database.png");
			
			//CellRendererPixbuf iconRenderer = new CellRendererPixbuf();
			//viewModules.AppendColumn(new TreeViewColumn("", iconRenderer, "stock", 1));
			viewModules.AppendColumn(new TreeViewColumn("Modul", new CellRendererText(), "text", 2));
			viewModules.HeadersVisible = false;
			//viewModules.RowActivated += ViewModulesRowActivated;
			
			TreeIter adresa = tree.AppendValues("__raw", null, "Adresář", "select * from adresa");
			tree.AppendValues(adresa, "__raw", null, "Odběratelé", "select * from adresa");
			tree.AppendValues(adresa, "__raw", null, "Dodavatelé", "select * from adresa");
			TreeIter sklad = tree.AppendValues("", null, "Skladové hospodářství");
			TreeIter cisel = tree.AppendValues("", null, "Číselníky");
			tree.AppendValues(cisel, "__raw", null, "Sklady", "select * from c_sklad");
			
			viewModules.ExpandAll();
		}

		public void ViewModulesRowActivated (object o, RowActivatedArgs args)
		{
			TreeView view = o as TreeView;
			TreeStore store = view.Model as TreeStore;
			TreeIter iter;
			if(store.GetIter(out iter, args.Path))
			{
				string id = store.GetValue(iter, 0) as string;
				if(id == "__raw")
					NewTabBySQL(store.GetValue(iter, 2) as string, store.GetValue(iter, 3) as string);
			}
		}
		
		public void AppQuit(object sender, EventArgs args)
		{
			Application.Quit();
		}
		
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
				TreeView view = sender as TreeView;
				TreeModelAdapter adapter = view.Model as TreeModelAdapter;
				DataTableTreeModel model = adapter.Implementor as DataTableTreeModel;
				DataRow row = model.GetRow(args.Path);
				Console.WriteLine("ID: " + row["id"] );
			}
			catch(Exception err)
			{
				Console.WriteLine(err.ToString());
			}
		}

		public void NewTabBySQL(string label, string sql)
		{
			TreeView tw = new TreeView();
			tw.RowActivated += RowActivated;
			HBox header = new HBox();
			header.Add(new Label(label));
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
			
			DataSet ds = Connection.GetDataSetSimple(sql);
			DataTableTreeModel.AssignNew(tw, ds.Tables[0]);
			
			btnCloseTab.Clicked += delegate {
				nbData.Remove(header);
				nbData.Remove(page);
				tw.Dispose();
				page.Dispose();
				header.Dispose();
				ds.Dispose();
			};

			nbData.Page = pgIdx;
		}
		
	}
}
