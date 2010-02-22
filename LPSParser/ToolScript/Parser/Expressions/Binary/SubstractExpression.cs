using System;
using System.Text;

namespace LPS.ToolScript.Parser
{
	public class SubstractExpression : BinaryExpression
	{
		public SubstractExpression(IExpression Expr1, IExpression Expr2)
			: base(Expr1, Expr2)
		{
		}

		public override object Eval (Context context, object val1, object val2)
		{
			if(IsNumeric(val1) && IsNumeric(val2))
			{
				if(IsDecimal(val1) || IsDecimal(val2))
					return Convert.ToDecimal(val1) - Convert.ToDecimal(val2);
				else
					return Convert.ToInt64(val1) - Convert.ToInt64(val2);
			}
			else throw new Exception(String.Format("Nelze odčítat hodnoty '{0}' a '{1}' typu {2} a {3}",
				val1, val2,
				(val1 == null)?"null":val1.GetType().Name,
			    (val2 == null)?"null":val2.GetType().Name));
		}

		public override bool EvalAsBool (Context context, object val1, object val2)
		{
			throw new InvalidOperationException("Nelze převést výsledek odčítání na hodnotu boolean");
		}

	}
}
