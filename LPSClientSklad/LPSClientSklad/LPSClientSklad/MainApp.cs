using System;
using System.Web;
using Gtk;
using LPSServer;

namespace LPSClientSklad
{
	class MainApp
	{
		public static void Main (string[] args)
		{
			new MainApp(args);
		}
		
		public string[] Args { get; set; }
		
		public MainApp(string[] args)
		{
			Args = args;

			FormFactory.Register(new FormXmlResourceInfo<LoginDialog>("login", "ui.glade", "dialogLogin"));
			FormFactory.Register(new FormXmlResourceInfo<MainForm>("main", "ui.glade", "windowMain"));

			Application.Init ();

			using(LoginDialog login = FormFactory.Create<LoginDialog>("login"))
			{
				ResponseType response = login.Run();
				if(response == ResponseType.Cancel)
					return;
				
			}

			MainApp.MainForm = FormFactory.Create<MainForm>("main");
			MainApp.MainForm.ShowAll();
			
			Run();
		}
		
		public void Run()
		{
			Application.Run();
		}
		
		public static MainForm MainForm { get; set; }
		
		public static Window MainWindow { get { 
			if(MainApp.MainForm == null)
				return null;
			else 
				return MainApp.MainForm.Window; } 
		}
		
		public static void ShowMessage(Window parent, MessageType msgType, string caption, string text, params object[] args)
		{
			using(Gtk.MessageDialog d = 
				new Gtk.MessageDialog(
					parent, 
					DialogFlags.DestroyWithParent | DialogFlags.Modal,
					msgType, ButtonsType.Ok, text, args))
			{
				d.Title = caption;
				d.Run();
				d.Destroy();
			}
		}

		public static void ShowMessage(MessageType msgType, string caption, string text, params object[] args)
		{
			ShowMessage(MainApp.MainWindow, msgType, caption, text, args);
		}
	}
}
