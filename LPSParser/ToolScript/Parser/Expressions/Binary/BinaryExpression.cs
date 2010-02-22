using System;

namespace LPS.ToolScript.Parser
{
	public abstract class BinaryExpression : ExpressionBase
	{
		public IExpression Expr1 { get; private set; }
		public IExpression Expr2 { get; private set; }

		public BinaryExpression(IExpression Expr1, IExpression Expr2)
		{
			this.Expr1 = Expr1;
			this.Expr2 = Expr2;
		}

		public override object Eval (Context context)
		{
			return Eval(context, Expr1.Eval(context), Expr2.Eval(context));
		}

		public override bool EvalAsBool(Context context)
		{
			return EvalAsBool(context, Expr1.Eval(context), Expr2.Eval(context));
		}

		public abstract object Eval(Context context, object val1, object val2);
		public abstract bool EvalAsBool(Context context, object val1, object val2);
	}
}
