using System;
using System.Globalization;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public sealed class TimeSpanLiteral : LiteralBase, IConstantValue
	{
		private TimeSpan val;
		public TimeSpanLiteral(TimeSpan value)
		{
			val = value;
		}

		public override void Run(IExecutionContext context)
		{
		}

		public override object Eval(IExecutionContext context)
		{
			return val;
		}

		public static explicit operator TimeSpan(TimeSpanLiteral lit)
		{
			return lit.val;
		}

		public override string ToString ()
		{
			return val.ToString();
		}

		public override bool EvalAsBool(IExecutionContext context)
		{
			throw new Exception("Nelze vyhodnotit datum jako boolean");
		}

		public static TimeSpan Parse(string text)
		{
			int days = 0;
			int hours = 0;
			int minutes = 0;
			int seconds = 0;
			int milis = 0;
			bool minus = false;

			int idx = 0;
			text = text.ToLower();
			if(text.StartsWith("-"))
			{
				minus = true;
				text = text.Substring(1);
			}
			if((idx = text.IndexOf('d')) >= 0)
			{
				days = Int32.Parse(text.Substring(0, idx));
				text = text.Substring(idx + 1);
			}
			if((idx = text.IndexOf('h')) >= 0)
			{
				hours = Int32.Parse(text.Substring(0, idx));
				text = text.Substring(idx + 1);
			}
			if((idx = text.IndexOf('m')) >= 0)
			{
				minutes = Int32.Parse(text.Substring(0, idx));
				text = text.Substring(idx + 1);
			}
			if((idx = text.IndexOf('s')) >= 0)
			{
				string secs = text.Substring(0, idx);
				text = text.Substring(idx + 1);
				if((idx = secs.IndexOf('.')) >= 0)
				{
					seconds = Int32.Parse(secs.Substring(0, idx));
					milis = Int32.Parse(secs.Substring(idx + 1));
				}
				else
				{
					seconds = Int32.Parse(secs);
				}
			}
			if(text != "")
				throw new InvalidCastException("Chyba převodu řetězce na TimeSpan");

			TimeSpan span = new TimeSpan(days, hours, minutes, seconds, milis);
			return minus ? -span : span;
		}
	}
}
