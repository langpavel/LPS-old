using System;
using Gtk;
using System.IO;
using System.Text;

namespace LPS.Util
{
	public partial class UtilMainWindow : Gtk.Window
	{
		public static void Main(string[] args)
		{
			Application.Init();
			LPS.Client.LogWindow.Instance.Show();
			UtilMainWindow win = new UtilMainWindow();
			win.Show();
			win.InitCmds();
			Application.Run();
		}

		CommandCollection commands;
		public UtilMainWindow() : base(Gtk.WindowType.Toplevel)
		{
			this.Build();

			this.WindowPosition = WindowPosition.Center;
			this.WidthRequest = 800;
			this.HeightRequest = 500;
			this.DeleteEvent += WindowClosed;

			commands = new CommandCollection();

			TextTag tag = new TextTag("command");
			tag.Weight = Pango.Weight.Bold;
			tag.WeightSet = true;
			outputView.Buffer.TagTable.Add(tag);
		}

		public void InitCmds()
		{
			ExecuteCommand("cd ../../../LPSServer/resources/tables");
			ExecuteCommand("login http://localhost/LPS/Server.asmx,langpa,");
		}

		public void AppQuit()
		{
			Application.Quit();
		}

		private void WindowClosed (object o, DeleteEventArgs args)
		{
			AppQuit();
		}

		public void ExecuteCommand(string cmd)
		{
			TextBuffer  buffer = this.outputView.Buffer;
			TextIter iter = buffer.EndIter;

			TextMark mark = buffer.CreateMark(null, iter, true);

			buffer.Insert(ref iter, String.Format("Executing {0}\n", cmd));
			iter = buffer.GetIterAtMark(mark);
			buffer.ApplyTag("command", iter, buffer.EndIter);

			StringBuilder buider = new StringBuilder();
			using(StringWriter writer = new StringWriter(buider))
			{
				commands.Execute(cmd, writer, writer, writer);
			}
			string output = buider.ToString().Trim();
			if(output != "")
				output += "\n";
			iter = buffer.EndIter;
			buffer.Insert(ref iter, output);

			outputView.ScrollToMark(mark, 0.0, true, 0.0, 0.0);
		}

		protected virtual void OnEntry1Activated (object sender, System.EventArgs e)
		{
			Entry edt = (Entry)sender;
			if(edt.Text == "cls" || edt.Text == "clear")
				this.outputView.Buffer.Clear();
			else if(edt.Text == "exit" || edt.Text == "quit")
				AppQuit();
			else
			{
				ExecuteCommand(edt.Text);
			}
			edt.Text = "";
		}
		
		
	}
}
