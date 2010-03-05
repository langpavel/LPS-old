using System;

namespace LPS.ToolScript.Parser
{
	public abstract class UnaryExpression: ExpressionBase
	{
		public IExpression Expr { get; private set; }

		public UnaryExpression(IExpression Expr)
		{
			this.Expr = Expr;
		}

		public override object Eval (IExecutionContext context)
		{
			return Eval(context, Expr.Eval(context));
		}

		public override bool EvalAsBool (IExecutionContext context)
		{
			return EvalAsBool(context, Expr.EvalAsBool(context));
		}

		public abstract object Eval (IExecutionContext context, object val);

		public abstract bool EvalAsBool (IExecutionContext context, object val);
	}
}
