using System;

namespace LPS.ToolScript.Parser
{
	public class LabelExpression : WidgetBase
	{
		public string Markup { get; private set; }
		public LabelExpression(string Name, string Markup, EvaluatedAttributeList Params)
			: base(Name, Params)
		{
			this.Markup = Markup;
		}

		protected override Gtk.Widget CreateWidget ()
		{
			Gtk.Label l = new Gtk.Label();
			l.Markup = this.Markup;
			return l;
		}

	}
}
