using System;
using Gtk;
using System.IO;
using System.Text;
using LPS.ToolScript;
using System.Reflection;
using System.Collections.Generic;

namespace LPS.Util
{
	public class UtilMainWindow:Window
	{

		public void AddCommand(ICommand cmd)
		{
			ParserContext.InitVariable(cmd.Name, cmd);
		}

		public Context ParserContext { get;	private set; }

		public UtilMainWindow() : base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			
			this.WindowPosition = WindowPosition.Center;
			this.WidthRequest = 800;
			this.HeightRequest = 500;
			this.DeleteEvent += WindowClosed;
			
			ParserContext = Context.CreateRootContext(new ToolScriptParser());
			
			TextTag tag = new TextTag("command");
			tag.Weight = Pango.Weight.Bold;
			tag.WeightSet = true;
			outputView.Buffer.TagTable.Add(tag);
			
			TextTag tagerror = new TextTag("error");
			tagerror.Weight = Pango.Weight.Bold;
//			tagerror.WeightSet = true;
			tagerror.Font = "Courier Bold";
			tagerror.Foreground = "#880000";
			outputView.Buffer.TagTable.Add(tagerror);
		}

		public void InitCmds()
		{
			AddCommand(new LoginCommand("login"));
			AddCommand(new ChangeDirCommand("cd"));
			AddCommand(new EchoCommand("echo"));
			AddCommand(new HelpCommand("help"));
			AddCommand(new LoadTxtTableCommand("loadtxt"));
			AddCommand(new LsDirCommand("ls"));
			AddCommand(new LsDirCommand("dir"));
			AddCommand(new PingCommand("ping"));
			AddCommand(new RefreshCommand("refresh"));
			AddCommand(new SqlTablesCommand("sqltab"));
			AddCommand(new XtableCommand("xtab"));
			AddCommand(new WriteFileCommand("writefile"));
			AddCommand(new ReadFileCommand("readfile"));
			
			ParserContext.InitVariable("__STD_OUT", TextWriter.Null);
			ParserContext.InitVariable("__STD_INFO", TextWriter.Null);
			ParserContext.InitVariable("__STD_ERR", TextWriter.Null);
			
			string path = Assembly.GetExecutingAssembly().Location;
			path = System.IO.Path.GetDirectoryName(path);
			path = System.IO.Path.Combine(path, "Scripts");
			List<string> files = new List<string>(Directory.GetFiles(path, "*.ts"));
			files.Sort();
			foreach(string file in files)
			{
				string filename = System.IO.Path.Combine(path, file);
				using(Log.Scope("Reading {0}", filename))
				{
					try
					{
						using(StreamReader reader = new StreamReader(filename))
						{
							ParserContext.Eval(reader.ReadToEnd());
						}
					}
					catch(Exception err)
					{
						Log.Error(err);
						WriteBufferWithTag("error", "Chyba při zpracování souboru {0}:\n{1}", filename, err);
					}
				}
			}
		}

		public void AppQuit()
		{
			Application.Quit();
		}

		private void WindowClosed(object o, DeleteEventArgs args)
		{
			AppQuit();
		}

		public void WriteBufferWithTag(string tagname, string text, params object[] args)
		{
			TextBuffer buffer = this.outputView.Buffer;
			TextIter iter = buffer.EndIter;
			TextMark mark = buffer.CreateMark(null, iter, true);
			buffer.Insert(ref iter, String.Format(text, args));
			iter = buffer.GetIterAtMark(mark);
			buffer.ApplyTag(tagname, iter, buffer.EndIter);
		}

		public void ExecuteCommand(string cmd)
		{
			TextBuffer buffer = this.outputView.Buffer;
			TextIter iter = buffer.EndIter;
			
			TextMark mark = buffer.CreateMark(null, iter, true);
			
			buffer.Insert(ref iter, String.Format("Executing {0}\n", cmd));
			iter = buffer.GetIterAtMark(mark);
			buffer.ApplyTag("command", iter, buffer.EndIter);
			
			StringBuilder buider = new StringBuilder();
			using(StringWriter writer = new StringWriter(buider))
			{
				ParserContext.InitVariable("__STD_OUT", writer);
				ParserContext.InitVariable("__STD_INFO", writer);
				ParserContext.InitVariable("__STD_ERR", writer);
				try
				{
					ParserContext.Eval(cmd);
				}
				catch(Exception err)
				{
					WriteBufferWithTag("error", "Chyba: {0}\n", err);
				}
			}
			string output = buider.ToString().Trim();
			if(output != "")
				output += "\n";
			iter = buffer.EndIter;
			buffer.Insert(ref iter, output);
			
			outputView.ScrollToMark(mark, 0.0, true, 0.0, 0.0);
		}

		protected virtual void OnEntry1Activated(object sender, System.EventArgs e)
		{
			Entry edt = (Entry) sender;
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


		#region generated code
		private Gtk.VBox vbox2;

		private Gtk.Entry entry1;

		private Gtk.ScrolledWindow GtkScrolledWindow;

		private Gtk.TextView outputView;

		protected virtual void Build()
		{
			// Widget LPS.Util.UtilMainWindow
			this.Name = "LPS.Util.UtilMainWindow";
			this.Title = Mono.Unix.Catalog.GetString("UtilMainWindow");
			this.WindowPosition = ((Gtk.WindowPosition) (4));
			// Container child LPS.Util.UtilMainWindow.Gtk.Container+ContainerChild
			this.vbox2 = new Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.entry1 = new Gtk.Entry();
			this.entry1.CanFocus = true;
			this.entry1.Name = "entry1";
			this.entry1.IsEditable = true;
			this.entry1.InvisibleChar = '●';
			this.vbox2.Add(this.entry1);
			Gtk.Box.BoxChild w1 = ((Gtk.Box.BoxChild) (this.vbox2[this.entry1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((Gtk.ShadowType) (1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.outputView = new Gtk.TextView();
			this.outputView.CanFocus = true;
			this.outputView.Name = "outputView";
			this.outputView.Editable = false;
			this.GtkScrolledWindow.Add(this.outputView);
			this.vbox2.Add(this.GtkScrolledWindow);
			Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild) (this.vbox2[this.GtkScrolledWindow]));
			w3.Position = 1;
			this.Add(this.vbox2);
			if((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show();
			this.entry1.Activated += new System.EventHandler(this.OnEntry1Activated);
		}
		#endregion
		
	}
}
