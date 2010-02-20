namespace LPS.ToolScript
{

    /// <sumarry>Symboly</summary>
    enum SymbolConstants : int
    {
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
        Value = 70 

    };

    /// <sumarry>Pravidla</summary>
    enum RuleConstants : int
    {
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        StmIfLparanRparan = 0,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Stm&gt;</c></para>
        /// </summary>
        StmIfLparanRparanElse = 1,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        StmWhileLparanRparan = 2,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        StmForLparanSemiSemiRparan = 3,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= foreach '(' ID in &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        StmForeachLparanIdInRparan = 4,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        Stm = 5,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Then Stm&gt;</c></para>
        /// </summary>
        ThenstmIfLparanRparanElse = 6,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        ThenstmWhileLparanRparan = 7,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        ThenstmForLparanSemiSemiRparan = 8,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        Thenstm = 9,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= do &lt;Stm&gt; while '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        NormalstmDoWhileLparanRparan = 10,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= switch '(' &lt;Expr&gt; ')' '{' &lt;Case Stms&gt; '}'</c></para>
        /// </summary>
        NormalstmSwitchLparanRparanLbraceRbrace = 11,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Block&gt;</c></para>
        /// </summary>
        Normalstm = 12,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Expr&gt; ';'</c></para>
        /// </summary>
        NormalstmSemi = 13,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= break ';'</c></para>
        /// </summary>
        NormalstmBreakSemi = 14,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= continue ';'</c></para>
        /// </summary>
        NormalstmContinueSemi = 15,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= return &lt;Expr&gt; ';'</c></para>
        /// </summary>
        NormalstmReturnSemi = 16,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= ';'</c></para>
        /// </summary>
        NormalstmSemi2 = 17,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Expr&gt; ',' &lt;Args&gt;</c></para>
        /// </summary>
        ArgsComma = 18,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Expr&gt;</c></para>
        /// </summary>
        Args = 19,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= case &lt;Value&gt; ':' &lt;Stm List&gt; &lt;Case Stms&gt;</c></para>
        /// </summary>
        CasestmsCaseColon = 20,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= default ':' &lt;Stm List&gt;</c></para>
        /// </summary>
        CasestmsDefaultColon = 21,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= </c></para>
        /// </summary>
        Casestms = 22,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Block&gt; ::= '{' &lt;Stm List&gt; '}'</c></para>
        /// </summary>
        BlockLbraceRbrace = 23,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= &lt;Stm&gt; &lt;Stm List&gt;</c></para>
        /// </summary>
        Stmlist = 24,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= </c></para>
        /// </summary>
        Stmlist2 = 25,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt; '=' &lt;Expr&gt;</c></para>
        /// </summary>
        ExprEq = 26,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op If&gt;</c></para>
        /// </summary>
        Expr = 27,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt; '?' &lt;Op If&gt; ':' &lt;Op If&gt;</c></para>
        /// </summary>
        OpifQuestionColon = 28,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt;</c></para>
        /// </summary>
        Opif = 29,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op Or&gt; or &lt;Op And&gt;</c></para>
        /// </summary>
        OporOr = 30,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op And&gt;</c></para>
        /// </summary>
        Opor = 31,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op And&gt; and &lt;Op Equate&gt;</c></para>
        /// </summary>
        OpandAnd = 32,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op Equate&gt;</c></para>
        /// </summary>
        Opand = 33,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '==' &lt;Op Compare&gt;</c></para>
        /// </summary>
        OpequateEqeq = 34,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '!=' &lt;Op Compare&gt;</c></para>
        /// </summary>
        OpequateExclameq = 35,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Compare&gt;</c></para>
        /// </summary>
        Opequate = 36,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;' &lt;Op Add&gt;</c></para>
        /// </summary>
        OpcompareLt = 37,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;' &lt;Op Add&gt;</c></para>
        /// </summary>
        OpcompareGt = 38,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;=' &lt;Op Add&gt;</c></para>
        /// </summary>
        OpcompareLteq = 39,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;=' &lt;Op Add&gt;</c></para>
        /// </summary>
        OpcompareGteq = 40,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Add&gt;</c></para>
        /// </summary>
        Opcompare = 41,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '+' &lt;Op Mult&gt;</c></para>
        /// </summary>
        OpaddPlus = 42,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '-' &lt;Op Mult&gt;</c></para>
        /// </summary>
        OpaddMinus = 43,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Mult&gt;</c></para>
        /// </summary>
        Opadd = 44,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '*' &lt;Op Unary&gt;</c></para>
        /// </summary>
        OpmultTimes = 45,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '/' &lt;Op Unary&gt;</c></para>
        /// </summary>
        OpmultDiv = 46,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '%' &lt;Op Unary&gt;</c></para>
        /// </summary>
        OpmultPercent = 47,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Unary&gt;</c></para>
        /// </summary>
        Opmult = 48,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= not &lt;Op Unary&gt;</c></para>
        /// </summary>
        OpunaryNot = 49,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= '-' &lt;Op Unary&gt;</c></para>
        /// </summary>
        OpunaryMinus = 50,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= cast &lt;Op Unary&gt; as ID</c></para>
        /// </summary>
        OpunaryCastAsId = 51,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt;</c></para>
        /// </summary>
        Opunary = 52,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '.' &lt;Value&gt;</c></para>
        /// </summary>
        OppointerDot = 53,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '-&gt;' &lt;Value&gt;</c></para>
        /// </summary>
        OppointerMinusgt = 54,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '[' &lt;Expr&gt; ']'</c></para>
        /// </summary>
        OppointerLbracketRbracket = 55,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Value&gt;</c></para>
        /// </summary>
        Oppointer = 56,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= IntLiteral</c></para>
        /// </summary>
        ValueIntliteral = 57,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= StringLiteral</c></para>
        /// </summary>
        ValueStringliteral = 58,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= DecimalLiteral</c></para>
        /// </summary>
        ValueDecimalliteral = 59,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= type ID</c></para>
        /// </summary>
        ValueTypeId = 60,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID '(' &lt;Args&gt; ')'</c></para>
        /// </summary>
        ValueIdLparanRparan = 61,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID '(' ')'</c></para>
        /// </summary>
        ValueIdLparanRparan2 = 62,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID</c></para>
        /// </summary>
        ValueId = 63,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        ValueLparanRparan = 64 

    };
}

