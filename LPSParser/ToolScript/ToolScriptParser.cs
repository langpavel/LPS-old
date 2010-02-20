using com.calitha.goldparser;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using LPS.ToolScript.Tokens;

namespace LPS.ToolScript
{
    public class ToolScriptParser : ToolScriptParserBase
    {
		protected bool IsRule(NonterminalToken token, params Symbols[] symbols)
		{
			return NonterminalBase.IsRule(token, symbols);
		}

		protected void CheckRule(NonterminalToken token, params Symbols[] symbols)
		{
			if(!IsRule(token, symbols))
				throw new Exception("Nesprávné pravidlo");
		}

		protected override object CreateTerminalStringliteral (TerminalToken token)
		{
			return new StringLiteral(token);
		}

		protected override object CreateTerminalIntliteral (TerminalToken token)
		{
			return new IntLiteral(token);
		}

		protected override object CreateTerminalDecimalliteral (TerminalToken token)
		{
			return new DecimalLiteral(token);
		}

		protected override object CreateNonterminalValueTypeId (NonterminalToken token)
		{
			CheckRule(token, Symbols.Type, Symbols.Id);
			return new TypeLiteral((TerminalToken)token.Tokens[1]);
		}

    }
}
