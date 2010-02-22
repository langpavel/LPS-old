using com.calitha.goldparser;
using LPS.ToolScript.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace LPS.ToolScript
{

    /// <sumarry>Symboly terminalu a neterminalu</summary>
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
        /// <para>Symbol: --</para>
        /// <para><c>--</c></para>
        /// </summary>
        Minusminus = 7,

        /// <summary>
        /// <para>Symbol: ,</para>
        /// <para><c>','</c></para>
        /// </summary>
        Comma = 8,

        /// <summary>
        /// <para>Symbol: ;</para>
        /// <para><c>';'</c></para>
        /// </summary>
        Semi = 9,

        /// <summary>
        /// <para>Symbol: :</para>
        /// <para><c>':'</c></para>
        /// </summary>
        Colon = 10,

        /// <summary>
        /// <para>Symbol: !</para>
        /// <para><c>'!'</c></para>
        /// </summary>
        Exclam = 11,

        /// <summary>
        /// <para>Symbol: !=</para>
        /// <para><c>'!='</c></para>
        /// </summary>
        Exclameq = 12,

        /// <summary>
        /// <para>Symbol: ?</para>
        /// <para><c>'?'</c></para>
        /// </summary>
        Question = 13,

        /// <summary>
        /// <para>Symbol: .</para>
        /// <para><c>'.'</c></para>
        /// </summary>
        Dot = 14,

        /// <summary>
        /// <para>Symbol: (</para>
        /// <para><c>'('</c></para>
        /// </summary>
        Lparan = 15,

        /// <summary>
        /// <para>Symbol: )</para>
        /// <para><c>')'</c></para>
        /// </summary>
        Rparan = 16,

        /// <summary>
        /// <para>Symbol: [</para>
        /// <para><c>'['</c></para>
        /// </summary>
        Lbracket = 17,

        /// <summary>
        /// <para>Symbol: ]</para>
        /// <para><c>']'</c></para>
        /// </summary>
        Rbracket = 18,

        /// <summary>
        /// <para>Symbol: {</para>
        /// <para><c>'{'</c></para>
        /// </summary>
        Lbrace = 19,

        /// <summary>
        /// <para>Symbol: }</para>
        /// <para><c>'}'</c></para>
        /// </summary>
        Rbrace = 20,

        /// <summary>
        /// <para>Symbol: *</para>
        /// <para><c>'*'</c></para>
        /// </summary>
        Times = 21,

        /// <summary>
        /// <para>Symbol: *=</para>
        /// <para><c>'*='</c></para>
        /// </summary>
        Timeseq = 22,

        /// <summary>
        /// <para>Symbol: /</para>
        /// <para><c>'/'</c></para>
        /// </summary>
        Div = 23,

        /// <summary>
        /// <para>Symbol: /=</para>
        /// <para><c>'/='</c></para>
        /// </summary>
        Diveq = 24,

        /// <summary>
        /// <para>Symbol: %</para>
        /// <para><c>'%'</c></para>
        /// </summary>
        Percent = 25,

        /// <summary>
        /// <para>Symbol: +</para>
        /// <para><c>'+'</c></para>
        /// </summary>
        Plus = 26,

        /// <summary>
        /// <para>Symbol: ++</para>
        /// <para><c>'++'</c></para>
        /// </summary>
        Plusplus = 27,

        /// <summary>
        /// <para>Symbol: +=</para>
        /// <para><c>'+='</c></para>
        /// </summary>
        Pluseq = 28,

        /// <summary>
        /// <para>Symbol: &lt;</para>
        /// <para><c>'&lt;'</c></para>
        /// </summary>
        Lt = 29,

        /// <summary>
        /// <para>Symbol: &lt;=</para>
        /// <para><c>'&lt;='</c></para>
        /// </summary>
        Lteq = 30,

        /// <summary>
        /// <para>Symbol: &lt;==</para>
        /// <para><c>'&lt;=='</c></para>
        /// </summary>
        Lteqeq = 31,

        /// <summary>
        /// <para>Symbol: &lt;==&gt;</para>
        /// <para><c>'&lt;==&gt;'</c></para>
        /// </summary>
        Lteqeqgt = 32,

        /// <summary>
        /// <para>Symbol: =</para>
        /// <para><c>'='</c></para>
        /// </summary>
        Eq = 33,

        /// <summary>
        /// <para>Symbol: -=</para>
        /// <para><c>'-='</c></para>
        /// </summary>
        Minuseq = 34,

        /// <summary>
        /// <para>Symbol: ==</para>
        /// <para><c>'=='</c></para>
        /// </summary>
        Eqeq = 35,

        /// <summary>
        /// <para>Symbol: &gt;</para>
        /// <para><c>'&gt;'</c></para>
        /// </summary>
        Gt = 36,

        /// <summary>
        /// <para>Symbol: -&gt;</para>
        /// <para><c>'-&gt;'</c></para>
        /// </summary>
        Minusgt = 37,

        /// <summary>
        /// <para>Symbol: &gt;=</para>
        /// <para><c>'&gt;='</c></para>
        /// </summary>
        Gteq = 38,

        /// <summary>
        /// <para>Symbol: and</para>
        /// <para><c>and</c></para>
        /// </summary>
        And = 39,

        /// <summary>
        /// <para>Symbol: as</para>
        /// <para><c>as</c></para>
        /// </summary>
        As = 40,

        /// <summary>
        /// <para>Symbol: break</para>
        /// <para><c>break</c></para>
        /// </summary>
        Break = 41,

        /// <summary>
        /// <para>Symbol: case</para>
        /// <para><c>case</c></para>
        /// </summary>
        Case = 42,

        /// <summary>
        /// <para>Symbol: cast</para>
        /// <para><c>cast</c></para>
        /// </summary>
        Cast = 43,

        /// <summary>
        /// <para>Symbol: continue</para>
        /// <para><c>continue</c></para>
        /// </summary>
        Continue = 44,

        /// <summary>
        /// <para>Symbol: DecimalLiteral</para>
        /// <para><c>DecimalLiteral</c></para>
        /// </summary>
        Decimalliteral = 45,

        /// <summary>
        /// <para>Symbol: default</para>
        /// <para><c>default</c></para>
        /// </summary>
        Default = 46,

        /// <summary>
        /// <para>Symbol: do</para>
        /// <para><c>do</c></para>
        /// </summary>
        Do = 47,

        /// <summary>
        /// <para>Symbol: else</para>
        /// <para><c>else</c></para>
        /// </summary>
        Else = 48,

        /// <summary>
        /// <para>Symbol: false</para>
        /// <para><c>false</c></para>
        /// </summary>
        False = 49,

        /// <summary>
        /// <para>Symbol: for</para>
        /// <para><c>for</c></para>
        /// </summary>
        For = 50,

        /// <summary>
        /// <para>Symbol: foreach</para>
        /// <para><c>foreach</c></para>
        /// </summary>
        Foreach = 51,

        /// <summary>
        /// <para>Symbol: function</para>
        /// <para><c>function</c></para>
        /// </summary>
        Function = 52,

        /// <summary>
        /// <para>Symbol: ID</para>
        /// <para><c>ID</c></para>
        /// </summary>
        Id = 53,

        /// <summary>
        /// <para>Symbol: if</para>
        /// <para><c>if</c></para>
        /// </summary>
        If = 54,

        /// <summary>
        /// <para>Symbol: in</para>
        /// <para><c>in</c></para>
        /// </summary>
        In = 55,

        /// <summary>
        /// <para>Symbol: IntLiteral</para>
        /// <para><c>IntLiteral</c></para>
        /// </summary>
        Intliteral = 56,

        /// <summary>
        /// <para>Symbol: is</para>
        /// <para><c>is</c></para>
        /// </summary>
        Is = 57,

        /// <summary>
        /// <para>Symbol: new</para>
        /// <para><c>new</c></para>
        /// </summary>
        New = 58,

        /// <summary>
        /// <para>Symbol: not</para>
        /// <para><c>not</c></para>
        /// </summary>
        Not = 59,

        /// <summary>
        /// <para>Symbol: null</para>
        /// <para><c>null</c></para>
        /// </summary>
        Null = 60,

        /// <summary>
        /// <para>Symbol: observed</para>
        /// <para><c>observed</c></para>
        /// </summary>
        Observed = 61,

        /// <summary>
        /// <para>Symbol: or</para>
        /// <para><c>or</c></para>
        /// </summary>
        Or = 62,

        /// <summary>
        /// <para>Symbol: QualifiedName</para>
        /// <para><c>QualifiedName</c></para>
        /// </summary>
        Qualifiedname = 63,

        /// <summary>
        /// <para>Symbol: return</para>
        /// <para><c>return</c></para>
        /// </summary>
        Return = 64,

        /// <summary>
        /// <para>Symbol: static</para>
        /// <para><c>static</c></para>
        /// </summary>
        Static = 65,

        /// <summary>
        /// <para>Symbol: StringLiteral</para>
        /// <para><c>StringLiteral</c></para>
        /// </summary>
        Stringliteral = 66,

        /// <summary>
        /// <para>Symbol: switch</para>
        /// <para><c>switch</c></para>
        /// </summary>
        Switch = 67,

        /// <summary>
        /// <para>Symbol: true</para>
        /// <para><c>true</c></para>
        /// </summary>
        True = 68,

        /// <summary>
        /// <para>Symbol: type</para>
        /// <para><c>type</c></para>
        /// </summary>
        Type = 69,

        /// <summary>
        /// <para>Symbol: using</para>
        /// <para><c>using</c></para>
        /// </summary>
        Using = 70,

        /// <summary>
        /// <para>Symbol: var</para>
        /// <para><c>var</c></para>
        /// </summary>
        Var = 71,

        /// <summary>
        /// <para>Symbol: while</para>
        /// <para><c>while</c></para>
        /// </summary>
        While = 72,

        /// <summary>
        /// <para>Symbol: Args</para>
        /// <para><c>&lt;Args&gt;</c></para>
        /// </summary>
        Args = 73,

        /// <summary>
        /// <para>Symbol: Block</para>
        /// <para><c>&lt;Block&gt;</c></para>
        /// </summary>
        Block = 74,

        /// <summary>
        /// <para>Symbol: Case Stms</para>
        /// <para><c>&lt;Case Stms&gt;</c></para>
        /// </summary>
        Casestms = 75,

        /// <summary>
        /// <para>Symbol: Expr</para>
        /// <para><c>&lt;Expr&gt;</c></para>
        /// </summary>
        Expr = 76,

        /// <summary>
        /// <para>Symbol: Expr List</para>
        /// <para><c>&lt;Expr List&gt;</c></para>
        /// </summary>
        Exprlist = 77,

        /// <summary>
        /// <para>Symbol: Function</para>
        /// <para><c>&lt;Function&gt;</c></para>
        /// </summary>
        Function2 = 78,

        /// <summary>
        /// <para>Symbol: Normal Stm</para>
        /// <para><c>&lt;Normal Stm&gt;</c></para>
        /// </summary>
        Normalstm = 79,

        /// <summary>
        /// <para>Symbol: Op Add</para>
        /// <para><c>&lt;Op Add&gt;</c></para>
        /// </summary>
        Opadd = 80,

        /// <summary>
        /// <para>Symbol: Op And</para>
        /// <para><c>&lt;Op And&gt;</c></para>
        /// </summary>
        Opand = 81,

        /// <summary>
        /// <para>Symbol: Op Compare</para>
        /// <para><c>&lt;Op Compare&gt;</c></para>
        /// </summary>
        Opcompare = 82,

        /// <summary>
        /// <para>Symbol: Op Equate</para>
        /// <para><c>&lt;Op Equate&gt;</c></para>
        /// </summary>
        Opequate = 83,

        /// <summary>
        /// <para>Symbol: Op If</para>
        /// <para><c>&lt;Op If&gt;</c></para>
        /// </summary>
        Opif = 84,

        /// <summary>
        /// <para>Symbol: Op In</para>
        /// <para><c>&lt;Op In&gt;</c></para>
        /// </summary>
        Opin = 85,

        /// <summary>
        /// <para>Symbol: Op Mult</para>
        /// <para><c>&lt;Op Mult&gt;</c></para>
        /// </summary>
        Opmult = 86,

        /// <summary>
        /// <para>Symbol: Op Or</para>
        /// <para><c>&lt;Op Or&gt;</c></para>
        /// </summary>
        Opor = 87,

        /// <summary>
        /// <para>Symbol: Op Pointer</para>
        /// <para><c>&lt;Op Pointer&gt;</c></para>
        /// </summary>
        Oppointer = 88,

        /// <summary>
        /// <para>Symbol: Op Unary</para>
        /// <para><c>&lt;Op Unary&gt;</c></para>
        /// </summary>
        Opunary = 89,

        /// <summary>
        /// <para>Symbol: Param decl</para>
        /// <para><c>&lt;Param decl&gt;</c></para>
        /// </summary>
        Paramdecl = 90,

        /// <summary>
        /// <para>Symbol: Param List</para>
        /// <para><c>&lt;Param List&gt;</c></para>
        /// </summary>
        Paramlist = 91,

        /// <summary>
        /// <para>Symbol: Stm</para>
        /// <para><c>&lt;Stm&gt;</c></para>
        /// </summary>
        Stm = 92,

        /// <summary>
        /// <para>Symbol: Stm List</para>
        /// <para><c>&lt;Stm List&gt;</c></para>
        /// </summary>
        Stmlist = 93,

        /// <summary>
        /// <para>Symbol: Then Stm</para>
        /// <para><c>&lt;Then Stm&gt;</c></para>
        /// </summary>
        Thenstm = 94,

        /// <summary>
        /// <para>Symbol: Value</para>
        /// <para><c>&lt;Value&gt;</c></para>
        /// </summary>
        Value = 95,

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
        /// <para><c>&lt;Stm&gt; ::= observed '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmObservedLparanRparan = ToolScriptParserBase.RulesOffset + 5,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= using ID ';'</c></para>
        /// </summary>
        RuleStmUsingIdSemi = ToolScriptParserBase.RulesOffset + 6,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= using QualifiedName ';'</c></para>
        /// </summary>
        RuleStmUsingQualifiednameSemi = ToolScriptParserBase.RulesOffset + 7,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= using StringLiteral ';'</c></para>
        /// </summary>
        RuleStmUsingStringliteralSemi = ToolScriptParserBase.RulesOffset + 8,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= using QualifiedName as ID ';'</c></para>
        /// </summary>
        RuleStmUsingQualifiednameAsIdSemi = ToolScriptParserBase.RulesOffset + 9,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        RuleStm = ToolScriptParserBase.RulesOffset + 10,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Then Stm&gt;</c></para>
        /// </summary>
        RuleThenstmIfLparanRparanElse = ToolScriptParserBase.RulesOffset + 11,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        RuleThenstmWhileLparanRparan = ToolScriptParserBase.RulesOffset + 12,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        RuleThenstmForLparanSemiSemiRparan = ToolScriptParserBase.RulesOffset + 13,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        RuleThenstm = ToolScriptParserBase.RulesOffset + 14,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= do &lt;Stm&gt; while '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        RuleNormalstmDoWhileLparanRparan = ToolScriptParserBase.RulesOffset + 15,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= switch '(' &lt;Expr&gt; ')' '{' &lt;Case Stms&gt; '}'</c></para>
        /// </summary>
        RuleNormalstmSwitchLparanRparanLbraceRbrace = ToolScriptParserBase.RulesOffset + 16,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Block&gt;</c></para>
        /// </summary>
        RuleNormalstm = ToolScriptParserBase.RulesOffset + 17,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleNormalstmSemi = ToolScriptParserBase.RulesOffset + 18,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= break ';'</c></para>
        /// </summary>
        RuleNormalstmBreakSemi = ToolScriptParserBase.RulesOffset + 19,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= continue ';'</c></para>
        /// </summary>
        RuleNormalstmContinueSemi = ToolScriptParserBase.RulesOffset + 20,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= return &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleNormalstmReturnSemi = ToolScriptParserBase.RulesOffset + 21,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= ';'</c></para>
        /// </summary>
        RuleNormalstmSemi2 = ToolScriptParserBase.RulesOffset + 22,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Expr&gt; ',' &lt;Args&gt;</c></para>
        /// </summary>
        RuleArgsComma = ToolScriptParserBase.RulesOffset + 23,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Expr&gt;</c></para>
        /// </summary>
        RuleArgs = ToolScriptParserBase.RulesOffset + 24,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= case &lt;Value&gt; ':' &lt;Stm List&gt; &lt;Case Stms&gt;</c></para>
        /// </summary>
        RuleCasestmsCaseColon = ToolScriptParserBase.RulesOffset + 25,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= default ':' &lt;Stm List&gt;</c></para>
        /// </summary>
        RuleCasestmsDefaultColon = ToolScriptParserBase.RulesOffset + 26,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= </c></para>
        /// </summary>
        RuleCasestms = ToolScriptParserBase.RulesOffset + 27,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Block&gt; ::= '{' &lt;Stm List&gt; '}'</c></para>
        /// </summary>
        RuleBlockLbraceRbrace = ToolScriptParserBase.RulesOffset + 28,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= &lt;Stm&gt; &lt;Stm List&gt;</c></para>
        /// </summary>
        RuleStmlist = ToolScriptParserBase.RulesOffset + 29,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= </c></para>
        /// </summary>
        RuleStmlist2 = ToolScriptParserBase.RulesOffset + 30,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Function&gt; ::= function ID '(' &lt;Param List&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleFunctionFunctionIdLparanRparan = ToolScriptParserBase.RulesOffset + 31,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Function&gt; ::= function ID '(' ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleFunctionFunctionIdLparanRparan2 = ToolScriptParserBase.RulesOffset + 32,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Function&gt; ::= function '(' &lt;Param List&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleFunctionFunctionLparanRparan = ToolScriptParserBase.RulesOffset + 33,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Function&gt; ::= function '(' ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleFunctionFunctionLparanRparan2 = ToolScriptParserBase.RulesOffset + 34,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Param List&gt; ::= &lt;Param List&gt; ',' &lt;Param decl&gt;</c></para>
        /// </summary>
        RuleParamlistComma = ToolScriptParserBase.RulesOffset + 35,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Param List&gt; ::= &lt;Param decl&gt;</c></para>
        /// </summary>
        RuleParamlist = ToolScriptParserBase.RulesOffset + 36,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Param decl&gt; ::= ID</c></para>
        /// </summary>
        RuleParamdeclId = ToolScriptParserBase.RulesOffset + 37,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr List&gt; ::= &lt;Expr List&gt; ',' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprlistComma = ToolScriptParserBase.RulesOffset + 38,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr List&gt; ::= &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprlist = ToolScriptParserBase.RulesOffset + 39,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprEq = ToolScriptParserBase.RulesOffset + 40,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '+=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprPluseq = ToolScriptParserBase.RulesOffset + 41,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '-=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprMinuseq = ToolScriptParserBase.RulesOffset + 42,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '*=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprTimeseq = ToolScriptParserBase.RulesOffset + 43,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '/=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprDiveq = ToolScriptParserBase.RulesOffset + 44,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '&lt;==' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprLteqeq = ToolScriptParserBase.RulesOffset + 45,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '&lt;==&gt;' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprLteqeqgt = ToolScriptParserBase.RulesOffset + 46,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt;</c></para>
        /// </summary>
        RuleExpr = ToolScriptParserBase.RulesOffset + 47,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt; '?' &lt;Op If&gt; ':' &lt;Op If&gt;</c></para>
        /// </summary>
        RuleOpifQuestionColon = ToolScriptParserBase.RulesOffset + 48,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt;</c></para>
        /// </summary>
        RuleOpif = ToolScriptParserBase.RulesOffset + 49,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op Or&gt; or &lt;Op And&gt;</c></para>
        /// </summary>
        RuleOporOr = ToolScriptParserBase.RulesOffset + 50,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op And&gt;</c></para>
        /// </summary>
        RuleOpor = ToolScriptParserBase.RulesOffset + 51,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op And&gt; and &lt;Op Equate&gt;</c></para>
        /// </summary>
        RuleOpandAnd = ToolScriptParserBase.RulesOffset + 52,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op Equate&gt;</c></para>
        /// </summary>
        RuleOpand = ToolScriptParserBase.RulesOffset + 53,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '==' &lt;Op Compare&gt;</c></para>
        /// </summary>
        RuleOpequateEqeq = ToolScriptParserBase.RulesOffset + 54,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '!=' &lt;Op Compare&gt;</c></para>
        /// </summary>
        RuleOpequateExclameq = ToolScriptParserBase.RulesOffset + 55,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Compare&gt;</c></para>
        /// </summary>
        RuleOpequate = ToolScriptParserBase.RulesOffset + 56,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;' &lt;Op In&gt;</c></para>
        /// </summary>
        RuleOpcompareLt = ToolScriptParserBase.RulesOffset + 57,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;' &lt;Op In&gt;</c></para>
        /// </summary>
        RuleOpcompareGt = ToolScriptParserBase.RulesOffset + 58,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;=' &lt;Op In&gt;</c></para>
        /// </summary>
        RuleOpcompareLteq = ToolScriptParserBase.RulesOffset + 59,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;=' &lt;Op In&gt;</c></para>
        /// </summary>
        RuleOpcompareGteq = ToolScriptParserBase.RulesOffset + 60,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op In&gt;</c></para>
        /// </summary>
        RuleOpcompare = ToolScriptParserBase.RulesOffset + 61,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op In&gt; ::= &lt;Op In&gt; in &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpinIn = ToolScriptParserBase.RulesOffset + 62,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op In&gt; ::= &lt;Op In&gt; in '&lt;' &lt;Op Add&gt; ',' &lt;Op Add&gt; '&gt;'</c></para>
        /// </summary>
        RuleOpinInLtCommaGt = ToolScriptParserBase.RulesOffset + 63,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op In&gt; ::= &lt;Op In&gt; in '&lt;' &lt;Op Add&gt; ',' &lt;Op Add&gt; ')'</c></para>
        /// </summary>
        RuleOpinInLtCommaRparan = ToolScriptParserBase.RulesOffset + 64,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op In&gt; ::= &lt;Op In&gt; in '(' &lt;Op Add&gt; ',' &lt;Op Add&gt; '&gt;'</c></para>
        /// </summary>
        RuleOpinInLparanCommaGt = ToolScriptParserBase.RulesOffset + 65,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op In&gt; ::= &lt;Op In&gt; in '(' &lt;Op Add&gt; ',' &lt;Op Add&gt; ')'</c></para>
        /// </summary>
        RuleOpinInLparanCommaRparan = ToolScriptParserBase.RulesOffset + 66,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op In&gt; ::= &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpin = ToolScriptParserBase.RulesOffset + 67,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '+' &lt;Op Mult&gt;</c></para>
        /// </summary>
        RuleOpaddPlus = ToolScriptParserBase.RulesOffset + 68,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '-' &lt;Op Mult&gt;</c></para>
        /// </summary>
        RuleOpaddMinus = ToolScriptParserBase.RulesOffset + 69,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Mult&gt;</c></para>
        /// </summary>
        RuleOpadd = ToolScriptParserBase.RulesOffset + 70,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '*' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmultTimes = ToolScriptParserBase.RulesOffset + 71,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '/' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmultDiv = ToolScriptParserBase.RulesOffset + 72,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '%' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmultPercent = ToolScriptParserBase.RulesOffset + 73,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmult = ToolScriptParserBase.RulesOffset + 74,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= not &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryNot = ToolScriptParserBase.RulesOffset + 75,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= '!' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryExclam = ToolScriptParserBase.RulesOffset + 76,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= '-' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryMinus = ToolScriptParserBase.RulesOffset + 77,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= cast &lt;Op Unary&gt; as ID</c></para>
        /// </summary>
        RuleOpunaryCastAsId = ToolScriptParserBase.RulesOffset + 78,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= cast &lt;Op Unary&gt; as QualifiedName</c></para>
        /// </summary>
        RuleOpunaryCastAsQualifiedname = ToolScriptParserBase.RulesOffset + 79,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= '++' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryPlusplus = ToolScriptParserBase.RulesOffset + 80,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= -- &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryMinusminus = ToolScriptParserBase.RulesOffset + 81,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt; '++'</c></para>
        /// </summary>
        RuleOpunaryPlusplus2 = ToolScriptParserBase.RulesOffset + 82,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt; --</c></para>
        /// </summary>
        RuleOpunaryMinusminus2 = ToolScriptParserBase.RulesOffset + 83,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt; is null</c></para>
        /// </summary>
        RuleOpunaryIsNull = ToolScriptParserBase.RulesOffset + 84,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt; not null</c></para>
        /// </summary>
        RuleOpunaryNotNull = ToolScriptParserBase.RulesOffset + 85,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt; is not null</c></para>
        /// </summary>
        RuleOpunaryIsNotNull = ToolScriptParserBase.RulesOffset + 86,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt;</c></para>
        /// </summary>
        RuleOpunary = ToolScriptParserBase.RulesOffset + 87,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '.' &lt;Value&gt;</c></para>
        /// </summary>
        RuleOppointerDot = ToolScriptParserBase.RulesOffset + 88,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '-&gt;' &lt;Value&gt;</c></para>
        /// </summary>
        RuleOppointerMinusgt = ToolScriptParserBase.RulesOffset + 89,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '[' &lt;Expr&gt; ']'</c></para>
        /// </summary>
        RuleOppointerLbracketRbracket = ToolScriptParserBase.RulesOffset + 90,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Value&gt;</c></para>
        /// </summary>
        RuleOppointer = ToolScriptParserBase.RulesOffset + 91,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= IntLiteral</c></para>
        /// </summary>
        RuleValueIntliteral = ToolScriptParserBase.RulesOffset + 92,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= StringLiteral</c></para>
        /// </summary>
        RuleValueStringliteral = ToolScriptParserBase.RulesOffset + 93,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= DecimalLiteral</c></para>
        /// </summary>
        RuleValueDecimalliteral = ToolScriptParserBase.RulesOffset + 94,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= type ID</c></para>
        /// </summary>
        RuleValueTypeId = ToolScriptParserBase.RulesOffset + 95,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= type QualifiedName</c></para>
        /// </summary>
        RuleValueTypeQualifiedname = ToolScriptParserBase.RulesOffset + 96,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID '(' &lt;Args&gt; ')'</c></para>
        /// </summary>
        RuleValueIdLparanRparan = ToolScriptParserBase.RulesOffset + 97,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID '(' ')'</c></para>
        /// </summary>
        RuleValueIdLparanRparan2 = ToolScriptParserBase.RulesOffset + 98,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= &lt;Function&gt;</c></para>
        /// </summary>
        RuleValue = ToolScriptParserBase.RulesOffset + 99,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= QualifiedName '(' &lt;Args&gt; ')'</c></para>
        /// </summary>
        RuleValueQualifiednameLparanRparan = ToolScriptParserBase.RulesOffset + 100,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= QualifiedName '(' ')'</c></para>
        /// </summary>
        RuleValueQualifiednameLparanRparan2 = ToolScriptParserBase.RulesOffset + 101,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= new ID '(' &lt;Args&gt; ')'</c></para>
        /// </summary>
        RuleValueNewIdLparanRparan = ToolScriptParserBase.RulesOffset + 102,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= new ID '(' ')'</c></para>
        /// </summary>
        RuleValueNewIdLparanRparan2 = ToolScriptParserBase.RulesOffset + 103,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= new QualifiedName '(' &lt;Args&gt; ')'</c></para>
        /// </summary>
        RuleValueNewQualifiednameLparanRparan = ToolScriptParserBase.RulesOffset + 104,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= new QualifiedName '(' ')'</c></para>
        /// </summary>
        RuleValueNewQualifiednameLparanRparan2 = ToolScriptParserBase.RulesOffset + 105,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID</c></para>
        /// </summary>
        RuleValueId = ToolScriptParserBase.RulesOffset + 106,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= var ID</c></para>
        /// </summary>
        RuleValueVarId = ToolScriptParserBase.RulesOffset + 107,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= static ID</c></para>
        /// </summary>
        RuleValueStaticId = ToolScriptParserBase.RulesOffset + 108,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        RuleValueLparanRparan = ToolScriptParserBase.RulesOffset + 109,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= '{' &lt;Expr List&gt; '}'</c></para>
        /// </summary>
        RuleValueLbraceRbrace = ToolScriptParserBase.RulesOffset + 110,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= null</c></para>
        /// </summary>
        RuleValueNull = ToolScriptParserBase.RulesOffset + 111,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= true</c></para>
        /// </summary>
        RuleValueTrue = ToolScriptParserBase.RulesOffset + 112,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= false</c></para>
        /// </summary>
        RuleValueFalse = ToolScriptParserBase.RulesOffset + 113 

	    #endregion
    };

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

        public virtual StatementList Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token == null)
	            return null;
	        return CreateObject(token) as StatementList;
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

		protected void CheckRule(NonterminalToken token, params Symbols[] symbols)
		{
			if(!IsRule(token, symbols))
				throw new Exception("Nesprávné pravidlo");
		}

        public virtual object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

		public static Symbols GetSymbol(Token tok)
		{
			if(tok is TerminalToken)
				return (Symbols)(((TerminalToken)tok).Symbol.Id);
			else
				return (Symbols)(((NonterminalToken)tok).Rule.Id + ToolScriptParserBase.RulesOffset);
		}

		#region ToolScriptParserBase.Symboly
		/// <summary>
		/// <para>Symbol: EOF</para>
		/// <para><c>(EOF)</c></para>
		/// </summary>
		protected virtual object TerminalEof(TerminalToken token)
		{
			throw new NotImplementedException("Symbol EOF");
		}

		/// <summary>
		/// <para>Symbol: Error</para>
		/// <para><c>(Error)</c></para>
		/// </summary>
		protected virtual object TerminalError(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Error");
		}

		/// <summary>
		/// <para>Symbol: Whitespace</para>
		/// <para><c>(Whitespace)</c></para>
		/// </summary>
		protected virtual object TerminalWhitespace(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Whitespace");
		}

		/// <summary>
		/// <para>Symbol: Comment End</para>
		/// <para><c>(Comment End)</c></para>
		/// </summary>
		protected virtual object TerminalCommentend(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Comment End");
		}

		/// <summary>
		/// <para>Symbol: Comment Line</para>
		/// <para><c>(Comment Line)</c></para>
		/// </summary>
		protected virtual object TerminalCommentline(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Comment Line");
		}

		/// <summary>
		/// <para>Symbol: Comment Start</para>
		/// <para><c>(Comment Start)</c></para>
		/// </summary>
		protected virtual object TerminalCommentstart(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Comment Start");
		}

		/// <summary>
		/// <para>Symbol: -</para>
		/// <para><c>'-'</c></para>
		/// </summary>
		protected virtual object TerminalMinus(TerminalToken token)
		{
			throw new NotImplementedException("Symbol -");
		}

		/// <summary>
		/// <para>Symbol: --</para>
		/// <para><c>--</c></para>
		/// </summary>
		protected virtual object TerminalMinusminus(TerminalToken token)
		{
			throw new NotImplementedException("Symbol --");
		}

		/// <summary>
		/// <para>Symbol: ,</para>
		/// <para><c>','</c></para>
		/// </summary>
		protected virtual object TerminalComma(TerminalToken token)
		{
			throw new NotImplementedException("Symbol ,");
		}

		/// <summary>
		/// <para>Symbol: ;</para>
		/// <para><c>';'</c></para>
		/// </summary>
		protected virtual object TerminalSemi(TerminalToken token)
		{
			throw new NotImplementedException("Symbol ;");
		}

		/// <summary>
		/// <para>Symbol: :</para>
		/// <para><c>':'</c></para>
		/// </summary>
		protected virtual object TerminalColon(TerminalToken token)
		{
			throw new NotImplementedException("Symbol :");
		}

		/// <summary>
		/// <para>Symbol: !</para>
		/// <para><c>'!'</c></para>
		/// </summary>
		protected virtual object TerminalExclam(TerminalToken token)
		{
			throw new NotImplementedException("Symbol !");
		}

		/// <summary>
		/// <para>Symbol: !=</para>
		/// <para><c>'!='</c></para>
		/// </summary>
		protected virtual object TerminalExclameq(TerminalToken token)
		{
			throw new NotImplementedException("Symbol !=");
		}

		/// <summary>
		/// <para>Symbol: ?</para>
		/// <para><c>'?'</c></para>
		/// </summary>
		protected virtual object TerminalQuestion(TerminalToken token)
		{
			throw new NotImplementedException("Symbol ?");
		}

		/// <summary>
		/// <para>Symbol: .</para>
		/// <para><c>'.'</c></para>
		/// </summary>
		protected virtual object TerminalDot(TerminalToken token)
		{
			throw new NotImplementedException("Symbol .");
		}

		/// <summary>
		/// <para>Symbol: (</para>
		/// <para><c>'('</c></para>
		/// </summary>
		protected virtual object TerminalLparan(TerminalToken token)
		{
			throw new NotImplementedException("Symbol (");
		}

		/// <summary>
		/// <para>Symbol: )</para>
		/// <para><c>')'</c></para>
		/// </summary>
		protected virtual object TerminalRparan(TerminalToken token)
		{
			throw new NotImplementedException("Symbol )");
		}

		/// <summary>
		/// <para>Symbol: [</para>
		/// <para><c>'['</c></para>
		/// </summary>
		protected virtual object TerminalLbracket(TerminalToken token)
		{
			throw new NotImplementedException("Symbol [");
		}

		/// <summary>
		/// <para>Symbol: ]</para>
		/// <para><c>']'</c></para>
		/// </summary>
		protected virtual object TerminalRbracket(TerminalToken token)
		{
			throw new NotImplementedException("Symbol ]");
		}

		/// <summary>
		/// <para>Symbol: {</para>
		/// <para><c>'{'</c></para>
		/// </summary>
		protected virtual object TerminalLbrace(TerminalToken token)
		{
			throw new NotImplementedException("Symbol {");
		}

		/// <summary>
		/// <para>Symbol: }</para>
		/// <para><c>'}'</c></para>
		/// </summary>
		protected virtual object TerminalRbrace(TerminalToken token)
		{
			throw new NotImplementedException("Symbol }");
		}

		/// <summary>
		/// <para>Symbol: *</para>
		/// <para><c>'*'</c></para>
		/// </summary>
		protected virtual object TerminalTimes(TerminalToken token)
		{
			throw new NotImplementedException("Symbol *");
		}

		/// <summary>
		/// <para>Symbol: *=</para>
		/// <para><c>'*='</c></para>
		/// </summary>
		protected virtual object TerminalTimeseq(TerminalToken token)
		{
			throw new NotImplementedException("Symbol *=");
		}

		/// <summary>
		/// <para>Symbol: /</para>
		/// <para><c>'/'</c></para>
		/// </summary>
		protected virtual object TerminalDiv(TerminalToken token)
		{
			throw new NotImplementedException("Symbol /");
		}

		/// <summary>
		/// <para>Symbol: /=</para>
		/// <para><c>'/='</c></para>
		/// </summary>
		protected virtual object TerminalDiveq(TerminalToken token)
		{
			throw new NotImplementedException("Symbol /=");
		}

		/// <summary>
		/// <para>Symbol: %</para>
		/// <para><c>'%'</c></para>
		/// </summary>
		protected virtual object TerminalPercent(TerminalToken token)
		{
			throw new NotImplementedException("Symbol %");
		}

		/// <summary>
		/// <para>Symbol: +</para>
		/// <para><c>'+'</c></para>
		/// </summary>
		protected virtual object TerminalPlus(TerminalToken token)
		{
			throw new NotImplementedException("Symbol +");
		}

		/// <summary>
		/// <para>Symbol: ++</para>
		/// <para><c>'++'</c></para>
		/// </summary>
		protected virtual object TerminalPlusplus(TerminalToken token)
		{
			throw new NotImplementedException("Symbol ++");
		}

		/// <summary>
		/// <para>Symbol: +=</para>
		/// <para><c>'+='</c></para>
		/// </summary>
		protected virtual object TerminalPluseq(TerminalToken token)
		{
			throw new NotImplementedException("Symbol +=");
		}

		/// <summary>
		/// <para>Symbol: &lt;</para>
		/// <para><c>'&lt;'</c></para>
		/// </summary>
		protected virtual object TerminalLt(TerminalToken token)
		{
			throw new NotImplementedException("Symbol <");
		}

		/// <summary>
		/// <para>Symbol: &lt;=</para>
		/// <para><c>'&lt;='</c></para>
		/// </summary>
		protected virtual object TerminalLteq(TerminalToken token)
		{
			throw new NotImplementedException("Symbol <=");
		}

		/// <summary>
		/// <para>Symbol: &lt;==</para>
		/// <para><c>'&lt;=='</c></para>
		/// </summary>
		protected virtual object TerminalLteqeq(TerminalToken token)
		{
			throw new NotImplementedException("Symbol <==");
		}

		/// <summary>
		/// <para>Symbol: &lt;==&gt;</para>
		/// <para><c>'&lt;==&gt;'</c></para>
		/// </summary>
		protected virtual object TerminalLteqeqgt(TerminalToken token)
		{
			throw new NotImplementedException("Symbol <==>");
		}

		/// <summary>
		/// <para>Symbol: =</para>
		/// <para><c>'='</c></para>
		/// </summary>
		protected virtual object TerminalEq(TerminalToken token)
		{
			throw new NotImplementedException("Symbol =");
		}

		/// <summary>
		/// <para>Symbol: -=</para>
		/// <para><c>'-='</c></para>
		/// </summary>
		protected virtual object TerminalMinuseq(TerminalToken token)
		{
			throw new NotImplementedException("Symbol -=");
		}

		/// <summary>
		/// <para>Symbol: ==</para>
		/// <para><c>'=='</c></para>
		/// </summary>
		protected virtual object TerminalEqeq(TerminalToken token)
		{
			throw new NotImplementedException("Symbol ==");
		}

		/// <summary>
		/// <para>Symbol: &gt;</para>
		/// <para><c>'&gt;'</c></para>
		/// </summary>
		protected virtual object TerminalGt(TerminalToken token)
		{
			throw new NotImplementedException("Symbol >");
		}

		/// <summary>
		/// <para>Symbol: -&gt;</para>
		/// <para><c>'-&gt;'</c></para>
		/// </summary>
		protected virtual object TerminalMinusgt(TerminalToken token)
		{
			throw new NotImplementedException("Symbol ->");
		}

		/// <summary>
		/// <para>Symbol: &gt;=</para>
		/// <para><c>'&gt;='</c></para>
		/// </summary>
		protected virtual object TerminalGteq(TerminalToken token)
		{
			throw new NotImplementedException("Symbol >=");
		}

		/// <summary>
		/// <para>Symbol: and</para>
		/// <para><c>and</c></para>
		/// </summary>
		protected virtual object TerminalAnd(TerminalToken token)
		{
			throw new NotImplementedException("Symbol and");
		}

		/// <summary>
		/// <para>Symbol: as</para>
		/// <para><c>as</c></para>
		/// </summary>
		protected virtual object TerminalAs(TerminalToken token)
		{
			throw new NotImplementedException("Symbol as");
		}

		/// <summary>
		/// <para>Symbol: break</para>
		/// <para><c>break</c></para>
		/// </summary>
		protected virtual object TerminalBreak(TerminalToken token)
		{
			throw new NotImplementedException("Symbol break");
		}

		/// <summary>
		/// <para>Symbol: case</para>
		/// <para><c>case</c></para>
		/// </summary>
		protected virtual object TerminalCase(TerminalToken token)
		{
			throw new NotImplementedException("Symbol case");
		}

		/// <summary>
		/// <para>Symbol: cast</para>
		/// <para><c>cast</c></para>
		/// </summary>
		protected virtual object TerminalCast(TerminalToken token)
		{
			throw new NotImplementedException("Symbol cast");
		}

		/// <summary>
		/// <para>Symbol: continue</para>
		/// <para><c>continue</c></para>
		/// </summary>
		protected virtual object TerminalContinue(TerminalToken token)
		{
			throw new NotImplementedException("Symbol continue");
		}

		/// <summary>
		/// <para>Symbol: DecimalLiteral</para>
		/// <para><c>DecimalLiteral</c></para>
		/// </summary>
		protected virtual object TerminalDecimalliteral(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DecimalLiteral");
		}

		/// <summary>
		/// <para>Symbol: default</para>
		/// <para><c>default</c></para>
		/// </summary>
		protected virtual object TerminalDefault(TerminalToken token)
		{
			throw new NotImplementedException("Symbol default");
		}

		/// <summary>
		/// <para>Symbol: do</para>
		/// <para><c>do</c></para>
		/// </summary>
		protected virtual object TerminalDo(TerminalToken token)
		{
			throw new NotImplementedException("Symbol do");
		}

		/// <summary>
		/// <para>Symbol: else</para>
		/// <para><c>else</c></para>
		/// </summary>
		protected virtual object TerminalElse(TerminalToken token)
		{
			throw new NotImplementedException("Symbol else");
		}

		/// <summary>
		/// <para>Symbol: false</para>
		/// <para><c>false</c></para>
		/// </summary>
		protected virtual object TerminalFalse(TerminalToken token)
		{
			throw new NotImplementedException("Symbol false");
		}

		/// <summary>
		/// <para>Symbol: for</para>
		/// <para><c>for</c></para>
		/// </summary>
		protected virtual object TerminalFor(TerminalToken token)
		{
			throw new NotImplementedException("Symbol for");
		}

		/// <summary>
		/// <para>Symbol: foreach</para>
		/// <para><c>foreach</c></para>
		/// </summary>
		protected virtual object TerminalForeach(TerminalToken token)
		{
			throw new NotImplementedException("Symbol foreach");
		}

		/// <summary>
		/// <para>Symbol: function</para>
		/// <para><c>function</c></para>
		/// </summary>
		protected virtual object TerminalFunction(TerminalToken token)
		{
			throw new NotImplementedException("Symbol function");
		}

		/// <summary>
		/// <para>Symbol: ID</para>
		/// <para><c>ID</c></para>
		/// </summary>
		protected virtual object TerminalId(TerminalToken token)
		{
			throw new NotImplementedException("Symbol ID");
		}

		/// <summary>
		/// <para>Symbol: if</para>
		/// <para><c>if</c></para>
		/// </summary>
		protected virtual object TerminalIf(TerminalToken token)
		{
			throw new NotImplementedException("Symbol if");
		}

		/// <summary>
		/// <para>Symbol: in</para>
		/// <para><c>in</c></para>
		/// </summary>
		protected virtual object TerminalIn(TerminalToken token)
		{
			throw new NotImplementedException("Symbol in");
		}

		/// <summary>
		/// <para>Symbol: IntLiteral</para>
		/// <para><c>IntLiteral</c></para>
		/// </summary>
		protected virtual object TerminalIntliteral(TerminalToken token)
		{
			throw new NotImplementedException("Symbol IntLiteral");
		}

		/// <summary>
		/// <para>Symbol: is</para>
		/// <para><c>is</c></para>
		/// </summary>
		protected virtual object TerminalIs(TerminalToken token)
		{
			throw new NotImplementedException("Symbol is");
		}

		/// <summary>
		/// <para>Symbol: new</para>
		/// <para><c>new</c></para>
		/// </summary>
		protected virtual object TerminalNew(TerminalToken token)
		{
			throw new NotImplementedException("Symbol new");
		}

		/// <summary>
		/// <para>Symbol: not</para>
		/// <para><c>not</c></para>
		/// </summary>
		protected virtual object TerminalNot(TerminalToken token)
		{
			throw new NotImplementedException("Symbol not");
		}

		/// <summary>
		/// <para>Symbol: null</para>
		/// <para><c>null</c></para>
		/// </summary>
		protected virtual object TerminalNull(TerminalToken token)
		{
			throw new NotImplementedException("Symbol null");
		}

		/// <summary>
		/// <para>Symbol: observed</para>
		/// <para><c>observed</c></para>
		/// </summary>
		protected virtual object TerminalObserved(TerminalToken token)
		{
			throw new NotImplementedException("Symbol observed");
		}

		/// <summary>
		/// <para>Symbol: or</para>
		/// <para><c>or</c></para>
		/// </summary>
		protected virtual object TerminalOr(TerminalToken token)
		{
			throw new NotImplementedException("Symbol or");
		}

		/// <summary>
		/// <para>Symbol: QualifiedName</para>
		/// <para><c>QualifiedName</c></para>
		/// </summary>
		protected virtual object TerminalQualifiedname(TerminalToken token)
		{
			throw new NotImplementedException("Symbol QualifiedName");
		}

		/// <summary>
		/// <para>Symbol: return</para>
		/// <para><c>return</c></para>
		/// </summary>
		protected virtual object TerminalReturn(TerminalToken token)
		{
			throw new NotImplementedException("Symbol return");
		}

		/// <summary>
		/// <para>Symbol: static</para>
		/// <para><c>static</c></para>
		/// </summary>
		protected virtual object TerminalStatic(TerminalToken token)
		{
			throw new NotImplementedException("Symbol static");
		}

		/// <summary>
		/// <para>Symbol: StringLiteral</para>
		/// <para><c>StringLiteral</c></para>
		/// </summary>
		protected virtual object TerminalStringliteral(TerminalToken token)
		{
			throw new NotImplementedException("Symbol StringLiteral");
		}

		/// <summary>
		/// <para>Symbol: switch</para>
		/// <para><c>switch</c></para>
		/// </summary>
		protected virtual object TerminalSwitch(TerminalToken token)
		{
			throw new NotImplementedException("Symbol switch");
		}

		/// <summary>
		/// <para>Symbol: true</para>
		/// <para><c>true</c></para>
		/// </summary>
		protected virtual object TerminalTrue(TerminalToken token)
		{
			throw new NotImplementedException("Symbol true");
		}

		/// <summary>
		/// <para>Symbol: type</para>
		/// <para><c>type</c></para>
		/// </summary>
		protected virtual object TerminalType(TerminalToken token)
		{
			throw new NotImplementedException("Symbol type");
		}

		/// <summary>
		/// <para>Symbol: using</para>
		/// <para><c>using</c></para>
		/// </summary>
		protected virtual object TerminalUsing(TerminalToken token)
		{
			throw new NotImplementedException("Symbol using");
		}

		/// <summary>
		/// <para>Symbol: var</para>
		/// <para><c>var</c></para>
		/// </summary>
		protected virtual object TerminalVar(TerminalToken token)
		{
			throw new NotImplementedException("Symbol var");
		}

		/// <summary>
		/// <para>Symbol: while</para>
		/// <para><c>while</c></para>
		/// </summary>
		protected virtual object TerminalWhile(TerminalToken token)
		{
			throw new NotImplementedException("Symbol while");
		}

		/// <summary>
		/// <para>Symbol: Args</para>
		/// <para><c>&lt;Args&gt;</c></para>
		/// </summary>
		protected virtual object TerminalArgs(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Args");
		}

		/// <summary>
		/// <para>Symbol: Block</para>
		/// <para><c>&lt;Block&gt;</c></para>
		/// </summary>
		protected virtual object TerminalBlock(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Block");
		}

		/// <summary>
		/// <para>Symbol: Case Stms</para>
		/// <para><c>&lt;Case Stms&gt;</c></para>
		/// </summary>
		protected virtual object TerminalCasestms(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Case Stms");
		}

		/// <summary>
		/// <para>Symbol: Expr</para>
		/// <para><c>&lt;Expr&gt;</c></para>
		/// </summary>
		protected virtual object TerminalExpr(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Expr");
		}

		/// <summary>
		/// <para>Symbol: Expr List</para>
		/// <para><c>&lt;Expr List&gt;</c></para>
		/// </summary>
		protected virtual object TerminalExprlist(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Expr List");
		}

		/// <summary>
		/// <para>Symbol: Function</para>
		/// <para><c>&lt;Function&gt;</c></para>
		/// </summary>
		protected virtual object TerminalFunction2(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Function");
		}

		/// <summary>
		/// <para>Symbol: Normal Stm</para>
		/// <para><c>&lt;Normal Stm&gt;</c></para>
		/// </summary>
		protected virtual object TerminalNormalstm(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Normal Stm");
		}

		/// <summary>
		/// <para>Symbol: Op Add</para>
		/// <para><c>&lt;Op Add&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOpadd(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op Add");
		}

		/// <summary>
		/// <para>Symbol: Op And</para>
		/// <para><c>&lt;Op And&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOpand(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op And");
		}

		/// <summary>
		/// <para>Symbol: Op Compare</para>
		/// <para><c>&lt;Op Compare&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOpcompare(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op Compare");
		}

		/// <summary>
		/// <para>Symbol: Op Equate</para>
		/// <para><c>&lt;Op Equate&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOpequate(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op Equate");
		}

		/// <summary>
		/// <para>Symbol: Op If</para>
		/// <para><c>&lt;Op If&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOpif(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op If");
		}

		/// <summary>
		/// <para>Symbol: Op In</para>
		/// <para><c>&lt;Op In&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOpin(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op In");
		}

		/// <summary>
		/// <para>Symbol: Op Mult</para>
		/// <para><c>&lt;Op Mult&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOpmult(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op Mult");
		}

		/// <summary>
		/// <para>Symbol: Op Or</para>
		/// <para><c>&lt;Op Or&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOpor(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op Or");
		}

		/// <summary>
		/// <para>Symbol: Op Pointer</para>
		/// <para><c>&lt;Op Pointer&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOppointer(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op Pointer");
		}

		/// <summary>
		/// <para>Symbol: Op Unary</para>
		/// <para><c>&lt;Op Unary&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOpunary(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op Unary");
		}

		/// <summary>
		/// <para>Symbol: Param decl</para>
		/// <para><c>&lt;Param decl&gt;</c></para>
		/// </summary>
		protected virtual object TerminalParamdecl(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Param decl");
		}

		/// <summary>
		/// <para>Symbol: Param List</para>
		/// <para><c>&lt;Param List&gt;</c></para>
		/// </summary>
		protected virtual object TerminalParamlist(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Param List");
		}

		/// <summary>
		/// <para>Symbol: Stm</para>
		/// <para><c>&lt;Stm&gt;</c></para>
		/// </summary>
		protected virtual object TerminalStm(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Stm");
		}

		/// <summary>
		/// <para>Symbol: Stm List</para>
		/// <para><c>&lt;Stm List&gt;</c></para>
		/// </summary>
		protected virtual object TerminalStmlist(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Stm List");
		}

		/// <summary>
		/// <para>Symbol: Then Stm</para>
		/// <para><c>&lt;Then Stm&gt;</c></para>
		/// </summary>
		protected virtual object TerminalThenstm(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Then Stm");
		}

		/// <summary>
		/// <para>Symbol: Value</para>
		/// <para><c>&lt;Value&gt;</c></para>
		/// </summary>
		protected virtual object TerminalValue(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Value");
		}

		#endregion

        public virtual object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)Symbols.Eof: //(EOF)
                	return TerminalEof(token);

                case (int)Symbols.Error: //(Error)
                	return TerminalError(token);

                case (int)Symbols.Whitespace: //(Whitespace)
                	return TerminalWhitespace(token);

                case (int)Symbols.Commentend: //(Comment End)
                	return TerminalCommentend(token);

                case (int)Symbols.Commentline: //(Comment Line)
                	return TerminalCommentline(token);

                case (int)Symbols.Commentstart: //(Comment Start)
                	return TerminalCommentstart(token);

                case (int)Symbols.Minus: //'-'
                	return TerminalMinus(token);

                case (int)Symbols.Minusminus: //--
                	return TerminalMinusminus(token);

                case (int)Symbols.Comma: //','
                	return TerminalComma(token);

                case (int)Symbols.Semi: //';'
                	return TerminalSemi(token);

                case (int)Symbols.Colon: //':'
                	return TerminalColon(token);

                case (int)Symbols.Exclam: //'!'
                	return TerminalExclam(token);

                case (int)Symbols.Exclameq: //'!='
                	return TerminalExclameq(token);

                case (int)Symbols.Question: //'?'
                	return TerminalQuestion(token);

                case (int)Symbols.Dot: //'.'
                	return TerminalDot(token);

                case (int)Symbols.Lparan: //'('
                	return TerminalLparan(token);

                case (int)Symbols.Rparan: //')'
                	return TerminalRparan(token);

                case (int)Symbols.Lbracket: //'['
                	return TerminalLbracket(token);

                case (int)Symbols.Rbracket: //']'
                	return TerminalRbracket(token);

                case (int)Symbols.Lbrace: //'{'
                	return TerminalLbrace(token);

                case (int)Symbols.Rbrace: //'}'
                	return TerminalRbrace(token);

                case (int)Symbols.Times: //'*'
                	return TerminalTimes(token);

                case (int)Symbols.Timeseq: //'*='
                	return TerminalTimeseq(token);

                case (int)Symbols.Div: //'/'
                	return TerminalDiv(token);

                case (int)Symbols.Diveq: //'/='
                	return TerminalDiveq(token);

                case (int)Symbols.Percent: //'%'
                	return TerminalPercent(token);

                case (int)Symbols.Plus: //'+'
                	return TerminalPlus(token);

                case (int)Symbols.Plusplus: //'++'
                	return TerminalPlusplus(token);

                case (int)Symbols.Pluseq: //'+='
                	return TerminalPluseq(token);

                case (int)Symbols.Lt: //'<'
                	return TerminalLt(token);

                case (int)Symbols.Lteq: //'<='
                	return TerminalLteq(token);

                case (int)Symbols.Lteqeq: //'<=='
                	return TerminalLteqeq(token);

                case (int)Symbols.Lteqeqgt: //'<==>'
                	return TerminalLteqeqgt(token);

                case (int)Symbols.Eq: //'='
                	return TerminalEq(token);

                case (int)Symbols.Minuseq: //'-='
                	return TerminalMinuseq(token);

                case (int)Symbols.Eqeq: //'=='
                	return TerminalEqeq(token);

                case (int)Symbols.Gt: //'>'
                	return TerminalGt(token);

                case (int)Symbols.Minusgt: //'->'
                	return TerminalMinusgt(token);

                case (int)Symbols.Gteq: //'>='
                	return TerminalGteq(token);

                case (int)Symbols.And: //and
                	return TerminalAnd(token);

                case (int)Symbols.As: //as
                	return TerminalAs(token);

                case (int)Symbols.Break: //break
                	return TerminalBreak(token);

                case (int)Symbols.Case: //case
                	return TerminalCase(token);

                case (int)Symbols.Cast: //cast
                	return TerminalCast(token);

                case (int)Symbols.Continue: //continue
                	return TerminalContinue(token);

                case (int)Symbols.Decimalliteral: //DecimalLiteral
                	return TerminalDecimalliteral(token);

                case (int)Symbols.Default: //default
                	return TerminalDefault(token);

                case (int)Symbols.Do: //do
                	return TerminalDo(token);

                case (int)Symbols.Else: //else
                	return TerminalElse(token);

                case (int)Symbols.False: //false
                	return TerminalFalse(token);

                case (int)Symbols.For: //for
                	return TerminalFor(token);

                case (int)Symbols.Foreach: //foreach
                	return TerminalForeach(token);

                case (int)Symbols.Function: //function
                	return TerminalFunction(token);

                case (int)Symbols.Id: //ID
                	return TerminalId(token);

                case (int)Symbols.If: //if
                	return TerminalIf(token);

                case (int)Symbols.In: //in
                	return TerminalIn(token);

                case (int)Symbols.Intliteral: //IntLiteral
                	return TerminalIntliteral(token);

                case (int)Symbols.Is: //is
                	return TerminalIs(token);

                case (int)Symbols.New: //new
                	return TerminalNew(token);

                case (int)Symbols.Not: //not
                	return TerminalNot(token);

                case (int)Symbols.Null: //null
                	return TerminalNull(token);

                case (int)Symbols.Observed: //observed
                	return TerminalObserved(token);

                case (int)Symbols.Or: //or
                	return TerminalOr(token);

                case (int)Symbols.Qualifiedname: //QualifiedName
                	return TerminalQualifiedname(token);

                case (int)Symbols.Return: //return
                	return TerminalReturn(token);

                case (int)Symbols.Static: //static
                	return TerminalStatic(token);

                case (int)Symbols.Stringliteral: //StringLiteral
                	return TerminalStringliteral(token);

                case (int)Symbols.Switch: //switch
                	return TerminalSwitch(token);

                case (int)Symbols.True: //true
                	return TerminalTrue(token);

                case (int)Symbols.Type: //type
                	return TerminalType(token);

                case (int)Symbols.Using: //using
                	return TerminalUsing(token);

                case (int)Symbols.Var: //var
                	return TerminalVar(token);

                case (int)Symbols.While: //while
                	return TerminalWhile(token);

                case (int)Symbols.Args: //<Args>
                	return TerminalArgs(token);

                case (int)Symbols.Block: //<Block>
                	return TerminalBlock(token);

                case (int)Symbols.Casestms: //<Case Stms>
                	return TerminalCasestms(token);

                case (int)Symbols.Expr: //<Expr>
                	return TerminalExpr(token);

                case (int)Symbols.Exprlist: //<Expr List>
                	return TerminalExprlist(token);

                case (int)Symbols.Function2: //<Function>
                	return TerminalFunction2(token);

                case (int)Symbols.Normalstm: //<Normal Stm>
                	return TerminalNormalstm(token);

                case (int)Symbols.Opadd: //<Op Add>
                	return TerminalOpadd(token);

                case (int)Symbols.Opand: //<Op And>
                	return TerminalOpand(token);

                case (int)Symbols.Opcompare: //<Op Compare>
                	return TerminalOpcompare(token);

                case (int)Symbols.Opequate: //<Op Equate>
                	return TerminalOpequate(token);

                case (int)Symbols.Opif: //<Op If>
                	return TerminalOpif(token);

                case (int)Symbols.Opin: //<Op In>
                	return TerminalOpin(token);

                case (int)Symbols.Opmult: //<Op Mult>
                	return TerminalOpmult(token);

                case (int)Symbols.Opor: //<Op Or>
                	return TerminalOpor(token);

                case (int)Symbols.Oppointer: //<Op Pointer>
                	return TerminalOppointer(token);

                case (int)Symbols.Opunary: //<Op Unary>
                	return TerminalOpunary(token);

                case (int)Symbols.Paramdecl: //<Param decl>
                	return TerminalParamdecl(token);

                case (int)Symbols.Paramlist: //<Param List>
                	return TerminalParamlist(token);

                case (int)Symbols.Stm: //<Stm>
                	return TerminalStm(token);

                case (int)Symbols.Stmlist: //<Stm List>
                	return TerminalStmlist(token);

                case (int)Symbols.Thenstm: //<Then Stm>
                	return TerminalThenstm(token);

                case (int)Symbols.Value: //<Value>
                	return TerminalValue(token);

            }
            throw new SymbolException("Unknown symbol");
        }

		public string TokenSymbolsToString(params Token[] tokens)
		{
			StringBuilder sb = new StringBuilder();
			foreach(Token tok in tokens)
				sb.Append(GetSymbol(tok)).Append(" ");
			return sb.ToString();
		}

		#region ToolScriptParserBase.Pravidla
        protected abstract object RuleStmIfLparanRparan(NonterminalToken token); // <Stm> ::= if '(' <Expr> ')' <Stm>
        protected abstract object RuleStmIfLparanRparanElse(NonterminalToken token); // <Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>
        protected abstract object RuleStmWhileLparanRparan(NonterminalToken token); // <Stm> ::= while '(' <Expr> ')' <Stm>
        protected abstract object RuleStmForLparanSemiSemiRparan(NonterminalToken token); // <Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>
        protected abstract object RuleStmForeachLparanIdInRparan(NonterminalToken token); // <Stm> ::= foreach '(' ID in <Expr> ')' <Stm>
        protected abstract object RuleStmObservedLparanRparan(NonterminalToken token); // <Stm> ::= observed '(' <Expr> ')' <Stm>
        protected abstract object RuleStmUsingIdSemi(NonterminalToken token); // <Stm> ::= using ID ';'
        protected abstract object RuleStmUsingQualifiednameSemi(NonterminalToken token); // <Stm> ::= using QualifiedName ';'
        protected abstract object RuleStmUsingStringliteralSemi(NonterminalToken token); // <Stm> ::= using StringLiteral ';'
        protected abstract object RuleStmUsingQualifiednameAsIdSemi(NonterminalToken token); // <Stm> ::= using QualifiedName as ID ';'
        protected abstract object RuleStm(NonterminalToken token); // <Stm> ::= <Normal Stm>
        protected abstract object RuleThenstmIfLparanRparanElse(NonterminalToken token); // <Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>
        protected abstract object RuleThenstmWhileLparanRparan(NonterminalToken token); // <Then Stm> ::= while '(' <Expr> ')' <Then Stm>
        protected abstract object RuleThenstmForLparanSemiSemiRparan(NonterminalToken token); // <Then Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Then Stm>
        protected abstract object RuleThenstm(NonterminalToken token); // <Then Stm> ::= <Normal Stm>
        protected abstract object RuleNormalstmDoWhileLparanRparan(NonterminalToken token); // <Normal Stm> ::= do <Stm> while '(' <Expr> ')'
        protected abstract object RuleNormalstmSwitchLparanRparanLbraceRbrace(NonterminalToken token); // <Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'
        protected abstract object RuleNormalstm(NonterminalToken token); // <Normal Stm> ::= <Block>
        protected abstract object RuleNormalstmSemi(NonterminalToken token); // <Normal Stm> ::= <Expr> ';'
        protected abstract object RuleNormalstmBreakSemi(NonterminalToken token); // <Normal Stm> ::= break ';'
        protected abstract object RuleNormalstmContinueSemi(NonterminalToken token); // <Normal Stm> ::= continue ';'
        protected abstract object RuleNormalstmReturnSemi(NonterminalToken token); // <Normal Stm> ::= return <Expr> ';'
        protected abstract object RuleNormalstmSemi2(NonterminalToken token); // <Normal Stm> ::= ';'
        protected abstract object RuleArgsComma(NonterminalToken token); // <Args> ::= <Expr> ',' <Args>
        protected abstract object RuleArgs(NonterminalToken token); // <Args> ::= <Expr>
        protected abstract object RuleCasestmsCaseColon(NonterminalToken token); // <Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>
        protected abstract object RuleCasestmsDefaultColon(NonterminalToken token); // <Case Stms> ::= default ':' <Stm List>
        protected abstract object RuleCasestms(NonterminalToken token); // <Case Stms> ::= 
        protected abstract object RuleBlockLbraceRbrace(NonterminalToken token); // <Block> ::= '{' <Stm List> '}'
        protected abstract object RuleStmlist(NonterminalToken token); // <Stm List> ::= <Stm> <Stm List>
        protected abstract object RuleStmlist2(NonterminalToken token); // <Stm List> ::= 
        protected abstract object RuleFunctionFunctionIdLparanRparan(NonterminalToken token); // <Function> ::= function ID '(' <Param List> ')' <Stm>
        protected abstract object RuleFunctionFunctionIdLparanRparan2(NonterminalToken token); // <Function> ::= function ID '(' ')' <Stm>
        protected abstract object RuleFunctionFunctionLparanRparan(NonterminalToken token); // <Function> ::= function '(' <Param List> ')' <Stm>
        protected abstract object RuleFunctionFunctionLparanRparan2(NonterminalToken token); // <Function> ::= function '(' ')' <Stm>
        protected abstract object RuleParamlistComma(NonterminalToken token); // <Param List> ::= <Param List> ',' <Param decl>
        protected abstract object RuleParamlist(NonterminalToken token); // <Param List> ::= <Param decl>
        protected abstract object RuleParamdeclId(NonterminalToken token); // <Param decl> ::= ID
        protected abstract object RuleExprlistComma(NonterminalToken token); // <Expr List> ::= <Expr List> ',' <Expr>
        protected abstract object RuleExprlist(NonterminalToken token); // <Expr List> ::= <Expr>
        protected abstract object RuleExprEq(NonterminalToken token); // <Expr> ::= <Op If> '=' <Expr>
        protected abstract object RuleExprPluseq(NonterminalToken token); // <Expr> ::= <Op If> '+=' <Expr>
        protected abstract object RuleExprMinuseq(NonterminalToken token); // <Expr> ::= <Op If> '-=' <Expr>
        protected abstract object RuleExprTimeseq(NonterminalToken token); // <Expr> ::= <Op If> '*=' <Expr>
        protected abstract object RuleExprDiveq(NonterminalToken token); // <Expr> ::= <Op If> '/=' <Expr>
        protected abstract object RuleExprLteqeq(NonterminalToken token); // <Expr> ::= <Op If> '<==' <Expr>
        protected abstract object RuleExprLteqeqgt(NonterminalToken token); // <Expr> ::= <Op If> '<==>' <Expr>
        protected abstract object RuleExpr(NonterminalToken token); // <Expr> ::= <Op If>
        protected abstract object RuleOpifQuestionColon(NonterminalToken token); // <Op If> ::= <Op Or> '?' <Op If> ':' <Op If>
        protected abstract object RuleOpif(NonterminalToken token); // <Op If> ::= <Op Or>
        protected abstract object RuleOporOr(NonterminalToken token); // <Op Or> ::= <Op Or> or <Op And>
        protected abstract object RuleOpor(NonterminalToken token); // <Op Or> ::= <Op And>
        protected abstract object RuleOpandAnd(NonterminalToken token); // <Op And> ::= <Op And> and <Op Equate>
        protected abstract object RuleOpand(NonterminalToken token); // <Op And> ::= <Op Equate>
        protected abstract object RuleOpequateEqeq(NonterminalToken token); // <Op Equate> ::= <Op Equate> '==' <Op Compare>
        protected abstract object RuleOpequateExclameq(NonterminalToken token); // <Op Equate> ::= <Op Equate> '!=' <Op Compare>
        protected abstract object RuleOpequate(NonterminalToken token); // <Op Equate> ::= <Op Compare>
        protected abstract object RuleOpcompareLt(NonterminalToken token); // <Op Compare> ::= <Op Compare> '<' <Op In>
        protected abstract object RuleOpcompareGt(NonterminalToken token); // <Op Compare> ::= <Op Compare> '>' <Op In>
        protected abstract object RuleOpcompareLteq(NonterminalToken token); // <Op Compare> ::= <Op Compare> '<=' <Op In>
        protected abstract object RuleOpcompareGteq(NonterminalToken token); // <Op Compare> ::= <Op Compare> '>=' <Op In>
        protected abstract object RuleOpcompare(NonterminalToken token); // <Op Compare> ::= <Op In>
        protected abstract object RuleOpinIn(NonterminalToken token); // <Op In> ::= <Op In> in <Op Add>
        protected abstract object RuleOpinInLtCommaGt(NonterminalToken token); // <Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> '>'
        protected abstract object RuleOpinInLtCommaRparan(NonterminalToken token); // <Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> ')'
        protected abstract object RuleOpinInLparanCommaGt(NonterminalToken token); // <Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> '>'
        protected abstract object RuleOpinInLparanCommaRparan(NonterminalToken token); // <Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> ')'
        protected abstract object RuleOpin(NonterminalToken token); // <Op In> ::= <Op Add>
        protected abstract object RuleOpaddPlus(NonterminalToken token); // <Op Add> ::= <Op Add> '+' <Op Mult>
        protected abstract object RuleOpaddMinus(NonterminalToken token); // <Op Add> ::= <Op Add> '-' <Op Mult>
        protected abstract object RuleOpadd(NonterminalToken token); // <Op Add> ::= <Op Mult>
        protected abstract object RuleOpmultTimes(NonterminalToken token); // <Op Mult> ::= <Op Mult> '*' <Op Unary>
        protected abstract object RuleOpmultDiv(NonterminalToken token); // <Op Mult> ::= <Op Mult> '/' <Op Unary>
        protected abstract object RuleOpmultPercent(NonterminalToken token); // <Op Mult> ::= <Op Mult> '%' <Op Unary>
        protected abstract object RuleOpmult(NonterminalToken token); // <Op Mult> ::= <Op Unary>
        protected abstract object RuleOpunaryNot(NonterminalToken token); // <Op Unary> ::= not <Op Unary>
        protected abstract object RuleOpunaryExclam(NonterminalToken token); // <Op Unary> ::= '!' <Op Unary>
        protected abstract object RuleOpunaryMinus(NonterminalToken token); // <Op Unary> ::= '-' <Op Unary>
        protected abstract object RuleOpunaryCastAsId(NonterminalToken token); // <Op Unary> ::= cast <Op Unary> as ID
        protected abstract object RuleOpunaryCastAsQualifiedname(NonterminalToken token); // <Op Unary> ::= cast <Op Unary> as QualifiedName
        protected abstract object RuleOpunaryPlusplus(NonterminalToken token); // <Op Unary> ::= '++' <Op Unary>
        protected abstract object RuleOpunaryMinusminus(NonterminalToken token); // <Op Unary> ::= -- <Op Unary>
        protected abstract object RuleOpunaryPlusplus2(NonterminalToken token); // <Op Unary> ::= <Op Pointer> '++'
        protected abstract object RuleOpunaryMinusminus2(NonterminalToken token); // <Op Unary> ::= <Op Pointer> --
        protected abstract object RuleOpunaryIsNull(NonterminalToken token); // <Op Unary> ::= <Op Pointer> is null
        protected abstract object RuleOpunaryNotNull(NonterminalToken token); // <Op Unary> ::= <Op Pointer> not null
        protected abstract object RuleOpunaryIsNotNull(NonterminalToken token); // <Op Unary> ::= <Op Pointer> is not null
        protected abstract object RuleOpunary(NonterminalToken token); // <Op Unary> ::= <Op Pointer>
        protected abstract object RuleOppointerDot(NonterminalToken token); // <Op Pointer> ::= <Op Pointer> '.' <Value>
        protected abstract object RuleOppointerMinusgt(NonterminalToken token); // <Op Pointer> ::= <Op Pointer> '->' <Value>
        protected abstract object RuleOppointerLbracketRbracket(NonterminalToken token); // <Op Pointer> ::= <Op Pointer> '[' <Expr> ']'
        protected abstract object RuleOppointer(NonterminalToken token); // <Op Pointer> ::= <Value>
        protected abstract object RuleValueIntliteral(NonterminalToken token); // <Value> ::= IntLiteral
        protected abstract object RuleValueStringliteral(NonterminalToken token); // <Value> ::= StringLiteral
        protected abstract object RuleValueDecimalliteral(NonterminalToken token); // <Value> ::= DecimalLiteral
        protected abstract object RuleValueTypeId(NonterminalToken token); // <Value> ::= type ID
        protected abstract object RuleValueTypeQualifiedname(NonterminalToken token); // <Value> ::= type QualifiedName
        protected abstract object RuleValueIdLparanRparan(NonterminalToken token); // <Value> ::= ID '(' <Args> ')'
        protected abstract object RuleValueIdLparanRparan2(NonterminalToken token); // <Value> ::= ID '(' ')'
        protected abstract object RuleValue(NonterminalToken token); // <Value> ::= <Function>
        protected abstract object RuleValueQualifiednameLparanRparan(NonterminalToken token); // <Value> ::= QualifiedName '(' <Args> ')'
        protected abstract object RuleValueQualifiednameLparanRparan2(NonterminalToken token); // <Value> ::= QualifiedName '(' ')'
        protected abstract object RuleValueNewIdLparanRparan(NonterminalToken token); // <Value> ::= new ID '(' <Args> ')'
        protected abstract object RuleValueNewIdLparanRparan2(NonterminalToken token); // <Value> ::= new ID '(' ')'
        protected abstract object RuleValueNewQualifiednameLparanRparan(NonterminalToken token); // <Value> ::= new QualifiedName '(' <Args> ')'
        protected abstract object RuleValueNewQualifiednameLparanRparan2(NonterminalToken token); // <Value> ::= new QualifiedName '(' ')'
        protected abstract object RuleValueId(NonterminalToken token); // <Value> ::= ID
        protected abstract object RuleValueVarId(NonterminalToken token); // <Value> ::= var ID
        protected abstract object RuleValueStaticId(NonterminalToken token); // <Value> ::= static ID
        protected abstract object RuleValueLparanRparan(NonterminalToken token); // <Value> ::= '(' <Expr> ')'
        protected abstract object RuleValueLbraceRbrace(NonterminalToken token); // <Value> ::= '{' <Expr List> '}'
        protected abstract object RuleValueNull(NonterminalToken token); // <Value> ::= null
        protected abstract object RuleValueTrue(NonterminalToken token); // <Value> ::= true
        protected abstract object RuleValueFalse(NonterminalToken token); // <Value> ::= false


		#endregion

        public virtual object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id + ToolScriptParserBase.RulesOffset)
            {
                case (int)Symbols.RuleStmIfLparanRparan: //<Stm> ::= if '(' <Expr> ')' <Stm>
                	return RuleStmIfLparanRparan(token);
                case (int)Symbols.RuleStmIfLparanRparanElse: //<Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>
                	return RuleStmIfLparanRparanElse(token);
                case (int)Symbols.RuleStmWhileLparanRparan: //<Stm> ::= while '(' <Expr> ')' <Stm>
                	return RuleStmWhileLparanRparan(token);
                case (int)Symbols.RuleStmForLparanSemiSemiRparan: //<Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>
                	return RuleStmForLparanSemiSemiRparan(token);
                case (int)Symbols.RuleStmForeachLparanIdInRparan: //<Stm> ::= foreach '(' ID in <Expr> ')' <Stm>
                	return RuleStmForeachLparanIdInRparan(token);
                case (int)Symbols.RuleStmObservedLparanRparan: //<Stm> ::= observed '(' <Expr> ')' <Stm>
                	return RuleStmObservedLparanRparan(token);
                case (int)Symbols.RuleStmUsingIdSemi: //<Stm> ::= using ID ';'
                	return RuleStmUsingIdSemi(token);
                case (int)Symbols.RuleStmUsingQualifiednameSemi: //<Stm> ::= using QualifiedName ';'
                	return RuleStmUsingQualifiednameSemi(token);
                case (int)Symbols.RuleStmUsingStringliteralSemi: //<Stm> ::= using StringLiteral ';'
                	return RuleStmUsingStringliteralSemi(token);
                case (int)Symbols.RuleStmUsingQualifiednameAsIdSemi: //<Stm> ::= using QualifiedName as ID ';'
                	return RuleStmUsingQualifiednameAsIdSemi(token);
                case (int)Symbols.RuleStm: //<Stm> ::= <Normal Stm>
                	return RuleStm(token);
                case (int)Symbols.RuleThenstmIfLparanRparanElse: //<Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>
                	return RuleThenstmIfLparanRparanElse(token);
                case (int)Symbols.RuleThenstmWhileLparanRparan: //<Then Stm> ::= while '(' <Expr> ')' <Then Stm>
                	return RuleThenstmWhileLparanRparan(token);
                case (int)Symbols.RuleThenstmForLparanSemiSemiRparan: //<Then Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Then Stm>
                	return RuleThenstmForLparanSemiSemiRparan(token);
                case (int)Symbols.RuleThenstm: //<Then Stm> ::= <Normal Stm>
                	return RuleThenstm(token);
                case (int)Symbols.RuleNormalstmDoWhileLparanRparan: //<Normal Stm> ::= do <Stm> while '(' <Expr> ')'
                	return RuleNormalstmDoWhileLparanRparan(token);
                case (int)Symbols.RuleNormalstmSwitchLparanRparanLbraceRbrace: //<Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'
                	return RuleNormalstmSwitchLparanRparanLbraceRbrace(token);
                case (int)Symbols.RuleNormalstm: //<Normal Stm> ::= <Block>
                	return RuleNormalstm(token);
                case (int)Symbols.RuleNormalstmSemi: //<Normal Stm> ::= <Expr> ';'
                	return RuleNormalstmSemi(token);
                case (int)Symbols.RuleNormalstmBreakSemi: //<Normal Stm> ::= break ';'
                	return RuleNormalstmBreakSemi(token);
                case (int)Symbols.RuleNormalstmContinueSemi: //<Normal Stm> ::= continue ';'
                	return RuleNormalstmContinueSemi(token);
                case (int)Symbols.RuleNormalstmReturnSemi: //<Normal Stm> ::= return <Expr> ';'
                	return RuleNormalstmReturnSemi(token);
                case (int)Symbols.RuleNormalstmSemi2: //<Normal Stm> ::= ';'
                	return RuleNormalstmSemi2(token);
                case (int)Symbols.RuleArgsComma: //<Args> ::= <Expr> ',' <Args>
                	return RuleArgsComma(token);
                case (int)Symbols.RuleArgs: //<Args> ::= <Expr>
                	return RuleArgs(token);
                case (int)Symbols.RuleCasestmsCaseColon: //<Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>
                	return RuleCasestmsCaseColon(token);
                case (int)Symbols.RuleCasestmsDefaultColon: //<Case Stms> ::= default ':' <Stm List>
                	return RuleCasestmsDefaultColon(token);
                case (int)Symbols.RuleCasestms: //<Case Stms> ::= 
                	return RuleCasestms(token);
                case (int)Symbols.RuleBlockLbraceRbrace: //<Block> ::= '{' <Stm List> '}'
                	return RuleBlockLbraceRbrace(token);
                case (int)Symbols.RuleStmlist: //<Stm List> ::= <Stm> <Stm List>
                	return RuleStmlist(token);
                case (int)Symbols.RuleStmlist2: //<Stm List> ::= 
                	return RuleStmlist2(token);
                case (int)Symbols.RuleFunctionFunctionIdLparanRparan: //<Function> ::= function ID '(' <Param List> ')' <Stm>
                	return RuleFunctionFunctionIdLparanRparan(token);
                case (int)Symbols.RuleFunctionFunctionIdLparanRparan2: //<Function> ::= function ID '(' ')' <Stm>
                	return RuleFunctionFunctionIdLparanRparan2(token);
                case (int)Symbols.RuleFunctionFunctionLparanRparan: //<Function> ::= function '(' <Param List> ')' <Stm>
                	return RuleFunctionFunctionLparanRparan(token);
                case (int)Symbols.RuleFunctionFunctionLparanRparan2: //<Function> ::= function '(' ')' <Stm>
                	return RuleFunctionFunctionLparanRparan2(token);
                case (int)Symbols.RuleParamlistComma: //<Param List> ::= <Param List> ',' <Param decl>
                	return RuleParamlistComma(token);
                case (int)Symbols.RuleParamlist: //<Param List> ::= <Param decl>
                	return RuleParamlist(token);
                case (int)Symbols.RuleParamdeclId: //<Param decl> ::= ID
                	return RuleParamdeclId(token);
                case (int)Symbols.RuleExprlistComma: //<Expr List> ::= <Expr List> ',' <Expr>
                	return RuleExprlistComma(token);
                case (int)Symbols.RuleExprlist: //<Expr List> ::= <Expr>
                	return RuleExprlist(token);
                case (int)Symbols.RuleExprEq: //<Expr> ::= <Op If> '=' <Expr>
                	return RuleExprEq(token);
                case (int)Symbols.RuleExprPluseq: //<Expr> ::= <Op If> '+=' <Expr>
                	return RuleExprPluseq(token);
                case (int)Symbols.RuleExprMinuseq: //<Expr> ::= <Op If> '-=' <Expr>
                	return RuleExprMinuseq(token);
                case (int)Symbols.RuleExprTimeseq: //<Expr> ::= <Op If> '*=' <Expr>
                	return RuleExprTimeseq(token);
                case (int)Symbols.RuleExprDiveq: //<Expr> ::= <Op If> '/=' <Expr>
                	return RuleExprDiveq(token);
                case (int)Symbols.RuleExprLteqeq: //<Expr> ::= <Op If> '<==' <Expr>
                	return RuleExprLteqeq(token);
                case (int)Symbols.RuleExprLteqeqgt: //<Expr> ::= <Op If> '<==>' <Expr>
                	return RuleExprLteqeqgt(token);
                case (int)Symbols.RuleExpr: //<Expr> ::= <Op If>
                	return RuleExpr(token);
                case (int)Symbols.RuleOpifQuestionColon: //<Op If> ::= <Op Or> '?' <Op If> ':' <Op If>
                	return RuleOpifQuestionColon(token);
                case (int)Symbols.RuleOpif: //<Op If> ::= <Op Or>
                	return RuleOpif(token);
                case (int)Symbols.RuleOporOr: //<Op Or> ::= <Op Or> or <Op And>
                	return RuleOporOr(token);
                case (int)Symbols.RuleOpor: //<Op Or> ::= <Op And>
                	return RuleOpor(token);
                case (int)Symbols.RuleOpandAnd: //<Op And> ::= <Op And> and <Op Equate>
                	return RuleOpandAnd(token);
                case (int)Symbols.RuleOpand: //<Op And> ::= <Op Equate>
                	return RuleOpand(token);
                case (int)Symbols.RuleOpequateEqeq: //<Op Equate> ::= <Op Equate> '==' <Op Compare>
                	return RuleOpequateEqeq(token);
                case (int)Symbols.RuleOpequateExclameq: //<Op Equate> ::= <Op Equate> '!=' <Op Compare>
                	return RuleOpequateExclameq(token);
                case (int)Symbols.RuleOpequate: //<Op Equate> ::= <Op Compare>
                	return RuleOpequate(token);
                case (int)Symbols.RuleOpcompareLt: //<Op Compare> ::= <Op Compare> '<' <Op In>
                	return RuleOpcompareLt(token);
                case (int)Symbols.RuleOpcompareGt: //<Op Compare> ::= <Op Compare> '>' <Op In>
                	return RuleOpcompareGt(token);
                case (int)Symbols.RuleOpcompareLteq: //<Op Compare> ::= <Op Compare> '<=' <Op In>
                	return RuleOpcompareLteq(token);
                case (int)Symbols.RuleOpcompareGteq: //<Op Compare> ::= <Op Compare> '>=' <Op In>
                	return RuleOpcompareGteq(token);
                case (int)Symbols.RuleOpcompare: //<Op Compare> ::= <Op In>
                	return RuleOpcompare(token);
                case (int)Symbols.RuleOpinIn: //<Op In> ::= <Op In> in <Op Add>
                	return RuleOpinIn(token);
                case (int)Symbols.RuleOpinInLtCommaGt: //<Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> '>'
                	return RuleOpinInLtCommaGt(token);
                case (int)Symbols.RuleOpinInLtCommaRparan: //<Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> ')'
                	return RuleOpinInLtCommaRparan(token);
                case (int)Symbols.RuleOpinInLparanCommaGt: //<Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> '>'
                	return RuleOpinInLparanCommaGt(token);
                case (int)Symbols.RuleOpinInLparanCommaRparan: //<Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> ')'
                	return RuleOpinInLparanCommaRparan(token);
                case (int)Symbols.RuleOpin: //<Op In> ::= <Op Add>
                	return RuleOpin(token);
                case (int)Symbols.RuleOpaddPlus: //<Op Add> ::= <Op Add> '+' <Op Mult>
                	return RuleOpaddPlus(token);
                case (int)Symbols.RuleOpaddMinus: //<Op Add> ::= <Op Add> '-' <Op Mult>
                	return RuleOpaddMinus(token);
                case (int)Symbols.RuleOpadd: //<Op Add> ::= <Op Mult>
                	return RuleOpadd(token);
                case (int)Symbols.RuleOpmultTimes: //<Op Mult> ::= <Op Mult> '*' <Op Unary>
                	return RuleOpmultTimes(token);
                case (int)Symbols.RuleOpmultDiv: //<Op Mult> ::= <Op Mult> '/' <Op Unary>
                	return RuleOpmultDiv(token);
                case (int)Symbols.RuleOpmultPercent: //<Op Mult> ::= <Op Mult> '%' <Op Unary>
                	return RuleOpmultPercent(token);
                case (int)Symbols.RuleOpmult: //<Op Mult> ::= <Op Unary>
                	return RuleOpmult(token);
                case (int)Symbols.RuleOpunaryNot: //<Op Unary> ::= not <Op Unary>
                	return RuleOpunaryNot(token);
                case (int)Symbols.RuleOpunaryExclam: //<Op Unary> ::= '!' <Op Unary>
                	return RuleOpunaryExclam(token);
                case (int)Symbols.RuleOpunaryMinus: //<Op Unary> ::= '-' <Op Unary>
                	return RuleOpunaryMinus(token);
                case (int)Symbols.RuleOpunaryCastAsId: //<Op Unary> ::= cast <Op Unary> as ID
                	return RuleOpunaryCastAsId(token);
                case (int)Symbols.RuleOpunaryCastAsQualifiedname: //<Op Unary> ::= cast <Op Unary> as QualifiedName
                	return RuleOpunaryCastAsQualifiedname(token);
                case (int)Symbols.RuleOpunaryPlusplus: //<Op Unary> ::= '++' <Op Unary>
                	return RuleOpunaryPlusplus(token);
                case (int)Symbols.RuleOpunaryMinusminus: //<Op Unary> ::= -- <Op Unary>
                	return RuleOpunaryMinusminus(token);
                case (int)Symbols.RuleOpunaryPlusplus2: //<Op Unary> ::= <Op Pointer> '++'
                	return RuleOpunaryPlusplus2(token);
                case (int)Symbols.RuleOpunaryMinusminus2: //<Op Unary> ::= <Op Pointer> --
                	return RuleOpunaryMinusminus2(token);
                case (int)Symbols.RuleOpunaryIsNull: //<Op Unary> ::= <Op Pointer> is null
                	return RuleOpunaryIsNull(token);
                case (int)Symbols.RuleOpunaryNotNull: //<Op Unary> ::= <Op Pointer> not null
                	return RuleOpunaryNotNull(token);
                case (int)Symbols.RuleOpunaryIsNotNull: //<Op Unary> ::= <Op Pointer> is not null
                	return RuleOpunaryIsNotNull(token);
                case (int)Symbols.RuleOpunary: //<Op Unary> ::= <Op Pointer>
                	return RuleOpunary(token);
                case (int)Symbols.RuleOppointerDot: //<Op Pointer> ::= <Op Pointer> '.' <Value>
                	return RuleOppointerDot(token);
                case (int)Symbols.RuleOppointerMinusgt: //<Op Pointer> ::= <Op Pointer> '->' <Value>
                	return RuleOppointerMinusgt(token);
                case (int)Symbols.RuleOppointerLbracketRbracket: //<Op Pointer> ::= <Op Pointer> '[' <Expr> ']'
                	return RuleOppointerLbracketRbracket(token);
                case (int)Symbols.RuleOppointer: //<Op Pointer> ::= <Value>
                	return RuleOppointer(token);
                case (int)Symbols.RuleValueIntliteral: //<Value> ::= IntLiteral
                	return RuleValueIntliteral(token);
                case (int)Symbols.RuleValueStringliteral: //<Value> ::= StringLiteral
                	return RuleValueStringliteral(token);
                case (int)Symbols.RuleValueDecimalliteral: //<Value> ::= DecimalLiteral
                	return RuleValueDecimalliteral(token);
                case (int)Symbols.RuleValueTypeId: //<Value> ::= type ID
                	return RuleValueTypeId(token);
                case (int)Symbols.RuleValueTypeQualifiedname: //<Value> ::= type QualifiedName
                	return RuleValueTypeQualifiedname(token);
                case (int)Symbols.RuleValueIdLparanRparan: //<Value> ::= ID '(' <Args> ')'
                	return RuleValueIdLparanRparan(token);
                case (int)Symbols.RuleValueIdLparanRparan2: //<Value> ::= ID '(' ')'
                	return RuleValueIdLparanRparan2(token);
                case (int)Symbols.RuleValue: //<Value> ::= <Function>
                	return RuleValue(token);
                case (int)Symbols.RuleValueQualifiednameLparanRparan: //<Value> ::= QualifiedName '(' <Args> ')'
                	return RuleValueQualifiednameLparanRparan(token);
                case (int)Symbols.RuleValueQualifiednameLparanRparan2: //<Value> ::= QualifiedName '(' ')'
                	return RuleValueQualifiednameLparanRparan2(token);
                case (int)Symbols.RuleValueNewIdLparanRparan: //<Value> ::= new ID '(' <Args> ')'
                	return RuleValueNewIdLparanRparan(token);
                case (int)Symbols.RuleValueNewIdLparanRparan2: //<Value> ::= new ID '(' ')'
                	return RuleValueNewIdLparanRparan2(token);
                case (int)Symbols.RuleValueNewQualifiednameLparanRparan: //<Value> ::= new QualifiedName '(' <Args> ')'
                	return RuleValueNewQualifiednameLparanRparan(token);
                case (int)Symbols.RuleValueNewQualifiednameLparanRparan2: //<Value> ::= new QualifiedName '(' ')'
                	return RuleValueNewQualifiednameLparanRparan2(token);
                case (int)Symbols.RuleValueId: //<Value> ::= ID
                	return RuleValueId(token);
                case (int)Symbols.RuleValueVarId: //<Value> ::= var ID
                	return RuleValueVarId(token);
                case (int)Symbols.RuleValueStaticId: //<Value> ::= static ID
                	return RuleValueStaticId(token);
                case (int)Symbols.RuleValueLparanRparan: //<Value> ::= '(' <Expr> ')'
                	return RuleValueLparanRparan(token);
                case (int)Symbols.RuleValueLbraceRbrace: //<Value> ::= '{' <Expr List> '}'
                	return RuleValueLbraceRbrace(token);
                case (int)Symbols.RuleValueNull: //<Value> ::= null
                	return RuleValueNull(token);
                case (int)Symbols.RuleValueTrue: //<Value> ::= true
                	return RuleValueTrue(token);
                case (int)Symbols.RuleValueFalse: //<Value> ::= false
                	return RuleValueFalse(token);
            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            throw new Exception(message);
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            throw new Exception(message);
        }

		#endregion

		#region Templates for user overrided rules functions
		/*
		// <Stm> ::= if '(' <Expr> ')' <Stm>
		protected override object RuleStmIfLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= if '(' <Expr> ')' <Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>
		protected override object RuleStmIfLparanRparanElse(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= while '(' <Expr> ')' <Stm>
		protected override object RuleStmWhileLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= while '(' <Expr> ')' <Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>
		protected override object RuleStmForLparanSemiSemiRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= foreach '(' ID in <Expr> ')' <Stm>
		protected override object RuleStmForeachLparanIdInRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= foreach '(' ID in <Expr> ')' <Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= observed '(' <Expr> ')' <Stm>
		protected override object RuleStmObservedLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= observed '(' <Expr> ')' <Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= using ID ';'
		protected override object RuleStmUsingIdSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using ID ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= using QualifiedName ';'
		protected override object RuleStmUsingQualifiednameSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using QualifiedName ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= using StringLiteral ';'
		protected override object RuleStmUsingStringliteralSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using StringLiteral ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= using QualifiedName as ID ';'
		protected override object RuleStmUsingQualifiednameAsIdSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using QualifiedName as ID ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= <Normal Stm>
		protected override object RuleStm(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= <Normal Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>
		protected override object RuleThenstmIfLparanRparanElse(NonterminalToken token)
		{
			throw new NotImplementedException("<Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Then Stm> ::= while '(' <Expr> ')' <Then Stm>
		protected override object RuleThenstmWhileLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Then Stm> ::= while '(' <Expr> ')' <Then Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Then Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Then Stm>
		protected override object RuleThenstmForLparanSemiSemiRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Then Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Then Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Then Stm> ::= <Normal Stm>
		protected override object RuleThenstm(NonterminalToken token)
		{
			throw new NotImplementedException("<Then Stm> ::= <Normal Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Normal Stm> ::= do <Stm> while '(' <Expr> ')'
		protected override object RuleNormalstmDoWhileLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= do <Stm> while '(' <Expr> ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'
		protected override object RuleNormalstmSwitchLparanRparanLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Normal Stm> ::= <Block>
		protected override object RuleNormalstm(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= <Block>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Normal Stm> ::= <Expr> ';'
		protected override object RuleNormalstmSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= <Expr> ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Normal Stm> ::= break ';'
		protected override object RuleNormalstmBreakSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= break ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Normal Stm> ::= continue ';'
		protected override object RuleNormalstmContinueSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= continue ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Normal Stm> ::= return <Expr> ';'
		protected override object RuleNormalstmReturnSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= return <Expr> ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Normal Stm> ::= ';'
		protected override object RuleNormalstmSemi2(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Args> ::= <Expr> ',' <Args>
		protected override object RuleArgsComma(NonterminalToken token)
		{
			throw new NotImplementedException("<Args> ::= <Expr> ',' <Args>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Args> ::= <Expr>
		protected override object RuleArgs(NonterminalToken token)
		{
			throw new NotImplementedException("<Args> ::= <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>
		protected override object RuleCasestmsCaseColon(NonterminalToken token)
		{
			throw new NotImplementedException("<Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Case Stms> ::= default ':' <Stm List>
		protected override object RuleCasestmsDefaultColon(NonterminalToken token)
		{
			throw new NotImplementedException("<Case Stms> ::= default ':' <Stm List>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Case Stms> ::= 
		protected override object RuleCasestms(NonterminalToken token)
		{
			throw new NotImplementedException("<Case Stms> ::= ");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Block> ::= '{' <Stm List> '}'
		protected override object RuleBlockLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<Block> ::= '{' <Stm List> '}'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm List> ::= <Stm> <Stm List>
		protected override object RuleStmlist(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm List> ::= <Stm> <Stm List>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm List> ::= 
		protected override object RuleStmlist2(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm List> ::= ");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Function> ::= function ID '(' <Param List> ')' <Stm>
		protected override object RuleFunctionFunctionIdLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Function> ::= function ID '(' <Param List> ')' <Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Function> ::= function ID '(' ')' <Stm>
		protected override object RuleFunctionFunctionIdLparanRparan2(NonterminalToken token)
		{
			throw new NotImplementedException("<Function> ::= function ID '(' ')' <Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Function> ::= function '(' <Param List> ')' <Stm>
		protected override object RuleFunctionFunctionLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Function> ::= function '(' <Param List> ')' <Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Function> ::= function '(' ')' <Stm>
		protected override object RuleFunctionFunctionLparanRparan2(NonterminalToken token)
		{
			throw new NotImplementedException("<Function> ::= function '(' ')' <Stm>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Param List> ::= <Param List> ',' <Param decl>
		protected override object RuleParamlistComma(NonterminalToken token)
		{
			throw new NotImplementedException("<Param List> ::= <Param List> ',' <Param decl>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Param List> ::= <Param decl>
		protected override object RuleParamlist(NonterminalToken token)
		{
			throw new NotImplementedException("<Param List> ::= <Param decl>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Param decl> ::= ID
		protected override object RuleParamdeclId(NonterminalToken token)
		{
			throw new NotImplementedException("<Param decl> ::= ID");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Expr List> ::= <Expr List> ',' <Expr>
		protected override object RuleExprlistComma(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr List> ::= <Expr List> ',' <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Expr List> ::= <Expr>
		protected override object RuleExprlist(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr List> ::= <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Expr> ::= <Op If> '=' <Expr>
		protected override object RuleExprEq(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op If> '=' <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Expr> ::= <Op If> '+=' <Expr>
		protected override object RuleExprPluseq(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op If> '+=' <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Expr> ::= <Op If> '-=' <Expr>
		protected override object RuleExprMinuseq(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op If> '-=' <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Expr> ::= <Op If> '*=' <Expr>
		protected override object RuleExprTimeseq(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op If> '*=' <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Expr> ::= <Op If> '/=' <Expr>
		protected override object RuleExprDiveq(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op If> '/=' <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Expr> ::= <Op If> '<==' <Expr>
		protected override object RuleExprLteqeq(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op If> '<==' <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Expr> ::= <Op If> '<==>' <Expr>
		protected override object RuleExprLteqeqgt(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op If> '<==>' <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Expr> ::= <Op If>
		protected override object RuleExpr(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op If>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op If> ::= <Op Or> '?' <Op If> ':' <Op If>
		protected override object RuleOpifQuestionColon(NonterminalToken token)
		{
			throw new NotImplementedException("<Op If> ::= <Op Or> '?' <Op If> ':' <Op If>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op If> ::= <Op Or>
		protected override object RuleOpif(NonterminalToken token)
		{
			throw new NotImplementedException("<Op If> ::= <Op Or>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Or> ::= <Op Or> or <Op And>
		protected override object RuleOporOr(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Or> ::= <Op Or> or <Op And>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Or> ::= <Op And>
		protected override object RuleOpor(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Or> ::= <Op And>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op And> ::= <Op And> and <Op Equate>
		protected override object RuleOpandAnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Op And> ::= <Op And> and <Op Equate>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op And> ::= <Op Equate>
		protected override object RuleOpand(NonterminalToken token)
		{
			throw new NotImplementedException("<Op And> ::= <Op Equate>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Equate> ::= <Op Equate> '==' <Op Compare>
		protected override object RuleOpequateEqeq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Equate> ::= <Op Equate> '==' <Op Compare>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Equate> ::= <Op Equate> '!=' <Op Compare>
		protected override object RuleOpequateExclameq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Equate> ::= <Op Equate> '!=' <Op Compare>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Equate> ::= <Op Compare>
		protected override object RuleOpequate(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Equate> ::= <Op Compare>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Compare> ::= <Op Compare> '<' <Op In>
		protected override object RuleOpcompareLt(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Compare> ::= <Op Compare> '<' <Op In>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Compare> ::= <Op Compare> '>' <Op In>
		protected override object RuleOpcompareGt(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Compare> ::= <Op Compare> '>' <Op In>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Compare> ::= <Op Compare> '<=' <Op In>
		protected override object RuleOpcompareLteq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Compare> ::= <Op Compare> '<=' <Op In>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Compare> ::= <Op Compare> '>=' <Op In>
		protected override object RuleOpcompareGteq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Compare> ::= <Op Compare> '>=' <Op In>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Compare> ::= <Op In>
		protected override object RuleOpcompare(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Compare> ::= <Op In>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op In> ::= <Op In> in <Op Add>
		protected override object RuleOpinIn(NonterminalToken token)
		{
			throw new NotImplementedException("<Op In> ::= <Op In> in <Op Add>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> '>'
		protected override object RuleOpinInLtCommaGt(NonterminalToken token)
		{
			throw new NotImplementedException("<Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> '>'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> ')'
		protected override object RuleOpinInLtCommaRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> '>'
		protected override object RuleOpinInLparanCommaGt(NonterminalToken token)
		{
			throw new NotImplementedException("<Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> '>'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> ')'
		protected override object RuleOpinInLparanCommaRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op In> ::= <Op Add>
		protected override object RuleOpin(NonterminalToken token)
		{
			throw new NotImplementedException("<Op In> ::= <Op Add>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Add> ::= <Op Add> '+' <Op Mult>
		protected override object RuleOpaddPlus(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Add> ::= <Op Add> '+' <Op Mult>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Add> ::= <Op Add> '-' <Op Mult>
		protected override object RuleOpaddMinus(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Add> ::= <Op Add> '-' <Op Mult>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Add> ::= <Op Mult>
		protected override object RuleOpadd(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Add> ::= <Op Mult>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Mult> ::= <Op Mult> '*' <Op Unary>
		protected override object RuleOpmultTimes(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Mult> ::= <Op Mult> '*' <Op Unary>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Mult> ::= <Op Mult> '/' <Op Unary>
		protected override object RuleOpmultDiv(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Mult> ::= <Op Mult> '/' <Op Unary>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Mult> ::= <Op Mult> '%' <Op Unary>
		protected override object RuleOpmultPercent(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Mult> ::= <Op Mult> '%' <Op Unary>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Mult> ::= <Op Unary>
		protected override object RuleOpmult(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Mult> ::= <Op Unary>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= not <Op Unary>
		protected override object RuleOpunaryNot(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= not <Op Unary>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= '!' <Op Unary>
		protected override object RuleOpunaryExclam(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= '!' <Op Unary>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= '-' <Op Unary>
		protected override object RuleOpunaryMinus(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= '-' <Op Unary>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= cast <Op Unary> as ID
		protected override object RuleOpunaryCastAsId(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= cast <Op Unary> as ID");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= cast <Op Unary> as QualifiedName
		protected override object RuleOpunaryCastAsQualifiedname(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= cast <Op Unary> as QualifiedName");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= '++' <Op Unary>
		protected override object RuleOpunaryPlusplus(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= '++' <Op Unary>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= -- <Op Unary>
		protected override object RuleOpunaryMinusminus(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= -- <Op Unary>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= <Op Pointer> '++'
		protected override object RuleOpunaryPlusplus2(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer> '++'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= <Op Pointer> --
		protected override object RuleOpunaryMinusminus2(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer> --");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= <Op Pointer> is null
		protected override object RuleOpunaryIsNull(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer> is null");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= <Op Pointer> not null
		protected override object RuleOpunaryNotNull(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer> not null");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= <Op Pointer> is not null
		protected override object RuleOpunaryIsNotNull(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer> is not null");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Unary> ::= <Op Pointer>
		protected override object RuleOpunary(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Pointer> ::= <Op Pointer> '.' <Value>
		protected override object RuleOppointerDot(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Pointer> ::= <Op Pointer> '.' <Value>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Pointer> ::= <Op Pointer> '->' <Value>
		protected override object RuleOppointerMinusgt(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Pointer> ::= <Op Pointer> '->' <Value>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Pointer> ::= <Op Pointer> '[' <Expr> ']'
		protected override object RuleOppointerLbracketRbracket(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Pointer> ::= <Op Pointer> '[' <Expr> ']'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Pointer> ::= <Value>
		protected override object RuleOppointer(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Pointer> ::= <Value>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= IntLiteral
		protected override object RuleValueIntliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= IntLiteral");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= StringLiteral
		protected override object RuleValueStringliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= StringLiteral");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= DecimalLiteral
		protected override object RuleValueDecimalliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= DecimalLiteral");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= type ID
		protected override object RuleValueTypeId(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= type ID");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= type QualifiedName
		protected override object RuleValueTypeQualifiedname(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= type QualifiedName");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= ID '(' <Args> ')'
		protected override object RuleValueIdLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= ID '(' <Args> ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= ID '(' ')'
		protected override object RuleValueIdLparanRparan2(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= ID '(' ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= <Function>
		protected override object RuleValue(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= <Function>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= QualifiedName '(' <Args> ')'
		protected override object RuleValueQualifiednameLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= QualifiedName '(' <Args> ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= QualifiedName '(' ')'
		protected override object RuleValueQualifiednameLparanRparan2(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= QualifiedName '(' ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= new ID '(' <Args> ')'
		protected override object RuleValueNewIdLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= new ID '(' <Args> ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= new ID '(' ')'
		protected override object RuleValueNewIdLparanRparan2(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= new ID '(' ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= new QualifiedName '(' <Args> ')'
		protected override object RuleValueNewQualifiednameLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= new QualifiedName '(' <Args> ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= new QualifiedName '(' ')'
		protected override object RuleValueNewQualifiednameLparanRparan2(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= new QualifiedName '(' ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= ID
		protected override object RuleValueId(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= ID");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= var ID
		protected override object RuleValueVarId(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= var ID");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= static ID
		protected override object RuleValueStaticId(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= static ID");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= '(' <Expr> ')'
		protected override object RuleValueLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= '(' <Expr> ')'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= '{' <Expr List> '}'
		protected override object RuleValueLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= '{' <Expr List> '}'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= null
		protected override object RuleValueNull(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= null");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= true
		protected override object RuleValueTrue(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= true");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= false
		protected override object RuleValueFalse(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= false");
			//CheckRule(token, Symbols);
			//return new
		}

		*/
		#endregion

		#region Test templates for user overrided rules functions
		/*
			Test(@"<Stm> ::= if '(' <Expr> ')' <Stm>");
			Test(@"<Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>");
			Test(@"<Stm> ::= while '(' <Expr> ')' <Stm>");
			Test(@"<Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>");
			Test(@"<Stm> ::= foreach '(' ID in <Expr> ')' <Stm>");
			Test(@"<Stm> ::= observed '(' <Expr> ')' <Stm>");
			Test(@"<Stm> ::= using ID ';'");
			Test(@"<Stm> ::= using QualifiedName ';'");
			Test(@"<Stm> ::= using StringLiteral ';'");
			Test(@"<Stm> ::= using QualifiedName as ID ';'");
			Test(@"<Stm> ::= <Normal Stm>");
			Test(@"<Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>");
			Test(@"<Then Stm> ::= while '(' <Expr> ')' <Then Stm>");
			Test(@"<Then Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Then Stm>");
			Test(@"<Then Stm> ::= <Normal Stm>");
			Test(@"<Normal Stm> ::= do <Stm> while '(' <Expr> ')'");
			Test(@"<Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'");
			Test(@"<Normal Stm> ::= <Block>");
			Test(@"<Normal Stm> ::= <Expr> ';'");
			Test(@"<Normal Stm> ::= break ';'");
			Test(@"<Normal Stm> ::= continue ';'");
			Test(@"<Normal Stm> ::= return <Expr> ';'");
			Test(@"<Normal Stm> ::= ';'");
			Test(@"<Args> ::= <Expr> ',' <Args>");
			Test(@"<Args> ::= <Expr>");
			Test(@"<Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>");
			Test(@"<Case Stms> ::= default ':' <Stm List>");
			Test(@"<Case Stms> ::= ");
			Test(@"<Block> ::= '{' <Stm List> '}'");
			Test(@"<Stm List> ::= <Stm> <Stm List>");
			Test(@"<Stm List> ::= ");
			Test(@"<Function> ::= function ID '(' <Param List> ')' <Stm>");
			Test(@"<Function> ::= function ID '(' ')' <Stm>");
			Test(@"<Function> ::= function '(' <Param List> ')' <Stm>");
			Test(@"<Function> ::= function '(' ')' <Stm>");
			Test(@"<Param List> ::= <Param List> ',' <Param decl>");
			Test(@"<Param List> ::= <Param decl>");
			Test(@"<Param decl> ::= ID");
			Test(@"<Expr List> ::= <Expr List> ',' <Expr>");
			Test(@"<Expr List> ::= <Expr>");
			Test(@"<Expr> ::= <Op If> '=' <Expr>");
			Test(@"<Expr> ::= <Op If> '+=' <Expr>");
			Test(@"<Expr> ::= <Op If> '-=' <Expr>");
			Test(@"<Expr> ::= <Op If> '*=' <Expr>");
			Test(@"<Expr> ::= <Op If> '/=' <Expr>");
			Test(@"<Expr> ::= <Op If> '<==' <Expr>");
			Test(@"<Expr> ::= <Op If> '<==>' <Expr>");
			Test(@"<Expr> ::= <Op If>");
			Test(@"<Op If> ::= <Op Or> '?' <Op If> ':' <Op If>");
			Test(@"<Op If> ::= <Op Or>");
			Test(@"<Op Or> ::= <Op Or> or <Op And>");
			Test(@"<Op Or> ::= <Op And>");
			Test(@"<Op And> ::= <Op And> and <Op Equate>");
			Test(@"<Op And> ::= <Op Equate>");
			Test(@"<Op Equate> ::= <Op Equate> '==' <Op Compare>");
			Test(@"<Op Equate> ::= <Op Equate> '!=' <Op Compare>");
			Test(@"<Op Equate> ::= <Op Compare>");
			Test(@"<Op Compare> ::= <Op Compare> '<' <Op In>");
			Test(@"<Op Compare> ::= <Op Compare> '>' <Op In>");
			Test(@"<Op Compare> ::= <Op Compare> '<=' <Op In>");
			Test(@"<Op Compare> ::= <Op Compare> '>=' <Op In>");
			Test(@"<Op Compare> ::= <Op In>");
			Test(@"<Op In> ::= <Op In> in <Op Add>");
			Test(@"<Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> '>'");
			Test(@"<Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> ')'");
			Test(@"<Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> '>'");
			Test(@"<Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> ')'");
			Test(@"<Op In> ::= <Op Add>");
			Test(@"<Op Add> ::= <Op Add> '+' <Op Mult>");
			Test(@"<Op Add> ::= <Op Add> '-' <Op Mult>");
			Test(@"<Op Add> ::= <Op Mult>");
			Test(@"<Op Mult> ::= <Op Mult> '*' <Op Unary>");
			Test(@"<Op Mult> ::= <Op Mult> '/' <Op Unary>");
			Test(@"<Op Mult> ::= <Op Mult> '%' <Op Unary>");
			Test(@"<Op Mult> ::= <Op Unary>");
			Test(@"<Op Unary> ::= not <Op Unary>");
			Test(@"<Op Unary> ::= '!' <Op Unary>");
			Test(@"<Op Unary> ::= '-' <Op Unary>");
			Test(@"<Op Unary> ::= cast <Op Unary> as ID");
			Test(@"<Op Unary> ::= cast <Op Unary> as QualifiedName");
			Test(@"<Op Unary> ::= '++' <Op Unary>");
			Test(@"<Op Unary> ::= -- <Op Unary>");
			Test(@"<Op Unary> ::= <Op Pointer> '++'");
			Test(@"<Op Unary> ::= <Op Pointer> --");
			Test(@"<Op Unary> ::= <Op Pointer> is null");
			Test(@"<Op Unary> ::= <Op Pointer> not null");
			Test(@"<Op Unary> ::= <Op Pointer> is not null");
			Test(@"<Op Unary> ::= <Op Pointer>");
			Test(@"<Op Pointer> ::= <Op Pointer> '.' <Value>");
			Test(@"<Op Pointer> ::= <Op Pointer> '->' <Value>");
			Test(@"<Op Pointer> ::= <Op Pointer> '[' <Expr> ']'");
			Test(@"<Op Pointer> ::= <Value>");
			Test(@"<Value> ::= IntLiteral");
			Test(@"<Value> ::= StringLiteral");
			Test(@"<Value> ::= DecimalLiteral");
			Test(@"<Value> ::= type ID");
			Test(@"<Value> ::= type QualifiedName");
			Test(@"<Value> ::= ID '(' <Args> ')'");
			Test(@"<Value> ::= ID '(' ')'");
			Test(@"<Value> ::= <Function>");
			Test(@"<Value> ::= QualifiedName '(' <Args> ')'");
			Test(@"<Value> ::= QualifiedName '(' ')'");
			Test(@"<Value> ::= new ID '(' <Args> ')'");
			Test(@"<Value> ::= new ID '(' ')'");
			Test(@"<Value> ::= new QualifiedName '(' <Args> ')'");
			Test(@"<Value> ::= new QualifiedName '(' ')'");
			Test(@"<Value> ::= ID");
			Test(@"<Value> ::= var ID");
			Test(@"<Value> ::= static ID");
			Test(@"<Value> ::= '(' <Expr> ')'");
			Test(@"<Value> ::= '{' <Expr List> '}'");
			Test(@"<Value> ::= null");
			Test(@"<Value> ::= true");
			Test(@"<Value> ::= false");
		*/
		#endregion

    }
}
