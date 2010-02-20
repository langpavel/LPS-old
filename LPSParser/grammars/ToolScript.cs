using com.calitha.commons;
using com.calitha.goldparser;
using com.calitha.goldparser.lalr;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using LPS.ToolScript.Tokens;

namespace LPS.ToolScript
{

    /// <sumarry>Symboly</summary>
    public enum Symbols : int
    {
		#region Terminaly
        /// <summary>
        /// <para>Symbol: EOF</para>
        /// <para><c>(EOF)</c></para>
        /// </summary>
        Eof = 0,

        /// <summary>
        /// <para>Symbol: Error</para>
        /// <para><c>(Error)</c></para>
        /// </summary>
        Error = 1,

        /// <summary>
        /// <para>Symbol: Whitespace</para>
        /// <para><c>(Whitespace)</c></para>
        /// </summary>
        Whitespace = 2,

        /// <summary>
        /// <para>Symbol: Comment End</para>
        /// <para><c>(Comment End)</c></para>
        /// </summary>
        Commentend = 3,

        /// <summary>
        /// <para>Symbol: Comment Line</para>
        /// <para><c>(Comment Line)</c></para>
        /// </summary>
        Commentline = 4,

        /// <summary>
        /// <para>Symbol: Comment Start</para>
        /// <para><c>(Comment Start)</c></para>
        /// </summary>
        Commentstart = 5,

        /// <summary>
        /// <para>Symbol: -</para>
        /// <para><c>'-'</c></para>
        /// </summary>
        Minus = 6,

        /// <summary>
        /// <para>Symbol: ,</para>
        /// <para><c>','</c></para>
        /// </summary>
        Comma = 7,

        /// <summary>
        /// <para>Symbol: ;</para>
        /// <para><c>';'</c></para>
        /// </summary>
        Semi = 8,

        /// <summary>
        /// <para>Symbol: :</para>
        /// <para><c>':'</c></para>
        /// </summary>
        Colon = 9,

        /// <summary>
        /// <para>Symbol: !=</para>
        /// <para><c>'!='</c></para>
        /// </summary>
        Exclameq = 10,

        /// <summary>
        /// <para>Symbol: ?</para>
        /// <para><c>'?'</c></para>
        /// </summary>
        Question = 11,

        /// <summary>
        /// <para>Symbol: .</para>
        /// <para><c>'.'</c></para>
        /// </summary>
        Dot = 12,

        /// <summary>
        /// <para>Symbol: (</para>
        /// <para><c>'('</c></para>
        /// </summary>
        Lparan = 13,

        /// <summary>
        /// <para>Symbol: )</para>
        /// <para><c>')'</c></para>
        /// </summary>
        Rparan = 14,

        /// <summary>
        /// <para>Symbol: [</para>
        /// <para><c>'['</c></para>
        /// </summary>
        Lbracket = 15,

        /// <summary>
        /// <para>Symbol: ]</para>
        /// <para><c>']'</c></para>
        /// </summary>
        Rbracket = 16,

        /// <summary>
        /// <para>Symbol: {</para>
        /// <para><c>'{'</c></para>
        /// </summary>
        Lbrace = 17,

        /// <summary>
        /// <para>Symbol: }</para>
        /// <para><c>'}'</c></para>
        /// </summary>
        Rbrace = 18,

        /// <summary>
        /// <para>Symbol: *</para>
        /// <para><c>'*'</c></para>
        /// </summary>
        Times = 19,

        /// <summary>
        /// <para>Symbol: /</para>
        /// <para><c>'/'</c></para>
        /// </summary>
        Div = 20,

        /// <summary>
        /// <para>Symbol: %</para>
        /// <para><c>'%'</c></para>
        /// </summary>
        Percent = 21,

        /// <summary>
        /// <para>Symbol: +</para>
        /// <para><c>'+'</c></para>
        /// </summary>
        Plus = 22,

        /// <summary>
        /// <para>Symbol: &lt;</para>
        /// <para><c>'&lt;'</c></para>
        /// </summary>
        Lt = 23,

        /// <summary>
        /// <para>Symbol: &lt;=</para>
        /// <para><c>'&lt;='</c></para>
        /// </summary>
        Lteq = 24,

        /// <summary>
        /// <para>Symbol: =</para>
        /// <para><c>'='</c></para>
        /// </summary>
        Eq = 25,

        /// <summary>
        /// <para>Symbol: ==</para>
        /// <para><c>'=='</c></para>
        /// </summary>
        Eqeq = 26,

        /// <summary>
        /// <para>Symbol: &gt;</para>
        /// <para><c>'&gt;'</c></para>
        /// </summary>
        Gt = 27,

        /// <summary>
        /// <para>Symbol: -&gt;</para>
        /// <para><c>'-&gt;'</c></para>
        /// </summary>
        Minusgt = 28,

        /// <summary>
        /// <para>Symbol: &gt;=</para>
        /// <para><c>'&gt;='</c></para>
        /// </summary>
        Gteq = 29,

        /// <summary>
        /// <para>Symbol: and</para>
        /// <para><c>and</c></para>
        /// </summary>
        And = 30,

        /// <summary>
        /// <para>Symbol: as</para>
        /// <para><c>as</c></para>
        /// </summary>
        As = 31,

        /// <summary>
        /// <para>Symbol: break</para>
        /// <para><c>break</c></para>
        /// </summary>
        Break = 32,

        /// <summary>
        /// <para>Symbol: case</para>
        /// <para><c>case</c></para>
        /// </summary>
        Case = 33,

        /// <summary>
        /// <para>Symbol: cast</para>
        /// <para><c>cast</c></para>
        /// </summary>
        Cast = 34,

        /// <summary>
        /// <para>Symbol: continue</para>
        /// <para><c>continue</c></para>
        /// </summary>
        Continue = 35,

        /// <summary>
        /// <para>Symbol: DecimalLiteral</para>
        /// <para><c>DecimalLiteral</c></para>
        /// </summary>
        Decimalliteral = 36,

        /// <summary>
        /// <para>Symbol: default</para>
        /// <para><c>default</c></para>
        /// </summary>
        Default = 37,

        /// <summary>
        /// <para>Symbol: do</para>
        /// <para><c>do</c></para>
        /// </summary>
        Do = 38,

        /// <summary>
        /// <para>Symbol: else</para>
        /// <para><c>else</c></para>
        /// </summary>
        Else = 39,

        /// <summary>
        /// <para>Symbol: for</para>
        /// <para><c>for</c></para>
        /// </summary>
        For = 40,

        /// <summary>
        /// <para>Symbol: foreach</para>
        /// <para><c>foreach</c></para>
        /// </summary>
        Foreach = 41,

        /// <summary>
        /// <para>Symbol: ID</para>
        /// <para><c>ID</c></para>
        /// </summary>
        Id = 42,

        /// <summary>
        /// <para>Symbol: if</para>
        /// <para><c>if</c></para>
        /// </summary>
        If = 43,

        /// <summary>
        /// <para>Symbol: in</para>
        /// <para><c>in</c></para>
        /// </summary>
        In = 44,

        /// <summary>
        /// <para>Symbol: IntLiteral</para>
        /// <para><c>IntLiteral</c></para>
        /// </summary>
        Intliteral = 45,

        /// <summary>
        /// <para>Symbol: not</para>
        /// <para><c>not</c></para>
        /// </summary>
        Not = 46,

        /// <summary>
        /// <para>Symbol: or</para>
        /// <para><c>or</c></para>
        /// </summary>
        Or = 47,

        /// <summary>
        /// <para>Symbol: return</para>
        /// <para><c>return</c></para>
        /// </summary>
        Return = 48,

        /// <summary>
        /// <para>Symbol: StringLiteral</para>
        /// <para><c>StringLiteral</c></para>
        /// </summary>
        Stringliteral = 49,

        /// <summary>
        /// <para>Symbol: switch</para>
        /// <para><c>switch</c></para>
        /// </summary>
        Switch = 50,

        /// <summary>
        /// <para>Symbol: type</para>
        /// <para><c>type</c></para>
        /// </summary>
        Type = 51,

        /// <summary>
        /// <para>Symbol: while</para>
        /// <para><c>while</c></para>
        /// </summary>
        While = 52,

        /// <summary>
        /// <para>Symbol: Args</para>
        /// <para><c>&lt;Args&gt;</c></para>
        /// </summary>
        Args = 53,

        /// <summary>
        /// <para>Symbol: Block</para>
        /// <para><c>&lt;Block&gt;</c></para>
        /// </summary>
        Block = 54,

        /// <summary>
        /// <para>Symbol: Case Stms</para>
        /// <para><c>&lt;Case Stms&gt;</c></para>
        /// </summary>
        Casestms = 55,

        /// <summary>
        /// <para>Symbol: Expr</para>
        /// <para><c>&lt;Expr&gt;</c></para>
        /// </summary>
        Expr = 56,

        /// <summary>
        /// <para>Symbol: Normal Stm</para>
        /// <para><c>&lt;Normal Stm&gt;</c></para>
        /// </summary>
        Normalstm = 57,

        /// <summary>
        /// <para>Symbol: Op Add</para>
        /// <para><c>&lt;Op Add&gt;</c></para>
        /// </summary>
        Opadd = 58,

        /// <summary>
        /// <para>Symbol: Op And</para>
        /// <para><c>&lt;Op And&gt;</c></para>
        /// </summary>
        Opand = 59,

        /// <summary>
        /// <para>Symbol: Op Compare</para>
        /// <para><c>&lt;Op Compare&gt;</c></para>
        /// </summary>
        Opcompare = 60,

        /// <summary>
        /// <para>Symbol: Op Equate</para>
        /// <para><c>&lt;Op Equate&gt;</c></para>
        /// </summary>
        Opequate = 61,

        /// <summary>
        /// <para>Symbol: Op If</para>
        /// <para><c>&lt;Op If&gt;</c></para>
        /// </summary>
        Opif = 62,

        /// <summary>
        /// <para>Symbol: Op Mult</para>
        /// <para><c>&lt;Op Mult&gt;</c></para>
        /// </summary>
        Opmult = 63,

        /// <summary>
        /// <para>Symbol: Op Or</para>
        /// <para><c>&lt;Op Or&gt;</c></para>
        /// </summary>
        Opor = 64,

        /// <summary>
        /// <para>Symbol: Op Pointer</para>
        /// <para><c>&lt;Op Pointer&gt;</c></para>
        /// </summary>
        Oppointer = 65,

        /// <summary>
        /// <para>Symbol: Op Unary</para>
        /// <para><c>&lt;Op Unary&gt;</c></para>
        /// </summary>
        Opunary = 66,

        /// <summary>
        /// <para>Symbol: Stm</para>
        /// <para><c>&lt;Stm&gt;</c></para>
        /// </summary>
        Stm = 67,

        /// <summary>
        /// <para>Symbol: Stm List</para>
        /// <para><c>&lt;Stm List&gt;</c></para>
        /// </summary>
        Stmlist = 68,

        /// <summary>
        /// <para>Symbol: Then Stm</para>
        /// <para><c>&lt;Then Stm&gt;</c></para>
        /// </summary>
        Thenstm = 69,

        /// <summary>
        /// <para>Symbol: Value</para>
        /// <para><c>&lt;Value&gt;</c></para>
        /// </summary>
        Value = 70,

		#endregion

		#region Neterminaly
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmIfLparanRparan = ToolScriptParserBase.RulesOffset + 0,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmIfLparanRparanElse = ToolScriptParserBase.RulesOffset + 1,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmWhileLparanRparan = ToolScriptParserBase.RulesOffset + 2,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmForLparanSemiSemiRparan = ToolScriptParserBase.RulesOffset + 3,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= foreach '(' ID in &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmForeachLparanIdInRparan = ToolScriptParserBase.RulesOffset + 4,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        RuleStm = ToolScriptParserBase.RulesOffset + 5,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Then Stm&gt;</c></para>
        /// </summary>
        RuleThenstmIfLparanRparanElse = ToolScriptParserBase.RulesOffset + 6,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        RuleThenstmWhileLparanRparan = ToolScriptParserBase.RulesOffset + 7,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        RuleThenstmForLparanSemiSemiRparan = ToolScriptParserBase.RulesOffset + 8,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        RuleThenstm = ToolScriptParserBase.RulesOffset + 9,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= do &lt;Stm&gt; while '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        RuleNormalstmDoWhileLparanRparan = ToolScriptParserBase.RulesOffset + 10,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= switch '(' &lt;Expr&gt; ')' '{' &lt;Case Stms&gt; '}'</c></para>
        /// </summary>
        RuleNormalstmSwitchLparanRparanLbraceRbrace = ToolScriptParserBase.RulesOffset + 11,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Block&gt;</c></para>
        /// </summary>
        RuleNormalstm = ToolScriptParserBase.RulesOffset + 12,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleNormalstmSemi = ToolScriptParserBase.RulesOffset + 13,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= break ';'</c></para>
        /// </summary>
        RuleNormalstmBreakSemi = ToolScriptParserBase.RulesOffset + 14,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= continue ';'</c></para>
        /// </summary>
        RuleNormalstmContinueSemi = ToolScriptParserBase.RulesOffset + 15,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= return &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleNormalstmReturnSemi = ToolScriptParserBase.RulesOffset + 16,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= ';'</c></para>
        /// </summary>
        RuleNormalstmSemi2 = ToolScriptParserBase.RulesOffset + 17,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Expr&gt; ',' &lt;Args&gt;</c></para>
        /// </summary>
        RuleArgsComma = ToolScriptParserBase.RulesOffset + 18,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Expr&gt;</c></para>
        /// </summary>
        RuleArgs = ToolScriptParserBase.RulesOffset + 19,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= case &lt;Value&gt; ':' &lt;Stm List&gt; &lt;Case Stms&gt;</c></para>
        /// </summary>
        RuleCasestmsCaseColon = ToolScriptParserBase.RulesOffset + 20,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= default ':' &lt;Stm List&gt;</c></para>
        /// </summary>
        RuleCasestmsDefaultColon = ToolScriptParserBase.RulesOffset + 21,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= </c></para>
        /// </summary>
        RuleCasestms = ToolScriptParserBase.RulesOffset + 22,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Block&gt; ::= '{' &lt;Stm List&gt; '}'</c></para>
        /// </summary>
        RuleBlockLbraceRbrace = ToolScriptParserBase.RulesOffset + 23,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= &lt;Stm&gt; &lt;Stm List&gt;</c></para>
        /// </summary>
        RuleStmlist = ToolScriptParserBase.RulesOffset + 24,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= </c></para>
        /// </summary>
        RuleStmlist2 = ToolScriptParserBase.RulesOffset + 25,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprEq = ToolScriptParserBase.RulesOffset + 26,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt;</c></para>
        /// </summary>
        RuleExpr = ToolScriptParserBase.RulesOffset + 27,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt; '?' &lt;Op If&gt; ':' &lt;Op If&gt;</c></para>
        /// </summary>
        RuleOpifQuestionColon = ToolScriptParserBase.RulesOffset + 28,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt;</c></para>
        /// </summary>
        RuleOpif = ToolScriptParserBase.RulesOffset + 29,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op Or&gt; or &lt;Op And&gt;</c></para>
        /// </summary>
        RuleOporOr = ToolScriptParserBase.RulesOffset + 30,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op And&gt;</c></para>
        /// </summary>
        RuleOpor = ToolScriptParserBase.RulesOffset + 31,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op And&gt; and &lt;Op Equate&gt;</c></para>
        /// </summary>
        RuleOpandAnd = ToolScriptParserBase.RulesOffset + 32,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op Equate&gt;</c></para>
        /// </summary>
        RuleOpand = ToolScriptParserBase.RulesOffset + 33,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '==' &lt;Op Compare&gt;</c></para>
        /// </summary>
        RuleOpequateEqeq = ToolScriptParserBase.RulesOffset + 34,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '!=' &lt;Op Compare&gt;</c></para>
        /// </summary>
        RuleOpequateExclameq = ToolScriptParserBase.RulesOffset + 35,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Compare&gt;</c></para>
        /// </summary>
        RuleOpequate = ToolScriptParserBase.RulesOffset + 36,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;' &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpcompareLt = ToolScriptParserBase.RulesOffset + 37,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;' &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpcompareGt = ToolScriptParserBase.RulesOffset + 38,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;=' &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpcompareLteq = ToolScriptParserBase.RulesOffset + 39,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;=' &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpcompareGteq = ToolScriptParserBase.RulesOffset + 40,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpcompare = ToolScriptParserBase.RulesOffset + 41,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '+' &lt;Op Mult&gt;</c></para>
        /// </summary>
        RuleOpaddPlus = ToolScriptParserBase.RulesOffset + 42,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '-' &lt;Op Mult&gt;</c></para>
        /// </summary>
        RuleOpaddMinus = ToolScriptParserBase.RulesOffset + 43,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Mult&gt;</c></para>
        /// </summary>
        RuleOpadd = ToolScriptParserBase.RulesOffset + 44,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '*' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmultTimes = ToolScriptParserBase.RulesOffset + 45,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '/' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmultDiv = ToolScriptParserBase.RulesOffset + 46,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '%' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmultPercent = ToolScriptParserBase.RulesOffset + 47,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmult = ToolScriptParserBase.RulesOffset + 48,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= not &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryNot = ToolScriptParserBase.RulesOffset + 49,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= '-' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryMinus = ToolScriptParserBase.RulesOffset + 50,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= cast &lt;Op Unary&gt; as ID</c></para>
        /// </summary>
        RuleOpunaryCastAsId = ToolScriptParserBase.RulesOffset + 51,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt;</c></para>
        /// </summary>
        RuleOpunary = ToolScriptParserBase.RulesOffset + 52,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '.' &lt;Value&gt;</c></para>
        /// </summary>
        RuleOppointerDot = ToolScriptParserBase.RulesOffset + 53,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '-&gt;' &lt;Value&gt;</c></para>
        /// </summary>
        RuleOppointerMinusgt = ToolScriptParserBase.RulesOffset + 54,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '[' &lt;Expr&gt; ']'</c></para>
        /// </summary>
        RuleOppointerLbracketRbracket = ToolScriptParserBase.RulesOffset + 55,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Value&gt;</c></para>
        /// </summary>
        RuleOppointer = ToolScriptParserBase.RulesOffset + 56,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= IntLiteral</c></para>
        /// </summary>
        RuleValueIntliteral = ToolScriptParserBase.RulesOffset + 57,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= StringLiteral</c></para>
        /// </summary>
        RuleValueStringliteral = ToolScriptParserBase.RulesOffset + 58,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= DecimalLiteral</c></para>
        /// </summary>
        RuleValueDecimalliteral = ToolScriptParserBase.RulesOffset + 59,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= type ID</c></para>
        /// </summary>
        RuleValueTypeId = ToolScriptParserBase.RulesOffset + 60,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID '(' &lt;Args&gt; ')'</c></para>
        /// </summary>
        RuleValueIdLparanRparan = ToolScriptParserBase.RulesOffset + 61,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID '(' ')'</c></para>
        /// </summary>
        RuleValueIdLparanRparan2 = ToolScriptParserBase.RulesOffset + 62,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID</c></para>
        /// </summary>
        RuleValueId = ToolScriptParserBase.RulesOffset + 63,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        RuleValueLparanRparan = ToolScriptParserBase.RulesOffset + 64 

    };
    #endregion

	#region ToolScriptParserBase
    public abstract class ToolScriptParserBase
    {
		public const int RulesOffset = 10000;
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

        public virtual object Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token == null)
	            return null;
	        return CreateObject(token);
        }

        protected virtual object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

		#region ToolScriptParserBase.Symboly
        /// <summary>
        /// <para>Symbol: EOF</para>
        /// <para><c>(EOF)</c></para>
        /// </summary>
        protected virtual object CreateTerminalEof(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Error</para>
        /// <para><c>(Error)</c></para>
        /// </summary>
        protected virtual object CreateTerminalError(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Whitespace</para>
        /// <para><c>(Whitespace)</c></para>
        /// </summary>
        protected virtual object CreateTerminalWhitespace(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Comment End</para>
        /// <para><c>(Comment End)</c></para>
        /// </summary>
        protected virtual object CreateTerminalCommentend(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Comment Line</para>
        /// <para><c>(Comment Line)</c></para>
        /// </summary>
        protected virtual object CreateTerminalCommentline(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Comment Start</para>
        /// <para><c>(Comment Start)</c></para>
        /// </summary>
        protected virtual object CreateTerminalCommentstart(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: -</para>
        /// <para><c>'-'</c></para>
        /// </summary>
        protected virtual object CreateTerminalMinus(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: ,</para>
        /// <para><c>','</c></para>
        /// </summary>
        protected virtual object CreateTerminalComma(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: ;</para>
        /// <para><c>';'</c></para>
        /// </summary>
        protected virtual object CreateTerminalSemi(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: :</para>
        /// <para><c>':'</c></para>
        /// </summary>
        protected virtual object CreateTerminalColon(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: !=</para>
        /// <para><c>'!='</c></para>
        /// </summary>
        protected virtual object CreateTerminalExclameq(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: ?</para>
        /// <para><c>'?'</c></para>
        /// </summary>
        protected virtual object CreateTerminalQuestion(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: .</para>
        /// <para><c>'.'</c></para>
        /// </summary>
        protected virtual object CreateTerminalDot(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: (</para>
        /// <para><c>'('</c></para>
        /// </summary>
        protected virtual object CreateTerminalLparan(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: )</para>
        /// <para><c>')'</c></para>
        /// </summary>
        protected virtual object CreateTerminalRparan(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: [</para>
        /// <para><c>'['</c></para>
        /// </summary>
        protected virtual object CreateTerminalLbracket(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: ]</para>
        /// <para><c>']'</c></para>
        /// </summary>
        protected virtual object CreateTerminalRbracket(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: {</para>
        /// <para><c>'{'</c></para>
        /// </summary>
        protected virtual object CreateTerminalLbrace(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: }</para>
        /// <para><c>'}'</c></para>
        /// </summary>
        protected virtual object CreateTerminalRbrace(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: *</para>
        /// <para><c>'*'</c></para>
        /// </summary>
        protected virtual object CreateTerminalTimes(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: /</para>
        /// <para><c>'/'</c></para>
        /// </summary>
        protected virtual object CreateTerminalDiv(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: %</para>
        /// <para><c>'%'</c></para>
        /// </summary>
        protected virtual object CreateTerminalPercent(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: +</para>
        /// <para><c>'+'</c></para>
        /// </summary>
        protected virtual object CreateTerminalPlus(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: &lt;</para>
        /// <para><c>'&lt;'</c></para>
        /// </summary>
        protected virtual object CreateTerminalLt(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: &lt;=</para>
        /// <para><c>'&lt;='</c></para>
        /// </summary>
        protected virtual object CreateTerminalLteq(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: =</para>
        /// <para><c>'='</c></para>
        /// </summary>
        protected virtual object CreateTerminalEq(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: ==</para>
        /// <para><c>'=='</c></para>
        /// </summary>
        protected virtual object CreateTerminalEqeq(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: &gt;</para>
        /// <para><c>'&gt;'</c></para>
        /// </summary>
        protected virtual object CreateTerminalGt(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: -&gt;</para>
        /// <para><c>'-&gt;'</c></para>
        /// </summary>
        protected virtual object CreateTerminalMinusgt(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: &gt;=</para>
        /// <para><c>'&gt;='</c></para>
        /// </summary>
        protected virtual object CreateTerminalGteq(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: and</para>
        /// <para><c>and</c></para>
        /// </summary>
        protected virtual object CreateTerminalAnd(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: as</para>
        /// <para><c>as</c></para>
        /// </summary>
        protected virtual object CreateTerminalAs(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: break</para>
        /// <para><c>break</c></para>
        /// </summary>
        protected virtual object CreateTerminalBreak(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: case</para>
        /// <para><c>case</c></para>
        /// </summary>
        protected virtual object CreateTerminalCase(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: cast</para>
        /// <para><c>cast</c></para>
        /// </summary>
        protected virtual object CreateTerminalCast(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: continue</para>
        /// <para><c>continue</c></para>
        /// </summary>
        protected virtual object CreateTerminalContinue(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: DecimalLiteral</para>
        /// <para><c>DecimalLiteral</c></para>
        /// </summary>
        protected virtual object CreateTerminalDecimalliteral(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: default</para>
        /// <para><c>default</c></para>
        /// </summary>
        protected virtual object CreateTerminalDefault(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: do</para>
        /// <para><c>do</c></para>
        /// </summary>
        protected virtual object CreateTerminalDo(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: else</para>
        /// <para><c>else</c></para>
        /// </summary>
        protected virtual object CreateTerminalElse(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: for</para>
        /// <para><c>for</c></para>
        /// </summary>
        protected virtual object CreateTerminalFor(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: foreach</para>
        /// <para><c>foreach</c></para>
        /// </summary>
        protected virtual object CreateTerminalForeach(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: ID</para>
        /// <para><c>ID</c></para>
        /// </summary>
        protected virtual object CreateTerminalId(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: if</para>
        /// <para><c>if</c></para>
        /// </summary>
        protected virtual object CreateTerminalIf(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: in</para>
        /// <para><c>in</c></para>
        /// </summary>
        protected virtual object CreateTerminalIn(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: IntLiteral</para>
        /// <para><c>IntLiteral</c></para>
        /// </summary>
        protected virtual object CreateTerminalIntliteral(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: not</para>
        /// <para><c>not</c></para>
        /// </summary>
        protected virtual object CreateTerminalNot(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: or</para>
        /// <para><c>or</c></para>
        /// </summary>
        protected virtual object CreateTerminalOr(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: return</para>
        /// <para><c>return</c></para>
        /// </summary>
        protected virtual object CreateTerminalReturn(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: StringLiteral</para>
        /// <para><c>StringLiteral</c></para>
        /// </summary>
        protected virtual object CreateTerminalStringliteral(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: switch</para>
        /// <para><c>switch</c></para>
        /// </summary>
        protected virtual object CreateTerminalSwitch(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: type</para>
        /// <para><c>type</c></para>
        /// </summary>
        protected virtual object CreateTerminalType(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: while</para>
        /// <para><c>while</c></para>
        /// </summary>
        protected virtual object CreateTerminalWhile(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Args</para>
        /// <para><c>&lt;Args&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalArgs(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Block</para>
        /// <para><c>&lt;Block&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalBlock(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Case Stms</para>
        /// <para><c>&lt;Case Stms&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalCasestms(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Expr</para>
        /// <para><c>&lt;Expr&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalExpr(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Normal Stm</para>
        /// <para><c>&lt;Normal Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalNormalstm(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Op Add</para>
        /// <para><c>&lt;Op Add&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalOpadd(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Op And</para>
        /// <para><c>&lt;Op And&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalOpand(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Op Compare</para>
        /// <para><c>&lt;Op Compare&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalOpcompare(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Op Equate</para>
        /// <para><c>&lt;Op Equate&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalOpequate(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Op If</para>
        /// <para><c>&lt;Op If&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalOpif(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Op Mult</para>
        /// <para><c>&lt;Op Mult&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalOpmult(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Op Or</para>
        /// <para><c>&lt;Op Or&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalOpor(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Op Pointer</para>
        /// <para><c>&lt;Op Pointer&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalOppointer(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Op Unary</para>
        /// <para><c>&lt;Op Unary&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalOpunary(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Stm</para>
        /// <para><c>&lt;Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalStm(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Stm List</para>
        /// <para><c>&lt;Stm List&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalStmlist(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Then Stm</para>
        /// <para><c>&lt;Then Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalThenstm(TerminalToken token)
        {
        	return token;
        }

        /// <summary>
        /// <para>Symbol: Value</para>
        /// <para><c>&lt;Value&gt;</c></para>
        /// </summary>
        protected virtual object CreateTerminalValue(TerminalToken token)
        {
        	return token;
        }

		#endregion

        protected virtual object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)Symbols.Eof: //(EOF)
                	return CreateTerminalEof(token);

                case (int)Symbols.Error: //(Error)
                	return CreateTerminalError(token);

                case (int)Symbols.Whitespace: //(Whitespace)
                	return CreateTerminalWhitespace(token);

                case (int)Symbols.Commentend: //(Comment End)
                	return CreateTerminalCommentend(token);

                case (int)Symbols.Commentline: //(Comment Line)
                	return CreateTerminalCommentline(token);

                case (int)Symbols.Commentstart: //(Comment Start)
                	return CreateTerminalCommentstart(token);

                case (int)Symbols.Minus: //'-'
                	return CreateTerminalMinus(token);

                case (int)Symbols.Comma: //','
                	return CreateTerminalComma(token);

                case (int)Symbols.Semi: //';'
                	return CreateTerminalSemi(token);

                case (int)Symbols.Colon: //':'
                	return CreateTerminalColon(token);

                case (int)Symbols.Exclameq: //'!='
                	return CreateTerminalExclameq(token);

                case (int)Symbols.Question: //'?'
                	return CreateTerminalQuestion(token);

                case (int)Symbols.Dot: //'.'
                	return CreateTerminalDot(token);

                case (int)Symbols.Lparan: //'('
                	return CreateTerminalLparan(token);

                case (int)Symbols.Rparan: //')'
                	return CreateTerminalRparan(token);

                case (int)Symbols.Lbracket: //'['
                	return CreateTerminalLbracket(token);

                case (int)Symbols.Rbracket: //']'
                	return CreateTerminalRbracket(token);

                case (int)Symbols.Lbrace: //'{'
                	return CreateTerminalLbrace(token);

                case (int)Symbols.Rbrace: //'}'
                	return CreateTerminalRbrace(token);

                case (int)Symbols.Times: //'*'
                	return CreateTerminalTimes(token);

                case (int)Symbols.Div: //'/'
                	return CreateTerminalDiv(token);

                case (int)Symbols.Percent: //'%'
                	return CreateTerminalPercent(token);

                case (int)Symbols.Plus: //'+'
                	return CreateTerminalPlus(token);

                case (int)Symbols.Lt: //'<'
                	return CreateTerminalLt(token);

                case (int)Symbols.Lteq: //'<='
                	return CreateTerminalLteq(token);

                case (int)Symbols.Eq: //'='
                	return CreateTerminalEq(token);

                case (int)Symbols.Eqeq: //'=='
                	return CreateTerminalEqeq(token);

                case (int)Symbols.Gt: //'>'
                	return CreateTerminalGt(token);

                case (int)Symbols.Minusgt: //'->'
                	return CreateTerminalMinusgt(token);

                case (int)Symbols.Gteq: //'>='
                	return CreateTerminalGteq(token);

                case (int)Symbols.And: //and
                	return CreateTerminalAnd(token);

                case (int)Symbols.As: //as
                	return CreateTerminalAs(token);

                case (int)Symbols.Break: //break
                	return CreateTerminalBreak(token);

                case (int)Symbols.Case: //case
                	return CreateTerminalCase(token);

                case (int)Symbols.Cast: //cast
                	return CreateTerminalCast(token);

                case (int)Symbols.Continue: //continue
                	return CreateTerminalContinue(token);

                case (int)Symbols.Decimalliteral: //DecimalLiteral
                	return CreateTerminalDecimalliteral(token);

                case (int)Symbols.Default: //default
                	return CreateTerminalDefault(token);

                case (int)Symbols.Do: //do
                	return CreateTerminalDo(token);

                case (int)Symbols.Else: //else
                	return CreateTerminalElse(token);

                case (int)Symbols.For: //for
                	return CreateTerminalFor(token);

                case (int)Symbols.Foreach: //foreach
                	return CreateTerminalForeach(token);

                case (int)Symbols.Id: //ID
                	return CreateTerminalId(token);

                case (int)Symbols.If: //if
                	return CreateTerminalIf(token);

                case (int)Symbols.In: //in
                	return CreateTerminalIn(token);

                case (int)Symbols.Intliteral: //IntLiteral
                	return CreateTerminalIntliteral(token);

                case (int)Symbols.Not: //not
                	return CreateTerminalNot(token);

                case (int)Symbols.Or: //or
                	return CreateTerminalOr(token);

                case (int)Symbols.Return: //return
                	return CreateTerminalReturn(token);

                case (int)Symbols.Stringliteral: //StringLiteral
                	return CreateTerminalStringliteral(token);

                case (int)Symbols.Switch: //switch
                	return CreateTerminalSwitch(token);

                case (int)Symbols.Type: //type
                	return CreateTerminalType(token);

                case (int)Symbols.While: //while
                	return CreateTerminalWhile(token);

                case (int)Symbols.Args: //<Args>
                	return CreateTerminalArgs(token);

                case (int)Symbols.Block: //<Block>
                	return CreateTerminalBlock(token);

                case (int)Symbols.Casestms: //<Case Stms>
                	return CreateTerminalCasestms(token);

                case (int)Symbols.Expr: //<Expr>
                	return CreateTerminalExpr(token);

                case (int)Symbols.Normalstm: //<Normal Stm>
                	return CreateTerminalNormalstm(token);

                case (int)Symbols.Opadd: //<Op Add>
                	return CreateTerminalOpadd(token);

                case (int)Symbols.Opand: //<Op And>
                	return CreateTerminalOpand(token);

                case (int)Symbols.Opcompare: //<Op Compare>
                	return CreateTerminalOpcompare(token);

                case (int)Symbols.Opequate: //<Op Equate>
                	return CreateTerminalOpequate(token);

                case (int)Symbols.Opif: //<Op If>
                	return CreateTerminalOpif(token);

                case (int)Symbols.Opmult: //<Op Mult>
                	return CreateTerminalOpmult(token);

                case (int)Symbols.Opor: //<Op Or>
                	return CreateTerminalOpor(token);

                case (int)Symbols.Oppointer: //<Op Pointer>
                	return CreateTerminalOppointer(token);

                case (int)Symbols.Opunary: //<Op Unary>
                	return CreateTerminalOpunary(token);

                case (int)Symbols.Stm: //<Stm>
                	return CreateTerminalStm(token);

                case (int)Symbols.Stmlist: //<Stm List>
                	return CreateTerminalStmlist(token);

                case (int)Symbols.Thenstm: //<Then Stm>
                	return CreateTerminalThenstm(token);

                case (int)Symbols.Value: //<Value>
                	return CreateTerminalValue(token);

            }
            throw new SymbolException("Unknown symbol");
        }

		#region ToolScriptParserBase.Pravidla
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalStmIfLparanRparan(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalStmIfLparanRparanElse(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalStmWhileLparanRparan(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalStmForLparanSemiSemiRparan(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= foreach '(' ID in &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalStmForeachLparanIdInRparan(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalStm(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Then Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalThenstmIfLparanRparanElse(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalThenstmWhileLparanRparan(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalThenstmForLparanSemiSemiRparan(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalThenstm(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= do &lt;Stm&gt; while '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalNormalstmDoWhileLparanRparan(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= switch '(' &lt;Expr&gt; ')' '{' &lt;Case Stms&gt; '}'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalNormalstmSwitchLparanRparanLbraceRbrace(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Block&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalNormalstm(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Expr&gt; ';'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalNormalstmSemi(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= break ';'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalNormalstmBreakSemi(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= continue ';'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalNormalstmContinueSemi(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= return &lt;Expr&gt; ';'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalNormalstmReturnSemi(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= ';'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalNormalstmSemi2(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Expr&gt; ',' &lt;Args&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalArgsComma(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Expr&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalArgs(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= case &lt;Value&gt; ':' &lt;Stm List&gt; &lt;Case Stms&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalCasestmsCaseColon(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= default ':' &lt;Stm List&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalCasestmsDefaultColon(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= </c></para>
        /// </summary>
        protected virtual object CreateNonterminalCasestms(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Block&gt; ::= '{' &lt;Stm List&gt; '}'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalBlockLbraceRbrace(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= &lt;Stm&gt; &lt;Stm List&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalStmlist(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= </c></para>
        /// </summary>
        protected virtual object CreateNonterminalStmlist2(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '=' &lt;Expr&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalExprEq(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalExpr(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt; '?' &lt;Op If&gt; ':' &lt;Op If&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpifQuestionColon(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpif(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op Or&gt; or &lt;Op And&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOporOr(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op And&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpor(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op And&gt; and &lt;Op Equate&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpandAnd(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op Equate&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpand(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '==' &lt;Op Compare&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpequateEqeq(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '!=' &lt;Op Compare&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpequateExclameq(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Compare&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpequate(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;' &lt;Op Add&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpcompareLt(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;' &lt;Op Add&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpcompareGt(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;=' &lt;Op Add&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpcompareLteq(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;=' &lt;Op Add&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpcompareGteq(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Add&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpcompare(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '+' &lt;Op Mult&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpaddPlus(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '-' &lt;Op Mult&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpaddMinus(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Mult&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpadd(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '*' &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpmultTimes(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '/' &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpmultDiv(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '%' &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpmultPercent(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpmult(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= not &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpunaryNot(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= '-' &lt;Op Unary&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpunaryMinus(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= cast &lt;Op Unary&gt; as ID</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpunaryCastAsId(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOpunary(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '.' &lt;Value&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOppointerDot(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '-&gt;' &lt;Value&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOppointerMinusgt(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '[' &lt;Expr&gt; ']'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOppointerLbracketRbracket(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Value&gt;</c></para>
        /// </summary>
        protected virtual object CreateNonterminalOppointer(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= IntLiteral</c></para>
        /// </summary>
        protected virtual object CreateNonterminalValueIntliteral(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= StringLiteral</c></para>
        /// </summary>
        protected virtual object CreateNonterminalValueStringliteral(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= DecimalLiteral</c></para>
        /// </summary>
        protected virtual object CreateNonterminalValueDecimalliteral(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= type ID</c></para>
        /// </summary>
        protected virtual object CreateNonterminalValueTypeId(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID '(' &lt;Args&gt; ')'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalValueIdLparanRparan(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID '(' ')'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalValueIdLparanRparan2(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID</c></para>
        /// </summary>
        protected virtual object CreateNonterminalValueId(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        protected virtual object CreateNonterminalValueLparanRparan(NonterminalToken token)
        {
        	if(token.Tokens.Length == 0)
        		return null;
        	if(token.Tokens.Length == 1)
	        	return CreateObject(token.Tokens[0]);
        	ArrayList result = new ArrayList();
        	foreach(Token tok in token.Tokens)
        		result.Add(CreateObject(tok));
        	return result.ToArray();
        }
        
		#endregion

        protected virtual object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id + ToolScriptParserBase.RulesOffset)
            {
                case (int)Symbols.RuleStmIfLparanRparan: //<Stm> ::= if '(' <Expr> ')' <Stm>
                	return CreateNonterminalStmIfLparanRparan(token);
                case (int)Symbols.RuleStmIfLparanRparanElse: //<Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>
                	return CreateNonterminalStmIfLparanRparanElse(token);
                case (int)Symbols.RuleStmWhileLparanRparan: //<Stm> ::= while '(' <Expr> ')' <Stm>
                	return CreateNonterminalStmWhileLparanRparan(token);
                case (int)Symbols.RuleStmForLparanSemiSemiRparan: //<Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>
                	return CreateNonterminalStmForLparanSemiSemiRparan(token);
                case (int)Symbols.RuleStmForeachLparanIdInRparan: //<Stm> ::= foreach '(' ID in <Expr> ')' <Stm>
                	return CreateNonterminalStmForeachLparanIdInRparan(token);
                case (int)Symbols.RuleStm: //<Stm> ::= <Normal Stm>
                	return CreateNonterminalStm(token);
                case (int)Symbols.RuleThenstmIfLparanRparanElse: //<Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>
                	return CreateNonterminalThenstmIfLparanRparanElse(token);
                case (int)Symbols.RuleThenstmWhileLparanRparan: //<Then Stm> ::= while '(' <Expr> ')' <Then Stm>
                	return CreateNonterminalThenstmWhileLparanRparan(token);
                case (int)Symbols.RuleThenstmForLparanSemiSemiRparan: //<Then Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Then Stm>
                	return CreateNonterminalThenstmForLparanSemiSemiRparan(token);
                case (int)Symbols.RuleThenstm: //<Then Stm> ::= <Normal Stm>
                	return CreateNonterminalThenstm(token);
                case (int)Symbols.RuleNormalstmDoWhileLparanRparan: //<Normal Stm> ::= do <Stm> while '(' <Expr> ')'
                	return CreateNonterminalNormalstmDoWhileLparanRparan(token);
                case (int)Symbols.RuleNormalstmSwitchLparanRparanLbraceRbrace: //<Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'
                	return CreateNonterminalNormalstmSwitchLparanRparanLbraceRbrace(token);
                case (int)Symbols.RuleNormalstm: //<Normal Stm> ::= <Block>
                	return CreateNonterminalNormalstm(token);
                case (int)Symbols.RuleNormalstmSemi: //<Normal Stm> ::= <Expr> ';'
                	return CreateNonterminalNormalstmSemi(token);
                case (int)Symbols.RuleNormalstmBreakSemi: //<Normal Stm> ::= break ';'
                	return CreateNonterminalNormalstmBreakSemi(token);
                case (int)Symbols.RuleNormalstmContinueSemi: //<Normal Stm> ::= continue ';'
                	return CreateNonterminalNormalstmContinueSemi(token);
                case (int)Symbols.RuleNormalstmReturnSemi: //<Normal Stm> ::= return <Expr> ';'
                	return CreateNonterminalNormalstmReturnSemi(token);
                case (int)Symbols.RuleNormalstmSemi2: //<Normal Stm> ::= ';'
                	return CreateNonterminalNormalstmSemi2(token);
                case (int)Symbols.RuleArgsComma: //<Args> ::= <Expr> ',' <Args>
                	return CreateNonterminalArgsComma(token);
                case (int)Symbols.RuleArgs: //<Args> ::= <Expr>
                	return CreateNonterminalArgs(token);
                case (int)Symbols.RuleCasestmsCaseColon: //<Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>
                	return CreateNonterminalCasestmsCaseColon(token);
                case (int)Symbols.RuleCasestmsDefaultColon: //<Case Stms> ::= default ':' <Stm List>
                	return CreateNonterminalCasestmsDefaultColon(token);
                case (int)Symbols.RuleCasestms: //<Case Stms> ::= 
                	return CreateNonterminalCasestms(token);
                case (int)Symbols.RuleBlockLbraceRbrace: //<Block> ::= '{' <Stm List> '}'
                	return CreateNonterminalBlockLbraceRbrace(token);
                case (int)Symbols.RuleStmlist: //<Stm List> ::= <Stm> <Stm List>
                	return CreateNonterminalStmlist(token);
                case (int)Symbols.RuleStmlist2: //<Stm List> ::= 
                	return CreateNonterminalStmlist2(token);
                case (int)Symbols.RuleExprEq: //<Expr> ::= <Op If> '=' <Expr>
                	return CreateNonterminalExprEq(token);
                case (int)Symbols.RuleExpr: //<Expr> ::= <Op If>
                	return CreateNonterminalExpr(token);
                case (int)Symbols.RuleOpifQuestionColon: //<Op If> ::= <Op Or> '?' <Op If> ':' <Op If>
                	return CreateNonterminalOpifQuestionColon(token);
                case (int)Symbols.RuleOpif: //<Op If> ::= <Op Or>
                	return CreateNonterminalOpif(token);
                case (int)Symbols.RuleOporOr: //<Op Or> ::= <Op Or> or <Op And>
                	return CreateNonterminalOporOr(token);
                case (int)Symbols.RuleOpor: //<Op Or> ::= <Op And>
                	return CreateNonterminalOpor(token);
                case (int)Symbols.RuleOpandAnd: //<Op And> ::= <Op And> and <Op Equate>
                	return CreateNonterminalOpandAnd(token);
                case (int)Symbols.RuleOpand: //<Op And> ::= <Op Equate>
                	return CreateNonterminalOpand(token);
                case (int)Symbols.RuleOpequateEqeq: //<Op Equate> ::= <Op Equate> '==' <Op Compare>
                	return CreateNonterminalOpequateEqeq(token);
                case (int)Symbols.RuleOpequateExclameq: //<Op Equate> ::= <Op Equate> '!=' <Op Compare>
                	return CreateNonterminalOpequateExclameq(token);
                case (int)Symbols.RuleOpequate: //<Op Equate> ::= <Op Compare>
                	return CreateNonterminalOpequate(token);
                case (int)Symbols.RuleOpcompareLt: //<Op Compare> ::= <Op Compare> '<' <Op Add>
                	return CreateNonterminalOpcompareLt(token);
                case (int)Symbols.RuleOpcompareGt: //<Op Compare> ::= <Op Compare> '>' <Op Add>
                	return CreateNonterminalOpcompareGt(token);
                case (int)Symbols.RuleOpcompareLteq: //<Op Compare> ::= <Op Compare> '<=' <Op Add>
                	return CreateNonterminalOpcompareLteq(token);
                case (int)Symbols.RuleOpcompareGteq: //<Op Compare> ::= <Op Compare> '>=' <Op Add>
                	return CreateNonterminalOpcompareGteq(token);
                case (int)Symbols.RuleOpcompare: //<Op Compare> ::= <Op Add>
                	return CreateNonterminalOpcompare(token);
                case (int)Symbols.RuleOpaddPlus: //<Op Add> ::= <Op Add> '+' <Op Mult>
                	return CreateNonterminalOpaddPlus(token);
                case (int)Symbols.RuleOpaddMinus: //<Op Add> ::= <Op Add> '-' <Op Mult>
                	return CreateNonterminalOpaddMinus(token);
                case (int)Symbols.RuleOpadd: //<Op Add> ::= <Op Mult>
                	return CreateNonterminalOpadd(token);
                case (int)Symbols.RuleOpmultTimes: //<Op Mult> ::= <Op Mult> '*' <Op Unary>
                	return CreateNonterminalOpmultTimes(token);
                case (int)Symbols.RuleOpmultDiv: //<Op Mult> ::= <Op Mult> '/' <Op Unary>
                	return CreateNonterminalOpmultDiv(token);
                case (int)Symbols.RuleOpmultPercent: //<Op Mult> ::= <Op Mult> '%' <Op Unary>
                	return CreateNonterminalOpmultPercent(token);
                case (int)Symbols.RuleOpmult: //<Op Mult> ::= <Op Unary>
                	return CreateNonterminalOpmult(token);
                case (int)Symbols.RuleOpunaryNot: //<Op Unary> ::= not <Op Unary>
                	return CreateNonterminalOpunaryNot(token);
                case (int)Symbols.RuleOpunaryMinus: //<Op Unary> ::= '-' <Op Unary>
                	return CreateNonterminalOpunaryMinus(token);
                case (int)Symbols.RuleOpunaryCastAsId: //<Op Unary> ::= cast <Op Unary> as ID
                	return CreateNonterminalOpunaryCastAsId(token);
                case (int)Symbols.RuleOpunary: //<Op Unary> ::= <Op Pointer>
                	return CreateNonterminalOpunary(token);
                case (int)Symbols.RuleOppointerDot: //<Op Pointer> ::= <Op Pointer> '.' <Value>
                	return CreateNonterminalOppointerDot(token);
                case (int)Symbols.RuleOppointerMinusgt: //<Op Pointer> ::= <Op Pointer> '->' <Value>
                	return CreateNonterminalOppointerMinusgt(token);
                case (int)Symbols.RuleOppointerLbracketRbracket: //<Op Pointer> ::= <Op Pointer> '[' <Expr> ']'
                	return CreateNonterminalOppointerLbracketRbracket(token);
                case (int)Symbols.RuleOppointer: //<Op Pointer> ::= <Value>
                	return CreateNonterminalOppointer(token);
                case (int)Symbols.RuleValueIntliteral: //<Value> ::= IntLiteral
                	return CreateNonterminalValueIntliteral(token);
                case (int)Symbols.RuleValueStringliteral: //<Value> ::= StringLiteral
                	return CreateNonterminalValueStringliteral(token);
                case (int)Symbols.RuleValueDecimalliteral: //<Value> ::= DecimalLiteral
                	return CreateNonterminalValueDecimalliteral(token);
                case (int)Symbols.RuleValueTypeId: //<Value> ::= type ID
                	return CreateNonterminalValueTypeId(token);
                case (int)Symbols.RuleValueIdLparanRparan: //<Value> ::= ID '(' <Args> ')'
                	return CreateNonterminalValueIdLparanRparan(token);
                case (int)Symbols.RuleValueIdLparanRparan2: //<Value> ::= ID '(' ')'
                	return CreateNonterminalValueIdLparanRparan2(token);
                case (int)Symbols.RuleValueId: //<Value> ::= ID
                	return CreateNonterminalValueId(token);
                case (int)Symbols.RuleValueLparanRparan: //<Value> ::= '(' <Expr> ')'
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

		#endregion

    }
}
