using System;
using System.Data;
using Gtk;

namespace LPSClientSklad
{

	public class MainForm : XmlWindowBase
	{
		[Glade.Widget] TreeView viewModules;
		[Glade.Widget] Notebook nbData;
		
		public LPSServer.Server Connection { get; set; }
		public string ServerUrl { get; set; }
		public string UserLogin { get; set; }

		public MainForm ()
		{
		}
		
		public override void OnCreate()
		{
			this.Connection = MainApp.Connection;
			this.ServerUrl = MainApp.ServerUrl;
			this.UserLogin = MainApp.UserLogin;
			
			this.Window.Title = "LPS Sklad: " + this.UserLogin + " @ " + this.ServerUrl;
			
			this.Window.DeleteEvent += delegate(object o, DeleteEventArgs args) {
				Application.Quit();
			};
			
			//viewModules.
			
			NewTabBySQL("Sklady", "select * from c_sklad;");
			NewTabBySQL("Sklady 2", "select * from c_sklad;");
			NewTabBySQL("Sklady", "select * from c_sklad;");
			NewTabBySQL("Sklady 2", "select * from c_sklad;");
			NewTabBySQL("Sklady", "select * from c_sklad;");
			NewTabBySQL("Sklady 2", "select * from c_sklad;");
			NewTabBySQL("Sklady", "select * from c_sklad;");
			NewTabBySQL("Sklady 2", "select * from c_sklad;");
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
			
		}
		
	}
}
