using System;
using System.Globalization;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public sealed class DecimalLiteral : LiteralBase, IConstantValue
	{
		private Decimal val;
		public DecimalLiteral(Decimal value)
		{
			val = value;
		}

		public DecimalLiteral(TerminalToken token)
		{
			val = Parse(token.Text);
		}

		public static Decimal Parse(string text)
		{
			return Decimal.Parse(text, CultureInfo.InvariantCulture);
		}

		public override void Run(Context context)
		{
		}

		public override object Eval(Context context)
		{
			return val;
		}

		public static explicit operator Decimal(DecimalLiteral lit)
		{
			return lit.val;
		}

		public override string ToString ()
		{
			return string.Format("{0}", val);
		}

		public override bool EvalAsBool(Context context)
		{
			throw new Exception("Nelze vyhodnotit decimal jako boolean");
		}
	}
}
