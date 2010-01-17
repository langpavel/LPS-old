using System;
using Gtk;

namespace LPSClientSklad
{

	public class LoginDialog : XmlDialogBase
	{
		[Glade.Widget] public Entry edtServer;
		[Glade.Widget] public Entry edtLogin;
		[Glade.Widget] public Entry edtPassword;
		[Glade.Widget] public Label laMessage;
		
		public LoginDialog ()
		{
		}
		
		public override void OnCreate ()
		{
			base.OnCreate ();
			laMessage.Markup = "";
		}

		
		public void btnTestServer_clicked(object sender, EventArgs args)
		{
			try
			{
				using(LPSServer.Server srv = new LPSServer.Server(edtServer.Text))
					srv.Ping();
				laMessage.Markup = "<span color=\"#00cc00\">Spojení se serverem bylo navázáno</span>";
				laMessage.TooltipText = "";
				//MainApp.ShowMessage(this.Window, MessageType.Info, "Spojení OK", "Spojení se serverem bylo navázáno");
			}
			catch(Exception err)
			{
				laMessage.Markup = "<span color=\"#ff0000\">Spojení se serverem selhalo</span>";
				laMessage.TooltipText = err.ToString();
				//MainApp.ShowMessage(this.Window, MessageType.Error, "Spojení selhalo", "Spojení selhalo." + err.ToString());
			}
		}
		
		public LPSServer.Server TryConnect()
		{
			try
			{
				LPSServer.Server srv = new LPSServer.Server(edtServer.Text);
				srv.CookieContainer = new System.Net.CookieContainer();
				srv.Ping();
				return srv;
			}
			catch(Exception)
			{
				return null;
			}
		}
	}
}
