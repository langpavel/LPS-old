using System;

namespace LPS.ToolScript.Parser
{
	public class AlignExpression : SingleWidgetContainerBase
	{
		public Type BinType { get; private set; }
		public AlignExpression(string Name, EvaluatedAttributeList Params, IWidgetBuilder Child)
			: base(Name, Params, Child)
		{
		}

		private float GetAtr(string name, float defval)
		{
			float val = this.GetAttribute<float>(name, defval);
			if(val > 1.0f || val < 0.0f)
				throw new Exception("Hodnota zarovnání musí být v intervalu <0; 1>");
			return val;
		}

		protected override Gtk.Widget CreateWidget()
		{
			if(Child == null)
				throw new Exception("Align musí obsahovat widget");
			Gtk.Alignment a = new Gtk.Alignment(
				GetAtr("x", 0.5f),
				GetAtr("y", 0.5f),
				GetAtr("xs", 1.0f),
				GetAtr("ys", 1.0f));
			a.Child = Child.Build();
			return a;
		}
	}
}
