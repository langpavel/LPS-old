using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public sealed class IntLiteral : LiteralBase, IConstantValue
	{
		private long val;
		public IntLiteral(TerminalToken token)
		{
			val = Int64.Parse(token.Text);
		}

		public IntLiteral(long value)
		{
			val = value;
		}

		public override object Eval(Context context)
		{
			return val;
		}

		public static explicit operator long(IntLiteral lit)
		{
			return lit.val;
		}

		public override string ToString ()
		{
			return string.Format("{0}", val);
		}

		public override bool EvalAsBool(Context context)
		{
			throw new Exception("Nelze vyhodnotit int jako boolean");
		}
	}
}
