using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public abstract class TerminalBase : TokenBase
	{
		public TerminalBase(TerminalToken token)
			: base(token)
		{
		}

		public override string ToString ()
		{
			return this.Terminal.Text;
		}

		public string TerminalText
		{
			get { return this.Terminal.Text; }
		}
	}
}
