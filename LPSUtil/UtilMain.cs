using System;
using Gtk;

namespace LPS.Util
{
	public static class UtilMain
	{
		public static void Main(string[] args)
		{
			Application.Init();
			LPS.Client.LogWindow.Instance.Show();
			UtilMainWindow win = new UtilMainWindow();
			win.Show();
			win.InitCmds();
			win.LoadLastBuffer();
			Application.Run();
		}
	}
}
