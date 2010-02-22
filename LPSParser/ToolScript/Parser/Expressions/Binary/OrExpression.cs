using System;

namespace LPS.ToolScript.Parser
{
	public class OrExpression : BinaryExpression
	{
		public OrExpression(IExpression Expr1, IExpression Expr2)
			: base(Expr1, Expr2)
		{
		}

		public override object Eval (Context context, object val1, object val2)
		{
			return ((val1 is bool && (bool)val1 == true) || (val1 != null)) ? val1 : val2;
		}

		public override bool EvalAsBool (Context context, object val1, object val2)
		{
			return (bool)val1 || (bool)val2;
		}

	}
}
