using System;
using System.Globalization;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public enum DateTimeType
	{
		DateTime,
		Date,
		Time,
		Now,
		Today,
		Yesterday,
		Tomorrow
	}

	public sealed class DatetimeLiteral : LiteralBase, IConstantValue
	{
		public DateTimeType DateTimeType { get; private set; }
		public DateTime Value { get; private set; }

		public DatetimeLiteral(DateTimeType DateTimeType)
		{
			this.DateTimeType = DateTimeType;
		}

		public DatetimeLiteral(DateTimeType DateTimeType, DateTime Value)
		{
			this.DateTimeType = DateTimeType;
			this.Value = Value;
		}

		public override void Run(IExecutionContext context)
		{
		}

		public override object Eval(IExecutionContext context)
		{
			return (DateTime)this;
		}

		public static explicit operator DateTime(DatetimeLiteral lit)
		{
			switch(lit.DateTimeType)
			{
			case DateTimeType.DateTime:
			case DateTimeType.Date:
			case DateTimeType.Time:
				return lit.Value;
			case DateTimeType.Now:
				return DateTime.Now;
			case DateTimeType.Today:
				return DateTime.Today;
			case DateTimeType.Tomorrow:
				return DateTime.Today.AddDays(1.0);
			case DateTimeType.Yesterday:
				return DateTime.Today.AddDays(-1.0);
			default:
				throw new NotImplementedException();
			}
		}

		public override string ToString ()
		{
			switch(this.DateTimeType)
			{
			case DateTimeType.DateTime:
				return this.Value.ToString("'D'yyyy-MM-dd'T'hh:mm:ss.fff");
			case DateTimeType.Date:
				return this.Value.ToString("'D'yyyy-MM-dd'");
			case DateTimeType.Time:
				return this.Value.ToString("'T'hh:mm:ss.fff");
			case DateTimeType.Now:
				return "now";
			case DateTimeType.Today:
				return "today";
			case DateTimeType.Tomorrow:
				return "tomorrow";
			case DateTimeType.Yesterday:
				return "yesterday";
			default:
				throw new NotImplementedException();
			}
		}

		public override bool EvalAsBool(IExecutionContext context)
		{
			throw new Exception("Nelze vyhodnotit datum jako boolean");
		}

		public static DatetimeLiteral Create(string text)
		{
			text = text.ToLower();
			switch(text)
			{
			case "now":
				return new DatetimeLiteral(DateTimeType.Now);
			case "today":
				return new DatetimeLiteral(DateTimeType.Today);
			case "yesterday":
				return new DatetimeLiteral(DateTimeType.Yesterday);
			case "tomorrow":
				return new DatetimeLiteral(DateTimeType.Tomorrow);
			default:
				if(text.StartsWith("d"))
				{
					string[] bits = text.Split(new char[] {
						'd', 't', '-', ':', '.'}, StringSplitOptions.RemoveEmptyEntries);
					int[] v = new int[7];
					for(int i = 0; i < 7; i++)
						v[i] = (i < bits.Length) ? Int32.Parse(bits[i]) : 0;
					return new DatetimeLiteral(
						(bits.Length > 3) ? DateTimeType.DateTime : DateTimeType.Date,
						new DateTime(v[0], v[1], v[2], v[3], v[4], v[5], v[6]));
				}
				else if(text.StartsWith("t"))
				{
					string[] bits = text.Split(new char[] {
						't', '-', ':', '.'}, StringSplitOptions.RemoveEmptyEntries);
					int[] v = new int[4];
					for(int i = 0; i < 4; i++)
						v[i] = (i < bits.Length) ? Int32.Parse(bits[i]) : 0;
					return new DatetimeLiteral(
						DateTimeType.Time,
						new DateTime(1, 1, 1, v[0], v[1], v[2], v[3]));
				}
				else
					throw new FormatException("Formát data nebo času není správný");
			}
		}
	}
}
