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
		#region Terminals
        /// <summary>
        /// <para>Symbol: EOF</para>
        /// <para><c>(EOF)</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalEof(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Error</para>
        /// <para><c>(Error)</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalError(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Whitespace</para>
        /// <para><c>(Whitespace)</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalWhitespace(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Comment End</para>
        /// <para><c>(Comment End)</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalCommentend(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Comment Line</para>
        /// <para><c>(Comment Line)</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalCommentline(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Comment Start</para>
        /// <para><c>(Comment Start)</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalCommentstart(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: -</para>
        /// <para><c>'-'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalMinus(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: ,</para>
        /// <para><c>','</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalComma(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: { return null; }</para>
        /// <para><c>'{ return null; }'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalSemi(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: :</para>
        /// <para><c>':'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalColon(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: !=</para>
        /// <para><c>'!='</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalExclameq(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: ?</para>
        /// <para><c>'?'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalQuestion(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: .</para>
        /// <para><c>'.'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalDot(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: (</para>
        /// <para><c>'('</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalLparan(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: )</para>
        /// <para><c>')'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalRparan(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: [</para>
        /// <para><c>'['</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalLbracket(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: ]</para>
        /// <para><c>']'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalRbracket(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: {</para>
        /// <para><c>'{'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalLbrace(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: }</para>
        /// <para><c>'}'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalRbrace(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: *</para>
        /// <para><c>'*'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalTimes(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: /</para>
        /// <para><c>'/'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalDiv(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: %</para>
        /// <para><c>'%'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalPercent(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: +</para>
        /// <para><c>'+'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalPlus(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: &lt{ return null; }</para>
        /// <para><c>'&lt{ return null; }'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalLt(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: &lt{ return null; }=</para>
        /// <para><c>'&lt{ return null; }='</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalLteq(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: =</para>
        /// <para><c>'='</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalEq(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: ==</para>
        /// <para><c>'=='</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalEqeq(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: &gt{ return null; }</para>
        /// <para><c>'&gt{ return null; }'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalGt(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: -&gt{ return null; }</para>
        /// <para><c>'-&gt{ return null; }'</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalMinusgt(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: &gt{ return null; }=</para>
        /// <para><c>'&gt{ return null; }='</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalGteq(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: and</para>
        /// <para><c>and</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalAnd(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: as</para>
        /// <para><c>as</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalAs(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: break</para>
        /// <para><c>break</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalBreak(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: case</para>
        /// <para><c>case</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalCase(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: cast</para>
        /// <para><c>cast</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalCast(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: continue</para>
        /// <para><c>continue</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalContinue(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: DecimalLiteral</para>
        /// <para><c>DecimalLiteral</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalDecimalliteral(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: default</para>
        /// <para><c>default</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalDefault(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: do</para>
        /// <para><c>do</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalDo(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: else</para>
        /// <para><c>else</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalElse(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: for</para>
        /// <para><c>for</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalFor(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: foreach</para>
        /// <para><c>foreach</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalForeach(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: ID</para>
        /// <para><c>ID</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalId(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: if</para>
        /// <para><c>if</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalIf(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: in</para>
        /// <para><c>in</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalIn(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: IntLiteral</para>
        /// <para><c>IntLiteral</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalIntliteral(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: not</para>
        /// <para><c>not</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalNot(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: or</para>
        /// <para><c>or</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalOr(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: return</para>
        /// <para><c>return</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalReturn(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: StringLiteral</para>
        /// <para><c>StringLiteral</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalStringliteral(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: switch</para>
        /// <para><c>switch</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalSwitch(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: type</para>
        /// <para><c>type</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalType(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: while</para>
        /// <para><c>while</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalWhile(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Args</para>
        /// <para><c>&lt{ return null; }Args&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalArgs(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Block</para>
        /// <para><c>&lt{ return null; }Block&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalBlock(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Case Stms</para>
        /// <para><c>&lt{ return null; }Case Stms&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalCasestms(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Expr</para>
        /// <para><c>&lt{ return null; }Expr&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalExpr(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Normal Stm</para>
        /// <para><c>&lt{ return null; }Normal Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalNormalstm(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Op Add</para>
        /// <para><c>&lt{ return null; }Op Add&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalOpadd(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Op And</para>
        /// <para><c>&lt{ return null; }Op And&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalOpand(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Op Compare</para>
        /// <para><c>&lt{ return null; }Op Compare&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalOpcompare(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Op Equate</para>
        /// <para><c>&lt{ return null; }Op Equate&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalOpequate(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Op If</para>
        /// <para><c>&lt{ return null; }Op If&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalOpif(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Op Mult</para>
        /// <para><c>&lt{ return null; }Op Mult&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalOpmult(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Op Or</para>
        /// <para><c>&lt{ return null; }Op Or&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalOpor(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Op Pointer</para>
        /// <para><c>&lt{ return null; }Op Pointer&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalOppointer(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Op Unary</para>
        /// <para><c>&lt{ return null; }Op Unary&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalOpunary(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Stm</para>
        /// <para><c>&lt{ return null; }Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalStm(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Stm List</para>
        /// <para><c>&lt{ return null; }Stm List&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalStmlist(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Then Stm</para>
        /// <para><c>&lt{ return null; }Then Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalThenstm(TerminalToken token){ return null; }
        /// <summary>
        /// <para>Symbol: Value</para>
        /// <para><c>&lt{ return null; }Value&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateTerminalValue(TerminalToken token){ return null; }
		#endregion

		// ==============================================================

		#region Nonterminals
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Stm&gt{ return null; } ::= if '(' &lt{ return null; }Expr&gt{ return null; } ')' &lt{ return null; }Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalStmIfLparanRparan(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Stm&gt{ return null; } ::= if '(' &lt{ return null; }Expr&gt{ return null; } ')' &lt{ return null; }Then Stm&gt{ return null; } else &lt{ return null; }Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalStmIfLparanRparanElse(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Stm&gt{ return null; } ::= while '(' &lt{ return null; }Expr&gt{ return null; } ')' &lt{ return null; }Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalStmWhileLparanRparan(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Stm&gt{ return null; } ::= for '(' &lt{ return null; }Expr&gt{ return null; } '{ return null; }' &lt{ return null; }Expr&gt{ return null; } '{ return null; }' &lt{ return null; }Expr&gt{ return null; } ')' &lt{ return null; }Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalStmForLparanSemiSemiRparan(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Stm&gt{ return null; } ::= foreach '(' ID in &lt{ return null; }Expr&gt{ return null; } ')' &lt{ return null; }Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalStmForeachLparanIdInRparan(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Stm&gt{ return null; } ::= &lt{ return null; }Normal Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalStm(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Then Stm&gt{ return null; } ::= if '(' &lt{ return null; }Expr&gt{ return null; } ')' &lt{ return null; }Then Stm&gt{ return null; } else &lt{ return null; }Then Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalThenstmIfLparanRparanElse(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Then Stm&gt{ return null; } ::= while '(' &lt{ return null; }Expr&gt{ return null; } ')' &lt{ return null; }Then Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalThenstmWhileLparanRparan(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Then Stm&gt{ return null; } ::= for '(' &lt{ return null; }Expr&gt{ return null; } '{ return null; }' &lt{ return null; }Expr&gt{ return null; } '{ return null; }' &lt{ return null; }Expr&gt{ return null; } ')' &lt{ return null; }Then Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalThenstmForLparanSemiSemiRparan(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Then Stm&gt{ return null; } ::= &lt{ return null; }Normal Stm&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalThenstm(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Normal Stm&gt{ return null; } ::= do &lt{ return null; }Stm&gt{ return null; } while '(' &lt{ return null; }Expr&gt{ return null; } ')'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalNormalstmDoWhileLparanRparan(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Normal Stm&gt{ return null; } ::= switch '(' &lt{ return null; }Expr&gt{ return null; } ')' '{' &lt{ return null; }Case Stms&gt{ return null; } '}'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalNormalstmSwitchLparanRparanLbraceRbrace(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Normal Stm&gt{ return null; } ::= &lt{ return null; }Block&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalNormalstm(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Normal Stm&gt{ return null; } ::= &lt{ return null; }Expr&gt{ return null; } '{ return null; }'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalNormalstmSemi(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Normal Stm&gt{ return null; } ::= break '{ return null; }'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalNormalstmBreakSemi(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Normal Stm&gt{ return null; } ::= continue '{ return null; }'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalNormalstmContinueSemi(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Normal Stm&gt{ return null; } ::= return &lt{ return null; }Expr&gt{ return null; } '{ return null; }'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalNormalstmReturnSemi(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Normal Stm&gt{ return null; } ::= '{ return null; }'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalNormalstmSemi2(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Args&gt{ return null; } ::= &lt{ return null; }Expr&gt{ return null; } ',' &lt{ return null; }Args&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalArgsComma(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Args&gt{ return null; } ::= &lt{ return null; }Expr&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalArgs(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Case Stms&gt{ return null; } ::= case &lt{ return null; }Value&gt{ return null; } ':' &lt{ return null; }Stm List&gt{ return null; } &lt{ return null; }Case Stms&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalCasestmsCaseColon(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Case Stms&gt{ return null; } ::= default ':' &lt{ return null; }Stm List&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalCasestmsDefaultColon(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Case Stms&gt{ return null; } ::= </c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalCasestms(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Block&gt{ return null; } ::= '{' &lt{ return null; }Stm List&gt{ return null; } '}'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalBlockLbraceRbrace(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Stm List&gt{ return null; } ::= &lt{ return null; }Stm&gt{ return null; } &lt{ return null; }Stm List&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalStmlist(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Stm List&gt{ return null; } ::= </c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalStmlist2(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Expr&gt{ return null; } ::= &lt{ return null; }Op If&gt{ return null; } '=' &lt{ return null; }Expr&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalExprEq(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Expr&gt{ return null; } ::= &lt{ return null; }Op If&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalExpr(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op If&gt{ return null; } ::= &lt{ return null; }Op Or&gt{ return null; } '?' &lt{ return null; }Op If&gt{ return null; } ':' &lt{ return null; }Op If&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpifQuestionColon(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op If&gt{ return null; } ::= &lt{ return null; }Op Or&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpif(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Or&gt{ return null; } ::= &lt{ return null; }Op Or&gt{ return null; } or &lt{ return null; }Op And&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOporOr(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Or&gt{ return null; } ::= &lt{ return null; }Op And&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpor(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op And&gt{ return null; } ::= &lt{ return null; }Op And&gt{ return null; } and &lt{ return null; }Op Equate&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpandAnd(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op And&gt{ return null; } ::= &lt{ return null; }Op Equate&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpand(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Equate&gt{ return null; } ::= &lt{ return null; }Op Equate&gt{ return null; } '==' &lt{ return null; }Op Compare&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpequateEqeq(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Equate&gt{ return null; } ::= &lt{ return null; }Op Equate&gt{ return null; } '!=' &lt{ return null; }Op Compare&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpequateExclameq(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Equate&gt{ return null; } ::= &lt{ return null; }Op Compare&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpequate(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Compare&gt{ return null; } ::= &lt{ return null; }Op Compare&gt{ return null; } '&lt{ return null; }' &lt{ return null; }Op Add&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpcompareLt(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Compare&gt{ return null; } ::= &lt{ return null; }Op Compare&gt{ return null; } '&gt{ return null; }' &lt{ return null; }Op Add&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpcompareGt(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Compare&gt{ return null; } ::= &lt{ return null; }Op Compare&gt{ return null; } '&lt{ return null; }=' &lt{ return null; }Op Add&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpcompareLteq(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Compare&gt{ return null; } ::= &lt{ return null; }Op Compare&gt{ return null; } '&gt{ return null; }=' &lt{ return null; }Op Add&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpcompareGteq(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Compare&gt{ return null; } ::= &lt{ return null; }Op Add&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpcompare(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Add&gt{ return null; } ::= &lt{ return null; }Op Add&gt{ return null; } '+' &lt{ return null; }Op Mult&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpaddPlus(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Add&gt{ return null; } ::= &lt{ return null; }Op Add&gt{ return null; } '-' &lt{ return null; }Op Mult&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpaddMinus(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Add&gt{ return null; } ::= &lt{ return null; }Op Mult&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpadd(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Mult&gt{ return null; } ::= &lt{ return null; }Op Mult&gt{ return null; } '*' &lt{ return null; }Op Unary&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpmultTimes(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Mult&gt{ return null; } ::= &lt{ return null; }Op Mult&gt{ return null; } '/' &lt{ return null; }Op Unary&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpmultDiv(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Mult&gt{ return null; } ::= &lt{ return null; }Op Mult&gt{ return null; } '%' &lt{ return null; }Op Unary&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpmultPercent(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Mult&gt{ return null; } ::= &lt{ return null; }Op Unary&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpmult(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Unary&gt{ return null; } ::= not &lt{ return null; }Op Unary&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpunaryNot(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Unary&gt{ return null; } ::= '-' &lt{ return null; }Op Unary&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpunaryMinus(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Unary&gt{ return null; } ::= cast &lt{ return null; }Op Unary&gt{ return null; } as ID</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpunaryCastAsId(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Unary&gt{ return null; } ::= &lt{ return null; }Op Pointer&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOpunary(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Pointer&gt{ return null; } ::= &lt{ return null; }Op Pointer&gt{ return null; } '.' &lt{ return null; }Value&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOppointerDot(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Pointer&gt{ return null; } ::= &lt{ return null; }Op Pointer&gt{ return null; } '-&gt{ return null; }' &lt{ return null; }Value&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOppointerMinusgt(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Pointer&gt{ return null; } ::= &lt{ return null; }Op Pointer&gt{ return null; } '[' &lt{ return null; }Expr&gt{ return null; } ']'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOppointerLbracketRbracket(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Op Pointer&gt{ return null; } ::= &lt{ return null; }Value&gt{ return null; }</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalOppointer(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Value&gt{ return null; } ::= IntLiteral</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalValueIntliteral(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Value&gt{ return null; } ::= StringLiteral</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalValueStringliteral(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Value&gt{ return null; } ::= DecimalLiteral</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalValueDecimalliteral(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Value&gt{ return null; } ::= type ID</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalValueTypeId(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Value&gt{ return null; } ::= ID '(' &lt{ return null; }Args&gt{ return null; } ')'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalValueIdLparanRparan(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Value&gt{ return null; } ::= ID '(' ')'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalValueIdLparanRparan2(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Value&gt{ return null; } ::= ID</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalValueId(NonterminalToken token){ return null; }
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt{ return null; }Value&gt{ return null; } ::= '(' &lt{ return null; }Expr&gt{ return null; } ')'</c></para>
        /// </summary>
        protected override TokenBase CreateNonterminalValueLparanRparan(NonterminalToken token){ return null; }
		#endregion
    }
}
