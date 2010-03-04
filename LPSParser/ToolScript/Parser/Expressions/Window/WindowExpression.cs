using System;
using Gtk;

namespace LPS.ToolScript.Parser
{
	public class WindowExpression : SingleWidgetContainerBase
	{
		public WindowExpression(string Name, EvaluatedAttributeList Params, IWidgetBuilder Child)
			: base(Name, Params, Child)
		{
		}

		private void SetWindowAttribs(Window win)
		{
			if(HasAttribute("center"))
				win.WindowPosition = Gtk.WindowPosition.Center;
			else if(HasAttribute("center_parent"))
				win.WindowPosition = Gtk.WindowPosition.CenterOnParent;
		}

		protected override Gtk.Widget CreateWidget()
		{
			if(HasAttribute("dialog"))
			{
				Dialog dialog = new Dialog();
				SetWindowAttribs(dialog);
				if(Child != null)
					dialog.VBox.Add(Child.Build());
				foreach(string s in GetAttribute<Array>("dialog"))
				{
					switch(s.ToLower())
					{
					case "reject":
						dialog.AddButton(s, ResponseType.Reject);
						break;
					case "accept":
						dialog.AddButton(s, ResponseType.Accept);
						break;
					case "ok":
						dialog.AddButton(s, ResponseType.Ok);
						break;
					case "cancel":
						dialog.AddButton(s, ResponseType.Cancel);
						break;
					case "close":
						dialog.AddButton(s, ResponseType.Close);
						break;
					case "yes":
						dialog.AddButton(s, ResponseType.Yes);
						break;
					case "no":
						dialog.AddButton(s, ResponseType.No);
						break;
					case "apply":
						dialog.AddButton(s, ResponseType.Apply);
						break;
					case "help":
						dialog.AddButton(s, ResponseType.Help);
						break;
					default:
						dialog.AddButton(s, ResponseType.None);
						break;
					}
				}
				return dialog;
			}
			else
			{
				Window win = new Window(Gtk.WindowType.Toplevel);
				if(Child != null)
					win.Add(Child.Build());
				return win;
			}
		}
	}
}
