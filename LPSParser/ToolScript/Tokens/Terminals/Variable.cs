using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public sealed class Variable : TerminalBase, IVariable
	{
		public Variable(TerminalToken Token)
			: base(Token)
		{
		}

		public object Eval(Context context)
		{
			return context.GetVariable(this.TerminalText);
		}

		public void SetValue(Context context, object val)
		{
			context.SetVariable(this.TerminalText, val);
		}
	}
}
