using System;

namespace LPS.ToolScript.Tokens
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
			if(val1 is IComparable && val2 is IComparable)
				return ((IComparable)val1).CompareTo(val2);
			else if(val1 == null && val2 == null)
				return 0;
			else if(val1 != null && val1.Equals(val2))
				return 0;
			else if(val2 != null && val2.Equals(val1))
				return 0;
			else throw new Exception(String.Format("Nelze porovnat hodnoty '{0}' a '{1}' typu {2} a {3}",
				val1, val2,
				(val1 == null)?"null":val1.GetType().Name,
			    (val2 == null)?"null":val2.GetType().Name));
		}

		public override bool EvalAsBool (Context context, object e1, object e2)
		{
			switch(comptype)
			{
			case ComparisonType.Equal:
			return (e1 != null) ? e1.Equals(e2) : ( (e2 != null) ? e2.Equals(e1) : true );
			case ComparisonType.NonEqual:
			return ! ( (e1 != null) ? e1.Equals(e2) : ( (e2 != null) ? e2.Equals(e1) : true ) );
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

	}
}
