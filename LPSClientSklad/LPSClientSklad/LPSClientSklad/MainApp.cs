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

		public static LPSServer.Server Connection { get; set; }
		public static string ServerUrl { get; set; }
		public static string UserLogin { get; set; }
		
		public static MainForm MainForm { get; set; }
		
		public static Window MainWindow { get { 
			if(MainApp.MainForm == null)
				return null;
			else 
				return MainApp.MainForm.Window; } 
		}
		
			
		public MainApp(string[] args)
		{
			Args = args;

			FormFactory.Register(new FormXmlResourceInfo<LoginDialog>("login", "ui.glade", "dialogLogin"));
			FormFactory.Register(new FormXmlResourceInfo<MainForm>("main", "ui.glade", "windowMain"));

			Application.Init ();

			using(LoginDialog login = FormFactory.Create<LoginDialog>("login"))
			{
				while(true)
				{
					login.edtPassword.Text = "";
					ResponseType response = login.Run();
					if(response == ResponseType.Cancel)
						return;
					if(Connection == null || Connection.Url != login.edtServer.Text)
						Connection = login.TryConnect();
					if(Connection == null)
					{
						MainApp.ShowMessage(null, MessageType.Error, "Chyba", "Nezdařilo se připojení k serveru");
						continue;
					}
					try 
					{
						if(Connection != null && Connection.Login(login.edtLogin.Text, login.edtPassword.Text))
						{
							ServerUrl = login.edtServer.Text;
							UserLogin = login.edtLogin.Text;
							break;
						}
					}
					catch(Exception)
					{
						MainApp.ShowMessage(null, MessageType.Error, "Chyba", "Neplatné jméno nebo heslo");
					}
				}
			}

			MainForm frm = FormFactory.Create<MainForm>("main");
			MainApp.MainForm = frm;
			frm.ShowAll();
			
			Run();
		}
		
		public void Run()
		{
			Application.Run();
		}
		
		public static void ShowMessage(Window parent, MessageType msgType, string caption, string text, params object[] args)
		{
			string txt = String.Format(text, args);
			txt = txt.Replace("&","&amp;");
			txt = txt.Replace("<","&lt;");
			
			using(Gtk.MessageDialog d = 
				new Gtk.MessageDialog(
					parent, 
					DialogFlags.DestroyWithParent | DialogFlags.Modal,
					msgType, ButtonsType.Ok, txt))
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
