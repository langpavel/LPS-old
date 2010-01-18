using System;
using System.Data;
using Gtk;

namespace LPSClientSklad
{

	public class MainForm : XmlWindowBase
	{
		[Glade.Widget] TreeView treeData;
		
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
			
			LoadSQL("select * from c_sklad;");
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

		public void LoadSQL(string sql)
		{
			DataSet ds = Connection.GetDataSetSimple(sql);
			DataTableTreeModel model = new DataTableTreeModel();
			model.DataTable = ds.Tables[0];
			treeData.Model = new TreeModelAdapter(model);
			treeData.AppendColumn ("Id", new CellRendererText (), "text", 0);
			treeData.AppendColumn ("2", new CellRendererText (), "text", 1);
			treeData.AppendColumn ("3", new CellRendererText (), "text", 2);
		}
		
	}
}
