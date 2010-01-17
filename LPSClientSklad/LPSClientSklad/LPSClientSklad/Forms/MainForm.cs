using System;
using Gtk;

namespace LPSClientSklad
{

	public class MainForm : XmlWindowBase
	{
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
		}

		public void AppQuit(object sender, EventArgs args)
		{
			Application.Quit();
		}
		
	}
}
