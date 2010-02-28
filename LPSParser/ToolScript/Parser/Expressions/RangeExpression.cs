using System;

namespace LPS.ToolScript.Parser
{
	public class RangeExpression : ExpressionBase
	{
		public RangeType RangeType { get; private set; }
		public IExpression ExprLow { get; private set; }
		public IExpression ExprHigh { get; private set; }

		public RangeExpression(RangeType RangeType, IExpression ExprLow, IExpression ExprHigh)
		{
			this.RangeType = RangeType;
			this.ExprLow = ExprLow;
			this.ExprHigh = ExprHigh;
		}

		public override object Eval (Context context)
		{
			object vl = ExprLow.Eval(context);
			object vh = ExprHigh.Eval(context);
			if(vl != null && vh != null)
			{
				if(IsNumeric(vl) && IsNumeric(vh))
					return new Range<Decimal>(Convert.ToDecimal(vl), Convert.ToDecimal(vh), this.RangeType);
				if(vl is DateTime && vh is DateTime)
					return new Range<DateTime>((DateTime)vl, (DateTime)vh, this.RangeType);
			}
			else if(vl != null)
			{
				if(IsNumeric(vl))
					return new Range<Decimal>(Convert.ToDecimal(vl), null, this.RangeType);
				if(vl is DateTime)
					return new Range<DateTime>((DateTime)vl, null, this.RangeType);
			}
			else if(vh != null)
			{
				if(IsNumeric(vh))
					return new Range<Decimal>(null, Convert.ToDecimal(vh), this.RangeType);
				if(vl is DateTime && vh is DateTime)
					return new Range<DateTime>(null, (DateTime)vh, this.RangeType);
			}

			throw new InvalidOperationException();
		}

	}
}
