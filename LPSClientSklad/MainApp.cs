using System;
using System.Reflection;
using System.Web;
using Gtk;
using LPS;
using Microsoft.Win32;
using LPS.Client;
using System.Text;

namespace LPS.Client.Sklad
{
	internal class MainApp
	{
		public static void Main (string[] args)
		{
			new MainApp(args);
		}
		
		public string ExeName { get; set; }
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
			ExeName = Assembly.GetEntryAssembly().Location;
			
			Application.Init(ExeName, ref args);
			
			GtkTheme.LoadGtkResourceFile();

			FormFactory.Register(new FormXmlResourceInfo<LoginDialog>("login", "ui-shared.glade", "dialogLogin"));
			FormFactory.Register(new FormXmlResourceInfo<PasswdChDialog>("chpasswd", "ui-shared.glade", "dialogPswChange"));
			FormFactory.Register(new FormXmlResourceInfo<GenericDetailWindow>("generic", "ui-shared.glade", "windowGeneric"));
			FormFactory.Register(new FormXmlResourceInfo<LongMessageDialog>("long-message", "ui-shared.glade", "dialogLongMessage"));
			//FormFactory.Register(new FormXmlResourceInfo<HTMLMessageDialog>("html-message", "ui-shared.glade", "dialogHTMLMessage"));
			FormFactory.Register(new FormXmlResourceInfo<MainForm>("main", "ui-sklad.glade", "windowMain"));
			FormFactory.Register(new FormXmlResourceInfo<AdresaForm>("adresa", "adresa.glade", "windowAdresa"));

			GLib.ExceptionManager.UnhandledException += HandleUnhandledException;

			if(!DoLogin())
				return;

			MainForm frm = FormFactory.Create<MainForm>("main");
			MainApp.MainForm = frm;
			frm.ShowAll();
			
			Run();
		}

		void HandleUnhandledException (GLib.UnhandledExceptionArgs args)
		{
			if(args.IsTerminating)
				return;
			try
			{
				/*
				Exception err = args.ExceptionObject as Exception;
				int idx=-1;
				if(err != null)
					idx = err.Message.IndexOf("<HTML>");
				if(idx >= 0)
					ShowHtmlMessage(err.Message.Substring(idx));
				else if(idx < 0 && err.InnerException != null)
				{
					idx = err.InnerException.Message.IndexOf("<HTML>");
					if(idx >= 0)
						ShowHtmlMessage(err.InnerException.Message.Substring(idx));			
				}
				else
				*/
				StringBuilder sb = new StringBuilder();
				ServerException sex = args.ExceptionObject as ServerException;
				if(sex != null)
				{
					LPSClientShared.LPSServer.ExceptionInfo exi = sex.ExceptionInfo;
					while(exi != null)
					{
						sb.AppendLine(exi.Name + " " + exi.ErrCodeName ?? "");
						sb.AppendLine(exi.Message);
						sb.AppendLine(exi.StackTrace);
						sb.AppendLine("-------------------");
						exi = exi.InnerException;
					}
				}
				else
				{
					Exception ex = args.ExceptionObject as Exception;
					while(ex != null)
					{
						sb.AppendLine(ex.ToString());
						ex = ex.InnerException;
					}
				}
				ShowLongMessage("Chyba", "Nastala neošetřená vyjímka " + args.ExceptionObject.GetType().Name, sb.ToString());
				args.ExitApplication = false;
			}
			catch(Exception err)
			{
				ShowLongMessage("Chyba", err.Message, err.ToString());
			}
		}
		
		public void Run()
		{
			Application.Run();
		}
		
		public bool DoLogin()
		{
			using(LoginDialog login = FormFactory.Create<LoginDialog>("login"))
			{
				while(true)
				{
					login.edtPassword.Text = "";
					ResponseType response = login.Run();
					if(response != ResponseType.Ok)
						return false;
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
						if(ServerConnection.Instance.Login(login.edtLogin.Text, login.edtPassword.Text) != 0)
						{
							ServerUrl = login.edtServer.Text;
							UserLogin = login.edtLogin.Text;
							try {
								Registry.SetValue("HKEY_CURRENT_USER\\Software\\LPSoft", "LastServer", ServerUrl, RegistryValueKind.String);
								Registry.SetValue("HKEY_CURRENT_USER\\Software\\LPSoft", "LastUser", UserLogin, RegistryValueKind.String);
							} catch { }
			
							return true;
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

		public static void ShowLongMessage(string title, string title2, string text)
		{
			LongMessageDialog.Show(title, title2, text);
		}
		
		//public static void ShowHtmlMessage(string html)
		//{
		//	HTMLMessageDialog.Show(html);
		//}

	}
}
