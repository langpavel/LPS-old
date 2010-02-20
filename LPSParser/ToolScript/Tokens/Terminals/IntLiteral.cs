using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public sealed class IntLiteral : TerminalBase, IConstantValue
	{
		private long val;
		public IntLiteral(TerminalToken token)
			:base(token)
		{
			val = Int64.Parse(this.TerminalText);
		}

		public object Eval(Context context)
		{
			return val;
		}
	}
}
