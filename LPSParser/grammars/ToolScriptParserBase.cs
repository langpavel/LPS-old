using com.calitha.commons;
using com.calitha.goldparser;
using com.calitha.goldparser.lalr;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using LPS.ToolScript.Tokens;

namespace LPS.ToolScript
{
    public abstract class ToolScriptParserBase
    {
        private LALRParser parser;

        public ToolScriptParserBase()
        {
        	using(Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("toolscript.cgt"))
        		Init(s);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public virtual TokenBase Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token == null)
	            return null;
            TokenBase obj = CreateObject(token);
            return obj;
        }

        private TokenBase CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        /// <summary>
        /// <para>Symbol: EOF</para>
        /// <para><c>(EOF)</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalEof(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Error</para>
        /// <para><c>(Error)</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalError(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Whitespace</para>
        /// <para><c>(Whitespace)</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalWhitespace(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Comment End</para>
        /// <para><c>(Comment End)</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalCommentend(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Comment Line</para>
        /// <para><c>(Comment Line)</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalCommentline(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Comment Start</para>
        /// <para><c>(Comment Start)</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalCommentstart(TerminalToken token);
        /// <summary>
        /// <para>Symbol: -</para>
        /// <para><c>'-'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalMinus(TerminalToken token);
        /// <summary>
        /// <para>Symbol: ,</para>
        /// <para><c>','</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalComma(TerminalToken token);
        /// <summary>
        /// <para>Symbol: ;</para>
        /// <para><c>';'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalSemi(TerminalToken token);
        /// <summary>
        /// <para>Symbol: :</para>
        /// <para><c>':'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalColon(TerminalToken token);
        /// <summary>
        /// <para>Symbol: !=</para>
        /// <para><c>'!='</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalExclameq(TerminalToken token);
        /// <summary>
        /// <para>Symbol: ?</para>
        /// <para><c>'?'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalQuestion(TerminalToken token);
        /// <summary>
        /// <para>Symbol: .</para>
        /// <para><c>'.'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalDot(TerminalToken token);
        /// <summary>
        /// <para>Symbol: (</para>
        /// <para><c>'('</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalLparan(TerminalToken token);
        /// <summary>
        /// <para>Symbol: )</para>
        /// <para><c>')'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalRparan(TerminalToken token);
        /// <summary>
        /// <para>Symbol: [</para>
        /// <para><c>'['</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalLbracket(TerminalToken token);
        /// <summary>
        /// <para>Symbol: ]</para>
        /// <para><c>']'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalRbracket(TerminalToken token);
        /// <summary>
        /// <para>Symbol: {</para>
        /// <para><c>'{'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalLbrace(TerminalToken token);
        /// <summary>
        /// <para>Symbol: }</para>
        /// <para><c>'}'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalRbrace(TerminalToken token);
        /// <summary>
        /// <para>Symbol: *</para>
        /// <para><c>'*'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalTimes(TerminalToken token);
        /// <summary>
        /// <para>Symbol: /</para>
        /// <para><c>'/'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalDiv(TerminalToken token);
        /// <summary>
        /// <para>Symbol: %</para>
        /// <para><c>'%'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalPercent(TerminalToken token);
        /// <summary>
        /// <para>Symbol: +</para>
        /// <para><c>'+'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalPlus(TerminalToken token);
        /// <summary>
        /// <para>Symbol: &lt;</para>
        /// <para><c>'&lt;'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalLt(TerminalToken token);
        /// <summary>
        /// <para>Symbol: &lt;=</para>
        /// <para><c>'&lt;='</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalLteq(TerminalToken token);
        /// <summary>
        /// <para>Symbol: =</para>
        /// <para><c>'='</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalEq(TerminalToken token);
        /// <summary>
        /// <para>Symbol: ==</para>
        /// <para><c>'=='</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalEqeq(TerminalToken token);
        /// <summary>
        /// <para>Symbol: &gt;</para>
        /// <para><c>'&gt;'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalGt(TerminalToken token);
        /// <summary>
        /// <para>Symbol: -&gt;</para>
        /// <para><c>'-&gt;'</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalMinusgt(TerminalToken token);
        /// <summary>
        /// <para>Symbol: &gt;=</para>
        /// <para><c>'&gt;='</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalGteq(TerminalToken token);
        /// <summary>
        /// <para>Symbol: and</para>
        /// <para><c>and</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalAnd(TerminalToken token);
        /// <summary>
        /// <para>Symbol: as</para>
        /// <para><c>as</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalAs(TerminalToken token);
        /// <summary>
        /// <para>Symbol: break</para>
        /// <para><c>break</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalBreak(TerminalToken token);
        /// <summary>
        /// <para>Symbol: case</para>
        /// <para><c>case</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalCase(TerminalToken token);
        /// <summary>
        /// <para>Symbol: cast</para>
        /// <para><c>cast</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalCast(TerminalToken token);
        /// <summary>
        /// <para>Symbol: continue</para>
        /// <para><c>continue</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalContinue(TerminalToken token);
        /// <summary>
        /// <para>Symbol: DecimalLiteral</para>
        /// <para><c>DecimalLiteral</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalDecimalliteral(TerminalToken token);
        /// <summary>
        /// <para>Symbol: default</para>
        /// <para><c>default</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalDefault(TerminalToken token);
        /// <summary>
        /// <para>Symbol: do</para>
        /// <para><c>do</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalDo(TerminalToken token);
        /// <summary>
        /// <para>Symbol: else</para>
        /// <para><c>else</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalElse(TerminalToken token);
        /// <summary>
        /// <para>Symbol: for</para>
        /// <para><c>for</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalFor(TerminalToken token);
        /// <summary>
        /// <para>Symbol: foreach</para>
        /// <para><c>foreach</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalForeach(TerminalToken token);
        /// <summary>
        /// <para>Symbol: ID</para>
        /// <para><c>ID</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalId(TerminalToken token);
        /// <summary>
        /// <para>Symbol: if</para>
        /// <para><c>if</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalIf(TerminalToken token);
        /// <summary>
        /// <para>Symbol: in</para>
        /// <para><c>in</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalIn(TerminalToken token);
        /// <summary>
        /// <para>Symbol: IntLiteral</para>
        /// <para><c>IntLiteral</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalIntliteral(TerminalToken token);
        /// <summary>
        /// <para>Symbol: not</para>
        /// <para><c>not</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalNot(TerminalToken token);
        /// <summary>
        /// <para>Symbol: or</para>
        /// <para><c>or</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalOr(TerminalToken token);
        /// <summary>
        /// <para>Symbol: return</para>
        /// <para><c>return</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalReturn(TerminalToken token);
        /// <summary>
        /// <para>Symbol: StringLiteral</para>
        /// <para><c>StringLiteral</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalStringliteral(TerminalToken token);
        /// <summary>
        /// <para>Symbol: switch</para>
        /// <para><c>switch</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalSwitch(TerminalToken token);
        /// <summary>
        /// <para>Symbol: type</para>
        /// <para><c>type</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalType(TerminalToken token);
        /// <summary>
        /// <para>Symbol: while</para>
        /// <para><c>while</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalWhile(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Args</para>
        /// <para><c>&lt;Args&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalArgs(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Block</para>
        /// <para><c>&lt;Block&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalBlock(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Case Stms</para>
        /// <para><c>&lt;Case Stms&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalCasestms(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Expr</para>
        /// <para><c>&lt;Expr&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalExpr(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Normal Stm</para>
        /// <para><c>&lt;Normal Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalNormalstm(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Op Add</para>
        /// <para><c>&lt;Op Add&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalOpadd(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Op And</para>
        /// <para><c>&lt;Op And&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalOpand(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Op Compare</para>
        /// <para><c>&lt;Op Compare&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalOpcompare(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Op Equate</para>
        /// <para><c>&lt;Op Equate&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalOpequate(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Op If</para>
        /// <para><c>&lt;Op If&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalOpif(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Op Mult</para>
        /// <para><c>&lt;Op Mult&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalOpmult(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Op Or</para>
        /// <para><c>&lt;Op Or&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalOpor(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Op Pointer</para>
        /// <para><c>&lt;Op Pointer&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalOppointer(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Op Unary</para>
        /// <para><c>&lt;Op Unary&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalOpunary(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Stm</para>
        /// <para><c>&lt;Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalStm(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Stm List</para>
        /// <para><c>&lt;Stm List&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalStmlist(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Then Stm</para>
        /// <para><c>&lt;Then Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalThenstm(TerminalToken token);
        /// <summary>
        /// <para>Symbol: Value</para>
        /// <para><c>&lt;Value&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateTerminalValue(TerminalToken token);

        private TokenBase CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.Eof: //(EOF)
                	return CreateTerminalEof(token);

                case (int)SymbolConstants.Error: //(Error)
                	return CreateTerminalError(token);

                case (int)SymbolConstants.Whitespace: //(Whitespace)
                	return CreateTerminalWhitespace(token);

                case (int)SymbolConstants.Commentend: //(Comment End)
                	return CreateTerminalCommentend(token);

                case (int)SymbolConstants.Commentline: //(Comment Line)
                	return CreateTerminalCommentline(token);

                case (int)SymbolConstants.Commentstart: //(Comment Start)
                	return CreateTerminalCommentstart(token);

                case (int)SymbolConstants.Minus: //'-'
                	return CreateTerminalMinus(token);

                case (int)SymbolConstants.Comma: //','
                	return CreateTerminalComma(token);

                case (int)SymbolConstants.Semi: //';'
                	return CreateTerminalSemi(token);

                case (int)SymbolConstants.Colon: //':'
                	return CreateTerminalColon(token);

                case (int)SymbolConstants.Exclameq: //'!='
                	return CreateTerminalExclameq(token);

                case (int)SymbolConstants.Question: //'?'
                	return CreateTerminalQuestion(token);

                case (int)SymbolConstants.Dot: //'.'
                	return CreateTerminalDot(token);

                case (int)SymbolConstants.Lparan: //'('
                	return CreateTerminalLparan(token);

                case (int)SymbolConstants.Rparan: //')'
                	return CreateTerminalRparan(token);

                case (int)SymbolConstants.Lbracket: //'['
                	return CreateTerminalLbracket(token);

                case (int)SymbolConstants.Rbracket: //']'
                	return CreateTerminalRbracket(token);

                case (int)SymbolConstants.Lbrace: //'{'
                	return CreateTerminalLbrace(token);

                case (int)SymbolConstants.Rbrace: //'}'
                	return CreateTerminalRbrace(token);

                case (int)SymbolConstants.Times: //'*'
                	return CreateTerminalTimes(token);

                case (int)SymbolConstants.Div: //'/'
                	return CreateTerminalDiv(token);

                case (int)SymbolConstants.Percent: //'%'
                	return CreateTerminalPercent(token);

                case (int)SymbolConstants.Plus: //'+'
                	return CreateTerminalPlus(token);

                case (int)SymbolConstants.Lt: //'<'
                	return CreateTerminalLt(token);

                case (int)SymbolConstants.Lteq: //'<='
                	return CreateTerminalLteq(token);

                case (int)SymbolConstants.Eq: //'='
                	return CreateTerminalEq(token);

                case (int)SymbolConstants.Eqeq: //'=='
                	return CreateTerminalEqeq(token);

                case (int)SymbolConstants.Gt: //'>'
                	return CreateTerminalGt(token);

                case (int)SymbolConstants.Minusgt: //'->'
                	return CreateTerminalMinusgt(token);

                case (int)SymbolConstants.Gteq: //'>='
                	return CreateTerminalGteq(token);

                case (int)SymbolConstants.And: //and
                	return CreateTerminalAnd(token);

                case (int)SymbolConstants.As: //as
                	return CreateTerminalAs(token);

                case (int)SymbolConstants.Break: //break
                	return CreateTerminalBreak(token);

                case (int)SymbolConstants.Case: //case
                	return CreateTerminalCase(token);

                case (int)SymbolConstants.Cast: //cast
                	return CreateTerminalCast(token);

                case (int)SymbolConstants.Continue: //continue
                	return CreateTerminalContinue(token);

                case (int)SymbolConstants.Decimalliteral: //DecimalLiteral
                	return CreateTerminalDecimalliteral(token);

                case (int)SymbolConstants.Default: //default
                	return CreateTerminalDefault(token);

                case (int)SymbolConstants.Do: //do
                	return CreateTerminalDo(token);

                case (int)SymbolConstants.Else: //else
                	return CreateTerminalElse(token);

                case (int)SymbolConstants.For: //for
                	return CreateTerminalFor(token);

                case (int)SymbolConstants.Foreach: //foreach
                	return CreateTerminalForeach(token);

                case (int)SymbolConstants.Id: //ID
                	return CreateTerminalId(token);

                case (int)SymbolConstants.If: //if
                	return CreateTerminalIf(token);

                case (int)SymbolConstants.In: //in
                	return CreateTerminalIn(token);

                case (int)SymbolConstants.Intliteral: //IntLiteral
                	return CreateTerminalIntliteral(token);

                case (int)SymbolConstants.Not: //not
                	return CreateTerminalNot(token);

                case (int)SymbolConstants.Or: //or
                	return CreateTerminalOr(token);

                case (int)SymbolConstants.Return: //return
                	return CreateTerminalReturn(token);

                case (int)SymbolConstants.Stringliteral: //StringLiteral
                	return CreateTerminalStringliteral(token);

                case (int)SymbolConstants.Switch: //switch
                	return CreateTerminalSwitch(token);

                case (int)SymbolConstants.Type: //type
                	return CreateTerminalType(token);

                case (int)SymbolConstants.While: //while
                	return CreateTerminalWhile(token);

                case (int)SymbolConstants.Args: //<Args>
                	return CreateTerminalArgs(token);

                case (int)SymbolConstants.Block: //<Block>
                	return CreateTerminalBlock(token);

                case (int)SymbolConstants.Casestms: //<Case Stms>
                	return CreateTerminalCasestms(token);

                case (int)SymbolConstants.Expr: //<Expr>
                	return CreateTerminalExpr(token);

                case (int)SymbolConstants.Normalstm: //<Normal Stm>
                	return CreateTerminalNormalstm(token);

                case (int)SymbolConstants.Opadd: //<Op Add>
                	return CreateTerminalOpadd(token);

                case (int)SymbolConstants.Opand: //<Op And>
                	return CreateTerminalOpand(token);

                case (int)SymbolConstants.Opcompare: //<Op Compare>
                	return CreateTerminalOpcompare(token);

                case (int)SymbolConstants.Opequate: //<Op Equate>
                	return CreateTerminalOpequate(token);

                case (int)SymbolConstants.Opif: //<Op If>
                	return CreateTerminalOpif(token);

                case (int)SymbolConstants.Opmult: //<Op Mult>
                	return CreateTerminalOpmult(token);

                case (int)SymbolConstants.Opor: //<Op Or>
                	return CreateTerminalOpor(token);

                case (int)SymbolConstants.Oppointer: //<Op Pointer>
                	return CreateTerminalOppointer(token);

                case (int)SymbolConstants.Opunary: //<Op Unary>
                	return CreateTerminalOpunary(token);

                case (int)SymbolConstants.Stm: //<Stm>
                	return CreateTerminalStm(token);

                case (int)SymbolConstants.Stmlist: //<Stm List>
                	return CreateTerminalStmlist(token);

                case (int)SymbolConstants.Thenstm: //<Then Stm>
                	return CreateTerminalThenstm(token);

                case (int)SymbolConstants.Value: //<Value>
                	return CreateTerminalValue(token);

            }
            throw new SymbolException("Unknown symbol");
        }

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalStmIfLparanRparan(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalStmIfLparanRparanElse(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalStmWhileLparanRparan(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalStmForLparanSemiSemiRparan(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= foreach '(' ID in &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalStmForeachLparanIdInRparan(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalStm(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Then Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalThenstmIfLparanRparanElse(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalThenstmWhileLparanRparan(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalThenstmForLparanSemiSemiRparan(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalThenstm(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= do &lt;Stm&gt; while '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalNormalstmDoWhileLparanRparan(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= switch '(' &lt;Expr&gt; ')' '{' &lt;Case Stms&gt; '}'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalNormalstmSwitchLparanRparanLbraceRbrace(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Block&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalNormalstm(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Expr&gt; ';'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalNormalstmSemi(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= break ';'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalNormalstmBreakSemi(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= continue ';'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalNormalstmContinueSemi(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= return &lt;Expr&gt; ';'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalNormalstmReturnSemi(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= ';'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalNormalstmSemi2(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Expr&gt; ',' &lt;Args&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalArgsComma(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Expr&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalArgs(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= case &lt;Value&gt; ':' &lt;Stm List&gt; &lt;Case Stms&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalCasestmsCaseColon(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= default ':' &lt;Stm List&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalCasestmsDefaultColon(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= </c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalCasestms(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Block&gt; ::= '{' &lt;Stm List&gt; '}'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalBlockLbraceRbrace(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= &lt;Stm&gt; &lt;Stm List&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalStmlist(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= </c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalStmlist2(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '=' &lt;Expr&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalExprEq(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalExpr(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt; '?' &lt;Op If&gt; ':' &lt;Op If&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpifQuestionColon(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpif(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op Or&gt; or &lt;Op And&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOporOr(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op And&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpor(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op And&gt; and &lt;Op Equate&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpandAnd(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op Equate&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpand(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '==' &lt;Op Compare&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpequateEqeq(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '!=' &lt;Op Compare&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpequateExclameq(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Compare&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpequate(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;' &lt;Op Add&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpcompareLt(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;' &lt;Op Add&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpcompareGt(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;=' &lt;Op Add&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpcompareLteq(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;=' &lt;Op Add&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpcompareGteq(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Add&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpcompare(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '+' &lt;Op Mult&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpaddPlus(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '-' &lt;Op Mult&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpaddMinus(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Mult&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpadd(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '*' &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpmultTimes(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '/' &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpmultDiv(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '%' &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpmultPercent(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpmult(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= not &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpunaryNot(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= '-' &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpunaryMinus(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= cast &lt;Op Unary&gt; as ID</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpunaryCastAsId(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOpunary(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '.' &lt;Value&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOppointerDot(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '-&gt;' &lt;Value&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOppointerMinusgt(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '[' &lt;Expr&gt; ']'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOppointerLbracketRbracket(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Value&gt;</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalOppointer(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= IntLiteral</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalValueIntliteral(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= StringLiteral</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalValueStringliteral(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= DecimalLiteral</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalValueDecimalliteral(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= type ID</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalValueTypeId(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID '(' &lt;Args&gt; ')'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalValueIdLparanRparan(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID '(' ')'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalValueIdLparanRparan2(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalValueId(NonterminalToken token);
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        protected abstract TokenBase CreateNonterminalValueLparanRparan(NonterminalToken token);

        public TokenBase CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.StmIfLparanRparan: //<Stm> ::= if '(' <Expr> ')' <Stm>
                	return CreateNonterminalStmIfLparanRparan(token);
                case (int)RuleConstants.StmIfLparanRparanElse: //<Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>
                	return CreateNonterminalStmIfLparanRparanElse(token);
                case (int)RuleConstants.StmWhileLparanRparan: //<Stm> ::= while '(' <Expr> ')' <Stm>
                	return CreateNonterminalStmWhileLparanRparan(token);
                case (int)RuleConstants.StmForLparanSemiSemiRparan: //<Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>
                	return CreateNonterminalStmForLparanSemiSemiRparan(token);
                case (int)RuleConstants.StmForeachLparanIdInRparan: //<Stm> ::= foreach '(' ID in <Expr> ')' <Stm>
                	return CreateNonterminalStmForeachLparanIdInRparan(token);
                case (int)RuleConstants.Stm: //<Stm> ::= <Normal Stm>
                	return CreateNonterminalStm(token);
                case (int)RuleConstants.ThenstmIfLparanRparanElse: //<Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>
                	return CreateNonterminalThenstmIfLparanRparanElse(token);
                case (int)RuleConstants.ThenstmWhileLparanRparan: //<Then Stm> ::= while '(' <Expr> ')' <Then Stm>
                	return CreateNonterminalThenstmWhileLparanRparan(token);
                case (int)RuleConstants.ThenstmForLparanSemiSemiRparan: //<Then Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Then Stm>
                	return CreateNonterminalThenstmForLparanSemiSemiRparan(token);
                case (int)RuleConstants.Thenstm: //<Then Stm> ::= <Normal Stm>
                	return CreateNonterminalThenstm(token);
                case (int)RuleConstants.NormalstmDoWhileLparanRparan: //<Normal Stm> ::= do <Stm> while '(' <Expr> ')'
                	return CreateNonterminalNormalstmDoWhileLparanRparan(token);
                case (int)RuleConstants.NormalstmSwitchLparanRparanLbraceRbrace: //<Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'
                	return CreateNonterminalNormalstmSwitchLparanRparanLbraceRbrace(token);
                case (int)RuleConstants.Normalstm: //<Normal Stm> ::= <Block>
                	return CreateNonterminalNormalstm(token);
                case (int)RuleConstants.NormalstmSemi: //<Normal Stm> ::= <Expr> ';'
                	return CreateNonterminalNormalstmSemi(token);
                case (int)RuleConstants.NormalstmBreakSemi: //<Normal Stm> ::= break ';'
                	return CreateNonterminalNormalstmBreakSemi(token);
                case (int)RuleConstants.NormalstmContinueSemi: //<Normal Stm> ::= continue ';'
                	return CreateNonterminalNormalstmContinueSemi(token);
                case (int)RuleConstants.NormalstmReturnSemi: //<Normal Stm> ::= return <Expr> ';'
                	return CreateNonterminalNormalstmReturnSemi(token);
                case (int)RuleConstants.NormalstmSemi2: //<Normal Stm> ::= ';'
                	return CreateNonterminalNormalstmSemi2(token);
                case (int)RuleConstants.ArgsComma: //<Args> ::= <Expr> ',' <Args>
                	return CreateNonterminalArgsComma(token);
                case (int)RuleConstants.Args: //<Args> ::= <Expr>
                	return CreateNonterminalArgs(token);
                case (int)RuleConstants.CasestmsCaseColon: //<Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>
                	return CreateNonterminalCasestmsCaseColon(token);
                case (int)RuleConstants.CasestmsDefaultColon: //<Case Stms> ::= default ':' <Stm List>
                	return CreateNonterminalCasestmsDefaultColon(token);
                case (int)RuleConstants.Casestms: //<Case Stms> ::= 
                	return CreateNonterminalCasestms(token);
                case (int)RuleConstants.BlockLbraceRbrace: //<Block> ::= '{' <Stm List> '}'
                	return CreateNonterminalBlockLbraceRbrace(token);
                case (int)RuleConstants.Stmlist: //<Stm List> ::= <Stm> <Stm List>
                	return CreateNonterminalStmlist(token);
                case (int)RuleConstants.Stmlist2: //<Stm List> ::= 
                	return CreateNonterminalStmlist2(token);
                case (int)RuleConstants.ExprEq: //<Expr> ::= <Op If> '=' <Expr>
                	return CreateNonterminalExprEq(token);
                case (int)RuleConstants.Expr: //<Expr> ::= <Op If>
                	return CreateNonterminalExpr(token);
                case (int)RuleConstants.OpifQuestionColon: //<Op If> ::= <Op Or> '?' <Op If> ':' <Op If>
                	return CreateNonterminalOpifQuestionColon(token);
                case (int)RuleConstants.Opif: //<Op If> ::= <Op Or>
                	return CreateNonterminalOpif(token);
                case (int)RuleConstants.OporOr: //<Op Or> ::= <Op Or> or <Op And>
                	return CreateNonterminalOporOr(token);
                case (int)RuleConstants.Opor: //<Op Or> ::= <Op And>
                	return CreateNonterminalOpor(token);
                case (int)RuleConstants.OpandAnd: //<Op And> ::= <Op And> and <Op Equate>
                	return CreateNonterminalOpandAnd(token);
                case (int)RuleConstants.Opand: //<Op And> ::= <Op Equate>
                	return CreateNonterminalOpand(token);
                case (int)RuleConstants.OpequateEqeq: //<Op Equate> ::= <Op Equate> '==' <Op Compare>
                	return CreateNonterminalOpequateEqeq(token);
                case (int)RuleConstants.OpequateExclameq: //<Op Equate> ::= <Op Equate> '!=' <Op Compare>
                	return CreateNonterminalOpequateExclameq(token);
                case (int)RuleConstants.Opequate: //<Op Equate> ::= <Op Compare>
                	return CreateNonterminalOpequate(token);
                case (int)RuleConstants.OpcompareLt: //<Op Compare> ::= <Op Compare> '<' <Op Add>
                	return CreateNonterminalOpcompareLt(token);
                case (int)RuleConstants.OpcompareGt: //<Op Compare> ::= <Op Compare> '>' <Op Add>
                	return CreateNonterminalOpcompareGt(token);
                case (int)RuleConstants.OpcompareLteq: //<Op Compare> ::= <Op Compare> '<=' <Op Add>
                	return CreateNonterminalOpcompareLteq(token);
                case (int)RuleConstants.OpcompareGteq: //<Op Compare> ::= <Op Compare> '>=' <Op Add>
                	return CreateNonterminalOpcompareGteq(token);
                case (int)RuleConstants.Opcompare: //<Op Compare> ::= <Op Add>
                	return CreateNonterminalOpcompare(token);
                case (int)RuleConstants.OpaddPlus: //<Op Add> ::= <Op Add> '+' <Op Mult>
                	return CreateNonterminalOpaddPlus(token);
                case (int)RuleConstants.OpaddMinus: //<Op Add> ::= <Op Add> '-' <Op Mult>
                	return CreateNonterminalOpaddMinus(token);
                case (int)RuleConstants.Opadd: //<Op Add> ::= <Op Mult>
                	return CreateNonterminalOpadd(token);
                case (int)RuleConstants.OpmultTimes: //<Op Mult> ::= <Op Mult> '*' <Op Unary>
                	return CreateNonterminalOpmultTimes(token);
                case (int)RuleConstants.OpmultDiv: //<Op Mult> ::= <Op Mult> '/' <Op Unary>
                	return CreateNonterminalOpmultDiv(token);
                case (int)RuleConstants.OpmultPercent: //<Op Mult> ::= <Op Mult> '%' <Op Unary>
                	return CreateNonterminalOpmultPercent(token);
                case (int)RuleConstants.Opmult: //<Op Mult> ::= <Op Unary>
                	return CreateNonterminalOpmult(token);
                case (int)RuleConstants.OpunaryNot: //<Op Unary> ::= not <Op Unary>
                	return CreateNonterminalOpunaryNot(token);
                case (int)RuleConstants.OpunaryMinus: //<Op Unary> ::= '-' <Op Unary>
                	return CreateNonterminalOpunaryMinus(token);
                case (int)RuleConstants.OpunaryCastAsId: //<Op Unary> ::= cast <Op Unary> as ID
                	return CreateNonterminalOpunaryCastAsId(token);
                case (int)RuleConstants.Opunary: //<Op Unary> ::= <Op Pointer>
                	return CreateNonterminalOpunary(token);
                case (int)RuleConstants.OppointerDot: //<Op Pointer> ::= <Op Pointer> '.' <Value>
                	return CreateNonterminalOppointerDot(token);
                case (int)RuleConstants.OppointerMinusgt: //<Op Pointer> ::= <Op Pointer> '->' <Value>
                	return CreateNonterminalOppointerMinusgt(token);
                case (int)RuleConstants.OppointerLbracketRbracket: //<Op Pointer> ::= <Op Pointer> '[' <Expr> ']'
                	return CreateNonterminalOppointerLbracketRbracket(token);
                case (int)RuleConstants.Oppointer: //<Op Pointer> ::= <Value>
                	return CreateNonterminalOppointer(token);
                case (int)RuleConstants.ValueIntliteral: //<Value> ::= IntLiteral
                	return CreateNonterminalValueIntliteral(token);
                case (int)RuleConstants.ValueStringliteral: //<Value> ::= StringLiteral
                	return CreateNonterminalValueStringliteral(token);
                case (int)RuleConstants.ValueDecimalliteral: //<Value> ::= DecimalLiteral
                	return CreateNonterminalValueDecimalliteral(token);
                case (int)RuleConstants.ValueTypeId: //<Value> ::= type ID
                	return CreateNonterminalValueTypeId(token);
                case (int)RuleConstants.ValueIdLparanRparan: //<Value> ::= ID '(' <Args> ')'
                	return CreateNonterminalValueIdLparanRparan(token);
                case (int)RuleConstants.ValueIdLparanRparan2: //<Value> ::= ID '(' ')'
                	return CreateNonterminalValueIdLparanRparan2(token);
                case (int)RuleConstants.ValueId: //<Value> ::= ID
                	return CreateNonterminalValueId(token);
                case (int)RuleConstants.ValueLparanRparan: //<Value> ::= '(' <Expr> ')'
                	return CreateNonterminalValueLparanRparan(token);
            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            Log.Error(message);
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            Log.Error(message);
        }

    }
}
