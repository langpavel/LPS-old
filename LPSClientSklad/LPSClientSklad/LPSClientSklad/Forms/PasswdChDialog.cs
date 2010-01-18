using System;
using Gtk;

namespace LPSClientSklad
{
	public class PasswdChDialog : XmlDialogBase
	{
		[Glade.Widget] public Entry edtOldPsw;
		[Glade.Widget] public Entry edtNewPsw1;
		[Glade.Widget] public Entry edtNewPsw2;
		[Glade.Widget] public Label laChMessage;
		
		public PasswdChDialog ()
		{
		}
		
		public override void OnCreate ()
		{
			base.OnCreate ();
			
		}

		public void NewPswEntryChanged(object sender, EventArgs args)
		{
			if(edtNewPsw1.Text == edtNewPsw2.Text)
			{
				laChMessage.Text = "";
			}
			else
			{
				laChMessage.Text = "Nové heslo je zadáno rozdílně";
			}
		}
		
		public void Execute()
		{
			while(true)
			{
				ResponseType response = Run();
				if(response == ResponseType.Cancel)
					return;
				
				if(edtNewPsw1.Text != edtNewPsw2.Text)
				{
					MainApp.ShowMessage(this.Window, MessageType.Error, "Chyba", 
						"Nová hesla se neshodují");
					continue;
				}
				
				if(MainApp.Connection.ChangePassword(edtOldPsw.Text, edtNewPsw1.Text))
				{
					MainApp.ShowMessage(this.Window, MessageType.Info, "Informace", 
						"Heslo bylo úspěšně změněno");
					return;
				}
				else
				{
					MainApp.ShowMessage(this.Window, MessageType.Error, "Chyba", 
						"Nepodařilo se nastavit heslo");
				}
			}
		}
	}
}
