using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public abstract class NonterminalBase : TokenBase
	{
		public NonterminalBase(NonterminalToken token)
			: base(token)
		{
		}

		protected NonterminalToken GetNT(int index)
		{
			return this.Nonterminal.Tokens[index] as NonterminalToken;
		}

		protected TerminalToken GetT(int index)
		{
			return this.Nonterminal.Tokens[index] as TerminalToken;
		}

		public bool IsRule(params Symbols[] symbols)
		{
			return NonterminalBase.IsRule(this.Nonterminal, symbols);
		}

		public static bool IsRule(NonterminalToken token, params Symbols[] symbols)
		{
			if(symbols.Length != token.Tokens.Length)
				return false;
			for(int i = 0; i < symbols.Length; i++)
			{
				if(symbols[i] != GetSymbol(token.Tokens[i]))
					return false;
			}
			return true;
		}

	}
}
