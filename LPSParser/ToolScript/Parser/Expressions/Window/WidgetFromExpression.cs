using System;

namespace LPS.ToolScript.Parser
{
	public class WidgetFromExpression : WidgetBase
	{
		public IExpression Expr { get; private set; }

		private object Value;

		public WidgetFromExpression(IExpression Expr, WidgetParamList Params)
			: base(null, Params)
		{
			this.Expr = Expr;
		}

		public override object Eval (Context context)
		{
			object result = base.Eval(context);
			this.Value = Expr.Eval(context);
			return result;
		}

		protected override Gtk.Widget CreateWidget ()
		{
			if(Value == null)
				return new Gtk.Label("");
			if(Value is IWidgetBuilder)
				return ((IWidgetBuilder)Value).Build();
			if(Value is String)
				return new Gtk.Label((string)Value);
			throw new NotImplementedException();
		}

		public override T GetAttribute<T> (string name, T default_value)
		{
			if(Value is IWidgetBuilder)
				return base.GetAttribute(name, ((IWidgetBuilder)Value).GetAttribute<T>(name, default_value));
			return base.GetAttribute(name, default_value);
		}
	}
}
