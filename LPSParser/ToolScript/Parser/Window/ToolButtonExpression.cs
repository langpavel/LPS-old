using System;

namespace LPS.ToolScript.Parser
{
	public class ToolButtonExpression : SingleWidgetContainerBase
	{
		public ToolButtonExpression(string Name, EvaluatedAttributeList Params, IWidgetBuilder Child)
			:base(Name, Params, Child)
		{
		}

		protected override Gtk.Widget CreateWidget(WindowContext context)
		{
			if(this.Child != null)
			{
				Gtk.Widget child = this.Child.Build(context);
				return new Gtk.ToolButton(child, GetAttribute<string>("stock", ""));
			}
			return new Gtk.ToolButton(GetAttribute<string>("stock", ""));
		}

	}
}
