using System;

namespace LPS.ToolScript.Parser
{
	public class InRangeExpression : ExpressionBase
	{
		public IExpression Value { get; private set; }
		public IExpression LowBound { get; private set; }
		public IExpression HighBound { get; private set; }
		public bool LeftClosed { get; private set; }
		public bool RightClosed { get; private set; }

		/// <summary>
		/// low < (<=) value < (<=) high;
		/// compare if value is in range,
		/// if low or high bound evaluates as null or DBNull, partial comparison is true
		/// if value evaluates to null and partial range is set, evaluates to false
		/// </summary>
		public InRangeExpression(IExpression Value,
			IExpression LowBound, IExpression HighBound,
			bool LeftClosed, bool RightClosed)
		{
			this.Value = Value;
			this.LowBound = LowBound;
			this.HighBound = HighBound;
			this.LeftClosed = LeftClosed;
			this.RightClosed = RightClosed;
		}

		public override object Eval (Context context)
		{
			return EvalAsBool(context);
		}

		public override bool EvalAsBool (Context context)
		{
			object val = Value.Eval(context);
			object low = LowBound.Eval(context);
			object high = HighBound.Eval(context);

			// check interval
			if((low != null && low != DBNull.Value) && (high != null && high != DBNull.Value) &&
				CompareExpression.Compare(
					(LeftClosed && RightClosed) ? ComparisonType.Greater : ComparisonType.GreaterOrEqual,
					low, high))
			{
				Log.Warning("Podmínka nemůže být nikdy pravdivá, interval je prázdný !");
				return false;
			}

			if(val == null || val == DBNull.Value)
				return (low == null || low == DBNull.Value) && (high == null || low == DBNull.Value);
			if(low != null && low != DBNull.Value)
			{
				if(!CompareExpression.Compare(
					LeftClosed ? ComparisonType.LessOrEqual : ComparisonType.Less,
					low, val))
					return false;
			}
			if(high != null && high != DBNull.Value)
				if(!CompareExpression.Compare(
					RightClosed ? ComparisonType.LessOrEqual : ComparisonType.Less,
					val, high))
					return false;
			return true;
		}
	}
}
