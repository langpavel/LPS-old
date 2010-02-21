using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public sealed class StringLiteral : LiteralBase, IConstantValue
	{
		private string val;
		public StringLiteral(TerminalToken token)
		{
			val = token.Text;
			val = val.Substring(1, val.Length - 2);
		}

		public override object Eval(Context context)
		{
			return val;
		}

		public static explicit operator string(StringLiteral lit)
		{
			return lit.val;
		}

		public override string ToString ()
		{
			return string.Format("\"{0}\"", val.Replace("\"","\\\""));
		}

		public override bool EvalAsBool(Context context)
		{
			throw new Exception("Nelze vyhodnotit string jako boolean");
		}
	}
}
