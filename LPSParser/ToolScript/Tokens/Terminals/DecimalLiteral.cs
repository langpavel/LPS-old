using System;
using com.calitha.goldparser;
using System.Globalization;

namespace LPS.ToolScript.Tokens
{
	public sealed class DecimalLiteral : TerminalBase, IConstantValue
	{
		private Decimal val;
		public DecimalLiteral(TerminalToken token)
			:base(token)
		{
			val = Decimal.Parse(this.TerminalText, CultureInfo.InvariantCulture);
		}

		public object Eval(Context context)
		{
			return val;
		}
	}
}
