using System;

namespace LPS.ToolScript.Parser
{
	public class ImageExpression : WidgetBase
	{
		public ImageExpression(string Name, EvaluatedAttributeList Params)
			:base(Name, Params)
		{
		}

		protected override Gtk.Widget CreateWidget()
		{
			return new Gtk.Image();
		}
	}
}
