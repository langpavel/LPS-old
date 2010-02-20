using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public sealed class TypeLiteral : TerminalBase, IConstantValue
	{
		public TypeLiteral(TerminalToken token)
			:base(token)
		{
		}

		public string Name { get { return TerminalText; } }

		public object Eval(Context context)
		{
			return this;
		}
	}
}
