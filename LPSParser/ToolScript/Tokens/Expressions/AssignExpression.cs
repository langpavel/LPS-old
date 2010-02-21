using System;

namespace LPS.ToolScript.Tokens
{
	public class AssignExpression : ExpressionBase
	{
		IExpression expr1;
		IExpression expr2;
		public AssignExpression(IExpression expr1, IExpression expr2)
		{
			this.expr1 = expr1;
			this.expr2 = expr2;
		}

		public override object Eval (Context context)
		{
			object o = expr2.Eval(context);
			if(expr1 is VariableInit)
				(expr1 as Variable).Initialize(context, o);
			else
				(expr1 as IAssignable).AssignValue(context, o);
			return expr1.Eval(context);
		}
	}
}
