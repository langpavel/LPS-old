using System;
using Gtk;

namespace LPS.Client
{


	public class LongMessageDialog : XmlDialogBase
	{
		[Glade.Widget] Label laNadpis;
		[Glade.Widget] TextView textZprava;
		
		public LongMessageDialog ()
		{
		}
		
		public override void OnCreate ()
		{
			base.OnCreate ();
		}
		
		public string Title
		{
			get { return this.Window.Title; }
			set { this.Window.Title = value; }
		}
		
		public string Title2
		{
			get { return this.laNadpis.Text; }
			set { this.laNadpis.Text = value; }
		}

		public string Text
		{
			get { return this.textZprava.Buffer.Text; }
			set { this.textZprava.Buffer.Text = value; }
		}

		public static void Show(string nadpis1, string nadpis2, string text)
		{
			using(LongMessageDialog dlg = FormFactory.Create<LongMessageDialog>("long-message"))
			{
				dlg.Title = nadpis1;
				dlg.Title2 = nadpis2;
				dlg.Text = text;
				dlg.Run();
			}
		}
		
	}
}
