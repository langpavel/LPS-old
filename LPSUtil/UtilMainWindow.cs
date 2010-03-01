using System;
using Gtk;
using System.IO;
using System.Text;
using LPS.ToolScript;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

namespace LPS.Util
{
	public class UtilMainWindow:Window
	{

		public static UtilMainWindow Instance { get; private set; }

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

			TextTag tagcode = new TextTag("code");
			tagcode.Weight = Pango.Weight.Bold;
			tagcode.Font = "Courier Bold";
			outputView.Buffer.TagTable.Add(tagcode);

			Instance = this;
		}

		public void InitCmds()
		{
			AddCommand(new LoginCommand("login"));
			AddCommand(new ChangeDirCommand("cd"));
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
			AddCommand(new VarsCommand("vars"));
			AddCommand(new SerializeCommand("serialize"));

			ParserContext.InitVariable("__STD_OUT", TextWriter.Null);
			ParserContext.InitVariable("__STD_INFO", TextWriter.Null);
			ParserContext.InitVariable("__STD_ERR", TextWriter.Null);
			
			string path = Assembly.GetExecutingAssembly().Location;
			path = System.IO.Path.GetDirectoryName(path);
			path = System.IO.Path.Combine(path, "Scripts");
			List<string> files = new List<string>(Directory.GetFiles(path, "*.lps"));
			files.Sort();
			Log.Debug("Init files in order: {0}", String.Join("; ", files.ToArray()));
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
						Log.Debug("file loaded: {0}", file);
					}
					catch(Exception err)
					{
						Log.Error(err);
						WriteBufferWithTag("error", "Chyba při zpracování souboru {0}:\n{1}\n", filename, err);
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
			using(Log.Scope("Command: {0}", cmd))
			{
				TextBuffer buffer = this.outputView.Buffer;
				TextIter iter = buffer.EndIter;
				
				TextMark mark = buffer.CreateMark(null, iter, true);
				
				buffer.Insert(ref iter, String.Format("Executing {0}\n", cmd));
				iter = buffer.GetIterAtMark(mark);
				buffer.ApplyTag("command", iter, buffer.EndIter);
				
				StringBuilder buider = new StringBuilder();
				string str_result = "";
				using(StringWriter writer = new StringWriter(buider))
				{
					ParserContext.InitVariable("__STD_OUT", writer);
					ParserContext.InitVariable("__STD_INFO", writer);
					ParserContext.InitVariable("__STD_ERR", writer);
					try
					{
						object result = ParserContext.Eval(cmd);
						if(result != SpecialValue.Void)
						{
							StringBuilder sb = new StringBuilder();
							GetStringRepr(result, sb, false);
							sb.AppendLine();
							str_result = sb.ToString();
						}
						Log.Debug("Result: {0}", result);
					}
					catch(Exception err)
					{
						Log.Error(err);
						WriteBufferWithTag("error", "Chyba: {0}\n", err);
					}
				}
				string output = buider.ToString().Trim();
				if(output != "")
					output += "\n";
				iter = buffer.EndIter;
				buffer.Insert(ref iter, output);

				if(!String.IsNullOrEmpty(str_result))
				{
					WriteBufferWithTag("code", str_result);
				}

				outputView.ScrollToMark(mark, 0.0, true, 0.0, 0.0);
			}
		}
	
		protected virtual void OnEntry1Activated(object sender, System.EventArgs e)
		{
			Entry edt = (Entry) sender;
			if(edt.Text == "cls" || edt.Text == "clear")
				this.outputView.Buffer.Clear();
			else if(edt.Text == "exit" || edt.Text == "quit" || edt.Text == "q")
				AppQuit();
			else
			{
				ExecuteCommand(edt.Text);
			}
			edt.Text = "";
		}

		public static void GetStringRepr(object obj, StringBuilder sb, bool inline)
		{
			if(obj == null)
				sb.Append("(null)");
			else if(obj is string)
				sb.AppendFormat("'{0}'", (string)obj);
			else if(obj is Hashtable)
			{
				foreach(DictionaryEntry de in ((Hashtable)obj))
				{
					sb.Append("[");
					GetStringRepr(de.Key, sb, true);
					sb.Append("]:\t");

					GetStringRepr(de.Value, sb, true);
					if(!inline)
						sb.Append("\n");
					else
						sb.Append(";\t");
				}
			}
			else if(obj is IEnumerable)
			{
				sb.Append(obj.GetType().Name);
				if(inline)
					sb.Append(" [");
				else
					sb.Append(" [\n\t");
				bool first = true;
				foreach(object item in (IEnumerable)obj)
				{
					if(!first)
					{
						if(!inline)
							sb.Append("\n\t");
						else
							sb.Append(",\t");
					}
					else
						first = false;
					GetStringRepr(item, sb, true);
				}
				if(inline)
					sb.Append(']');
				else
					sb.Append("\n]\n");
			}
			else
				sb.Append(obj);
		}

		public void LoadLastBuffer()
		{
			try
			{
				tw.Buffer.Text = File.ReadAllText("last.lps");
			} catch { }
		}

		public bool SaveLastBuffer()
		{
			try
			{
				string s = tw.Buffer.Text.Trim();
				if(!String.IsNullOrEmpty(s))
					File.WriteAllText("last.lps",tw.Buffer.Text);
				return true;
			} catch { return false; }
		}

		#region generated code
		private Gtk.VBox vbox2;

		private Gtk.Entry entry1;

		private Gtk.ScrolledWindow GtkScrolledWindow;

		private Gtk.TextView outputView;

		private TextView tw;

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

			tw = new TextView();
			Pango.FontDescription font = new Pango.FontDescription();
			font.Family = "Courier";
			font.Weight = Pango.Weight.Bold;
			tw.ModifyFont(font);
			ScrolledWindow sc = new ScrolledWindow();
			sc.Add(tw);
			vbox2.Add(sc);
			tw.KeyPressEvent += HandleTwKeyPressEvent;

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

		void HandleTwKeyPressEvent (object o, KeyPressEventArgs args)
		{
			switch(args.Event.Key)
			{
			case Gdk.Key.F5: // run
				ExecuteCommand(tw.Buffer.Text);
				return;
			case Gdk.Key.F4: // open
				LoadLastBuffer();
				return;
			case Gdk.Key.F10: // save
				SaveLastBuffer();
				return;
			case Gdk.Key.F12: // save & quit
				if(SaveLastBuffer())
					Application.Quit();
				return;
			}
		}

		#endregion
		
	}
}
