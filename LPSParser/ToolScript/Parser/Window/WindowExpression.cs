using System;
using Gtk;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class WindowExpression : SingleWidgetContainerBase
	{
		private WindowContext wincontext;

		public WindowExpression(string Name, EvaluatedAttributeList Params, IWidgetBuilder Child)
			: base(Name, Params, Child)
		{
		}

		public override object Eval(IExecutionContext context)
		{
			if(context is WindowContext)
				wincontext = (WindowContext)context;
			else
				wincontext = WindowContext.CreateAsChild(context);
			return base.Eval(wincontext);
		}

		public WindowContext Create()
		{
			WindowContext result = (WindowContext)wincontext.Clone();
			result.Window = (Gtk.Window)Build(result);
			return result;
		}

		private void SetWindowAttribs(Window win)
		{
			if(HasAttribute("center"))
				win.WindowPosition = Gtk.WindowPosition.Center;
			else if(HasAttribute("center_parent"))
				win.WindowPosition = Gtk.WindowPosition.CenterOnParent;
		}

		protected override Gtk.Widget CreateWidget(WindowContext context)
		{
			Window win;
			if(HasAttribute("dialog"))
			{
				Dialog dialog = new Dialog();
				dialog.AddAccelGroup(context.AccelGroup);
				SetWindowAttribs(dialog);
				if(Child != null)
					dialog.VBox.Add(Child.Build(context));
				foreach(string s in GetAttribute<Array>("dialog"))
				{
					string[] bits = s.Split(':');
					string text = bits[0];
					int response = 0;
					if(bits.Length > 4)
						throw new Exception("Neplatný počet parametrů tlačítka v poli tlačítek dialogu");
					if(bits.Length > 1)
						response = (int)IntLiteral.Parse(bits[1]);
					Label l = new Label();
					l.Markup = text;
					Button btn;
					if(bits.Length > 2 && !String.IsNullOrEmpty(bits[2]))
					{
						HBox hbox = new HBox(false, 0);
						hbox.PackStart(ImageExpression.CreateImage(bits[2], IconSize.Button));
						hbox.PackStart(l);
						btn = new Button(hbox);
					}
					else
						btn = new Button(l);
					btn.ShowAll();
					dialog.AddActionWidget(btn, response);
					if(bits.Length > 3)
					{
						switch(bits[3].ToLower())
						{
						case "default":
							btn.CanDefault = true;
							dialog.Default = btn;
							break;
						case "cancel":
							// set as cancel action - how?
							break;
						case "":
						case "none":
							break;
						default:
							throw new Exception("Neznámý příznak tlačítka dialogu");
						}
					}

				}
				win = dialog;
			}
			else
			{
				win = new Window(Gtk.WindowType.Toplevel);
				win.AddAccelGroup(context.AccelGroup);
				if(Child != null)
					win.Add(Child.Build(context));
			}

			if(HasAttribute("iconset"))
			{
				IconSet icons = IconFactory.LookupDefault(GetAttribute<string>("iconset"));
				List<Gdk.Pixbuf> list = new List<Gdk.Pixbuf>();
				foreach(IconSize size in icons.Sizes)
				{
					list.Add(icons.RenderIcon(win.Style, TextDirection.Ltr, StateType.Normal, size, win, ""));
				}
				win.IconList = list.ToArray();
			}

			return win;
		}
	}
}
