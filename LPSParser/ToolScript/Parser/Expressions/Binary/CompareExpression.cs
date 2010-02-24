using System;

namespace LPS.ToolScript.Parser
{
	public enum ComparisonType
	{
		NonEqual,
		Less,
		LessOrEqual,
		Equal,
		GreaterOrEqual,
		Greater
	}

	public class CompareExpression : BinaryExpression
	{
		ComparisonType comptype;

		public CompareExpression(ComparisonType comptype, IExpression expr1, IExpression expr2)
			:base(expr1, expr2)
		{
			this.comptype = comptype;
		}

		public override object Eval (Context context, object val1, object val2)
		{
			return EvalAsBool(context, val1, val2);
		}

		public static int Compare(object val1, object val2)
		{
			if(val1 == null && val2 == null)
				return 0;
			else if(IsNumeric(val1) && IsNumeric(val2))
			{
				if(IsInteger(val1) && IsInteger(val2))
				{
					long i1 = Convert.ToInt64(val1);
					long i2 = Convert.ToInt64(val2);
					return i1.CompareTo(i2);
				}
				else
				{
					Decimal d1 = Convert.ToDecimal(val1);
					Decimal d2 = Convert.ToDecimal(val2);
					return d1.CompareTo(d2);
				}
			}
			else if(val1 is IComparable && val2 is IComparable)
				return ((IComparable)val1).CompareTo(val2);
			else throw new Exception(String.Format("Nelze porovnat hodnoty '{0}' a '{1}' typu {2} a {3}",
				val1, val2,
				(val1 == null)?"null":val1.GetType().Name,
			    (val2 == null)?"null":val2.GetType().Name));
		}

		public static bool Compare(ComparisonType comptype, object e1, object e2)
		{
			if(e1 is string && e2 is char)
				e2 = e2.ToString();
			else if(e2 is string && e1 is char)
				e1 = e1.ToString();

			switch(comptype)
			{
			case ComparisonType.Equal:
				return IsEqual(e1, e2);
			case ComparisonType.NonEqual:
				return !IsEqual(e1, e2);
			case ComparisonType.Less:
				return (Compare(e1, e2) < 0M);
			case ComparisonType.LessOrEqual:
				return (Compare(e1, e2) <= 0M);
			case ComparisonType.GreaterOrEqual:
				return (Compare(e1, e2) >= 0M);
			case ComparisonType.Greater:
				return (Compare(e1, e2) > 0M);
			}
			throw new NotImplementedException();
		}

		public override bool EvalAsBool (Context context, object e1, object e2)
		{
			return Compare(comptype, e1, e2);
		}

	}
}
