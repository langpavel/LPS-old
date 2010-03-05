using System;

namespace LPS.ToolScript.Parser
{
	public class ButtonExpression : SingleWidgetContainerBase
	{
		public Type BinType { get; private set; }
		public ButtonExpression(string Name, EvaluatedAttributeList Params, IWidgetBuilder Child)
			: base(Name, Params, Child)
		{
		}

		protected override Gtk.Widget CreateWidget(WindowContext context)
		{
			Gtk.Button btn;
			if(Child != null)
			{
				btn = new Gtk.Button(Child.Build(context));
				btn.ImagePosition = Gtk.PositionType.Left;
			}
			else
				btn = new Gtk.Button();
			return btn;
		}
	}
}
