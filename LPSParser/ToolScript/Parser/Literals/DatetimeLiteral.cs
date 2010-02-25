using System;
using System.Globalization;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public sealed class DatetimeLiteral : LiteralBase, IConstantValue
	{
		private DateTime val;
		public DatetimeLiteral(DateTime value)
		{
			val = value;
		}

		public override void Run(Context context)
		{
		}

		public override object Eval(Context context)
		{
			return val;
		}

		public static explicit operator DateTime(DatetimeLiteral lit)
		{
			return lit.val;
		}

		public override string ToString ()
		{
			return val.ToString("'D'yyyy-MM-dd'T'hh:mm:ss.fff");
		}

		public override bool EvalAsBool(Context context)
		{
			throw new Exception("Nelze vyhodnotit datum jako boolean");
		}

		public static DateTime Parse(string text)
		{
			string[] bits = text.Split(new char[] {
				'D', 'd', 'T', 't', '-', ':', '.'}, StringSplitOptions.RemoveEmptyEntries);
			int[] v = new int[7];
			for(int i = 0; i < 7; i++)
				v[i] = (i < bits.Length) ? Int32.Parse(bits[i]) : 0;
			return new DateTime(v[0], v[1], v[2], v[3], v[4], v[5], v[6]);
		}
	}
}
