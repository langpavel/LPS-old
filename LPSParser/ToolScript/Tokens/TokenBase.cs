using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public abstract class TokenBase
	{
		public TokenBase(Token token)
		{
			this.Token = token;
			token.UserObject = this;
		}

		public virtual bool IsTerminal { get { return this.Token is TerminalToken; } }
		public virtual bool IsNonTerminal { get { return this.Token is NonterminalToken; } }

		public Token Token { get; private set; }
		public TerminalToken Terminal { get { return this.Token as TerminalToken; } }
		public NonterminalToken Nonterminal { get { return this.Token as NonterminalToken; } }

		public virtual void Prepare(Context c)
		{
		}

		public virtual object Execute()
		{
			return null;
		}

		public override string ToString ()
		{
			return this.Token.ToString();
		}
	}
}
