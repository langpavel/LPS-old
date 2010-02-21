using System;

namespace LPS.ToolScript.Tokens
{
	public abstract class UnaryExpression: ExpressionBase
	{
		public IExpression Expr { get; private set; }

		public UnaryExpression(IExpression Expr)
		{
			this.Expr = Expr;
		}

		public override object Eval (Context context)
		{
			return Eval(context, Expr.Eval(context));
		}

		public override bool EvalAsBool (Context context)
		{
			return EvalAsBool(context, Expr.EvalAsBool(context));
		}

		public abstract object Eval (Context context, object val);

		public abstract bool EvalAsBool (Context context, object val);
	}
}
