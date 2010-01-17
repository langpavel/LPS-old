using System;
using Gtk;

namespace LPSClientSklad
{

	public class LoginDialog : XmlDialogBase
	{
		[Glade.Widget] public Entry edtServer;
		[Glade.Widget] public Entry edtLogin;
		[Glade.Widget] public Entry edtPassword;
		
		public LoginDialog ()
		{
		}
		
		public void btnTestServer_clicked(object sender, EventArgs args)
		{
			try
			{
				using(LPSServer.Server srv = new LPSServer.Server(edtServer.Text))
					srv.Ping();
				
				MainApp.ShowMessage(this.Window, MessageType.Info, "Spojení OK", "Spojení se serverem bylo navázáno");
			}
			catch(Exception err)
			{
				MainApp.ShowMessage(this.Window, MessageType.Error, "Spojení selhalo", "Spojení selhalo. " + err.ToString());
			}
		}
		
		public LPSServer.Server TryConnect()
		{
			try
			{
				LPSServer.Server srv = new LPSServer.Server(edtServer.Text);
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
