using System;

namespace LPS.ToolScript.Tokens
{
	public class UnaryIncDec : UnaryExpression
	{
		bool increment;
		bool post;
		public UnaryIncDec(IExpression Expr, bool increment, bool post)
			: base(Expr)
		{
			this.increment = increment;
			this.post = post;
		}

		public override object Eval (Context context, object val)
		{
			IAssignable v = (IAssignable)Expr;
			object result;
			if(val is Int64)
			{
				if(post)
				{
					if(increment)
					{
						result = val;
						v.AssignValue(context, (long)result + 1L);
					}
					else
					{
						result = val;
						v.AssignValue(context, (long)result - 1L);
					}
				}
				else
				{
					if(increment)
					{
						result = (long)val + 1L;
						v.AssignValue(context, result);
					}
					else
					{
						result = (long)val - 1L;
						v.AssignValue(context, result);
					}
				}
			} else if(val is Decimal)
			{
				if(post)
				{
					if(increment)
					{
						result = val;
						v.AssignValue(context, (Decimal)result + 1M);
					}
					else
					{
						result = val;
						v.AssignValue(context, (Decimal)result - 1M);
					}
				}
				else
				{
					if(increment)
					{
						result = (Decimal)val + 1M;
						v.AssignValue(context, result);
					}
					else
					{
						result = (Decimal)val - 1M;
						v.AssignValue(context, result);
					}
				}
			}
			else
			{
				throw new InvalidOperationException(String.Format("Na datovém typu {0} nelze provádět inkrementaci nebo dekrementaci"));
			}

			return result;
		}

		public override bool EvalAsBool (Context context, object val)
		{
			throw new InvalidOperationException("Nelze převést výsledek operace inkrementace nebo dekrementace na boolean");
		}

	}
}
