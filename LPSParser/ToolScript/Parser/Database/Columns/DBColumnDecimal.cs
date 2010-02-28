using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnDecimal : DBColumnBase
	{
		public int Precision { get; set; }
		public int Scale { get; set; }

		public DBColumnDecimal(int Precision, int Scale)
			: base(typeof(Decimal))
		{
			this.Precision = Precision;
			this.Scale = Scale;
		}

	}
}
