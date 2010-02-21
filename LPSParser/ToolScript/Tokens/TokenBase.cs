using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public abstract class TokenBase : IStatement
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

		public static Symbols GetSymbol(Token tok)
		{
			if(tok is TerminalToken)
				return (Symbols)(((TerminalToken)tok).Symbol.Id);
			else
				return (Symbols)(((NonterminalToken)tok).Rule.Id + ToolScriptParserBase.RulesOffset);
		}

		public override string ToString ()
		{
			return this.Token.ToString();
		}

		public virtual void Run(Context context)
		{
			IExpression expr = this as IExpression;
			if(expr != null)
				expr.Eval(context);
		}
	}
}
