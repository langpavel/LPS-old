using System;
using System.Web;
using Gtk;
using LPSServer;
using Microsoft.Win32;
using LPSClient;

namespace LPSClient.Sklad
{
	class MainApp
	{
		public static void Main (string[] args)
		{
			new MainApp(args);
		}
		
		public string[] Args { get; set; }

		public static ServerConnection Connection { get; set; }
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

			FormFactory.Register(new FormXmlResourceInfo<LoginDialog>("login", "ui-shared.glade", "dialogLogin"));
			FormFactory.Register(new FormXmlResourceInfo<PasswdChDialog>("chpasswd", "ui-shared.glade", "dialogPswChange"));
			FormFactory.Register(new FormXmlResourceInfo<MainForm>("main", "ui-sklad.glade", "windowMain"));

			Application.Init ();

			using(LoginDialog login = FormFactory.Create<LoginDialog>("login"))
			{
				while(true)
				{
					login.edtPassword.Text = "";
					ResponseType response = login.Run();
					if(response == ResponseType.Cancel)
						return;
					try
					{
						new ServerConnection(login.edtServer.Text);
					}
					catch
					{
						//MainApp.ShowMessage(null, MessageType.Error, "Chyba", "Nezdařilo se připojení k serveru");
						login.laMessage.Markup = "<span color=\"#ff0000\">Nezdařilo se připojení k serveru</span>";
						login.laMessage.TooltipText = "";
						continue;
					}
					try 
					{
						if(ServerConnection.Instance.Login(login.edtLogin.Text, login.edtPassword.Text))
						{
							ServerUrl = login.edtServer.Text;
							UserLogin = login.edtLogin.Text;
							try {
								Registry.SetValue("HKEY_CURRENT_USER\\Software\\LPSoft", "LastServer", ServerUrl, RegistryValueKind.String);
								Registry.SetValue("HKEY_CURRENT_USER\\Software\\LPSoft", "LastUser", UserLogin, RegistryValueKind.String);
							} catch { }
			
							break;
						}
					}
					catch(Exception err)
					{
						login.laMessage.Markup = "<span color=\"#ff0000\">Neplatné jméno nebo heslo</span>";
						login.laMessage.TooltipText = err.ToString();
						//MainApp.ShowMessage(null, MessageType.Error, "Chyba", "Neplatné jméno nebo heslo");
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
