using System;
using Gtk;

namespace LPSClientSklad
{

	public class MainForm : XmlWindowBase
	{
		public MainForm ()
		{
		}
		
		public override void OnCreate()
		{
		}

		public void AppQuit(object sender, EventArgs args)
		{
			Application.Quit();
		}
		
	}
}
