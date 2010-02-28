using System;

namespace LPS
{
	public enum RangeType : int
	{
		/// <summary>
		/// Range not specified, for evaluating need range to be provided
		/// </summary>
		NotSpecified = 0,
		/// <summary>
		/// interval low <= value <= high
		/// </summary>
		Closed = 1,
		/// <summary>
		/// interval low <= value < high
		/// </summary>
		LeftClosed = 2,
		/// <summary>
		/// interval low < value <= high
		/// </summary>
		RightClosed = 3,
		/// <summary>
		/// interval low < value < high
		/// </summary>
		Open = 4
	}

	public interface IRange
	{
		bool IsIn(RangeType Range, object value);
		bool IsIn(object value);
		Type ValueType { get; }
	}

	public struct Range<T> : IRange where T: struct, IComparable
	{
		public T? Low;
		public T? High;
		public RangeType RangeType;

		public Range(T? Low, T? High)
		{
			this.Low = Low;
			this.High = High;
			this.RangeType = RangeType.NotSpecified;
		}

		public Range(T? Low, T? High, RangeType Range)
		{
			this.Low = Low;
			this.High = High;
			this.RangeType = Range;
		}

		private bool Compare(T? a, T? b, bool allowequal)
		{
			if(!a.HasValue || !b.HasValue)
				return true;
			if(allowequal)
				return a.Value.CompareTo(b.Value) <= 0;
			else
				return a.Value.CompareTo(b.Value) < 0;
		}

		public bool IsIn(RangeType Range, T? value)
		{
			if(!value.HasValue)
				return !(Low.HasValue || High.HasValue);
			switch(Range)
			{
			case RangeType.NotSpecified:
				throw new InvalidOperationException("Rozsah intervalu není definovaný");
			case RangeType.Closed:
				return Compare(Low, value, true)  && Compare(value, High, true);
			case RangeType.LeftClosed:
				return Compare(Low, value, true)  && Compare(value, High, false);
			case RangeType.RightClosed:
				return Compare(Low, value, false) && Compare(value, High, true);
			case RangeType.Open:
				return Compare(Low, value, false) && Compare(value, High, false);
			default:
				throw new NotImplementedException();
			}
		}

		public bool IsIn(T? value)
		{
			return IsIn(this.RangeType, value);
		}

		bool IRange.IsIn (RangeType Range, object value)
		{
			if(value == null || value is DBNull)
				return this.IsIn(Range, null);
			else
				return this.IsIn(Range, (T)value);
		}

		bool IRange.IsIn (object value)
		{
			return ((IRange)this).IsIn(this.RangeType, value);
		}

		Type IRange.ValueType { get { return typeof(T); } }
	}
}
