using System;

namespace LPS.ToolScript.Parser
{
	public class ToolbarExpression : ContainerWidgetBase
	{
		public ToolbarExpression(string Name, EvaluatedAttributeList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected override Gtk.Widget CreateWidget()
		{
			Gtk.Toolbar toolbar = new Gtk.Toolbar();
			foreach(IWidgetBuilder builder in Childs)
			{
				toolbar.Add(builder.Build());
			}
			return toolbar;
		}

	}
}
