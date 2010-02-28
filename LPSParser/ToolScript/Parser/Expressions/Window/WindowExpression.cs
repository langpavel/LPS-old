using System;

namespace LPS.ToolScript.Parser
{
	public class WindowExpression : SingleWidgetContainerBase
	{
		public WindowExpression(string Name, EvaluatedAttributeList Params, IWidgetBuilder Child)
			: base(Name, Params, Child)
		{
		}

		protected override Gtk.Widget CreateWidget()
		{
			Gtk.Window win = new Gtk.Window(Gtk.WindowType.Toplevel);
			if(Child != null)
			{
				win.Add(Child.Build());
			}
			return win;
		}
	}
}
