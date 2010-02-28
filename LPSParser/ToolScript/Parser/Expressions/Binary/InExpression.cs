using System;
using System.Collections;

namespace LPS.ToolScript.Parser
{
	public class InExpression : BinaryExpression
	{
		public InExpression(IExpression expr1, IExpression expr2)
			:base(expr1, expr2)
		{
		}

		public override object Eval (Context context, object val1, object val2)
		{
			return EvalAsBool(context, val1, val2);
		}

		public override bool EvalAsBool (Context context, object e1, object e2)
		{
			if(e2 is IRange)
			{
				IRange range = (IRange)e2;
				if(e1 == null || e1 is DBNull)
					return range.IsIn(null);
				Type t = e1.GetType();
				if(t == range.ValueType || t.IsSubclassOf(range.ValueType))
					return range.IsIn(e1);
				else
					return range.IsIn(Convert.ChangeType(e1,range.ValueType));
			}
			else if(e2 is IEnumerable)
			{
				foreach(object val in (IEnumerable)e2)
					if(IsEqual(e1, val))
						return true;
				return false;
			}
			throw new InvalidOperationException();
		}

	}
}
