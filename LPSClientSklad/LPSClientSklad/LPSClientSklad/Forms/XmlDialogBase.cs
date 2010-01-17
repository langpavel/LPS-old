using System;
using Gtk;

namespace LPSClientSklad
{
	public class XmlDialogBase : XmlWindowBase
	{
		public Dialog Dialog { get { return this.Window as Dialog; } }

		public XmlDialogBase ()
		{
		}

		public ResponseType Run()
		{
			//this.Dialog.Modal = true;
			return (ResponseType) this.Dialog.Run();
		}
	}
}
