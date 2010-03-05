using System;

namespace LPS.ToolScript.Parser
{
	public class AndExpression : BinaryExpression
	{
		public AndExpression(IExpression Expr1, IExpression Expr2)
			: base(Expr1, Expr2)
		{
		}

		public override object Eval (IExecutionContext context, object val1, object val2)
		{
			return EvalAsBool(context, val1, val2);
		}

		public override bool EvalAsBool (IExecutionContext context, object val1, object val2)
		{
			return (bool)val1 && (bool)val2;
		}

	}
}
