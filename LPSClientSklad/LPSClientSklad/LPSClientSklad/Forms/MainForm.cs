using System;
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
			
			InitTreeDataSample();
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
		
		
		private ListStore DataStore { get; set; }
		public void InitTreeDataSample()
		{
			DataStore = new ListStore(typeof(string), typeof(string), typeof(string));
			for(int i = 1; i < 10000; i++)
			{
				DataStore.AppendValues("abc1 " + i.ToString(), "def2", "ghi1");
				DataStore.AppendValues("abc2 " + i.ToString(), "def3", "ghi2");
				DataStore.AppendValues("abc3 " + i.ToString(), "def4", "ghi3");
				DataStore.AppendValues("abc4 " + i.ToString(), "def5", "ghi4");
			}
			treeData.Model = DataStore;

			treeData.AppendColumn ("Demo", new CellRendererText (), "text", 0);

			TreeViewColumn col = new TreeViewColumn("Data list", new CellRendererText(), "text", 1);
			col.Resizable = true;
			col.FixedWidth = 25;
			col.Clickable = true;
			col.Alignment = 1.0f;
			treeData.AppendColumn(col);

			treeData.AppendColumn ("Data", new CellRendererText (), "text", 1);
			
			treeData.HeadersVisible = true;
			treeData.EnableGridLines = TreeViewGridLines.Both;
			treeData.EnableSearch = true;
			treeData.EnableTreeLines = false;
			treeData.HeadersClickable = true;
			treeData.HoverSelection = true;
			treeData.Reorderable = true;
			treeData.RubberBanding = true;
			treeData.RulesHint = true;
			treeData.SearchColumn = 0;
			treeData.ShowExpanders = false;
		}

		public void RowActivated(object sender, RowActivatedArgs args)
		{
			TreeIter iter;
			if(DataStore.GetIter(out iter,args.Path))
			{
				Console.WriteLine(DataStore.GetValue(iter, 0));
			}
		}
		
	}
}
