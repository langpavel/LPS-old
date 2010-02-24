using System;
using System.Text;

namespace LPS.ToolScript.Parser
{
	public class DivideExpression : BinaryExpression
	{
		public DivideExpression(IExpression Expr1, IExpression Expr2)
			: base(Expr1, Expr2)
		{
		}

		public override object Eval (Context context, object val1, object val2)
		{
			if(IsNumeric(val1) && IsNumeric(val2))
			{
				return Convert.ToDecimal(val1) / Convert.ToDecimal(val2);
			}
			else if(val1 is string && val2 is object[])
			{
				return String.Format((string)val1, (object[])val2);
			}
			else if(val1 is string && val2 is Array)
			{
				object[] o = new object[((Array)val2).Length];
				((Array)val2).CopyTo(o, 0);
				return String.Format((string)val1, o);
			}
			else if(val1 is string)
			{
				return String.Format((string)val1, val2);
			}
			else throw new Exception(String.Format("Nelze dělit hodnoty '{0}' a '{1}' typu {2} a {3}",
				val1, val2,
				(val1 == null)?"null":val1.GetType().Name,
			    (val2 == null)?"null":val2.GetType().Name));
		}

		public override bool EvalAsBool (Context context, object val1, object val2)
		{
			throw new InvalidOperationException("Nelze převést výsledek dělení na hodnotu boolean");
		}

	}
}
