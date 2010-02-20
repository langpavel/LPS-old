using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public sealed class StringLiteral : TerminalBase, IConstantValue
	{
		private string val;
		public StringLiteral(TerminalToken token)
			:base(token)
		{
			val = TerminalText;
			val = val.Substring(1, val.Length - 2);
		}

		public object Eval(Context context)
		{
			return val;
		}
	}
}
