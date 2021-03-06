using System;

namespace LPS.ToolScript.Parser
{
	public class NotExpression : UnaryExpression
	{
		public NotExpression(IExpression Expr)
			: base(Expr)
		{
		}

		public override object Eval (IExecutionContext context, object val)
		{
			return EvalAsBool(context, val);
		}

		public override bool EvalAsBool (IExecutionContext context, object val)
		{
			return ! AsBoolean(val);
		}

	}
}
