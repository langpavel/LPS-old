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
			MainApp.ShowMessage(this.Window, MessageType.Info, "Spojení se serverem", "TEST");
		}
	}
}
