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

		protected override string GetDBTypeName ()
		{
			return String.Format("decimal({0},{1})", Precision, Scale);
		}

	}
}
