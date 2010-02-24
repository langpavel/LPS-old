using System;

namespace LPS.ToolScript.Parser
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
			object o;
			if(expr1 is Variable && ((Variable)expr1).IsUnset(context)
			   && ((expr2 is AddExpression) || (expr2 is SubstractExpression) || (expr2 is MultiplyExpression))
			   && Object.ReferenceEquals(((BinaryExpression)expr2).Expr1, expr1))
			{
				o = ((BinaryExpression)expr2).Expr2.Eval(context);
			}
			else
			{
				o = expr2.Eval(context);
			}
			if(!(expr1 is IAssignable))
				throw new InvalidOperationException(String.Format("Nelze přiřadit hodnotu do výrazu typu {0}", expr1));
			(expr1 as IAssignable).AssignValue(context, o);
			return o;
			//return expr1.Eval(context); // to dela neplechu treba kdyz je v indexeru vyraz, zavola se dvakrat!!
		}
	}
}
