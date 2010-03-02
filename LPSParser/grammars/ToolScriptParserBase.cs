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
        /// <para>Symbol: after</para>
        /// <para><c>after</c></para>
        /// </summary>
        After = 39,

        /// <summary>
        /// <para>Symbol: and</para>
        /// <para><c>and</c></para>
        /// </summary>
        And = 40,

        /// <summary>
        /// <para>Symbol: as</para>
        /// <para><c>as</c></para>
        /// </summary>
        As = 41,

        /// <summary>
        /// <para>Symbol: before</para>
        /// <para><c>before</c></para>
        /// </summary>
        Before = 42,

        /// <summary>
        /// <para>Symbol: bool</para>
        /// <para><c>bool</c></para>
        /// </summary>
        Bool = 43,

        /// <summary>
        /// <para>Symbol: break</para>
        /// <para><c>break</c></para>
        /// </summary>
        Break = 44,

        /// <summary>
        /// <para>Symbol: button</para>
        /// <para><c>button</c></para>
        /// </summary>
        Button = 45,

        /// <summary>
        /// <para>Symbol: case</para>
        /// <para><c>case</c></para>
        /// </summary>
        Case = 46,

        /// <summary>
        /// <para>Symbol: cast</para>
        /// <para><c>cast</c></para>
        /// </summary>
        Cast = 47,

        /// <summary>
        /// <para>Symbol: catch</para>
        /// <para><c>catch</c></para>
        /// </summary>
        Catch = 48,

        /// <summary>
        /// <para>Symbol: continue</para>
        /// <para><c>continue</c></para>
        /// </summary>
        Continue = 49,

        /// <summary>
        /// <para>Symbol: database</para>
        /// <para><c>database</c></para>
        /// </summary>
        Database = 50,

        /// <summary>
        /// <para>Symbol: date</para>
        /// <para><c>date</c></para>
        /// </summary>
        Date = 51,

        /// <summary>
        /// <para>Symbol: daterange</para>
        /// <para><c>daterange</c></para>
        /// </summary>
        Daterange = 52,

        /// <summary>
        /// <para>Symbol: datetime</para>
        /// <para><c>datetime</c></para>
        /// </summary>
        Datetime = 53,

        /// <summary>
        /// <para>Symbol: DateTimeLiteral</para>
        /// <para><c>DateTimeLiteral</c></para>
        /// </summary>
        Datetimeliteral = 54,

        /// <summary>
        /// <para>Symbol: datetimerange</para>
        /// <para><c>datetimerange</c></para>
        /// </summary>
        Datetimerange = 55,

        /// <summary>
        /// <para>Symbol: decimal</para>
        /// <para><c>decimal</c></para>
        /// </summary>
        Decimal = 56,

        /// <summary>
        /// <para>Symbol: DecimalLiteral</para>
        /// <para><c>DecimalLiteral</c></para>
        /// </summary>
        Decimalliteral = 57,

        /// <summary>
        /// <para>Symbol: default</para>
        /// <para><c>default</c></para>
        /// </summary>
        Default = 58,

        /// <summary>
        /// <para>Symbol: delete</para>
        /// <para><c>delete</c></para>
        /// </summary>
        Delete = 59,

        /// <summary>
        /// <para>Symbol: dict</para>
        /// <para><c>dict</c></para>
        /// </summary>
        Dict = 60,

        /// <summary>
        /// <para>Symbol: do</para>
        /// <para><c>do</c></para>
        /// </summary>
        Do = 61,

        /// <summary>
        /// <para>Symbol: else</para>
        /// <para><c>else</c></para>
        /// </summary>
        Else = 62,

        /// <summary>
        /// <para>Symbol: end</para>
        /// <para><c>end</c></para>
        /// </summary>
        End = 63,

        /// <summary>
        /// <para>Symbol: extends</para>
        /// <para><c>extends</c></para>
        /// </summary>
        Extends = 64,

        /// <summary>
        /// <para>Symbol: false</para>
        /// <para><c>false</c></para>
        /// </summary>
        False = 65,

        /// <summary>
        /// <para>Symbol: finally</para>
        /// <para><c>finally</c></para>
        /// </summary>
        Finally = 66,

        /// <summary>
        /// <para>Symbol: for</para>
        /// <para><c>for</c></para>
        /// </summary>
        For = 67,

        /// <summary>
        /// <para>Symbol: foreach</para>
        /// <para><c>foreach</c></para>
        /// </summary>
        Foreach = 68,

        /// <summary>
        /// <para>Symbol: foreign</para>
        /// <para><c>foreign</c></para>
        /// </summary>
        Foreign = 69,

        /// <summary>
        /// <para>Symbol: function</para>
        /// <para><c>function</c></para>
        /// </summary>
        Function = 70,

        /// <summary>
        /// <para>Symbol: get</para>
        /// <para><c>get</c></para>
        /// </summary>
        Get = 71,

        /// <summary>
        /// <para>Symbol: hbox</para>
        /// <para><c>hbox</c></para>
        /// </summary>
        Hbox = 72,

        /// <summary>
        /// <para>Symbol: hbuttonbox</para>
        /// <para><c>hbuttonbox</c></para>
        /// </summary>
        Hbuttonbox = 73,

        /// <summary>
        /// <para>Symbol: ID</para>
        /// <para><c>ID</c></para>
        /// </summary>
        Id = 74,

        /// <summary>
        /// <para>Symbol: if</para>
        /// <para><c>if</c></para>
        /// </summary>
        If = 75,

        /// <summary>
        /// <para>Symbol: image</para>
        /// <para><c>image</c></para>
        /// </summary>
        Image = 76,

        /// <summary>
        /// <para>Symbol: in</para>
        /// <para><c>in</c></para>
        /// </summary>
        In = 77,

        /// <summary>
        /// <para>Symbol: index</para>
        /// <para><c>index</c></para>
        /// </summary>
        Index = 78,

        /// <summary>
        /// <para>Symbol: insert</para>
        /// <para><c>insert</c></para>
        /// </summary>
        Insert = 79,

        /// <summary>
        /// <para>Symbol: integer</para>
        /// <para><c>integer</c></para>
        /// </summary>
        Integer = 80,

        /// <summary>
        /// <para>Symbol: IntLiteral</para>
        /// <para><c>IntLiteral</c></para>
        /// </summary>
        Intliteral = 81,

        /// <summary>
        /// <para>Symbol: is</para>
        /// <para><c>is</c></para>
        /// </summary>
        Is = 82,

        /// <summary>
        /// <para>Symbol: list</para>
        /// <para><c>list</c></para>
        /// </summary>
        List = 83,

        /// <summary>
        /// <para>Symbol: many</para>
        /// <para><c>many</c></para>
        /// </summary>
        Many = 84,

        /// <summary>
        /// <para>Symbol: menu</para>
        /// <para><c>menu</c></para>
        /// </summary>
        Menu = 85,

        /// <summary>
        /// <para>Symbol: menuitem</para>
        /// <para><c>menuitem</c></para>
        /// </summary>
        Menuitem = 86,

        /// <summary>
        /// <para>Symbol: modified</para>
        /// <para><c>modified</c></para>
        /// </summary>
        Modified = 87,

        /// <summary>
        /// <para>Symbol: new</para>
        /// <para><c>new</c></para>
        /// </summary>
        New = 88,

        /// <summary>
        /// <para>Symbol: not</para>
        /// <para><c>not</c></para>
        /// </summary>
        Not = 89,

        /// <summary>
        /// <para>Symbol: null</para>
        /// <para><c>null</c></para>
        /// </summary>
        Null = 90,

        /// <summary>
        /// <para>Symbol: observed</para>
        /// <para><c>observed</c></para>
        /// </summary>
        Observed = 91,

        /// <summary>
        /// <para>Symbol: or</para>
        /// <para><c>or</c></para>
        /// </summary>
        Or = 92,

        /// <summary>
        /// <para>Symbol: position</para>
        /// <para><c>position</c></para>
        /// </summary>
        Position = 93,

        /// <summary>
        /// <para>Symbol: primary</para>
        /// <para><c>primary</c></para>
        /// </summary>
        Primary = 94,

        /// <summary>
        /// <para>Symbol: property</para>
        /// <para><c>property</c></para>
        /// </summary>
        Property = 95,

        /// <summary>
        /// <para>Symbol: range</para>
        /// <para><c>range</c></para>
        /// </summary>
        Range = 96,

        /// <summary>
        /// <para>Symbol: return</para>
        /// <para><c>return</c></para>
        /// </summary>
        Return = 97,

        /// <summary>
        /// <para>Symbol: select</para>
        /// <para><c>select</c></para>
        /// </summary>
        Select = 98,

        /// <summary>
        /// <para>Symbol: Separator</para>
        /// <para><c>Separator</c></para>
        /// </summary>
        Separator = 99,

        /// <summary>
        /// <para>Symbol: set</para>
        /// <para><c>set</c></para>
        /// </summary>
        Set = 100,

        /// <summary>
        /// <para>Symbol: static</para>
        /// <para><c>static</c></para>
        /// </summary>
        Static = 101,

        /// <summary>
        /// <para>Symbol: StringLiteral</para>
        /// <para><c>StringLiteral</c></para>
        /// </summary>
        Stringliteral = 102,

        /// <summary>
        /// <para>Symbol: switch</para>
        /// <para><c>switch</c></para>
        /// </summary>
        Switch = 103,

        /// <summary>
        /// <para>Symbol: table</para>
        /// <para><c>table</c></para>
        /// </summary>
        Table = 104,

        /// <summary>
        /// <para>Symbol: template</para>
        /// <para><c>template</c></para>
        /// </summary>
        Template = 105,

        /// <summary>
        /// <para>Symbol: through</para>
        /// <para><c>through</c></para>
        /// </summary>
        Through = 106,

        /// <summary>
        /// <para>Symbol: throw</para>
        /// <para><c>throw</c></para>
        /// </summary>
        Throw = 107,

        /// <summary>
        /// <para>Symbol: time</para>
        /// <para><c>time</c></para>
        /// </summary>
        Time = 108,

        /// <summary>
        /// <para>Symbol: timerange</para>
        /// <para><c>timerange</c></para>
        /// </summary>
        Timerange = 109,

        /// <summary>
        /// <para>Symbol: TimeSpanLiteral</para>
        /// <para><c>TimeSpanLiteral</c></para>
        /// </summary>
        Timespanliteral = 110,

        /// <summary>
        /// <para>Symbol: toolbar</para>
        /// <para><c>toolbar</c></para>
        /// </summary>
        Toolbar = 111,

        /// <summary>
        /// <para>Symbol: toolbutton</para>
        /// <para><c>toolbutton</c></para>
        /// </summary>
        Toolbutton = 112,

        /// <summary>
        /// <para>Symbol: true</para>
        /// <para><c>true</c></para>
        /// </summary>
        True = 113,

        /// <summary>
        /// <para>Symbol: try</para>
        /// <para><c>try</c></para>
        /// </summary>
        Try = 114,

        /// <summary>
        /// <para>Symbol: type</para>
        /// <para><c>type</c></para>
        /// </summary>
        Type = 115,

        /// <summary>
        /// <para>Symbol: unique</para>
        /// <para><c>unique</c></para>
        /// </summary>
        Unique = 116,

        /// <summary>
        /// <para>Symbol: update</para>
        /// <para><c>update</c></para>
        /// </summary>
        Update = 117,

        /// <summary>
        /// <para>Symbol: using</para>
        /// <para><c>using</c></para>
        /// </summary>
        Using = 118,

        /// <summary>
        /// <para>Symbol: var</para>
        /// <para><c>var</c></para>
        /// </summary>
        Var = 119,

        /// <summary>
        /// <para>Symbol: varchar</para>
        /// <para><c>varchar</c></para>
        /// </summary>
        Varchar = 120,

        /// <summary>
        /// <para>Symbol: vbox</para>
        /// <para><c>vbox</c></para>
        /// </summary>
        Vbox = 121,

        /// <summary>
        /// <para>Symbol: vbuttonbox</para>
        /// <para><c>vbuttonbox</c></para>
        /// </summary>
        Vbuttonbox = 122,

        /// <summary>
        /// <para>Symbol: while</para>
        /// <para><c>while</c></para>
        /// </summary>
        While = 123,

        /// <summary>
        /// <para>Symbol: widget</para>
        /// <para><c>widget</c></para>
        /// </summary>
        Widget = 124,

        /// <summary>
        /// <para>Symbol: window</para>
        /// <para><c>window</c></para>
        /// </summary>
        Window = 125,

        /// <summary>
        /// <para>Symbol: Arg</para>
        /// <para><c>&lt;Arg&gt;</c></para>
        /// </summary>
        Arg = 126,

        /// <summary>
        /// <para>Symbol: Args</para>
        /// <para><c>&lt;Args&gt;</c></para>
        /// </summary>
        Args = 127,

        /// <summary>
        /// <para>Symbol: Block</para>
        /// <para><c>&lt;Block&gt;</c></para>
        /// </summary>
        Block = 128,

        /// <summary>
        /// <para>Symbol: Case Stms</para>
        /// <para><c>&lt;Case Stms&gt;</c></para>
        /// </summary>
        Casestms = 129,

        /// <summary>
        /// <para>Symbol: Catch</para>
        /// <para><c>&lt;Catch&gt;</c></para>
        /// </summary>
        Catch2 = 130,

        /// <summary>
        /// <para>Symbol: Catchs</para>
        /// <para><c>&lt;Catchs&gt;</c></para>
        /// </summary>
        Catchs = 131,

        /// <summary>
        /// <para>Symbol: Database</para>
        /// <para><c>&lt;Database&gt;</c></para>
        /// </summary>
        Database2 = 132,

        /// <summary>
        /// <para>Symbol: DB Column</para>
        /// <para><c>&lt;DB Column&gt;</c></para>
        /// </summary>
        Dbcolumn = 133,

        /// <summary>
        /// <para>Symbol: DB Column Attr</para>
        /// <para><c>&lt;DB Column Attr&gt;</c></para>
        /// </summary>
        Dbcolumnattr = 134,

        /// <summary>
        /// <para>Symbol: DB Column Attr List</para>
        /// <para><c>&lt;DB Column Attr List&gt;</c></para>
        /// </summary>
        Dbcolumnattrlist = 135,

        /// <summary>
        /// <para>Symbol: DB Column Type</para>
        /// <para><c>&lt;DB Column Type&gt;</c></para>
        /// </summary>
        Dbcolumntype = 136,

        /// <summary>
        /// <para>Symbol: DB Columns</para>
        /// <para><c>&lt;DB Columns&gt;</c></para>
        /// </summary>
        Dbcolumns = 137,

        /// <summary>
        /// <para>Symbol: DB Table</para>
        /// <para><c>&lt;DB Table&gt;</c></para>
        /// </summary>
        Dbtable = 138,

        /// <summary>
        /// <para>Symbol: DB Table Attr</para>
        /// <para><c>&lt;DB Table Attr&gt;</c></para>
        /// </summary>
        Dbtableattr = 139,

        /// <summary>
        /// <para>Symbol: DB Table Attr List</para>
        /// <para><c>&lt;DB Table Attr List&gt;</c></para>
        /// </summary>
        Dbtableattrlist = 140,

        /// <summary>
        /// <para>Symbol: DB Tables</para>
        /// <para><c>&lt;DB Tables&gt;</c></para>
        /// </summary>
        Dbtables = 141,

        /// <summary>
        /// <para>Symbol: DB Trigger Run</para>
        /// <para><c>&lt;DB Trigger Run&gt;</c></para>
        /// </summary>
        Dbtriggerrun = 142,

        /// <summary>
        /// <para>Symbol: DB Trigger Runs</para>
        /// <para><c>&lt;DB Trigger Runs&gt;</c></para>
        /// </summary>
        Dbtriggerruns = 143,

        /// <summary>
        /// <para>Symbol: Dict List</para>
        /// <para><c>&lt;Dict List&gt;</c></para>
        /// </summary>
        Dictlist = 144,

        /// <summary>
        /// <para>Symbol: Expr</para>
        /// <para><c>&lt;Expr&gt;</c></para>
        /// </summary>
        Expr = 145,

        /// <summary>
        /// <para>Symbol: Expr List</para>
        /// <para><c>&lt;Expr List&gt;</c></para>
        /// </summary>
        Exprlist = 146,

        /// <summary>
        /// <para>Symbol: Func Arg</para>
        /// <para><c>&lt;Func Arg&gt;</c></para>
        /// </summary>
        Funcarg = 147,

        /// <summary>
        /// <para>Symbol: Func args</para>
        /// <para><c>&lt;Func args&gt;</c></para>
        /// </summary>
        Funcargs = 148,

        /// <summary>
        /// <para>Symbol: Function</para>
        /// <para><c>&lt;Function&gt;</c></para>
        /// </summary>
        Function2 = 149,

        /// <summary>
        /// <para>Symbol: ID List</para>
        /// <para><c>&lt;ID List&gt;</c></para>
        /// </summary>
        Idlist = 150,

        /// <summary>
        /// <para>Symbol: Layout Block</para>
        /// <para><c>&lt;Layout Block&gt;</c></para>
        /// </summary>
        Layoutblock = 151,

        /// <summary>
        /// <para>Symbol: Layout List</para>
        /// <para><c>&lt;Layout List&gt;</c></para>
        /// </summary>
        Layoutlist = 152,

        /// <summary>
        /// <para>Symbol: Menu Block</para>
        /// <para><c>&lt;Menu Block&gt;</c></para>
        /// </summary>
        Menublock = 153,

        /// <summary>
        /// <para>Symbol: Menu Item</para>
        /// <para><c>&lt;Menu Item&gt;</c></para>
        /// </summary>
        Menuitem2 = 154,

        /// <summary>
        /// <para>Symbol: MenuItems List</para>
        /// <para><c>&lt;MenuItems List&gt;</c></para>
        /// </summary>
        Menuitemslist = 155,

        /// <summary>
        /// <para>Symbol: Normal Stm</para>
        /// <para><c>&lt;Normal Stm&gt;</c></para>
        /// </summary>
        Normalstm = 156,

        /// <summary>
        /// <para>Symbol: NumLiteral</para>
        /// <para><c>&lt;NumLiteral&gt;</c></para>
        /// </summary>
        Numliteral = 157,

        /// <summary>
        /// <para>Symbol: Op Add</para>
        /// <para><c>&lt;Op Add&gt;</c></para>
        /// </summary>
        Opadd = 158,

        /// <summary>
        /// <para>Symbol: Op And</para>
        /// <para><c>&lt;Op And&gt;</c></para>
        /// </summary>
        Opand = 159,

        /// <summary>
        /// <para>Symbol: Op Assign</para>
        /// <para><c>&lt;Op Assign&gt;</c></para>
        /// </summary>
        Opassign = 160,

        /// <summary>
        /// <para>Symbol: Op Compare</para>
        /// <para><c>&lt;Op Compare&gt;</c></para>
        /// </summary>
        Opcompare = 161,

        /// <summary>
        /// <para>Symbol: Op Equate</para>
        /// <para><c>&lt;Op Equate&gt;</c></para>
        /// </summary>
        Opequate = 162,

        /// <summary>
        /// <para>Symbol: Op If</para>
        /// <para><c>&lt;Op If&gt;</c></para>
        /// </summary>
        Opif = 163,

        /// <summary>
        /// <para>Symbol: Op Mult</para>
        /// <para><c>&lt;Op Mult&gt;</c></para>
        /// </summary>
        Opmult = 164,

        /// <summary>
        /// <para>Symbol: Op Or</para>
        /// <para><c>&lt;Op Or&gt;</c></para>
        /// </summary>
        Opor = 165,

        /// <summary>
        /// <para>Symbol: Op Pointer</para>
        /// <para><c>&lt;Op Pointer&gt;</c></para>
        /// </summary>
        Oppointer = 166,

        /// <summary>
        /// <para>Symbol: Op Unary</para>
        /// <para><c>&lt;Op Unary&gt;</c></para>
        /// </summary>
        Opunary = 167,

        /// <summary>
        /// <para>Symbol: QualifiedName</para>
        /// <para><c>&lt;QualifiedName&gt;</c></para>
        /// </summary>
        Qualifiedname = 168,

        /// <summary>
        /// <para>Symbol: Stm</para>
        /// <para><c>&lt;Stm&gt;</c></para>
        /// </summary>
        Stm = 169,

        /// <summary>
        /// <para>Symbol: Stm List</para>
        /// <para><c>&lt;Stm List&gt;</c></para>
        /// </summary>
        Stmlist = 170,

        /// <summary>
        /// <para>Symbol: Then Stm</para>
        /// <para><c>&lt;Then Stm&gt;</c></para>
        /// </summary>
        Thenstm = 171,

        /// <summary>
        /// <para>Symbol: TryFinally</para>
        /// <para><c>&lt;TryFinally&gt;</c></para>
        /// </summary>
        Tryfinally = 172,

        /// <summary>
        /// <para>Symbol: Value</para>
        /// <para><c>&lt;Value&gt;</c></para>
        /// </summary>
        Value = 173,

        /// <summary>
        /// <para>Symbol: Widget</para>
        /// <para><c>&lt;Widget&gt;</c></para>
        /// </summary>
        Widget2 = 174,

        /// <summary>
        /// <para>Symbol: WndParam</para>
        /// <para><c>&lt;WndParam&gt;</c></para>
        /// </summary>
        Wndparam = 175,

        /// <summary>
        /// <para>Symbol: WndParam List</para>
        /// <para><c>&lt;WndParam List&gt;</c></para>
        /// </summary>
        Wndparamlist = 176,

		#endregion

		#region Neterminaly
        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;QualifiedName&gt; ::= &lt;QualifiedName&gt; '.' ID</c></para>
        /// </summary>
        RuleQualifiednameDotId = ToolScriptParserBase.RulesOffset + 0,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;QualifiedName&gt; ::= ID</c></para>
        /// </summary>
        RuleQualifiednameId = ToolScriptParserBase.RulesOffset + 1,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmIfLparanRparan = ToolScriptParserBase.RulesOffset + 2,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmIfLparanRparanElse = ToolScriptParserBase.RulesOffset + 3,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmWhileLparanRparan = ToolScriptParserBase.RulesOffset + 4,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmForLparanSemiSemiRparan = ToolScriptParserBase.RulesOffset + 5,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= foreach '(' ID in &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmForeachLparanIdInRparan = ToolScriptParserBase.RulesOffset + 6,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= using &lt;QualifiedName&gt; ';'</c></para>
        /// </summary>
        RuleStmUsingSemi = ToolScriptParserBase.RulesOffset + 7,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= using StringLiteral ';'</c></para>
        /// </summary>
        RuleStmUsingStringliteralSemi = ToolScriptParserBase.RulesOffset + 8,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= using &lt;QualifiedName&gt; as ID ';'</c></para>
        /// </summary>
        RuleStmUsingAsIdSemi = ToolScriptParserBase.RulesOffset + 9,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= using StringLiteral as ID ';'</c></para>
        /// </summary>
        RuleStmUsingStringliteralAsIdSemi = ToolScriptParserBase.RulesOffset + 10,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= using '(' &lt;Expr&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmUsingLparanRparan = ToolScriptParserBase.RulesOffset + 11,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= observed '(' &lt;Expr List&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmObservedLparanRparan = ToolScriptParserBase.RulesOffset + 12,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= &lt;TryFinally&gt;</c></para>
        /// </summary>
        RuleStm = ToolScriptParserBase.RulesOffset + 13,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        RuleStm2 = ToolScriptParserBase.RulesOffset + 14,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= if '(' &lt;Expr&gt; ')' &lt;Then Stm&gt; else &lt;Then Stm&gt;</c></para>
        /// </summary>
        RuleThenstmIfLparanRparanElse = ToolScriptParserBase.RulesOffset + 15,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= while '(' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        RuleThenstmWhileLparanRparan = ToolScriptParserBase.RulesOffset + 16,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= for '(' &lt;Expr&gt; ';' &lt;Expr&gt; ';' &lt;Expr&gt; ')' &lt;Then Stm&gt;</c></para>
        /// </summary>
        RuleThenstmForLparanSemiSemiRparan = ToolScriptParserBase.RulesOffset + 17,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Then Stm&gt; ::= &lt;Normal Stm&gt;</c></para>
        /// </summary>
        RuleThenstm = ToolScriptParserBase.RulesOffset + 18,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= do &lt;Stm&gt; while '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        RuleNormalstmDoWhileLparanRparan = ToolScriptParserBase.RulesOffset + 19,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= switch '(' &lt;Expr&gt; ')' '{' &lt;Case Stms&gt; '}'</c></para>
        /// </summary>
        RuleNormalstmSwitchLparanRparanLbraceRbrace = ToolScriptParserBase.RulesOffset + 20,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Block&gt;</c></para>
        /// </summary>
        RuleNormalstm = ToolScriptParserBase.RulesOffset + 21,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleNormalstmSemi = ToolScriptParserBase.RulesOffset + 22,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= break ';'</c></para>
        /// </summary>
        RuleNormalstmBreakSemi = ToolScriptParserBase.RulesOffset + 23,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= continue ';'</c></para>
        /// </summary>
        RuleNormalstmContinueSemi = ToolScriptParserBase.RulesOffset + 24,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= return &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleNormalstmReturnSemi = ToolScriptParserBase.RulesOffset + 25,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= throw &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleNormalstmThrowSemi = ToolScriptParserBase.RulesOffset + 26,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= throw &lt;Expr&gt; ',' &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleNormalstmThrowCommaSemi = ToolScriptParserBase.RulesOffset + 27,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Normal Stm&gt; ::= ';'</c></para>
        /// </summary>
        RuleNormalstmSemi2 = ToolScriptParserBase.RulesOffset + 28,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;TryFinally&gt; ::= try &lt;Normal Stm&gt; &lt;Catchs&gt; finally &lt;Normal Stm&gt;</c></para>
        /// </summary>
        RuleTryfinallyTryFinally = ToolScriptParserBase.RulesOffset + 29,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;TryFinally&gt; ::= try &lt;Normal Stm&gt; &lt;Catchs&gt;</c></para>
        /// </summary>
        RuleTryfinallyTry = ToolScriptParserBase.RulesOffset + 30,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Catchs&gt; ::= &lt;Catch&gt; &lt;Catchs&gt;</c></para>
        /// </summary>
        RuleCatchs = ToolScriptParserBase.RulesOffset + 31,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Catchs&gt; ::= </c></para>
        /// </summary>
        RuleCatchs2 = ToolScriptParserBase.RulesOffset + 32,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Catch&gt; ::= catch '(' &lt;QualifiedName&gt; ID ')' &lt;Normal Stm&gt;</c></para>
        /// </summary>
        RuleCatchCatchLparanIdRparan = ToolScriptParserBase.RulesOffset + 33,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Catch&gt; ::= catch '(' type &lt;QualifiedName&gt; ')' &lt;Normal Stm&gt;</c></para>
        /// </summary>
        RuleCatchCatchLparanTypeRparan = ToolScriptParserBase.RulesOffset + 34,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Catch&gt; ::= catch '(' var ID ')' &lt;Normal Stm&gt;</c></para>
        /// </summary>
        RuleCatchCatchLparanVarIdRparan = ToolScriptParserBase.RulesOffset + 35,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Catch&gt; ::= catch &lt;Block&gt;</c></para>
        /// </summary>
        RuleCatchCatch = ToolScriptParserBase.RulesOffset + 36,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Func args&gt; ::= &lt;Func args&gt; ',' &lt;Func Arg&gt;</c></para>
        /// </summary>
        RuleFuncargsComma = ToolScriptParserBase.RulesOffset + 37,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Func args&gt; ::= &lt;Func Arg&gt;</c></para>
        /// </summary>
        RuleFuncargs = ToolScriptParserBase.RulesOffset + 38,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Func args&gt; ::= </c></para>
        /// </summary>
        RuleFuncargs2 = ToolScriptParserBase.RulesOffset + 39,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Func Arg&gt; ::= ID</c></para>
        /// </summary>
        RuleFuncargId = ToolScriptParserBase.RulesOffset + 40,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Func Arg&gt; ::= ID '=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleFuncargIdEq = ToolScriptParserBase.RulesOffset + 41,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Args&gt; ',' &lt;Arg&gt;</c></para>
        /// </summary>
        RuleArgsComma = ToolScriptParserBase.RulesOffset + 42,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= &lt;Arg&gt;</c></para>
        /// </summary>
        RuleArgs = ToolScriptParserBase.RulesOffset + 43,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Args&gt; ::= </c></para>
        /// </summary>
        RuleArgs2 = ToolScriptParserBase.RulesOffset + 44,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Arg&gt; ::= &lt;Op If&gt;</c></para>
        /// </summary>
        RuleArg = ToolScriptParserBase.RulesOffset + 45,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Arg&gt; ::= ID '=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleArgIdEq = ToolScriptParserBase.RulesOffset + 46,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= case &lt;Value&gt; ':' &lt;Stm List&gt; &lt;Case Stms&gt;</c></para>
        /// </summary>
        RuleCasestmsCaseColon = ToolScriptParserBase.RulesOffset + 47,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= default ':' &lt;Stm List&gt;</c></para>
        /// </summary>
        RuleCasestmsDefaultColon = ToolScriptParserBase.RulesOffset + 48,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Case Stms&gt; ::= </c></para>
        /// </summary>
        RuleCasestms = ToolScriptParserBase.RulesOffset + 49,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Block&gt; ::= '{' &lt;Stm List&gt; '}'</c></para>
        /// </summary>
        RuleBlockLbraceRbrace = ToolScriptParserBase.RulesOffset + 50,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= &lt;Stm&gt; &lt;Stm List&gt;</c></para>
        /// </summary>
        RuleStmlist = ToolScriptParserBase.RulesOffset + 51,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Stm List&gt; ::= &lt;Stm&gt;</c></para>
        /// </summary>
        RuleStmlist2 = ToolScriptParserBase.RulesOffset + 52,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;ID List&gt; ::= &lt;ID List&gt; ',' ID</c></para>
        /// </summary>
        RuleIdlistCommaId = ToolScriptParserBase.RulesOffset + 53,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;ID List&gt; ::= ID</c></para>
        /// </summary>
        RuleIdlistId = ToolScriptParserBase.RulesOffset + 54,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;ID List&gt; ::= </c></para>
        /// </summary>
        RuleIdlist = ToolScriptParserBase.RulesOffset + 55,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Database&gt; ::= database ID '{' &lt;DB Tables&gt; '}'</c></para>
        /// </summary>
        RuleDatabaseDatabaseIdLbraceRbrace = ToolScriptParserBase.RulesOffset + 56,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Database&gt; ::= extends database ID '{' &lt;DB Tables&gt; '}'</c></para>
        /// </summary>
        RuleDatabaseExtendsDatabaseIdLbraceRbrace = ToolScriptParserBase.RulesOffset + 57,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Tables&gt; ::= &lt;DB Tables&gt; &lt;DB Table&gt;</c></para>
        /// </summary>
        RuleDbtables = ToolScriptParserBase.RulesOffset + 58,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Tables&gt; ::= </c></para>
        /// </summary>
        RuleDbtables2 = ToolScriptParserBase.RulesOffset + 59,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table&gt; ::= template table ID '{' &lt;DB Columns&gt; '}' &lt;DB Table Attr List&gt;</c></para>
        /// </summary>
        RuleDbtableTemplateTableIdLbraceRbrace = ToolScriptParserBase.RulesOffset + 60,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table&gt; ::= template table ID template ID '{' &lt;DB Columns&gt; '}' &lt;DB Table Attr List&gt;</c></para>
        /// </summary>
        RuleDbtableTemplateTableIdTemplateIdLbraceRbrace = ToolScriptParserBase.RulesOffset + 61,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table&gt; ::= table ID '{' &lt;DB Columns&gt; '}' &lt;DB Table Attr List&gt;</c></para>
        /// </summary>
        RuleDbtableTableIdLbraceRbrace = ToolScriptParserBase.RulesOffset + 62,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table&gt; ::= table ID template ID '{' &lt;DB Columns&gt; '}' &lt;DB Table Attr List&gt;</c></para>
        /// </summary>
        RuleDbtableTableIdTemplateIdLbraceRbrace = ToolScriptParserBase.RulesOffset + 63,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table&gt; ::= extends table ID '{' &lt;DB Columns&gt; '}' &lt;DB Table Attr List&gt;</c></para>
        /// </summary>
        RuleDbtableExtendsTableIdLbraceRbrace = ToolScriptParserBase.RulesOffset + 64,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Columns&gt; ::= &lt;DB Columns&gt; &lt;DB Column&gt;</c></para>
        /// </summary>
        RuleDbcolumns = ToolScriptParserBase.RulesOffset + 65,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Columns&gt; ::= </c></para>
        /// </summary>
        RuleDbcolumns2 = ToolScriptParserBase.RulesOffset + 66,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column&gt; ::= ID &lt;DB Column Type&gt; &lt;DB Column Attr List&gt; ';'</c></para>
        /// </summary>
        RuleDbcolumnIdSemi = ToolScriptParserBase.RulesOffset + 67,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= primary</c></para>
        /// </summary>
        RuleDbcolumntypePrimary = ToolScriptParserBase.RulesOffset + 68,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= foreign ID</c></para>
        /// </summary>
        RuleDbcolumntypeForeignId = ToolScriptParserBase.RulesOffset + 69,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= foreign ID '(' ID ')'</c></para>
        /// </summary>
        RuleDbcolumntypeForeignIdLparanIdRparan = ToolScriptParserBase.RulesOffset + 70,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= many ID</c></para>
        /// </summary>
        RuleDbcolumntypeManyId = ToolScriptParserBase.RulesOffset + 71,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= many ID through ID</c></para>
        /// </summary>
        RuleDbcolumntypeManyIdThroughId = ToolScriptParserBase.RulesOffset + 72,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= many ID through ID '(' ID ',' ID ')'</c></para>
        /// </summary>
        RuleDbcolumntypeManyIdThroughIdLparanIdCommaIdRparan = ToolScriptParserBase.RulesOffset + 73,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= varchar</c></para>
        /// </summary>
        RuleDbcolumntypeVarchar = ToolScriptParserBase.RulesOffset + 74,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= varchar '(' IntLiteral ')'</c></para>
        /// </summary>
        RuleDbcolumntypeVarcharLparanIntliteralRparan = ToolScriptParserBase.RulesOffset + 75,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= integer</c></para>
        /// </summary>
        RuleDbcolumntypeInteger = ToolScriptParserBase.RulesOffset + 76,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= bool</c></para>
        /// </summary>
        RuleDbcolumntypeBool = ToolScriptParserBase.RulesOffset + 77,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= decimal '(' IntLiteral ',' IntLiteral ')'</c></para>
        /// </summary>
        RuleDbcolumntypeDecimalLparanIntliteralCommaIntliteralRparan = ToolScriptParserBase.RulesOffset + 78,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= date</c></para>
        /// </summary>
        RuleDbcolumntypeDate = ToolScriptParserBase.RulesOffset + 79,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= time</c></para>
        /// </summary>
        RuleDbcolumntypeTime = ToolScriptParserBase.RulesOffset + 80,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= datetime</c></para>
        /// </summary>
        RuleDbcolumntypeDatetime = ToolScriptParserBase.RulesOffset + 81,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= daterange</c></para>
        /// </summary>
        RuleDbcolumntypeDaterange = ToolScriptParserBase.RulesOffset + 82,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= timerange</c></para>
        /// </summary>
        RuleDbcolumntypeTimerange = ToolScriptParserBase.RulesOffset + 83,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Type&gt; ::= datetimerange</c></para>
        /// </summary>
        RuleDbcolumntypeDatetimerange = ToolScriptParserBase.RulesOffset + 84,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Attr List&gt; ::= &lt;DB Column Attr List&gt; &lt;DB Column Attr&gt;</c></para>
        /// </summary>
        RuleDbcolumnattrlist = ToolScriptParserBase.RulesOffset + 85,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Attr List&gt; ::= </c></para>
        /// </summary>
        RuleDbcolumnattrlist2 = ToolScriptParserBase.RulesOffset + 86,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Attr&gt; ::= unique</c></para>
        /// </summary>
        RuleDbcolumnattrUnique = ToolScriptParserBase.RulesOffset + 87,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Attr&gt; ::= not null</c></para>
        /// </summary>
        RuleDbcolumnattrNotNull = ToolScriptParserBase.RulesOffset + 88,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Attr&gt; ::= index</c></para>
        /// </summary>
        RuleDbcolumnattrIndex = ToolScriptParserBase.RulesOffset + 89,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Attr&gt; ::= default &lt;Value&gt;</c></para>
        /// </summary>
        RuleDbcolumnattrDefault = ToolScriptParserBase.RulesOffset + 90,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Column Attr&gt; ::= ID '=' &lt;Value&gt;</c></para>
        /// </summary>
        RuleDbcolumnattrIdEq = ToolScriptParserBase.RulesOffset + 91,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table Attr List&gt; ::= &lt;DB Table Attr List&gt; &lt;DB Table Attr&gt;</c></para>
        /// </summary>
        RuleDbtableattrlist = ToolScriptParserBase.RulesOffset + 92,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table Attr List&gt; ::= </c></para>
        /// </summary>
        RuleDbtableattrlist2 = ToolScriptParserBase.RulesOffset + 93,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table Attr&gt; ::= index '(' &lt;ID List&gt; ')' ';'</c></para>
        /// </summary>
        RuleDbtableattrIndexLparanRparanSemi = ToolScriptParserBase.RulesOffset + 94,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table Attr&gt; ::= unique '(' &lt;ID List&gt; ')' ';'</c></para>
        /// </summary>
        RuleDbtableattrUniqueLparanRparanSemi = ToolScriptParserBase.RulesOffset + 95,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table Attr&gt; ::= &lt;DB Trigger Runs&gt; position &lt;NumLiteral&gt; &lt;Stm&gt;</c></para>
        /// </summary>
        RuleDbtableattrPosition = ToolScriptParserBase.RulesOffset + 96,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Table Attr&gt; ::= ID '=' &lt;Value&gt; ';'</c></para>
        /// </summary>
        RuleDbtableattrIdEqSemi = ToolScriptParserBase.RulesOffset + 97,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Runs&gt; ::= &lt;DB Trigger Runs&gt; ',' &lt;DB Trigger Run&gt;</c></para>
        /// </summary>
        RuleDbtriggerrunsComma = ToolScriptParserBase.RulesOffset + 98,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Runs&gt; ::= &lt;DB Trigger Run&gt;</c></para>
        /// </summary>
        RuleDbtriggerruns = ToolScriptParserBase.RulesOffset + 99,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Run&gt; ::= before select</c></para>
        /// </summary>
        RuleDbtriggerrunBeforeSelect = ToolScriptParserBase.RulesOffset + 100,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Run&gt; ::= after select</c></para>
        /// </summary>
        RuleDbtriggerrunAfterSelect = ToolScriptParserBase.RulesOffset + 101,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Run&gt; ::= before insert</c></para>
        /// </summary>
        RuleDbtriggerrunBeforeInsert = ToolScriptParserBase.RulesOffset + 102,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Run&gt; ::= after insert</c></para>
        /// </summary>
        RuleDbtriggerrunAfterInsert = ToolScriptParserBase.RulesOffset + 103,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Run&gt; ::= before update</c></para>
        /// </summary>
        RuleDbtriggerrunBeforeUpdate = ToolScriptParserBase.RulesOffset + 104,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Run&gt; ::= after update</c></para>
        /// </summary>
        RuleDbtriggerrunAfterUpdate = ToolScriptParserBase.RulesOffset + 105,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Run&gt; ::= before delete</c></para>
        /// </summary>
        RuleDbtriggerrunBeforeDelete = ToolScriptParserBase.RulesOffset + 106,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Run&gt; ::= after delete</c></para>
        /// </summary>
        RuleDbtriggerrunAfterDelete = ToolScriptParserBase.RulesOffset + 107,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Run&gt; ::= before modified</c></para>
        /// </summary>
        RuleDbtriggerrunBeforeModified = ToolScriptParserBase.RulesOffset + 108,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;DB Trigger Run&gt; ::= after modified</c></para>
        /// </summary>
        RuleDbtriggerrunAfterModified = ToolScriptParserBase.RulesOffset + 109,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;NumLiteral&gt; ::= IntLiteral</c></para>
        /// </summary>
        RuleNumliteralIntliteral = ToolScriptParserBase.RulesOffset + 110,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;NumLiteral&gt; ::= DecimalLiteral</c></para>
        /// </summary>
        RuleNumliteralDecimalliteral = ToolScriptParserBase.RulesOffset + 111,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Function&gt; ::= function ID '(' &lt;Func args&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleFunctionFunctionIdLparanRparan = ToolScriptParserBase.RulesOffset + 112,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Function&gt; ::= function '(' &lt;Func args&gt; ')' &lt;Stm&gt;</c></para>
        /// </summary>
        RuleFunctionFunctionLparanRparan = ToolScriptParserBase.RulesOffset + 113,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Widget&gt; ::= window ID &lt;WndParam List&gt; &lt;Layout Block&gt;</c></para>
        /// </summary>
        RuleWidgetWindowId = ToolScriptParserBase.RulesOffset + 114,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Widget&gt; ::= window &lt;WndParam List&gt; &lt;Layout Block&gt;</c></para>
        /// </summary>
        RuleWidgetWindow = ToolScriptParserBase.RulesOffset + 115,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Widget&gt; ::= widget ID &lt;Layout Block&gt;</c></para>
        /// </summary>
        RuleWidgetWidgetId = ToolScriptParserBase.RulesOffset + 116,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Widget&gt; ::= widget &lt;Layout Block&gt;</c></para>
        /// </summary>
        RuleWidgetWidget = ToolScriptParserBase.RulesOffset + 117,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;WndParam List&gt; ::= &lt;WndParam&gt; &lt;WndParam List&gt;</c></para>
        /// </summary>
        RuleWndparamlist = ToolScriptParserBase.RulesOffset + 118,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;WndParam List&gt; ::= </c></para>
        /// </summary>
        RuleWndparamlist2 = ToolScriptParserBase.RulesOffset + 119,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;WndParam&gt; ::= ID '=' &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleWndparamIdEqSemi = ToolScriptParserBase.RulesOffset + 120,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;WndParam&gt; ::= ID ';'</c></para>
        /// </summary>
        RuleWndparamIdSemi = ToolScriptParserBase.RulesOffset + 121,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout List&gt; ::= &lt;Layout List&gt; &lt;Layout Block&gt;</c></para>
        /// </summary>
        RuleLayoutlist = ToolScriptParserBase.RulesOffset + 122,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout List&gt; ::= </c></para>
        /// </summary>
        RuleLayoutlist2 = ToolScriptParserBase.RulesOffset + 123,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= hbox &lt;WndParam List&gt; &lt;Layout List&gt; end</c></para>
        /// </summary>
        RuleLayoutblockHboxEnd = ToolScriptParserBase.RulesOffset + 124,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= vbox &lt;WndParam List&gt; &lt;Layout List&gt; end</c></para>
        /// </summary>
        RuleLayoutblockVboxEnd = ToolScriptParserBase.RulesOffset + 125,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= hbuttonbox &lt;WndParam List&gt; &lt;Layout List&gt; end</c></para>
        /// </summary>
        RuleLayoutblockHbuttonboxEnd = ToolScriptParserBase.RulesOffset + 126,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= vbuttonbox &lt;WndParam List&gt; &lt;Layout List&gt; end</c></para>
        /// </summary>
        RuleLayoutblockVbuttonboxEnd = ToolScriptParserBase.RulesOffset + 127,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= table &lt;WndParam List&gt; &lt;Layout List&gt; end</c></para>
        /// </summary>
        RuleLayoutblockTableEnd = ToolScriptParserBase.RulesOffset + 128,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= toolbar &lt;WndParam List&gt; &lt;Layout List&gt; end</c></para>
        /// </summary>
        RuleLayoutblockToolbarEnd = ToolScriptParserBase.RulesOffset + 129,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= button &lt;WndParam List&gt; &lt;Layout Block&gt;</c></para>
        /// </summary>
        RuleLayoutblockButton = ToolScriptParserBase.RulesOffset + 130,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= button &lt;WndParam List&gt; end</c></para>
        /// </summary>
        RuleLayoutblockButtonEnd = ToolScriptParserBase.RulesOffset + 131,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= toolbutton &lt;WndParam List&gt; &lt;Layout Block&gt;</c></para>
        /// </summary>
        RuleLayoutblockToolbutton = ToolScriptParserBase.RulesOffset + 132,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= toolbutton &lt;WndParam List&gt; end</c></para>
        /// </summary>
        RuleLayoutblockToolbuttonEnd = ToolScriptParserBase.RulesOffset + 133,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= image &lt;WndParam List&gt;</c></para>
        /// </summary>
        RuleLayoutblockImage = ToolScriptParserBase.RulesOffset + 134,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= StringLiteral &lt;WndParam List&gt;</c></para>
        /// </summary>
        RuleLayoutblockStringliteral = ToolScriptParserBase.RulesOffset + 135,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= &lt;Menu Block&gt;</c></para>
        /// </summary>
        RuleLayoutblock = ToolScriptParserBase.RulesOffset + 136,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Layout Block&gt; ::= '[' &lt;Expr&gt; ']' &lt;WndParam List&gt;</c></para>
        /// </summary>
        RuleLayoutblockLbracketRbracket = ToolScriptParserBase.RulesOffset + 137,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Menu Block&gt; ::= menu &lt;WndParam List&gt; &lt;MenuItems List&gt; end</c></para>
        /// </summary>
        RuleMenublockMenuEnd = ToolScriptParserBase.RulesOffset + 138,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;MenuItems List&gt; ::= &lt;Menu Item&gt; &lt;MenuItems List&gt;</c></para>
        /// </summary>
        RuleMenuitemslist = ToolScriptParserBase.RulesOffset + 139,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;MenuItems List&gt; ::= </c></para>
        /// </summary>
        RuleMenuitemslist2 = ToolScriptParserBase.RulesOffset + 140,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Menu Item&gt; ::= &lt;Menu Block&gt;</c></para>
        /// </summary>
        RuleMenuitem = ToolScriptParserBase.RulesOffset + 141,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Menu Item&gt; ::= menuitem &lt;WndParam List&gt;</c></para>
        /// </summary>
        RuleMenuitemMenuitem = ToolScriptParserBase.RulesOffset + 142,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Menu Item&gt; ::= Separator</c></para>
        /// </summary>
        RuleMenuitemSeparator = ToolScriptParserBase.RulesOffset + 143,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr List&gt; ::= &lt;Expr List&gt; ',' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprlistComma = ToolScriptParserBase.RulesOffset + 144,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr List&gt; ::= &lt;Expr&gt;</c></para>
        /// </summary>
        RuleExprlist = ToolScriptParserBase.RulesOffset + 145,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Dict List&gt; ::= &lt;Expr&gt; ':' &lt;Expr&gt; ',' &lt;Dict List&gt;</c></para>
        /// </summary>
        RuleDictlistColonComma = ToolScriptParserBase.RulesOffset + 146,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Dict List&gt; ::= &lt;Expr&gt; ':' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleDictlistColon = ToolScriptParserBase.RulesOffset + 147,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Dict List&gt; ::= </c></para>
        /// </summary>
        RuleDictlist = ToolScriptParserBase.RulesOffset + 148,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Widget&gt;</c></para>
        /// </summary>
        RuleExpr = ToolScriptParserBase.RulesOffset + 149,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Database&gt;</c></para>
        /// </summary>
        RuleExpr2 = ToolScriptParserBase.RulesOffset + 150,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Expr&gt; ::= &lt;Op Assign&gt;</c></para>
        /// </summary>
        RuleExpr3 = ToolScriptParserBase.RulesOffset + 151,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Assign&gt; ::= &lt;Op If&gt; '=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleOpassignEq = ToolScriptParserBase.RulesOffset + 152,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Assign&gt; ::= &lt;Op If&gt; '+=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleOpassignPluseq = ToolScriptParserBase.RulesOffset + 153,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Assign&gt; ::= &lt;Op If&gt; '-=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleOpassignMinuseq = ToolScriptParserBase.RulesOffset + 154,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Assign&gt; ::= &lt;Op If&gt; '*=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleOpassignTimeseq = ToolScriptParserBase.RulesOffset + 155,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Assign&gt; ::= &lt;Op If&gt; '/=' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleOpassignDiveq = ToolScriptParserBase.RulesOffset + 156,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Assign&gt; ::= &lt;Op If&gt; '&lt;==' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleOpassignLteqeq = ToolScriptParserBase.RulesOffset + 157,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Assign&gt; ::= &lt;Op If&gt; '&lt;==&gt;' &lt;Expr&gt;</c></para>
        /// </summary>
        RuleOpassignLteqeqgt = ToolScriptParserBase.RulesOffset + 158,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Assign&gt; ::= &lt;Op If&gt;</c></para>
        /// </summary>
        RuleOpassign = ToolScriptParserBase.RulesOffset + 159,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt; '?' &lt;Op If&gt; ':' &lt;Op If&gt;</c></para>
        /// </summary>
        RuleOpifQuestionColon = ToolScriptParserBase.RulesOffset + 160,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op If&gt; ::= &lt;Op Or&gt;</c></para>
        /// </summary>
        RuleOpif = ToolScriptParserBase.RulesOffset + 161,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op Or&gt; or &lt;Op And&gt;</c></para>
        /// </summary>
        RuleOporOr = ToolScriptParserBase.RulesOffset + 162,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Or&gt; ::= &lt;Op And&gt;</c></para>
        /// </summary>
        RuleOpor = ToolScriptParserBase.RulesOffset + 163,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op And&gt; and &lt;Op Equate&gt;</c></para>
        /// </summary>
        RuleOpandAnd = ToolScriptParserBase.RulesOffset + 164,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op And&gt; ::= &lt;Op Equate&gt;</c></para>
        /// </summary>
        RuleOpand = ToolScriptParserBase.RulesOffset + 165,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '==' &lt;Op Compare&gt;</c></para>
        /// </summary>
        RuleOpequateEqeq = ToolScriptParserBase.RulesOffset + 166,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; '!=' &lt;Op Compare&gt;</c></para>
        /// </summary>
        RuleOpequateExclameq = ToolScriptParserBase.RulesOffset + 167,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Equate&gt; in &lt;Op Compare&gt;</c></para>
        /// </summary>
        RuleOpequateIn = ToolScriptParserBase.RulesOffset + 168,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Equate&gt; ::= &lt;Op Compare&gt;</c></para>
        /// </summary>
        RuleOpequate = ToolScriptParserBase.RulesOffset + 169,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;' &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpcompareLt = ToolScriptParserBase.RulesOffset + 170,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;' &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpcompareGt = ToolScriptParserBase.RulesOffset + 171,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&lt;=' &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpcompareLteq = ToolScriptParserBase.RulesOffset + 172,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Compare&gt; '&gt;=' &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpcompareGteq = ToolScriptParserBase.RulesOffset + 173,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Compare&gt; ::= &lt;Op Add&gt;</c></para>
        /// </summary>
        RuleOpcompare = ToolScriptParserBase.RulesOffset + 174,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '+' &lt;Op Mult&gt;</c></para>
        /// </summary>
        RuleOpaddPlus = ToolScriptParserBase.RulesOffset + 175,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Add&gt; '-' &lt;Op Mult&gt;</c></para>
        /// </summary>
        RuleOpaddMinus = ToolScriptParserBase.RulesOffset + 176,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Add&gt; ::= &lt;Op Mult&gt;</c></para>
        /// </summary>
        RuleOpadd = ToolScriptParserBase.RulesOffset + 177,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '*' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmultTimes = ToolScriptParserBase.RulesOffset + 178,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '/' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmultDiv = ToolScriptParserBase.RulesOffset + 179,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Mult&gt; '%' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmultPercent = ToolScriptParserBase.RulesOffset + 180,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Mult&gt; ::= &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpmult = ToolScriptParserBase.RulesOffset + 181,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= not &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryNot = ToolScriptParserBase.RulesOffset + 182,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= '!' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryExclam = ToolScriptParserBase.RulesOffset + 183,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= '-' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryMinus = ToolScriptParserBase.RulesOffset + 184,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= cast '(' &lt;Op Unary&gt; as &lt;QualifiedName&gt; ')'</c></para>
        /// </summary>
        RuleOpunaryCastLparanAsRparan = ToolScriptParserBase.RulesOffset + 185,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= '++' &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryPlusplus = ToolScriptParserBase.RulesOffset + 186,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= -- &lt;Op Unary&gt;</c></para>
        /// </summary>
        RuleOpunaryMinusminus = ToolScriptParserBase.RulesOffset + 187,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt; '++'</c></para>
        /// </summary>
        RuleOpunaryPlusplus2 = ToolScriptParserBase.RulesOffset + 188,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt; --</c></para>
        /// </summary>
        RuleOpunaryMinusminus2 = ToolScriptParserBase.RulesOffset + 189,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt; is null</c></para>
        /// </summary>
        RuleOpunaryIsNull = ToolScriptParserBase.RulesOffset + 190,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt; not null</c></para>
        /// </summary>
        RuleOpunaryNotNull = ToolScriptParserBase.RulesOffset + 191,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt; is not null</c></para>
        /// </summary>
        RuleOpunaryIsNotNull = ToolScriptParserBase.RulesOffset + 192,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Unary&gt; ::= &lt;Op Pointer&gt;</c></para>
        /// </summary>
        RuleOpunary = ToolScriptParserBase.RulesOffset + 193,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '.' &lt;Value&gt;</c></para>
        /// </summary>
        RuleOppointerDot = ToolScriptParserBase.RulesOffset + 194,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '-&gt;' &lt;Value&gt;</c></para>
        /// </summary>
        RuleOppointerMinusgt = ToolScriptParserBase.RulesOffset + 195,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '[' &lt;Expr&gt; ']'</c></para>
        /// </summary>
        RuleOppointerLbracketRbracket = ToolScriptParserBase.RulesOffset + 196,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Op Pointer&gt; '(' &lt;Args&gt; ')'</c></para>
        /// </summary>
        RuleOppointerLparanRparan = ToolScriptParserBase.RulesOffset + 197,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Op Pointer&gt; ::= &lt;Value&gt;</c></para>
        /// </summary>
        RuleOppointer = ToolScriptParserBase.RulesOffset + 198,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= IntLiteral</c></para>
        /// </summary>
        RuleValueIntliteral = ToolScriptParserBase.RulesOffset + 199,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= StringLiteral</c></para>
        /// </summary>
        RuleValueStringliteral = ToolScriptParserBase.RulesOffset + 200,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= DecimalLiteral</c></para>
        /// </summary>
        RuleValueDecimalliteral = ToolScriptParserBase.RulesOffset + 201,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= DateTimeLiteral</c></para>
        /// </summary>
        RuleValueDatetimeliteral = ToolScriptParserBase.RulesOffset + 202,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= TimeSpanLiteral</c></para>
        /// </summary>
        RuleValueTimespanliteral = ToolScriptParserBase.RulesOffset + 203,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= type '(' &lt;QualifiedName&gt; ')'</c></para>
        /// </summary>
        RuleValueTypeLparanRparan = ToolScriptParserBase.RulesOffset + 204,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= new &lt;QualifiedName&gt; '(' &lt;Args&gt; ')'</c></para>
        /// </summary>
        RuleValueNewLparanRparan = ToolScriptParserBase.RulesOffset + 205,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= ID</c></para>
        /// </summary>
        RuleValueId = ToolScriptParserBase.RulesOffset + 206,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= var ID</c></para>
        /// </summary>
        RuleValueVarId = ToolScriptParserBase.RulesOffset + 207,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= static ID</c></para>
        /// </summary>
        RuleValueStaticId = ToolScriptParserBase.RulesOffset + 208,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= '(' &lt;Expr&gt; ')'</c></para>
        /// </summary>
        RuleValueLparanRparan = ToolScriptParserBase.RulesOffset + 209,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= '[' &lt;Expr List&gt; ']'</c></para>
        /// </summary>
        RuleValueLbracketRbracket = ToolScriptParserBase.RulesOffset + 210,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= '{' &lt;Dict List&gt; '}'</c></para>
        /// </summary>
        RuleValueLbraceRbrace = ToolScriptParserBase.RulesOffset + 211,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= range '&lt;' &lt;Op Add&gt; ';' &lt;Op Add&gt; '&gt;'</c></para>
        /// </summary>
        RuleValueRangeLtSemiGt = ToolScriptParserBase.RulesOffset + 212,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= range '&lt;' &lt;Op Add&gt; ';' &lt;Op Add&gt; ')'</c></para>
        /// </summary>
        RuleValueRangeLtSemiRparan = ToolScriptParserBase.RulesOffset + 213,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= range '(' &lt;Op Add&gt; ';' &lt;Op Add&gt; '&gt;'</c></para>
        /// </summary>
        RuleValueRangeLparanSemiGt = ToolScriptParserBase.RulesOffset + 214,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= range '(' &lt;Op Add&gt; ';' &lt;Op Add&gt; ')'</c></para>
        /// </summary>
        RuleValueRangeLparanSemiRparan = ToolScriptParserBase.RulesOffset + 215,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= property &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleValuePropertySemi = ToolScriptParserBase.RulesOffset + 216,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= property &lt;Expr&gt; get &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleValuePropertyGetSemi = ToolScriptParserBase.RulesOffset + 217,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= property &lt;Expr&gt; get &lt;Expr&gt; set &lt;Expr&gt; ';'</c></para>
        /// </summary>
        RuleValuePropertyGetSetSemi = ToolScriptParserBase.RulesOffset + 218,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= &lt;Function&gt;</c></para>
        /// </summary>
        RuleValue = ToolScriptParserBase.RulesOffset + 219,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= dict</c></para>
        /// </summary>
        RuleValueDict = ToolScriptParserBase.RulesOffset + 220,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= list</c></para>
        /// </summary>
        RuleValueList = ToolScriptParserBase.RulesOffset + 221,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= null</c></para>
        /// </summary>
        RuleValueNull = ToolScriptParserBase.RulesOffset + 222,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= true</c></para>
        /// </summary>
        RuleValueTrue = ToolScriptParserBase.RulesOffset + 223,

        /// <summary>
        /// <para>Pravidlo: </para>
        /// <para><c>&lt;Value&gt; ::= false</c></para>
        /// </summary>
        RuleValueFalse = ToolScriptParserBase.RulesOffset + 224 

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
		/// <para>Symbol: after</para>
		/// <para><c>after</c></para>
		/// </summary>
		protected virtual object TerminalAfter(TerminalToken token)
		{
			throw new NotImplementedException("Symbol after");
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
		/// <para>Symbol: before</para>
		/// <para><c>before</c></para>
		/// </summary>
		protected virtual object TerminalBefore(TerminalToken token)
		{
			throw new NotImplementedException("Symbol before");
		}

		/// <summary>
		/// <para>Symbol: bool</para>
		/// <para><c>bool</c></para>
		/// </summary>
		protected virtual object TerminalBool(TerminalToken token)
		{
			throw new NotImplementedException("Symbol bool");
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
		/// <para>Symbol: button</para>
		/// <para><c>button</c></para>
		/// </summary>
		protected virtual object TerminalButton(TerminalToken token)
		{
			throw new NotImplementedException("Symbol button");
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
		/// <para>Symbol: catch</para>
		/// <para><c>catch</c></para>
		/// </summary>
		protected virtual object TerminalCatch(TerminalToken token)
		{
			throw new NotImplementedException("Symbol catch");
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
		/// <para>Symbol: database</para>
		/// <para><c>database</c></para>
		/// </summary>
		protected virtual object TerminalDatabase(TerminalToken token)
		{
			throw new NotImplementedException("Symbol database");
		}

		/// <summary>
		/// <para>Symbol: date</para>
		/// <para><c>date</c></para>
		/// </summary>
		protected virtual object TerminalDate(TerminalToken token)
		{
			throw new NotImplementedException("Symbol date");
		}

		/// <summary>
		/// <para>Symbol: daterange</para>
		/// <para><c>daterange</c></para>
		/// </summary>
		protected virtual object TerminalDaterange(TerminalToken token)
		{
			throw new NotImplementedException("Symbol daterange");
		}

		/// <summary>
		/// <para>Symbol: datetime</para>
		/// <para><c>datetime</c></para>
		/// </summary>
		protected virtual object TerminalDatetime(TerminalToken token)
		{
			throw new NotImplementedException("Symbol datetime");
		}

		/// <summary>
		/// <para>Symbol: DateTimeLiteral</para>
		/// <para><c>DateTimeLiteral</c></para>
		/// </summary>
		protected virtual object TerminalDatetimeliteral(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DateTimeLiteral");
		}

		/// <summary>
		/// <para>Symbol: datetimerange</para>
		/// <para><c>datetimerange</c></para>
		/// </summary>
		protected virtual object TerminalDatetimerange(TerminalToken token)
		{
			throw new NotImplementedException("Symbol datetimerange");
		}

		/// <summary>
		/// <para>Symbol: decimal</para>
		/// <para><c>decimal</c></para>
		/// </summary>
		protected virtual object TerminalDecimal(TerminalToken token)
		{
			throw new NotImplementedException("Symbol decimal");
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
		/// <para>Symbol: delete</para>
		/// <para><c>delete</c></para>
		/// </summary>
		protected virtual object TerminalDelete(TerminalToken token)
		{
			throw new NotImplementedException("Symbol delete");
		}

		/// <summary>
		/// <para>Symbol: dict</para>
		/// <para><c>dict</c></para>
		/// </summary>
		protected virtual object TerminalDict(TerminalToken token)
		{
			throw new NotImplementedException("Symbol dict");
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
		/// <para>Symbol: end</para>
		/// <para><c>end</c></para>
		/// </summary>
		protected virtual object TerminalEnd(TerminalToken token)
		{
			throw new NotImplementedException("Symbol end");
		}

		/// <summary>
		/// <para>Symbol: extends</para>
		/// <para><c>extends</c></para>
		/// </summary>
		protected virtual object TerminalExtends(TerminalToken token)
		{
			throw new NotImplementedException("Symbol extends");
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
		/// <para>Symbol: finally</para>
		/// <para><c>finally</c></para>
		/// </summary>
		protected virtual object TerminalFinally(TerminalToken token)
		{
			throw new NotImplementedException("Symbol finally");
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
		/// <para>Symbol: foreign</para>
		/// <para><c>foreign</c></para>
		/// </summary>
		protected virtual object TerminalForeign(TerminalToken token)
		{
			throw new NotImplementedException("Symbol foreign");
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
		/// <para>Symbol: get</para>
		/// <para><c>get</c></para>
		/// </summary>
		protected virtual object TerminalGet(TerminalToken token)
		{
			throw new NotImplementedException("Symbol get");
		}

		/// <summary>
		/// <para>Symbol: hbox</para>
		/// <para><c>hbox</c></para>
		/// </summary>
		protected virtual object TerminalHbox(TerminalToken token)
		{
			throw new NotImplementedException("Symbol hbox");
		}

		/// <summary>
		/// <para>Symbol: hbuttonbox</para>
		/// <para><c>hbuttonbox</c></para>
		/// </summary>
		protected virtual object TerminalHbuttonbox(TerminalToken token)
		{
			throw new NotImplementedException("Symbol hbuttonbox");
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
		/// <para>Symbol: image</para>
		/// <para><c>image</c></para>
		/// </summary>
		protected virtual object TerminalImage(TerminalToken token)
		{
			throw new NotImplementedException("Symbol image");
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
		/// <para>Symbol: index</para>
		/// <para><c>index</c></para>
		/// </summary>
		protected virtual object TerminalIndex(TerminalToken token)
		{
			throw new NotImplementedException("Symbol index");
		}

		/// <summary>
		/// <para>Symbol: insert</para>
		/// <para><c>insert</c></para>
		/// </summary>
		protected virtual object TerminalInsert(TerminalToken token)
		{
			throw new NotImplementedException("Symbol insert");
		}

		/// <summary>
		/// <para>Symbol: integer</para>
		/// <para><c>integer</c></para>
		/// </summary>
		protected virtual object TerminalInteger(TerminalToken token)
		{
			throw new NotImplementedException("Symbol integer");
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
		/// <para>Symbol: list</para>
		/// <para><c>list</c></para>
		/// </summary>
		protected virtual object TerminalList(TerminalToken token)
		{
			throw new NotImplementedException("Symbol list");
		}

		/// <summary>
		/// <para>Symbol: many</para>
		/// <para><c>many</c></para>
		/// </summary>
		protected virtual object TerminalMany(TerminalToken token)
		{
			throw new NotImplementedException("Symbol many");
		}

		/// <summary>
		/// <para>Symbol: menu</para>
		/// <para><c>menu</c></para>
		/// </summary>
		protected virtual object TerminalMenu(TerminalToken token)
		{
			throw new NotImplementedException("Symbol menu");
		}

		/// <summary>
		/// <para>Symbol: menuitem</para>
		/// <para><c>menuitem</c></para>
		/// </summary>
		protected virtual object TerminalMenuitem(TerminalToken token)
		{
			throw new NotImplementedException("Symbol menuitem");
		}

		/// <summary>
		/// <para>Symbol: modified</para>
		/// <para><c>modified</c></para>
		/// </summary>
		protected virtual object TerminalModified(TerminalToken token)
		{
			throw new NotImplementedException("Symbol modified");
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
		/// <para>Symbol: position</para>
		/// <para><c>position</c></para>
		/// </summary>
		protected virtual object TerminalPosition(TerminalToken token)
		{
			throw new NotImplementedException("Symbol position");
		}

		/// <summary>
		/// <para>Symbol: primary</para>
		/// <para><c>primary</c></para>
		/// </summary>
		protected virtual object TerminalPrimary(TerminalToken token)
		{
			throw new NotImplementedException("Symbol primary");
		}

		/// <summary>
		/// <para>Symbol: property</para>
		/// <para><c>property</c></para>
		/// </summary>
		protected virtual object TerminalProperty(TerminalToken token)
		{
			throw new NotImplementedException("Symbol property");
		}

		/// <summary>
		/// <para>Symbol: range</para>
		/// <para><c>range</c></para>
		/// </summary>
		protected virtual object TerminalRange(TerminalToken token)
		{
			throw new NotImplementedException("Symbol range");
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
		/// <para>Symbol: select</para>
		/// <para><c>select</c></para>
		/// </summary>
		protected virtual object TerminalSelect(TerminalToken token)
		{
			throw new NotImplementedException("Symbol select");
		}

		/// <summary>
		/// <para>Symbol: Separator</para>
		/// <para><c>Separator</c></para>
		/// </summary>
		protected virtual object TerminalSeparator(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Separator");
		}

		/// <summary>
		/// <para>Symbol: set</para>
		/// <para><c>set</c></para>
		/// </summary>
		protected virtual object TerminalSet(TerminalToken token)
		{
			throw new NotImplementedException("Symbol set");
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
		/// <para>Symbol: table</para>
		/// <para><c>table</c></para>
		/// </summary>
		protected virtual object TerminalTable(TerminalToken token)
		{
			throw new NotImplementedException("Symbol table");
		}

		/// <summary>
		/// <para>Symbol: template</para>
		/// <para><c>template</c></para>
		/// </summary>
		protected virtual object TerminalTemplate(TerminalToken token)
		{
			throw new NotImplementedException("Symbol template");
		}

		/// <summary>
		/// <para>Symbol: through</para>
		/// <para><c>through</c></para>
		/// </summary>
		protected virtual object TerminalThrough(TerminalToken token)
		{
			throw new NotImplementedException("Symbol through");
		}

		/// <summary>
		/// <para>Symbol: throw</para>
		/// <para><c>throw</c></para>
		/// </summary>
		protected virtual object TerminalThrow(TerminalToken token)
		{
			throw new NotImplementedException("Symbol throw");
		}

		/// <summary>
		/// <para>Symbol: time</para>
		/// <para><c>time</c></para>
		/// </summary>
		protected virtual object TerminalTime(TerminalToken token)
		{
			throw new NotImplementedException("Symbol time");
		}

		/// <summary>
		/// <para>Symbol: timerange</para>
		/// <para><c>timerange</c></para>
		/// </summary>
		protected virtual object TerminalTimerange(TerminalToken token)
		{
			throw new NotImplementedException("Symbol timerange");
		}

		/// <summary>
		/// <para>Symbol: TimeSpanLiteral</para>
		/// <para><c>TimeSpanLiteral</c></para>
		/// </summary>
		protected virtual object TerminalTimespanliteral(TerminalToken token)
		{
			throw new NotImplementedException("Symbol TimeSpanLiteral");
		}

		/// <summary>
		/// <para>Symbol: toolbar</para>
		/// <para><c>toolbar</c></para>
		/// </summary>
		protected virtual object TerminalToolbar(TerminalToken token)
		{
			throw new NotImplementedException("Symbol toolbar");
		}

		/// <summary>
		/// <para>Symbol: toolbutton</para>
		/// <para><c>toolbutton</c></para>
		/// </summary>
		protected virtual object TerminalToolbutton(TerminalToken token)
		{
			throw new NotImplementedException("Symbol toolbutton");
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
		/// <para>Symbol: try</para>
		/// <para><c>try</c></para>
		/// </summary>
		protected virtual object TerminalTry(TerminalToken token)
		{
			throw new NotImplementedException("Symbol try");
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
		/// <para>Symbol: unique</para>
		/// <para><c>unique</c></para>
		/// </summary>
		protected virtual object TerminalUnique(TerminalToken token)
		{
			throw new NotImplementedException("Symbol unique");
		}

		/// <summary>
		/// <para>Symbol: update</para>
		/// <para><c>update</c></para>
		/// </summary>
		protected virtual object TerminalUpdate(TerminalToken token)
		{
			throw new NotImplementedException("Symbol update");
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
		/// <para>Symbol: varchar</para>
		/// <para><c>varchar</c></para>
		/// </summary>
		protected virtual object TerminalVarchar(TerminalToken token)
		{
			throw new NotImplementedException("Symbol varchar");
		}

		/// <summary>
		/// <para>Symbol: vbox</para>
		/// <para><c>vbox</c></para>
		/// </summary>
		protected virtual object TerminalVbox(TerminalToken token)
		{
			throw new NotImplementedException("Symbol vbox");
		}

		/// <summary>
		/// <para>Symbol: vbuttonbox</para>
		/// <para><c>vbuttonbox</c></para>
		/// </summary>
		protected virtual object TerminalVbuttonbox(TerminalToken token)
		{
			throw new NotImplementedException("Symbol vbuttonbox");
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
		/// <para>Symbol: widget</para>
		/// <para><c>widget</c></para>
		/// </summary>
		protected virtual object TerminalWidget(TerminalToken token)
		{
			throw new NotImplementedException("Symbol widget");
		}

		/// <summary>
		/// <para>Symbol: window</para>
		/// <para><c>window</c></para>
		/// </summary>
		protected virtual object TerminalWindow(TerminalToken token)
		{
			throw new NotImplementedException("Symbol window");
		}

		/// <summary>
		/// <para>Symbol: Arg</para>
		/// <para><c>&lt;Arg&gt;</c></para>
		/// </summary>
		protected virtual object TerminalArg(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Arg");
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
		/// <para>Symbol: Catch</para>
		/// <para><c>&lt;Catch&gt;</c></para>
		/// </summary>
		protected virtual object TerminalCatch2(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Catch");
		}

		/// <summary>
		/// <para>Symbol: Catchs</para>
		/// <para><c>&lt;Catchs&gt;</c></para>
		/// </summary>
		protected virtual object TerminalCatchs(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Catchs");
		}

		/// <summary>
		/// <para>Symbol: Database</para>
		/// <para><c>&lt;Database&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDatabase2(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Database");
		}

		/// <summary>
		/// <para>Symbol: DB Column</para>
		/// <para><c>&lt;DB Column&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbcolumn(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Column");
		}

		/// <summary>
		/// <para>Symbol: DB Column Attr</para>
		/// <para><c>&lt;DB Column Attr&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbcolumnattr(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Column Attr");
		}

		/// <summary>
		/// <para>Symbol: DB Column Attr List</para>
		/// <para><c>&lt;DB Column Attr List&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbcolumnattrlist(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Column Attr List");
		}

		/// <summary>
		/// <para>Symbol: DB Column Type</para>
		/// <para><c>&lt;DB Column Type&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbcolumntype(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Column Type");
		}

		/// <summary>
		/// <para>Symbol: DB Columns</para>
		/// <para><c>&lt;DB Columns&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbcolumns(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Columns");
		}

		/// <summary>
		/// <para>Symbol: DB Table</para>
		/// <para><c>&lt;DB Table&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbtable(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Table");
		}

		/// <summary>
		/// <para>Symbol: DB Table Attr</para>
		/// <para><c>&lt;DB Table Attr&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbtableattr(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Table Attr");
		}

		/// <summary>
		/// <para>Symbol: DB Table Attr List</para>
		/// <para><c>&lt;DB Table Attr List&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbtableattrlist(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Table Attr List");
		}

		/// <summary>
		/// <para>Symbol: DB Tables</para>
		/// <para><c>&lt;DB Tables&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbtables(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Tables");
		}

		/// <summary>
		/// <para>Symbol: DB Trigger Run</para>
		/// <para><c>&lt;DB Trigger Run&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbtriggerrun(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Trigger Run");
		}

		/// <summary>
		/// <para>Symbol: DB Trigger Runs</para>
		/// <para><c>&lt;DB Trigger Runs&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDbtriggerruns(TerminalToken token)
		{
			throw new NotImplementedException("Symbol DB Trigger Runs");
		}

		/// <summary>
		/// <para>Symbol: Dict List</para>
		/// <para><c>&lt;Dict List&gt;</c></para>
		/// </summary>
		protected virtual object TerminalDictlist(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Dict List");
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
		/// <para>Symbol: Func Arg</para>
		/// <para><c>&lt;Func Arg&gt;</c></para>
		/// </summary>
		protected virtual object TerminalFuncarg(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Func Arg");
		}

		/// <summary>
		/// <para>Symbol: Func args</para>
		/// <para><c>&lt;Func args&gt;</c></para>
		/// </summary>
		protected virtual object TerminalFuncargs(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Func args");
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
		/// <para>Symbol: ID List</para>
		/// <para><c>&lt;ID List&gt;</c></para>
		/// </summary>
		protected virtual object TerminalIdlist(TerminalToken token)
		{
			throw new NotImplementedException("Symbol ID List");
		}

		/// <summary>
		/// <para>Symbol: Layout Block</para>
		/// <para><c>&lt;Layout Block&gt;</c></para>
		/// </summary>
		protected virtual object TerminalLayoutblock(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Layout Block");
		}

		/// <summary>
		/// <para>Symbol: Layout List</para>
		/// <para><c>&lt;Layout List&gt;</c></para>
		/// </summary>
		protected virtual object TerminalLayoutlist(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Layout List");
		}

		/// <summary>
		/// <para>Symbol: Menu Block</para>
		/// <para><c>&lt;Menu Block&gt;</c></para>
		/// </summary>
		protected virtual object TerminalMenublock(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Menu Block");
		}

		/// <summary>
		/// <para>Symbol: Menu Item</para>
		/// <para><c>&lt;Menu Item&gt;</c></para>
		/// </summary>
		protected virtual object TerminalMenuitem2(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Menu Item");
		}

		/// <summary>
		/// <para>Symbol: MenuItems List</para>
		/// <para><c>&lt;MenuItems List&gt;</c></para>
		/// </summary>
		protected virtual object TerminalMenuitemslist(TerminalToken token)
		{
			throw new NotImplementedException("Symbol MenuItems List");
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
		/// <para>Symbol: NumLiteral</para>
		/// <para><c>&lt;NumLiteral&gt;</c></para>
		/// </summary>
		protected virtual object TerminalNumliteral(TerminalToken token)
		{
			throw new NotImplementedException("Symbol NumLiteral");
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
		/// <para>Symbol: Op Assign</para>
		/// <para><c>&lt;Op Assign&gt;</c></para>
		/// </summary>
		protected virtual object TerminalOpassign(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Op Assign");
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
		/// <para>Symbol: QualifiedName</para>
		/// <para><c>&lt;QualifiedName&gt;</c></para>
		/// </summary>
		protected virtual object TerminalQualifiedname(TerminalToken token)
		{
			throw new NotImplementedException("Symbol QualifiedName");
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
		/// <para>Symbol: TryFinally</para>
		/// <para><c>&lt;TryFinally&gt;</c></para>
		/// </summary>
		protected virtual object TerminalTryfinally(TerminalToken token)
		{
			throw new NotImplementedException("Symbol TryFinally");
		}

		/// <summary>
		/// <para>Symbol: Value</para>
		/// <para><c>&lt;Value&gt;</c></para>
		/// </summary>
		protected virtual object TerminalValue(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Value");
		}

		/// <summary>
		/// <para>Symbol: Widget</para>
		/// <para><c>&lt;Widget&gt;</c></para>
		/// </summary>
		protected virtual object TerminalWidget2(TerminalToken token)
		{
			throw new NotImplementedException("Symbol Widget");
		}

		/// <summary>
		/// <para>Symbol: WndParam</para>
		/// <para><c>&lt;WndParam&gt;</c></para>
		/// </summary>
		protected virtual object TerminalWndparam(TerminalToken token)
		{
			throw new NotImplementedException("Symbol WndParam");
		}

		/// <summary>
		/// <para>Symbol: WndParam List</para>
		/// <para><c>&lt;WndParam List&gt;</c></para>
		/// </summary>
		protected virtual object TerminalWndparamlist(TerminalToken token)
		{
			throw new NotImplementedException("Symbol WndParam List");
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

                case (int)Symbols.After: //after
                	return TerminalAfter(token);

                case (int)Symbols.And: //and
                	return TerminalAnd(token);

                case (int)Symbols.As: //as
                	return TerminalAs(token);

                case (int)Symbols.Before: //before
                	return TerminalBefore(token);

                case (int)Symbols.Bool: //bool
                	return TerminalBool(token);

                case (int)Symbols.Break: //break
                	return TerminalBreak(token);

                case (int)Symbols.Button: //button
                	return TerminalButton(token);

                case (int)Symbols.Case: //case
                	return TerminalCase(token);

                case (int)Symbols.Cast: //cast
                	return TerminalCast(token);

                case (int)Symbols.Catch: //catch
                	return TerminalCatch(token);

                case (int)Symbols.Continue: //continue
                	return TerminalContinue(token);

                case (int)Symbols.Database: //database
                	return TerminalDatabase(token);

                case (int)Symbols.Date: //date
                	return TerminalDate(token);

                case (int)Symbols.Daterange: //daterange
                	return TerminalDaterange(token);

                case (int)Symbols.Datetime: //datetime
                	return TerminalDatetime(token);

                case (int)Symbols.Datetimeliteral: //DateTimeLiteral
                	return TerminalDatetimeliteral(token);

                case (int)Symbols.Datetimerange: //datetimerange
                	return TerminalDatetimerange(token);

                case (int)Symbols.Decimal: //decimal
                	return TerminalDecimal(token);

                case (int)Symbols.Decimalliteral: //DecimalLiteral
                	return TerminalDecimalliteral(token);

                case (int)Symbols.Default: //default
                	return TerminalDefault(token);

                case (int)Symbols.Delete: //delete
                	return TerminalDelete(token);

                case (int)Symbols.Dict: //dict
                	return TerminalDict(token);

                case (int)Symbols.Do: //do
                	return TerminalDo(token);

                case (int)Symbols.Else: //else
                	return TerminalElse(token);

                case (int)Symbols.End: //end
                	return TerminalEnd(token);

                case (int)Symbols.Extends: //extends
                	return TerminalExtends(token);

                case (int)Symbols.False: //false
                	return TerminalFalse(token);

                case (int)Symbols.Finally: //finally
                	return TerminalFinally(token);

                case (int)Symbols.For: //for
                	return TerminalFor(token);

                case (int)Symbols.Foreach: //foreach
                	return TerminalForeach(token);

                case (int)Symbols.Foreign: //foreign
                	return TerminalForeign(token);

                case (int)Symbols.Function: //function
                	return TerminalFunction(token);

                case (int)Symbols.Get: //get
                	return TerminalGet(token);

                case (int)Symbols.Hbox: //hbox
                	return TerminalHbox(token);

                case (int)Symbols.Hbuttonbox: //hbuttonbox
                	return TerminalHbuttonbox(token);

                case (int)Symbols.Id: //ID
                	return TerminalId(token);

                case (int)Symbols.If: //if
                	return TerminalIf(token);

                case (int)Symbols.Image: //image
                	return TerminalImage(token);

                case (int)Symbols.In: //in
                	return TerminalIn(token);

                case (int)Symbols.Index: //index
                	return TerminalIndex(token);

                case (int)Symbols.Insert: //insert
                	return TerminalInsert(token);

                case (int)Symbols.Integer: //integer
                	return TerminalInteger(token);

                case (int)Symbols.Intliteral: //IntLiteral
                	return TerminalIntliteral(token);

                case (int)Symbols.Is: //is
                	return TerminalIs(token);

                case (int)Symbols.List: //list
                	return TerminalList(token);

                case (int)Symbols.Many: //many
                	return TerminalMany(token);

                case (int)Symbols.Menu: //menu
                	return TerminalMenu(token);

                case (int)Symbols.Menuitem: //menuitem
                	return TerminalMenuitem(token);

                case (int)Symbols.Modified: //modified
                	return TerminalModified(token);

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

                case (int)Symbols.Position: //position
                	return TerminalPosition(token);

                case (int)Symbols.Primary: //primary
                	return TerminalPrimary(token);

                case (int)Symbols.Property: //property
                	return TerminalProperty(token);

                case (int)Symbols.Range: //range
                	return TerminalRange(token);

                case (int)Symbols.Return: //return
                	return TerminalReturn(token);

                case (int)Symbols.Select: //select
                	return TerminalSelect(token);

                case (int)Symbols.Separator: //Separator
                	return TerminalSeparator(token);

                case (int)Symbols.Set: //set
                	return TerminalSet(token);

                case (int)Symbols.Static: //static
                	return TerminalStatic(token);

                case (int)Symbols.Stringliteral: //StringLiteral
                	return TerminalStringliteral(token);

                case (int)Symbols.Switch: //switch
                	return TerminalSwitch(token);

                case (int)Symbols.Table: //table
                	return TerminalTable(token);

                case (int)Symbols.Template: //template
                	return TerminalTemplate(token);

                case (int)Symbols.Through: //through
                	return TerminalThrough(token);

                case (int)Symbols.Throw: //throw
                	return TerminalThrow(token);

                case (int)Symbols.Time: //time
                	return TerminalTime(token);

                case (int)Symbols.Timerange: //timerange
                	return TerminalTimerange(token);

                case (int)Symbols.Timespanliteral: //TimeSpanLiteral
                	return TerminalTimespanliteral(token);

                case (int)Symbols.Toolbar: //toolbar
                	return TerminalToolbar(token);

                case (int)Symbols.Toolbutton: //toolbutton
                	return TerminalToolbutton(token);

                case (int)Symbols.True: //true
                	return TerminalTrue(token);

                case (int)Symbols.Try: //try
                	return TerminalTry(token);

                case (int)Symbols.Type: //type
                	return TerminalType(token);

                case (int)Symbols.Unique: //unique
                	return TerminalUnique(token);

                case (int)Symbols.Update: //update
                	return TerminalUpdate(token);

                case (int)Symbols.Using: //using
                	return TerminalUsing(token);

                case (int)Symbols.Var: //var
                	return TerminalVar(token);

                case (int)Symbols.Varchar: //varchar
                	return TerminalVarchar(token);

                case (int)Symbols.Vbox: //vbox
                	return TerminalVbox(token);

                case (int)Symbols.Vbuttonbox: //vbuttonbox
                	return TerminalVbuttonbox(token);

                case (int)Symbols.While: //while
                	return TerminalWhile(token);

                case (int)Symbols.Widget: //widget
                	return TerminalWidget(token);

                case (int)Symbols.Window: //window
                	return TerminalWindow(token);

                case (int)Symbols.Arg: //<Arg>
                	return TerminalArg(token);

                case (int)Symbols.Args: //<Args>
                	return TerminalArgs(token);

                case (int)Symbols.Block: //<Block>
                	return TerminalBlock(token);

                case (int)Symbols.Casestms: //<Case Stms>
                	return TerminalCasestms(token);

                case (int)Symbols.Catch2: //<Catch>
                	return TerminalCatch2(token);

                case (int)Symbols.Catchs: //<Catchs>
                	return TerminalCatchs(token);

                case (int)Symbols.Database2: //<Database>
                	return TerminalDatabase2(token);

                case (int)Symbols.Dbcolumn: //<DB Column>
                	return TerminalDbcolumn(token);

                case (int)Symbols.Dbcolumnattr: //<DB Column Attr>
                	return TerminalDbcolumnattr(token);

                case (int)Symbols.Dbcolumnattrlist: //<DB Column Attr List>
                	return TerminalDbcolumnattrlist(token);

                case (int)Symbols.Dbcolumntype: //<DB Column Type>
                	return TerminalDbcolumntype(token);

                case (int)Symbols.Dbcolumns: //<DB Columns>
                	return TerminalDbcolumns(token);

                case (int)Symbols.Dbtable: //<DB Table>
                	return TerminalDbtable(token);

                case (int)Symbols.Dbtableattr: //<DB Table Attr>
                	return TerminalDbtableattr(token);

                case (int)Symbols.Dbtableattrlist: //<DB Table Attr List>
                	return TerminalDbtableattrlist(token);

                case (int)Symbols.Dbtables: //<DB Tables>
                	return TerminalDbtables(token);

                case (int)Symbols.Dbtriggerrun: //<DB Trigger Run>
                	return TerminalDbtriggerrun(token);

                case (int)Symbols.Dbtriggerruns: //<DB Trigger Runs>
                	return TerminalDbtriggerruns(token);

                case (int)Symbols.Dictlist: //<Dict List>
                	return TerminalDictlist(token);

                case (int)Symbols.Expr: //<Expr>
                	return TerminalExpr(token);

                case (int)Symbols.Exprlist: //<Expr List>
                	return TerminalExprlist(token);

                case (int)Symbols.Funcarg: //<Func Arg>
                	return TerminalFuncarg(token);

                case (int)Symbols.Funcargs: //<Func args>
                	return TerminalFuncargs(token);

                case (int)Symbols.Function2: //<Function>
                	return TerminalFunction2(token);

                case (int)Symbols.Idlist: //<ID List>
                	return TerminalIdlist(token);

                case (int)Symbols.Layoutblock: //<Layout Block>
                	return TerminalLayoutblock(token);

                case (int)Symbols.Layoutlist: //<Layout List>
                	return TerminalLayoutlist(token);

                case (int)Symbols.Menublock: //<Menu Block>
                	return TerminalMenublock(token);

                case (int)Symbols.Menuitem2: //<Menu Item>
                	return TerminalMenuitem2(token);

                case (int)Symbols.Menuitemslist: //<MenuItems List>
                	return TerminalMenuitemslist(token);

                case (int)Symbols.Normalstm: //<Normal Stm>
                	return TerminalNormalstm(token);

                case (int)Symbols.Numliteral: //<NumLiteral>
                	return TerminalNumliteral(token);

                case (int)Symbols.Opadd: //<Op Add>
                	return TerminalOpadd(token);

                case (int)Symbols.Opand: //<Op And>
                	return TerminalOpand(token);

                case (int)Symbols.Opassign: //<Op Assign>
                	return TerminalOpassign(token);

                case (int)Symbols.Opcompare: //<Op Compare>
                	return TerminalOpcompare(token);

                case (int)Symbols.Opequate: //<Op Equate>
                	return TerminalOpequate(token);

                case (int)Symbols.Opif: //<Op If>
                	return TerminalOpif(token);

                case (int)Symbols.Opmult: //<Op Mult>
                	return TerminalOpmult(token);

                case (int)Symbols.Opor: //<Op Or>
                	return TerminalOpor(token);

                case (int)Symbols.Oppointer: //<Op Pointer>
                	return TerminalOppointer(token);

                case (int)Symbols.Opunary: //<Op Unary>
                	return TerminalOpunary(token);

                case (int)Symbols.Qualifiedname: //<QualifiedName>
                	return TerminalQualifiedname(token);

                case (int)Symbols.Stm: //<Stm>
                	return TerminalStm(token);

                case (int)Symbols.Stmlist: //<Stm List>
                	return TerminalStmlist(token);

                case (int)Symbols.Thenstm: //<Then Stm>
                	return TerminalThenstm(token);

                case (int)Symbols.Tryfinally: //<TryFinally>
                	return TerminalTryfinally(token);

                case (int)Symbols.Value: //<Value>
                	return TerminalValue(token);

                case (int)Symbols.Widget2: //<Widget>
                	return TerminalWidget2(token);

                case (int)Symbols.Wndparam: //<WndParam>
                	return TerminalWndparam(token);

                case (int)Symbols.Wndparamlist: //<WndParam List>
                	return TerminalWndparamlist(token);

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
        protected abstract object RuleQualifiednameDotId(NonterminalToken token); // <QualifiedName> ::= <QualifiedName> '.' ID
        protected abstract object RuleQualifiednameId(NonterminalToken token); // <QualifiedName> ::= ID
        protected abstract object RuleStmIfLparanRparan(NonterminalToken token); // <Stm> ::= if '(' <Expr> ')' <Stm>
        protected abstract object RuleStmIfLparanRparanElse(NonterminalToken token); // <Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>
        protected abstract object RuleStmWhileLparanRparan(NonterminalToken token); // <Stm> ::= while '(' <Expr> ')' <Stm>
        protected abstract object RuleStmForLparanSemiSemiRparan(NonterminalToken token); // <Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>
        protected abstract object RuleStmForeachLparanIdInRparan(NonterminalToken token); // <Stm> ::= foreach '(' ID in <Expr> ')' <Stm>
        protected abstract object RuleStmUsingSemi(NonterminalToken token); // <Stm> ::= using <QualifiedName> ';'
        protected abstract object RuleStmUsingStringliteralSemi(NonterminalToken token); // <Stm> ::= using StringLiteral ';'
        protected abstract object RuleStmUsingAsIdSemi(NonterminalToken token); // <Stm> ::= using <QualifiedName> as ID ';'
        protected abstract object RuleStmUsingStringliteralAsIdSemi(NonterminalToken token); // <Stm> ::= using StringLiteral as ID ';'
        protected abstract object RuleStmUsingLparanRparan(NonterminalToken token); // <Stm> ::= using '(' <Expr> ')' <Stm>
        protected abstract object RuleStmObservedLparanRparan(NonterminalToken token); // <Stm> ::= observed '(' <Expr List> ')' <Stm>
        protected abstract object RuleStm(NonterminalToken token); // <Stm> ::= <TryFinally>
        protected abstract object RuleStm2(NonterminalToken token); // <Stm> ::= <Normal Stm>
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
        protected abstract object RuleNormalstmThrowSemi(NonterminalToken token); // <Normal Stm> ::= throw <Expr> ';'
        protected abstract object RuleNormalstmThrowCommaSemi(NonterminalToken token); // <Normal Stm> ::= throw <Expr> ',' <Expr> ';'
        protected abstract object RuleNormalstmSemi2(NonterminalToken token); // <Normal Stm> ::= ';'
        protected abstract object RuleTryfinallyTryFinally(NonterminalToken token); // <TryFinally> ::= try <Normal Stm> <Catchs> finally <Normal Stm>
        protected abstract object RuleTryfinallyTry(NonterminalToken token); // <TryFinally> ::= try <Normal Stm> <Catchs>
        protected abstract object RuleCatchs(NonterminalToken token); // <Catchs> ::= <Catch> <Catchs>
        protected abstract object RuleCatchs2(NonterminalToken token); // <Catchs> ::= 
        protected abstract object RuleCatchCatchLparanIdRparan(NonterminalToken token); // <Catch> ::= catch '(' <QualifiedName> ID ')' <Normal Stm>
        protected abstract object RuleCatchCatchLparanTypeRparan(NonterminalToken token); // <Catch> ::= catch '(' type <QualifiedName> ')' <Normal Stm>
        protected abstract object RuleCatchCatchLparanVarIdRparan(NonterminalToken token); // <Catch> ::= catch '(' var ID ')' <Normal Stm>
        protected abstract object RuleCatchCatch(NonterminalToken token); // <Catch> ::= catch <Block>
        protected abstract object RuleFuncargsComma(NonterminalToken token); // <Func args> ::= <Func args> ',' <Func Arg>
        protected abstract object RuleFuncargs(NonterminalToken token); // <Func args> ::= <Func Arg>
        protected abstract object RuleFuncargs2(NonterminalToken token); // <Func args> ::= 
        protected abstract object RuleFuncargId(NonterminalToken token); // <Func Arg> ::= ID
        protected abstract object RuleFuncargIdEq(NonterminalToken token); // <Func Arg> ::= ID '=' <Expr>
        protected abstract object RuleArgsComma(NonterminalToken token); // <Args> ::= <Args> ',' <Arg>
        protected abstract object RuleArgs(NonterminalToken token); // <Args> ::= <Arg>
        protected abstract object RuleArgs2(NonterminalToken token); // <Args> ::= 
        protected abstract object RuleArg(NonterminalToken token); // <Arg> ::= <Op If>
        protected abstract object RuleArgIdEq(NonterminalToken token); // <Arg> ::= ID '=' <Expr>
        protected abstract object RuleCasestmsCaseColon(NonterminalToken token); // <Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>
        protected abstract object RuleCasestmsDefaultColon(NonterminalToken token); // <Case Stms> ::= default ':' <Stm List>
        protected abstract object RuleCasestms(NonterminalToken token); // <Case Stms> ::= 
        protected abstract object RuleBlockLbraceRbrace(NonterminalToken token); // <Block> ::= '{' <Stm List> '}'
        protected abstract object RuleStmlist(NonterminalToken token); // <Stm List> ::= <Stm> <Stm List>
        protected abstract object RuleStmlist2(NonterminalToken token); // <Stm List> ::= <Stm>
        protected abstract object RuleIdlistCommaId(NonterminalToken token); // <ID List> ::= <ID List> ',' ID
        protected abstract object RuleIdlistId(NonterminalToken token); // <ID List> ::= ID
        protected abstract object RuleIdlist(NonterminalToken token); // <ID List> ::= 
        protected abstract object RuleDatabaseDatabaseIdLbraceRbrace(NonterminalToken token); // <Database> ::= database ID '{' <DB Tables> '}'
        protected abstract object RuleDatabaseExtendsDatabaseIdLbraceRbrace(NonterminalToken token); // <Database> ::= extends database ID '{' <DB Tables> '}'
        protected abstract object RuleDbtables(NonterminalToken token); // <DB Tables> ::= <DB Tables> <DB Table>
        protected abstract object RuleDbtables2(NonterminalToken token); // <DB Tables> ::= 
        protected abstract object RuleDbtableTemplateTableIdLbraceRbrace(NonterminalToken token); // <DB Table> ::= template table ID '{' <DB Columns> '}' <DB Table Attr List>
        protected abstract object RuleDbtableTemplateTableIdTemplateIdLbraceRbrace(NonterminalToken token); // <DB Table> ::= template table ID template ID '{' <DB Columns> '}' <DB Table Attr List>
        protected abstract object RuleDbtableTableIdLbraceRbrace(NonterminalToken token); // <DB Table> ::= table ID '{' <DB Columns> '}' <DB Table Attr List>
        protected abstract object RuleDbtableTableIdTemplateIdLbraceRbrace(NonterminalToken token); // <DB Table> ::= table ID template ID '{' <DB Columns> '}' <DB Table Attr List>
        protected abstract object RuleDbtableExtendsTableIdLbraceRbrace(NonterminalToken token); // <DB Table> ::= extends table ID '{' <DB Columns> '}' <DB Table Attr List>
        protected abstract object RuleDbcolumns(NonterminalToken token); // <DB Columns> ::= <DB Columns> <DB Column>
        protected abstract object RuleDbcolumns2(NonterminalToken token); // <DB Columns> ::= 
        protected abstract object RuleDbcolumnIdSemi(NonterminalToken token); // <DB Column> ::= ID <DB Column Type> <DB Column Attr List> ';'
        protected abstract object RuleDbcolumntypePrimary(NonterminalToken token); // <DB Column Type> ::= primary
        protected abstract object RuleDbcolumntypeForeignId(NonterminalToken token); // <DB Column Type> ::= foreign ID
        protected abstract object RuleDbcolumntypeForeignIdLparanIdRparan(NonterminalToken token); // <DB Column Type> ::= foreign ID '(' ID ')'
        protected abstract object RuleDbcolumntypeManyId(NonterminalToken token); // <DB Column Type> ::= many ID
        protected abstract object RuleDbcolumntypeManyIdThroughId(NonterminalToken token); // <DB Column Type> ::= many ID through ID
        protected abstract object RuleDbcolumntypeManyIdThroughIdLparanIdCommaIdRparan(NonterminalToken token); // <DB Column Type> ::= many ID through ID '(' ID ',' ID ')'
        protected abstract object RuleDbcolumntypeVarchar(NonterminalToken token); // <DB Column Type> ::= varchar
        protected abstract object RuleDbcolumntypeVarcharLparanIntliteralRparan(NonterminalToken token); // <DB Column Type> ::= varchar '(' IntLiteral ')'
        protected abstract object RuleDbcolumntypeInteger(NonterminalToken token); // <DB Column Type> ::= integer
        protected abstract object RuleDbcolumntypeBool(NonterminalToken token); // <DB Column Type> ::= bool
        protected abstract object RuleDbcolumntypeDecimalLparanIntliteralCommaIntliteralRparan(NonterminalToken token); // <DB Column Type> ::= decimal '(' IntLiteral ',' IntLiteral ')'
        protected abstract object RuleDbcolumntypeDate(NonterminalToken token); // <DB Column Type> ::= date
        protected abstract object RuleDbcolumntypeTime(NonterminalToken token); // <DB Column Type> ::= time
        protected abstract object RuleDbcolumntypeDatetime(NonterminalToken token); // <DB Column Type> ::= datetime
        protected abstract object RuleDbcolumntypeDaterange(NonterminalToken token); // <DB Column Type> ::= daterange
        protected abstract object RuleDbcolumntypeTimerange(NonterminalToken token); // <DB Column Type> ::= timerange
        protected abstract object RuleDbcolumntypeDatetimerange(NonterminalToken token); // <DB Column Type> ::= datetimerange
        protected abstract object RuleDbcolumnattrlist(NonterminalToken token); // <DB Column Attr List> ::= <DB Column Attr List> <DB Column Attr>
        protected abstract object RuleDbcolumnattrlist2(NonterminalToken token); // <DB Column Attr List> ::= 
        protected abstract object RuleDbcolumnattrUnique(NonterminalToken token); // <DB Column Attr> ::= unique
        protected abstract object RuleDbcolumnattrNotNull(NonterminalToken token); // <DB Column Attr> ::= not null
        protected abstract object RuleDbcolumnattrIndex(NonterminalToken token); // <DB Column Attr> ::= index
        protected abstract object RuleDbcolumnattrDefault(NonterminalToken token); // <DB Column Attr> ::= default <Value>
        protected abstract object RuleDbcolumnattrIdEq(NonterminalToken token); // <DB Column Attr> ::= ID '=' <Value>
        protected abstract object RuleDbtableattrlist(NonterminalToken token); // <DB Table Attr List> ::= <DB Table Attr List> <DB Table Attr>
        protected abstract object RuleDbtableattrlist2(NonterminalToken token); // <DB Table Attr List> ::= 
        protected abstract object RuleDbtableattrIndexLparanRparanSemi(NonterminalToken token); // <DB Table Attr> ::= index '(' <ID List> ')' ';'
        protected abstract object RuleDbtableattrUniqueLparanRparanSemi(NonterminalToken token); // <DB Table Attr> ::= unique '(' <ID List> ')' ';'
        protected abstract object RuleDbtableattrPosition(NonterminalToken token); // <DB Table Attr> ::= <DB Trigger Runs> position <NumLiteral> <Stm>
        protected abstract object RuleDbtableattrIdEqSemi(NonterminalToken token); // <DB Table Attr> ::= ID '=' <Value> ';'
        protected abstract object RuleDbtriggerrunsComma(NonterminalToken token); // <DB Trigger Runs> ::= <DB Trigger Runs> ',' <DB Trigger Run>
        protected abstract object RuleDbtriggerruns(NonterminalToken token); // <DB Trigger Runs> ::= <DB Trigger Run>
        protected abstract object RuleDbtriggerrunBeforeSelect(NonterminalToken token); // <DB Trigger Run> ::= before select
        protected abstract object RuleDbtriggerrunAfterSelect(NonterminalToken token); // <DB Trigger Run> ::= after select
        protected abstract object RuleDbtriggerrunBeforeInsert(NonterminalToken token); // <DB Trigger Run> ::= before insert
        protected abstract object RuleDbtriggerrunAfterInsert(NonterminalToken token); // <DB Trigger Run> ::= after insert
        protected abstract object RuleDbtriggerrunBeforeUpdate(NonterminalToken token); // <DB Trigger Run> ::= before update
        protected abstract object RuleDbtriggerrunAfterUpdate(NonterminalToken token); // <DB Trigger Run> ::= after update
        protected abstract object RuleDbtriggerrunBeforeDelete(NonterminalToken token); // <DB Trigger Run> ::= before delete
        protected abstract object RuleDbtriggerrunAfterDelete(NonterminalToken token); // <DB Trigger Run> ::= after delete
        protected abstract object RuleDbtriggerrunBeforeModified(NonterminalToken token); // <DB Trigger Run> ::= before modified
        protected abstract object RuleDbtriggerrunAfterModified(NonterminalToken token); // <DB Trigger Run> ::= after modified
        protected abstract object RuleNumliteralIntliteral(NonterminalToken token); // <NumLiteral> ::= IntLiteral
        protected abstract object RuleNumliteralDecimalliteral(NonterminalToken token); // <NumLiteral> ::= DecimalLiteral
        protected abstract object RuleFunctionFunctionIdLparanRparan(NonterminalToken token); // <Function> ::= function ID '(' <Func args> ')' <Stm>
        protected abstract object RuleFunctionFunctionLparanRparan(NonterminalToken token); // <Function> ::= function '(' <Func args> ')' <Stm>
        protected abstract object RuleWidgetWindowId(NonterminalToken token); // <Widget> ::= window ID <WndParam List> <Layout Block>
        protected abstract object RuleWidgetWindow(NonterminalToken token); // <Widget> ::= window <WndParam List> <Layout Block>
        protected abstract object RuleWidgetWidgetId(NonterminalToken token); // <Widget> ::= widget ID <Layout Block>
        protected abstract object RuleWidgetWidget(NonterminalToken token); // <Widget> ::= widget <Layout Block>
        protected abstract object RuleWndparamlist(NonterminalToken token); // <WndParam List> ::= <WndParam> <WndParam List>
        protected abstract object RuleWndparamlist2(NonterminalToken token); // <WndParam List> ::= 
        protected abstract object RuleWndparamIdEqSemi(NonterminalToken token); // <WndParam> ::= ID '=' <Expr> ';'
        protected abstract object RuleWndparamIdSemi(NonterminalToken token); // <WndParam> ::= ID ';'
        protected abstract object RuleLayoutlist(NonterminalToken token); // <Layout List> ::= <Layout List> <Layout Block>
        protected abstract object RuleLayoutlist2(NonterminalToken token); // <Layout List> ::= 
        protected abstract object RuleLayoutblockHboxEnd(NonterminalToken token); // <Layout Block> ::= hbox <WndParam List> <Layout List> end
        protected abstract object RuleLayoutblockVboxEnd(NonterminalToken token); // <Layout Block> ::= vbox <WndParam List> <Layout List> end
        protected abstract object RuleLayoutblockHbuttonboxEnd(NonterminalToken token); // <Layout Block> ::= hbuttonbox <WndParam List> <Layout List> end
        protected abstract object RuleLayoutblockVbuttonboxEnd(NonterminalToken token); // <Layout Block> ::= vbuttonbox <WndParam List> <Layout List> end
        protected abstract object RuleLayoutblockTableEnd(NonterminalToken token); // <Layout Block> ::= table <WndParam List> <Layout List> end
        protected abstract object RuleLayoutblockToolbarEnd(NonterminalToken token); // <Layout Block> ::= toolbar <WndParam List> <Layout List> end
        protected abstract object RuleLayoutblockButton(NonterminalToken token); // <Layout Block> ::= button <WndParam List> <Layout Block>
        protected abstract object RuleLayoutblockButtonEnd(NonterminalToken token); // <Layout Block> ::= button <WndParam List> end
        protected abstract object RuleLayoutblockToolbutton(NonterminalToken token); // <Layout Block> ::= toolbutton <WndParam List> <Layout Block>
        protected abstract object RuleLayoutblockToolbuttonEnd(NonterminalToken token); // <Layout Block> ::= toolbutton <WndParam List> end
        protected abstract object RuleLayoutblockImage(NonterminalToken token); // <Layout Block> ::= image <WndParam List>
        protected abstract object RuleLayoutblockStringliteral(NonterminalToken token); // <Layout Block> ::= StringLiteral <WndParam List>
        protected abstract object RuleLayoutblock(NonterminalToken token); // <Layout Block> ::= <Menu Block>
        protected abstract object RuleLayoutblockLbracketRbracket(NonterminalToken token); // <Layout Block> ::= '[' <Expr> ']' <WndParam List>
        protected abstract object RuleMenublockMenuEnd(NonterminalToken token); // <Menu Block> ::= menu <WndParam List> <MenuItems List> end
        protected abstract object RuleMenuitemslist(NonterminalToken token); // <MenuItems List> ::= <Menu Item> <MenuItems List>
        protected abstract object RuleMenuitemslist2(NonterminalToken token); // <MenuItems List> ::= 
        protected abstract object RuleMenuitem(NonterminalToken token); // <Menu Item> ::= <Menu Block>
        protected abstract object RuleMenuitemMenuitem(NonterminalToken token); // <Menu Item> ::= menuitem <WndParam List>
        protected abstract object RuleMenuitemSeparator(NonterminalToken token); // <Menu Item> ::= Separator
        protected abstract object RuleExprlistComma(NonterminalToken token); // <Expr List> ::= <Expr List> ',' <Expr>
        protected abstract object RuleExprlist(NonterminalToken token); // <Expr List> ::= <Expr>
        protected abstract object RuleDictlistColonComma(NonterminalToken token); // <Dict List> ::= <Expr> ':' <Expr> ',' <Dict List>
        protected abstract object RuleDictlistColon(NonterminalToken token); // <Dict List> ::= <Expr> ':' <Expr>
        protected abstract object RuleDictlist(NonterminalToken token); // <Dict List> ::= 
        protected abstract object RuleExpr(NonterminalToken token); // <Expr> ::= <Widget>
        protected abstract object RuleExpr2(NonterminalToken token); // <Expr> ::= <Database>
        protected abstract object RuleExpr3(NonterminalToken token); // <Expr> ::= <Op Assign>
        protected abstract object RuleOpassignEq(NonterminalToken token); // <Op Assign> ::= <Op If> '=' <Expr>
        protected abstract object RuleOpassignPluseq(NonterminalToken token); // <Op Assign> ::= <Op If> '+=' <Expr>
        protected abstract object RuleOpassignMinuseq(NonterminalToken token); // <Op Assign> ::= <Op If> '-=' <Expr>
        protected abstract object RuleOpassignTimeseq(NonterminalToken token); // <Op Assign> ::= <Op If> '*=' <Expr>
        protected abstract object RuleOpassignDiveq(NonterminalToken token); // <Op Assign> ::= <Op If> '/=' <Expr>
        protected abstract object RuleOpassignLteqeq(NonterminalToken token); // <Op Assign> ::= <Op If> '<==' <Expr>
        protected abstract object RuleOpassignLteqeqgt(NonterminalToken token); // <Op Assign> ::= <Op If> '<==>' <Expr>
        protected abstract object RuleOpassign(NonterminalToken token); // <Op Assign> ::= <Op If>
        protected abstract object RuleOpifQuestionColon(NonterminalToken token); // <Op If> ::= <Op Or> '?' <Op If> ':' <Op If>
        protected abstract object RuleOpif(NonterminalToken token); // <Op If> ::= <Op Or>
        protected abstract object RuleOporOr(NonterminalToken token); // <Op Or> ::= <Op Or> or <Op And>
        protected abstract object RuleOpor(NonterminalToken token); // <Op Or> ::= <Op And>
        protected abstract object RuleOpandAnd(NonterminalToken token); // <Op And> ::= <Op And> and <Op Equate>
        protected abstract object RuleOpand(NonterminalToken token); // <Op And> ::= <Op Equate>
        protected abstract object RuleOpequateEqeq(NonterminalToken token); // <Op Equate> ::= <Op Equate> '==' <Op Compare>
        protected abstract object RuleOpequateExclameq(NonterminalToken token); // <Op Equate> ::= <Op Equate> '!=' <Op Compare>
        protected abstract object RuleOpequateIn(NonterminalToken token); // <Op Equate> ::= <Op Equate> in <Op Compare>
        protected abstract object RuleOpequate(NonterminalToken token); // <Op Equate> ::= <Op Compare>
        protected abstract object RuleOpcompareLt(NonterminalToken token); // <Op Compare> ::= <Op Compare> '<' <Op Add>
        protected abstract object RuleOpcompareGt(NonterminalToken token); // <Op Compare> ::= <Op Compare> '>' <Op Add>
        protected abstract object RuleOpcompareLteq(NonterminalToken token); // <Op Compare> ::= <Op Compare> '<=' <Op Add>
        protected abstract object RuleOpcompareGteq(NonterminalToken token); // <Op Compare> ::= <Op Compare> '>=' <Op Add>
        protected abstract object RuleOpcompare(NonterminalToken token); // <Op Compare> ::= <Op Add>
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
        protected abstract object RuleOpunaryCastLparanAsRparan(NonterminalToken token); // <Op Unary> ::= cast '(' <Op Unary> as <QualifiedName> ')'
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
        protected abstract object RuleOppointerLparanRparan(NonterminalToken token); // <Op Pointer> ::= <Op Pointer> '(' <Args> ')'
        protected abstract object RuleOppointer(NonterminalToken token); // <Op Pointer> ::= <Value>
        protected abstract object RuleValueIntliteral(NonterminalToken token); // <Value> ::= IntLiteral
        protected abstract object RuleValueStringliteral(NonterminalToken token); // <Value> ::= StringLiteral
        protected abstract object RuleValueDecimalliteral(NonterminalToken token); // <Value> ::= DecimalLiteral
        protected abstract object RuleValueDatetimeliteral(NonterminalToken token); // <Value> ::= DateTimeLiteral
        protected abstract object RuleValueTimespanliteral(NonterminalToken token); // <Value> ::= TimeSpanLiteral
        protected abstract object RuleValueTypeLparanRparan(NonterminalToken token); // <Value> ::= type '(' <QualifiedName> ')'
        protected abstract object RuleValueNewLparanRparan(NonterminalToken token); // <Value> ::= new <QualifiedName> '(' <Args> ')'
        protected abstract object RuleValueId(NonterminalToken token); // <Value> ::= ID
        protected abstract object RuleValueVarId(NonterminalToken token); // <Value> ::= var ID
        protected abstract object RuleValueStaticId(NonterminalToken token); // <Value> ::= static ID
        protected abstract object RuleValueLparanRparan(NonterminalToken token); // <Value> ::= '(' <Expr> ')'
        protected abstract object RuleValueLbracketRbracket(NonterminalToken token); // <Value> ::= '[' <Expr List> ']'
        protected abstract object RuleValueLbraceRbrace(NonterminalToken token); // <Value> ::= '{' <Dict List> '}'
        protected abstract object RuleValueRangeLtSemiGt(NonterminalToken token); // <Value> ::= range '<' <Op Add> ';' <Op Add> '>'
        protected abstract object RuleValueRangeLtSemiRparan(NonterminalToken token); // <Value> ::= range '<' <Op Add> ';' <Op Add> ')'
        protected abstract object RuleValueRangeLparanSemiGt(NonterminalToken token); // <Value> ::= range '(' <Op Add> ';' <Op Add> '>'
        protected abstract object RuleValueRangeLparanSemiRparan(NonterminalToken token); // <Value> ::= range '(' <Op Add> ';' <Op Add> ')'
        protected abstract object RuleValuePropertySemi(NonterminalToken token); // <Value> ::= property <Expr> ';'
        protected abstract object RuleValuePropertyGetSemi(NonterminalToken token); // <Value> ::= property <Expr> get <Expr> ';'
        protected abstract object RuleValuePropertyGetSetSemi(NonterminalToken token); // <Value> ::= property <Expr> get <Expr> set <Expr> ';'
        protected abstract object RuleValue(NonterminalToken token); // <Value> ::= <Function>
        protected abstract object RuleValueDict(NonterminalToken token); // <Value> ::= dict
        protected abstract object RuleValueList(NonterminalToken token); // <Value> ::= list
        protected abstract object RuleValueNull(NonterminalToken token); // <Value> ::= null
        protected abstract object RuleValueTrue(NonterminalToken token); // <Value> ::= true
        protected abstract object RuleValueFalse(NonterminalToken token); // <Value> ::= false


		#endregion

        public virtual object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id + ToolScriptParserBase.RulesOffset)
            {
                case (int)Symbols.RuleQualifiednameDotId: //<QualifiedName> ::= <QualifiedName> '.' ID
                	return RuleQualifiednameDotId(token);
                case (int)Symbols.RuleQualifiednameId: //<QualifiedName> ::= ID
                	return RuleQualifiednameId(token);
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
                case (int)Symbols.RuleStmUsingSemi: //<Stm> ::= using <QualifiedName> ';'
                	return RuleStmUsingSemi(token);
                case (int)Symbols.RuleStmUsingStringliteralSemi: //<Stm> ::= using StringLiteral ';'
                	return RuleStmUsingStringliteralSemi(token);
                case (int)Symbols.RuleStmUsingAsIdSemi: //<Stm> ::= using <QualifiedName> as ID ';'
                	return RuleStmUsingAsIdSemi(token);
                case (int)Symbols.RuleStmUsingStringliteralAsIdSemi: //<Stm> ::= using StringLiteral as ID ';'
                	return RuleStmUsingStringliteralAsIdSemi(token);
                case (int)Symbols.RuleStmUsingLparanRparan: //<Stm> ::= using '(' <Expr> ')' <Stm>
                	return RuleStmUsingLparanRparan(token);
                case (int)Symbols.RuleStmObservedLparanRparan: //<Stm> ::= observed '(' <Expr List> ')' <Stm>
                	return RuleStmObservedLparanRparan(token);
                case (int)Symbols.RuleStm: //<Stm> ::= <TryFinally>
                	return RuleStm(token);
                case (int)Symbols.RuleStm2: //<Stm> ::= <Normal Stm>
                	return RuleStm2(token);
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
                case (int)Symbols.RuleNormalstmThrowSemi: //<Normal Stm> ::= throw <Expr> ';'
                	return RuleNormalstmThrowSemi(token);
                case (int)Symbols.RuleNormalstmThrowCommaSemi: //<Normal Stm> ::= throw <Expr> ',' <Expr> ';'
                	return RuleNormalstmThrowCommaSemi(token);
                case (int)Symbols.RuleNormalstmSemi2: //<Normal Stm> ::= ';'
                	return RuleNormalstmSemi2(token);
                case (int)Symbols.RuleTryfinallyTryFinally: //<TryFinally> ::= try <Normal Stm> <Catchs> finally <Normal Stm>
                	return RuleTryfinallyTryFinally(token);
                case (int)Symbols.RuleTryfinallyTry: //<TryFinally> ::= try <Normal Stm> <Catchs>
                	return RuleTryfinallyTry(token);
                case (int)Symbols.RuleCatchs: //<Catchs> ::= <Catch> <Catchs>
                	return RuleCatchs(token);
                case (int)Symbols.RuleCatchs2: //<Catchs> ::= 
                	return RuleCatchs2(token);
                case (int)Symbols.RuleCatchCatchLparanIdRparan: //<Catch> ::= catch '(' <QualifiedName> ID ')' <Normal Stm>
                	return RuleCatchCatchLparanIdRparan(token);
                case (int)Symbols.RuleCatchCatchLparanTypeRparan: //<Catch> ::= catch '(' type <QualifiedName> ')' <Normal Stm>
                	return RuleCatchCatchLparanTypeRparan(token);
                case (int)Symbols.RuleCatchCatchLparanVarIdRparan: //<Catch> ::= catch '(' var ID ')' <Normal Stm>
                	return RuleCatchCatchLparanVarIdRparan(token);
                case (int)Symbols.RuleCatchCatch: //<Catch> ::= catch <Block>
                	return RuleCatchCatch(token);
                case (int)Symbols.RuleFuncargsComma: //<Func args> ::= <Func args> ',' <Func Arg>
                	return RuleFuncargsComma(token);
                case (int)Symbols.RuleFuncargs: //<Func args> ::= <Func Arg>
                	return RuleFuncargs(token);
                case (int)Symbols.RuleFuncargs2: //<Func args> ::= 
                	return RuleFuncargs2(token);
                case (int)Symbols.RuleFuncargId: //<Func Arg> ::= ID
                	return RuleFuncargId(token);
                case (int)Symbols.RuleFuncargIdEq: //<Func Arg> ::= ID '=' <Expr>
                	return RuleFuncargIdEq(token);
                case (int)Symbols.RuleArgsComma: //<Args> ::= <Args> ',' <Arg>
                	return RuleArgsComma(token);
                case (int)Symbols.RuleArgs: //<Args> ::= <Arg>
                	return RuleArgs(token);
                case (int)Symbols.RuleArgs2: //<Args> ::= 
                	return RuleArgs2(token);
                case (int)Symbols.RuleArg: //<Arg> ::= <Op If>
                	return RuleArg(token);
                case (int)Symbols.RuleArgIdEq: //<Arg> ::= ID '=' <Expr>
                	return RuleArgIdEq(token);
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
                case (int)Symbols.RuleStmlist2: //<Stm List> ::= <Stm>
                	return RuleStmlist2(token);
                case (int)Symbols.RuleIdlistCommaId: //<ID List> ::= <ID List> ',' ID
                	return RuleIdlistCommaId(token);
                case (int)Symbols.RuleIdlistId: //<ID List> ::= ID
                	return RuleIdlistId(token);
                case (int)Symbols.RuleIdlist: //<ID List> ::= 
                	return RuleIdlist(token);
                case (int)Symbols.RuleDatabaseDatabaseIdLbraceRbrace: //<Database> ::= database ID '{' <DB Tables> '}'
                	return RuleDatabaseDatabaseIdLbraceRbrace(token);
                case (int)Symbols.RuleDatabaseExtendsDatabaseIdLbraceRbrace: //<Database> ::= extends database ID '{' <DB Tables> '}'
                	return RuleDatabaseExtendsDatabaseIdLbraceRbrace(token);
                case (int)Symbols.RuleDbtables: //<DB Tables> ::= <DB Tables> <DB Table>
                	return RuleDbtables(token);
                case (int)Symbols.RuleDbtables2: //<DB Tables> ::= 
                	return RuleDbtables2(token);
                case (int)Symbols.RuleDbtableTemplateTableIdLbraceRbrace: //<DB Table> ::= template table ID '{' <DB Columns> '}' <DB Table Attr List>
                	return RuleDbtableTemplateTableIdLbraceRbrace(token);
                case (int)Symbols.RuleDbtableTemplateTableIdTemplateIdLbraceRbrace: //<DB Table> ::= template table ID template ID '{' <DB Columns> '}' <DB Table Attr List>
                	return RuleDbtableTemplateTableIdTemplateIdLbraceRbrace(token);
                case (int)Symbols.RuleDbtableTableIdLbraceRbrace: //<DB Table> ::= table ID '{' <DB Columns> '}' <DB Table Attr List>
                	return RuleDbtableTableIdLbraceRbrace(token);
                case (int)Symbols.RuleDbtableTableIdTemplateIdLbraceRbrace: //<DB Table> ::= table ID template ID '{' <DB Columns> '}' <DB Table Attr List>
                	return RuleDbtableTableIdTemplateIdLbraceRbrace(token);
                case (int)Symbols.RuleDbtableExtendsTableIdLbraceRbrace: //<DB Table> ::= extends table ID '{' <DB Columns> '}' <DB Table Attr List>
                	return RuleDbtableExtendsTableIdLbraceRbrace(token);
                case (int)Symbols.RuleDbcolumns: //<DB Columns> ::= <DB Columns> <DB Column>
                	return RuleDbcolumns(token);
                case (int)Symbols.RuleDbcolumns2: //<DB Columns> ::= 
                	return RuleDbcolumns2(token);
                case (int)Symbols.RuleDbcolumnIdSemi: //<DB Column> ::= ID <DB Column Type> <DB Column Attr List> ';'
                	return RuleDbcolumnIdSemi(token);
                case (int)Symbols.RuleDbcolumntypePrimary: //<DB Column Type> ::= primary
                	return RuleDbcolumntypePrimary(token);
                case (int)Symbols.RuleDbcolumntypeForeignId: //<DB Column Type> ::= foreign ID
                	return RuleDbcolumntypeForeignId(token);
                case (int)Symbols.RuleDbcolumntypeForeignIdLparanIdRparan: //<DB Column Type> ::= foreign ID '(' ID ')'
                	return RuleDbcolumntypeForeignIdLparanIdRparan(token);
                case (int)Symbols.RuleDbcolumntypeManyId: //<DB Column Type> ::= many ID
                	return RuleDbcolumntypeManyId(token);
                case (int)Symbols.RuleDbcolumntypeManyIdThroughId: //<DB Column Type> ::= many ID through ID
                	return RuleDbcolumntypeManyIdThroughId(token);
                case (int)Symbols.RuleDbcolumntypeManyIdThroughIdLparanIdCommaIdRparan: //<DB Column Type> ::= many ID through ID '(' ID ',' ID ')'
                	return RuleDbcolumntypeManyIdThroughIdLparanIdCommaIdRparan(token);
                case (int)Symbols.RuleDbcolumntypeVarchar: //<DB Column Type> ::= varchar
                	return RuleDbcolumntypeVarchar(token);
                case (int)Symbols.RuleDbcolumntypeVarcharLparanIntliteralRparan: //<DB Column Type> ::= varchar '(' IntLiteral ')'
                	return RuleDbcolumntypeVarcharLparanIntliteralRparan(token);
                case (int)Symbols.RuleDbcolumntypeInteger: //<DB Column Type> ::= integer
                	return RuleDbcolumntypeInteger(token);
                case (int)Symbols.RuleDbcolumntypeBool: //<DB Column Type> ::= bool
                	return RuleDbcolumntypeBool(token);
                case (int)Symbols.RuleDbcolumntypeDecimalLparanIntliteralCommaIntliteralRparan: //<DB Column Type> ::= decimal '(' IntLiteral ',' IntLiteral ')'
                	return RuleDbcolumntypeDecimalLparanIntliteralCommaIntliteralRparan(token);
                case (int)Symbols.RuleDbcolumntypeDate: //<DB Column Type> ::= date
                	return RuleDbcolumntypeDate(token);
                case (int)Symbols.RuleDbcolumntypeTime: //<DB Column Type> ::= time
                	return RuleDbcolumntypeTime(token);
                case (int)Symbols.RuleDbcolumntypeDatetime: //<DB Column Type> ::= datetime
                	return RuleDbcolumntypeDatetime(token);
                case (int)Symbols.RuleDbcolumntypeDaterange: //<DB Column Type> ::= daterange
                	return RuleDbcolumntypeDaterange(token);
                case (int)Symbols.RuleDbcolumntypeTimerange: //<DB Column Type> ::= timerange
                	return RuleDbcolumntypeTimerange(token);
                case (int)Symbols.RuleDbcolumntypeDatetimerange: //<DB Column Type> ::= datetimerange
                	return RuleDbcolumntypeDatetimerange(token);
                case (int)Symbols.RuleDbcolumnattrlist: //<DB Column Attr List> ::= <DB Column Attr List> <DB Column Attr>
                	return RuleDbcolumnattrlist(token);
                case (int)Symbols.RuleDbcolumnattrlist2: //<DB Column Attr List> ::= 
                	return RuleDbcolumnattrlist2(token);
                case (int)Symbols.RuleDbcolumnattrUnique: //<DB Column Attr> ::= unique
                	return RuleDbcolumnattrUnique(token);
                case (int)Symbols.RuleDbcolumnattrNotNull: //<DB Column Attr> ::= not null
                	return RuleDbcolumnattrNotNull(token);
                case (int)Symbols.RuleDbcolumnattrIndex: //<DB Column Attr> ::= index
                	return RuleDbcolumnattrIndex(token);
                case (int)Symbols.RuleDbcolumnattrDefault: //<DB Column Attr> ::= default <Value>
                	return RuleDbcolumnattrDefault(token);
                case (int)Symbols.RuleDbcolumnattrIdEq: //<DB Column Attr> ::= ID '=' <Value>
                	return RuleDbcolumnattrIdEq(token);
                case (int)Symbols.RuleDbtableattrlist: //<DB Table Attr List> ::= <DB Table Attr List> <DB Table Attr>
                	return RuleDbtableattrlist(token);
                case (int)Symbols.RuleDbtableattrlist2: //<DB Table Attr List> ::= 
                	return RuleDbtableattrlist2(token);
                case (int)Symbols.RuleDbtableattrIndexLparanRparanSemi: //<DB Table Attr> ::= index '(' <ID List> ')' ';'
                	return RuleDbtableattrIndexLparanRparanSemi(token);
                case (int)Symbols.RuleDbtableattrUniqueLparanRparanSemi: //<DB Table Attr> ::= unique '(' <ID List> ')' ';'
                	return RuleDbtableattrUniqueLparanRparanSemi(token);
                case (int)Symbols.RuleDbtableattrPosition: //<DB Table Attr> ::= <DB Trigger Runs> position <NumLiteral> <Stm>
                	return RuleDbtableattrPosition(token);
                case (int)Symbols.RuleDbtableattrIdEqSemi: //<DB Table Attr> ::= ID '=' <Value> ';'
                	return RuleDbtableattrIdEqSemi(token);
                case (int)Symbols.RuleDbtriggerrunsComma: //<DB Trigger Runs> ::= <DB Trigger Runs> ',' <DB Trigger Run>
                	return RuleDbtriggerrunsComma(token);
                case (int)Symbols.RuleDbtriggerruns: //<DB Trigger Runs> ::= <DB Trigger Run>
                	return RuleDbtriggerruns(token);
                case (int)Symbols.RuleDbtriggerrunBeforeSelect: //<DB Trigger Run> ::= before select
                	return RuleDbtriggerrunBeforeSelect(token);
                case (int)Symbols.RuleDbtriggerrunAfterSelect: //<DB Trigger Run> ::= after select
                	return RuleDbtriggerrunAfterSelect(token);
                case (int)Symbols.RuleDbtriggerrunBeforeInsert: //<DB Trigger Run> ::= before insert
                	return RuleDbtriggerrunBeforeInsert(token);
                case (int)Symbols.RuleDbtriggerrunAfterInsert: //<DB Trigger Run> ::= after insert
                	return RuleDbtriggerrunAfterInsert(token);
                case (int)Symbols.RuleDbtriggerrunBeforeUpdate: //<DB Trigger Run> ::= before update
                	return RuleDbtriggerrunBeforeUpdate(token);
                case (int)Symbols.RuleDbtriggerrunAfterUpdate: //<DB Trigger Run> ::= after update
                	return RuleDbtriggerrunAfterUpdate(token);
                case (int)Symbols.RuleDbtriggerrunBeforeDelete: //<DB Trigger Run> ::= before delete
                	return RuleDbtriggerrunBeforeDelete(token);
                case (int)Symbols.RuleDbtriggerrunAfterDelete: //<DB Trigger Run> ::= after delete
                	return RuleDbtriggerrunAfterDelete(token);
                case (int)Symbols.RuleDbtriggerrunBeforeModified: //<DB Trigger Run> ::= before modified
                	return RuleDbtriggerrunBeforeModified(token);
                case (int)Symbols.RuleDbtriggerrunAfterModified: //<DB Trigger Run> ::= after modified
                	return RuleDbtriggerrunAfterModified(token);
                case (int)Symbols.RuleNumliteralIntliteral: //<NumLiteral> ::= IntLiteral
                	return RuleNumliteralIntliteral(token);
                case (int)Symbols.RuleNumliteralDecimalliteral: //<NumLiteral> ::= DecimalLiteral
                	return RuleNumliteralDecimalliteral(token);
                case (int)Symbols.RuleFunctionFunctionIdLparanRparan: //<Function> ::= function ID '(' <Func args> ')' <Stm>
                	return RuleFunctionFunctionIdLparanRparan(token);
                case (int)Symbols.RuleFunctionFunctionLparanRparan: //<Function> ::= function '(' <Func args> ')' <Stm>
                	return RuleFunctionFunctionLparanRparan(token);
                case (int)Symbols.RuleWidgetWindowId: //<Widget> ::= window ID <WndParam List> <Layout Block>
                	return RuleWidgetWindowId(token);
                case (int)Symbols.RuleWidgetWindow: //<Widget> ::= window <WndParam List> <Layout Block>
                	return RuleWidgetWindow(token);
                case (int)Symbols.RuleWidgetWidgetId: //<Widget> ::= widget ID <Layout Block>
                	return RuleWidgetWidgetId(token);
                case (int)Symbols.RuleWidgetWidget: //<Widget> ::= widget <Layout Block>
                	return RuleWidgetWidget(token);
                case (int)Symbols.RuleWndparamlist: //<WndParam List> ::= <WndParam> <WndParam List>
                	return RuleWndparamlist(token);
                case (int)Symbols.RuleWndparamlist2: //<WndParam List> ::= 
                	return RuleWndparamlist2(token);
                case (int)Symbols.RuleWndparamIdEqSemi: //<WndParam> ::= ID '=' <Expr> ';'
                	return RuleWndparamIdEqSemi(token);
                case (int)Symbols.RuleWndparamIdSemi: //<WndParam> ::= ID ';'
                	return RuleWndparamIdSemi(token);
                case (int)Symbols.RuleLayoutlist: //<Layout List> ::= <Layout List> <Layout Block>
                	return RuleLayoutlist(token);
                case (int)Symbols.RuleLayoutlist2: //<Layout List> ::= 
                	return RuleLayoutlist2(token);
                case (int)Symbols.RuleLayoutblockHboxEnd: //<Layout Block> ::= hbox <WndParam List> <Layout List> end
                	return RuleLayoutblockHboxEnd(token);
                case (int)Symbols.RuleLayoutblockVboxEnd: //<Layout Block> ::= vbox <WndParam List> <Layout List> end
                	return RuleLayoutblockVboxEnd(token);
                case (int)Symbols.RuleLayoutblockHbuttonboxEnd: //<Layout Block> ::= hbuttonbox <WndParam List> <Layout List> end
                	return RuleLayoutblockHbuttonboxEnd(token);
                case (int)Symbols.RuleLayoutblockVbuttonboxEnd: //<Layout Block> ::= vbuttonbox <WndParam List> <Layout List> end
                	return RuleLayoutblockVbuttonboxEnd(token);
                case (int)Symbols.RuleLayoutblockTableEnd: //<Layout Block> ::= table <WndParam List> <Layout List> end
                	return RuleLayoutblockTableEnd(token);
                case (int)Symbols.RuleLayoutblockToolbarEnd: //<Layout Block> ::= toolbar <WndParam List> <Layout List> end
                	return RuleLayoutblockToolbarEnd(token);
                case (int)Symbols.RuleLayoutblockButton: //<Layout Block> ::= button <WndParam List> <Layout Block>
                	return RuleLayoutblockButton(token);
                case (int)Symbols.RuleLayoutblockButtonEnd: //<Layout Block> ::= button <WndParam List> end
                	return RuleLayoutblockButtonEnd(token);
                case (int)Symbols.RuleLayoutblockToolbutton: //<Layout Block> ::= toolbutton <WndParam List> <Layout Block>
                	return RuleLayoutblockToolbutton(token);
                case (int)Symbols.RuleLayoutblockToolbuttonEnd: //<Layout Block> ::= toolbutton <WndParam List> end
                	return RuleLayoutblockToolbuttonEnd(token);
                case (int)Symbols.RuleLayoutblockImage: //<Layout Block> ::= image <WndParam List>
                	return RuleLayoutblockImage(token);
                case (int)Symbols.RuleLayoutblockStringliteral: //<Layout Block> ::= StringLiteral <WndParam List>
                	return RuleLayoutblockStringliteral(token);
                case (int)Symbols.RuleLayoutblock: //<Layout Block> ::= <Menu Block>
                	return RuleLayoutblock(token);
                case (int)Symbols.RuleLayoutblockLbracketRbracket: //<Layout Block> ::= '[' <Expr> ']' <WndParam List>
                	return RuleLayoutblockLbracketRbracket(token);
                case (int)Symbols.RuleMenublockMenuEnd: //<Menu Block> ::= menu <WndParam List> <MenuItems List> end
                	return RuleMenublockMenuEnd(token);
                case (int)Symbols.RuleMenuitemslist: //<MenuItems List> ::= <Menu Item> <MenuItems List>
                	return RuleMenuitemslist(token);
                case (int)Symbols.RuleMenuitemslist2: //<MenuItems List> ::= 
                	return RuleMenuitemslist2(token);
                case (int)Symbols.RuleMenuitem: //<Menu Item> ::= <Menu Block>
                	return RuleMenuitem(token);
                case (int)Symbols.RuleMenuitemMenuitem: //<Menu Item> ::= menuitem <WndParam List>
                	return RuleMenuitemMenuitem(token);
                case (int)Symbols.RuleMenuitemSeparator: //<Menu Item> ::= Separator
                	return RuleMenuitemSeparator(token);
                case (int)Symbols.RuleExprlistComma: //<Expr List> ::= <Expr List> ',' <Expr>
                	return RuleExprlistComma(token);
                case (int)Symbols.RuleExprlist: //<Expr List> ::= <Expr>
                	return RuleExprlist(token);
                case (int)Symbols.RuleDictlistColonComma: //<Dict List> ::= <Expr> ':' <Expr> ',' <Dict List>
                	return RuleDictlistColonComma(token);
                case (int)Symbols.RuleDictlistColon: //<Dict List> ::= <Expr> ':' <Expr>
                	return RuleDictlistColon(token);
                case (int)Symbols.RuleDictlist: //<Dict List> ::= 
                	return RuleDictlist(token);
                case (int)Symbols.RuleExpr: //<Expr> ::= <Widget>
                	return RuleExpr(token);
                case (int)Symbols.RuleExpr2: //<Expr> ::= <Database>
                	return RuleExpr2(token);
                case (int)Symbols.RuleExpr3: //<Expr> ::= <Op Assign>
                	return RuleExpr3(token);
                case (int)Symbols.RuleOpassignEq: //<Op Assign> ::= <Op If> '=' <Expr>
                	return RuleOpassignEq(token);
                case (int)Symbols.RuleOpassignPluseq: //<Op Assign> ::= <Op If> '+=' <Expr>
                	return RuleOpassignPluseq(token);
                case (int)Symbols.RuleOpassignMinuseq: //<Op Assign> ::= <Op If> '-=' <Expr>
                	return RuleOpassignMinuseq(token);
                case (int)Symbols.RuleOpassignTimeseq: //<Op Assign> ::= <Op If> '*=' <Expr>
                	return RuleOpassignTimeseq(token);
                case (int)Symbols.RuleOpassignDiveq: //<Op Assign> ::= <Op If> '/=' <Expr>
                	return RuleOpassignDiveq(token);
                case (int)Symbols.RuleOpassignLteqeq: //<Op Assign> ::= <Op If> '<==' <Expr>
                	return RuleOpassignLteqeq(token);
                case (int)Symbols.RuleOpassignLteqeqgt: //<Op Assign> ::= <Op If> '<==>' <Expr>
                	return RuleOpassignLteqeqgt(token);
                case (int)Symbols.RuleOpassign: //<Op Assign> ::= <Op If>
                	return RuleOpassign(token);
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
                case (int)Symbols.RuleOpequateIn: //<Op Equate> ::= <Op Equate> in <Op Compare>
                	return RuleOpequateIn(token);
                case (int)Symbols.RuleOpequate: //<Op Equate> ::= <Op Compare>
                	return RuleOpequate(token);
                case (int)Symbols.RuleOpcompareLt: //<Op Compare> ::= <Op Compare> '<' <Op Add>
                	return RuleOpcompareLt(token);
                case (int)Symbols.RuleOpcompareGt: //<Op Compare> ::= <Op Compare> '>' <Op Add>
                	return RuleOpcompareGt(token);
                case (int)Symbols.RuleOpcompareLteq: //<Op Compare> ::= <Op Compare> '<=' <Op Add>
                	return RuleOpcompareLteq(token);
                case (int)Symbols.RuleOpcompareGteq: //<Op Compare> ::= <Op Compare> '>=' <Op Add>
                	return RuleOpcompareGteq(token);
                case (int)Symbols.RuleOpcompare: //<Op Compare> ::= <Op Add>
                	return RuleOpcompare(token);
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
                case (int)Symbols.RuleOpunaryCastLparanAsRparan: //<Op Unary> ::= cast '(' <Op Unary> as <QualifiedName> ')'
                	return RuleOpunaryCastLparanAsRparan(token);
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
                case (int)Symbols.RuleOppointerLparanRparan: //<Op Pointer> ::= <Op Pointer> '(' <Args> ')'
                	return RuleOppointerLparanRparan(token);
                case (int)Symbols.RuleOppointer: //<Op Pointer> ::= <Value>
                	return RuleOppointer(token);
                case (int)Symbols.RuleValueIntliteral: //<Value> ::= IntLiteral
                	return RuleValueIntliteral(token);
                case (int)Symbols.RuleValueStringliteral: //<Value> ::= StringLiteral
                	return RuleValueStringliteral(token);
                case (int)Symbols.RuleValueDecimalliteral: //<Value> ::= DecimalLiteral
                	return RuleValueDecimalliteral(token);
                case (int)Symbols.RuleValueDatetimeliteral: //<Value> ::= DateTimeLiteral
                	return RuleValueDatetimeliteral(token);
                case (int)Symbols.RuleValueTimespanliteral: //<Value> ::= TimeSpanLiteral
                	return RuleValueTimespanliteral(token);
                case (int)Symbols.RuleValueTypeLparanRparan: //<Value> ::= type '(' <QualifiedName> ')'
                	return RuleValueTypeLparanRparan(token);
                case (int)Symbols.RuleValueNewLparanRparan: //<Value> ::= new <QualifiedName> '(' <Args> ')'
                	return RuleValueNewLparanRparan(token);
                case (int)Symbols.RuleValueId: //<Value> ::= ID
                	return RuleValueId(token);
                case (int)Symbols.RuleValueVarId: //<Value> ::= var ID
                	return RuleValueVarId(token);
                case (int)Symbols.RuleValueStaticId: //<Value> ::= static ID
                	return RuleValueStaticId(token);
                case (int)Symbols.RuleValueLparanRparan: //<Value> ::= '(' <Expr> ')'
                	return RuleValueLparanRparan(token);
                case (int)Symbols.RuleValueLbracketRbracket: //<Value> ::= '[' <Expr List> ']'
                	return RuleValueLbracketRbracket(token);
                case (int)Symbols.RuleValueLbraceRbrace: //<Value> ::= '{' <Dict List> '}'
                	return RuleValueLbraceRbrace(token);
                case (int)Symbols.RuleValueRangeLtSemiGt: //<Value> ::= range '<' <Op Add> ';' <Op Add> '>'
                	return RuleValueRangeLtSemiGt(token);
                case (int)Symbols.RuleValueRangeLtSemiRparan: //<Value> ::= range '<' <Op Add> ';' <Op Add> ')'
                	return RuleValueRangeLtSemiRparan(token);
                case (int)Symbols.RuleValueRangeLparanSemiGt: //<Value> ::= range '(' <Op Add> ';' <Op Add> '>'
                	return RuleValueRangeLparanSemiGt(token);
                case (int)Symbols.RuleValueRangeLparanSemiRparan: //<Value> ::= range '(' <Op Add> ';' <Op Add> ')'
                	return RuleValueRangeLparanSemiRparan(token);
                case (int)Symbols.RuleValuePropertySemi: //<Value> ::= property <Expr> ';'
                	return RuleValuePropertySemi(token);
                case (int)Symbols.RuleValuePropertyGetSemi: //<Value> ::= property <Expr> get <Expr> ';'
                	return RuleValuePropertyGetSemi(token);
                case (int)Symbols.RuleValuePropertyGetSetSemi: //<Value> ::= property <Expr> get <Expr> set <Expr> ';'
                	return RuleValuePropertyGetSetSemi(token);
                case (int)Symbols.RuleValue: //<Value> ::= <Function>
                	return RuleValue(token);
                case (int)Symbols.RuleValueDict: //<Value> ::= dict
                	return RuleValueDict(token);
                case (int)Symbols.RuleValueList: //<Value> ::= list
                	return RuleValueList(token);
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
            Log.Error(message);
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            Log.Error(message);
        }

		#endregion

		#region Templates for user overrided rules functions
		/*
		// <QualifiedName> ::= <QualifiedName> '.' ID
		protected override object RuleQualifiednameDotId(NonterminalToken token)
		{
			throw new NotImplementedException("<QualifiedName> ::= <QualifiedName> '.' ID");
			//return new
		}

		// <QualifiedName> ::= ID
		protected override object RuleQualifiednameId(NonterminalToken token)
		{
			throw new NotImplementedException("<QualifiedName> ::= ID");
			//return new
		}

		// <Stm> ::= if '(' <Expr> ')' <Stm>
		protected override object RuleStmIfLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= if '(' <Expr> ')' <Stm>");
			//return new
		}

		// <Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>
		protected override object RuleStmIfLparanRparanElse(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>");
			//return new
		}

		// <Stm> ::= while '(' <Expr> ')' <Stm>
		protected override object RuleStmWhileLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= while '(' <Expr> ')' <Stm>");
			//return new
		}

		// <Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>
		protected override object RuleStmForLparanSemiSemiRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>");
			//return new
		}

		// <Stm> ::= foreach '(' ID in <Expr> ')' <Stm>
		protected override object RuleStmForeachLparanIdInRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= foreach '(' ID in <Expr> ')' <Stm>");
			//return new
		}

		// <Stm> ::= using <QualifiedName> ';'
		protected override object RuleStmUsingSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using <QualifiedName> ';'");
			//return new
		}

		// <Stm> ::= using StringLiteral ';'
		protected override object RuleStmUsingStringliteralSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using StringLiteral ';'");
			//return new
		}

		// <Stm> ::= using <QualifiedName> as ID ';'
		protected override object RuleStmUsingAsIdSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using <QualifiedName> as ID ';'");
			//return new
		}

		// <Stm> ::= using StringLiteral as ID ';'
		protected override object RuleStmUsingStringliteralAsIdSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using StringLiteral as ID ';'");
			//return new
		}

		// <Stm> ::= using '(' <Expr> ')' <Stm>
		protected override object RuleStmUsingLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using '(' <Expr> ')' <Stm>");
			//return new
		}

		// <Stm> ::= observed '(' <Expr List> ')' <Stm>
		protected override object RuleStmObservedLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= observed '(' <Expr List> ')' <Stm>");
			//return new
		}

		// <Stm> ::= <TryFinally>
		protected override object RuleStm(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= <TryFinally>");
			//return new
		}

		// <Stm> ::= <Normal Stm>
		protected override object RuleStm2(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= <Normal Stm>");
			//return new
		}

		// <Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>
		protected override object RuleThenstmIfLparanRparanElse(NonterminalToken token)
		{
			throw new NotImplementedException("<Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>");
			//return new
		}

		// <Then Stm> ::= while '(' <Expr> ')' <Then Stm>
		protected override object RuleThenstmWhileLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Then Stm> ::= while '(' <Expr> ')' <Then Stm>");
			//return new
		}

		// <Then Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Then Stm>
		protected override object RuleThenstmForLparanSemiSemiRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Then Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Then Stm>");
			//return new
		}

		// <Then Stm> ::= <Normal Stm>
		protected override object RuleThenstm(NonterminalToken token)
		{
			throw new NotImplementedException("<Then Stm> ::= <Normal Stm>");
			//return new
		}

		// <Normal Stm> ::= do <Stm> while '(' <Expr> ')'
		protected override object RuleNormalstmDoWhileLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= do <Stm> while '(' <Expr> ')'");
			//return new
		}

		// <Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'
		protected override object RuleNormalstmSwitchLparanRparanLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'");
			//return new
		}

		// <Normal Stm> ::= <Block>
		protected override object RuleNormalstm(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= <Block>");
			//return new
		}

		// <Normal Stm> ::= <Expr> ';'
		protected override object RuleNormalstmSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= <Expr> ';'");
			//return new
		}

		// <Normal Stm> ::= break ';'
		protected override object RuleNormalstmBreakSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= break ';'");
			//return new
		}

		// <Normal Stm> ::= continue ';'
		protected override object RuleNormalstmContinueSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= continue ';'");
			//return new
		}

		// <Normal Stm> ::= return <Expr> ';'
		protected override object RuleNormalstmReturnSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= return <Expr> ';'");
			//return new
		}

		// <Normal Stm> ::= throw <Expr> ';'
		protected override object RuleNormalstmThrowSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= throw <Expr> ';'");
			//return new
		}

		// <Normal Stm> ::= throw <Expr> ',' <Expr> ';'
		protected override object RuleNormalstmThrowCommaSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= throw <Expr> ',' <Expr> ';'");
			//return new
		}

		// <Normal Stm> ::= ';'
		protected override object RuleNormalstmSemi2(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= ';'");
			//return new
		}

		// <TryFinally> ::= try <Normal Stm> <Catchs> finally <Normal Stm>
		protected override object RuleTryfinallyTryFinally(NonterminalToken token)
		{
			throw new NotImplementedException("<TryFinally> ::= try <Normal Stm> <Catchs> finally <Normal Stm>");
			//return new
		}

		// <TryFinally> ::= try <Normal Stm> <Catchs>
		protected override object RuleTryfinallyTry(NonterminalToken token)
		{
			throw new NotImplementedException("<TryFinally> ::= try <Normal Stm> <Catchs>");
			//return new
		}

		// <Catchs> ::= <Catch> <Catchs>
		protected override object RuleCatchs(NonterminalToken token)
		{
			throw new NotImplementedException("<Catchs> ::= <Catch> <Catchs>");
			//return new
		}

		// <Catchs> ::= 
		protected override object RuleCatchs2(NonterminalToken token)
		{
			throw new NotImplementedException("<Catchs> ::= ");
			//return new
		}

		// <Catch> ::= catch '(' <QualifiedName> ID ')' <Normal Stm>
		protected override object RuleCatchCatchLparanIdRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Catch> ::= catch '(' <QualifiedName> ID ')' <Normal Stm>");
			//return new
		}

		// <Catch> ::= catch '(' type <QualifiedName> ')' <Normal Stm>
		protected override object RuleCatchCatchLparanTypeRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Catch> ::= catch '(' type <QualifiedName> ')' <Normal Stm>");
			//return new
		}

		// <Catch> ::= catch '(' var ID ')' <Normal Stm>
		protected override object RuleCatchCatchLparanVarIdRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Catch> ::= catch '(' var ID ')' <Normal Stm>");
			//return new
		}

		// <Catch> ::= catch <Block>
		protected override object RuleCatchCatch(NonterminalToken token)
		{
			throw new NotImplementedException("<Catch> ::= catch <Block>");
			//return new
		}

		// <Func args> ::= <Func args> ',' <Func Arg>
		protected override object RuleFuncargsComma(NonterminalToken token)
		{
			throw new NotImplementedException("<Func args> ::= <Func args> ',' <Func Arg>");
			//return new
		}

		// <Func args> ::= <Func Arg>
		protected override object RuleFuncargs(NonterminalToken token)
		{
			throw new NotImplementedException("<Func args> ::= <Func Arg>");
			//return new
		}

		// <Func args> ::= 
		protected override object RuleFuncargs2(NonterminalToken token)
		{
			throw new NotImplementedException("<Func args> ::= ");
			//return new
		}

		// <Func Arg> ::= ID
		protected override object RuleFuncargId(NonterminalToken token)
		{
			throw new NotImplementedException("<Func Arg> ::= ID");
			//return new
		}

		// <Func Arg> ::= ID '=' <Expr>
		protected override object RuleFuncargIdEq(NonterminalToken token)
		{
			throw new NotImplementedException("<Func Arg> ::= ID '=' <Expr>");
			//return new
		}

		// <Args> ::= <Args> ',' <Arg>
		protected override object RuleArgsComma(NonterminalToken token)
		{
			throw new NotImplementedException("<Args> ::= <Args> ',' <Arg>");
			//return new
		}

		// <Args> ::= <Arg>
		protected override object RuleArgs(NonterminalToken token)
		{
			throw new NotImplementedException("<Args> ::= <Arg>");
			//return new
		}

		// <Args> ::= 
		protected override object RuleArgs2(NonterminalToken token)
		{
			throw new NotImplementedException("<Args> ::= ");
			//return new
		}

		// <Arg> ::= <Op If>
		protected override object RuleArg(NonterminalToken token)
		{
			throw new NotImplementedException("<Arg> ::= <Op If>");
			//return new
		}

		// <Arg> ::= ID '=' <Expr>
		protected override object RuleArgIdEq(NonterminalToken token)
		{
			throw new NotImplementedException("<Arg> ::= ID '=' <Expr>");
			//return new
		}

		// <Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>
		protected override object RuleCasestmsCaseColon(NonterminalToken token)
		{
			throw new NotImplementedException("<Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>");
			//return new
		}

		// <Case Stms> ::= default ':' <Stm List>
		protected override object RuleCasestmsDefaultColon(NonterminalToken token)
		{
			throw new NotImplementedException("<Case Stms> ::= default ':' <Stm List>");
			//return new
		}

		// <Case Stms> ::= 
		protected override object RuleCasestms(NonterminalToken token)
		{
			throw new NotImplementedException("<Case Stms> ::= ");
			//return new
		}

		// <Block> ::= '{' <Stm List> '}'
		protected override object RuleBlockLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<Block> ::= '{' <Stm List> '}'");
			//return new
		}

		// <Stm List> ::= <Stm> <Stm List>
		protected override object RuleStmlist(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm List> ::= <Stm> <Stm List>");
			//return new
		}

		// <Stm List> ::= <Stm>
		protected override object RuleStmlist2(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm List> ::= <Stm>");
			//return new
		}

		// <ID List> ::= <ID List> ',' ID
		protected override object RuleIdlistCommaId(NonterminalToken token)
		{
			throw new NotImplementedException("<ID List> ::= <ID List> ',' ID");
			//return new
		}

		// <ID List> ::= ID
		protected override object RuleIdlistId(NonterminalToken token)
		{
			throw new NotImplementedException("<ID List> ::= ID");
			//return new
		}

		// <ID List> ::= 
		protected override object RuleIdlist(NonterminalToken token)
		{
			throw new NotImplementedException("<ID List> ::= ");
			//return new
		}

		// <Database> ::= database ID '{' <DB Tables> '}'
		protected override object RuleDatabaseDatabaseIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<Database> ::= database ID '{' <DB Tables> '}'");
			//return new
		}

		// <Database> ::= extends database ID '{' <DB Tables> '}'
		protected override object RuleDatabaseExtendsDatabaseIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<Database> ::= extends database ID '{' <DB Tables> '}'");
			//return new
		}

		// <DB Tables> ::= <DB Tables> <DB Table>
		protected override object RuleDbtables(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Tables> ::= <DB Tables> <DB Table>");
			//return new
		}

		// <DB Tables> ::= 
		protected override object RuleDbtables2(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Tables> ::= ");
			//return new
		}

		// <DB Table> ::= template table ID '{' <DB Columns> '}' <DB Table Attr List>
		protected override object RuleDbtableTemplateTableIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table> ::= template table ID '{' <DB Columns> '}' <DB Table Attr List>");
			//return new
		}

		// <DB Table> ::= template table ID template ID '{' <DB Columns> '}' <DB Table Attr List>
		protected override object RuleDbtableTemplateTableIdTemplateIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table> ::= template table ID template ID '{' <DB Columns> '}' <DB Table Attr List>");
			//return new
		}

		// <DB Table> ::= table ID '{' <DB Columns> '}' <DB Table Attr List>
		protected override object RuleDbtableTableIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table> ::= table ID '{' <DB Columns> '}' <DB Table Attr List>");
			//return new
		}

		// <DB Table> ::= table ID template ID '{' <DB Columns> '}' <DB Table Attr List>
		protected override object RuleDbtableTableIdTemplateIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table> ::= table ID template ID '{' <DB Columns> '}' <DB Table Attr List>");
			//return new
		}

		// <DB Table> ::= extends table ID '{' <DB Columns> '}' <DB Table Attr List>
		protected override object RuleDbtableExtendsTableIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table> ::= extends table ID '{' <DB Columns> '}' <DB Table Attr List>");
			//return new
		}

		// <DB Columns> ::= <DB Columns> <DB Column>
		protected override object RuleDbcolumns(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Columns> ::= <DB Columns> <DB Column>");
			//return new
		}

		// <DB Columns> ::= 
		protected override object RuleDbcolumns2(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Columns> ::= ");
			//return new
		}

		// <DB Column> ::= ID <DB Column Type> <DB Column Attr List> ';'
		protected override object RuleDbcolumnIdSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column> ::= ID <DB Column Type> <DB Column Attr List> ';'");
			//return new
		}

		// <DB Column Type> ::= primary
		protected override object RuleDbcolumntypePrimary(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= primary");
			//return new
		}

		// <DB Column Type> ::= foreign ID
		protected override object RuleDbcolumntypeForeignId(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= foreign ID");
			//return new
		}

		// <DB Column Type> ::= foreign ID '(' ID ')'
		protected override object RuleDbcolumntypeForeignIdLparanIdRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= foreign ID '(' ID ')'");
			//return new
		}

		// <DB Column Type> ::= many ID
		protected override object RuleDbcolumntypeManyId(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= many ID");
			//return new
		}

		// <DB Column Type> ::= many ID through ID
		protected override object RuleDbcolumntypeManyIdThroughId(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= many ID through ID");
			//return new
		}

		// <DB Column Type> ::= many ID through ID '(' ID ',' ID ')'
		protected override object RuleDbcolumntypeManyIdThroughIdLparanIdCommaIdRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= many ID through ID '(' ID ',' ID ')'");
			//return new
		}

		// <DB Column Type> ::= varchar
		protected override object RuleDbcolumntypeVarchar(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= varchar");
			//return new
		}

		// <DB Column Type> ::= varchar '(' IntLiteral ')'
		protected override object RuleDbcolumntypeVarcharLparanIntliteralRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= varchar '(' IntLiteral ')'");
			//return new
		}

		// <DB Column Type> ::= integer
		protected override object RuleDbcolumntypeInteger(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= integer");
			//return new
		}

		// <DB Column Type> ::= bool
		protected override object RuleDbcolumntypeBool(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= bool");
			//return new
		}

		// <DB Column Type> ::= decimal '(' IntLiteral ',' IntLiteral ')'
		protected override object RuleDbcolumntypeDecimalLparanIntliteralCommaIntliteralRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= decimal '(' IntLiteral ',' IntLiteral ')'");
			//return new
		}

		// <DB Column Type> ::= date
		protected override object RuleDbcolumntypeDate(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= date");
			//return new
		}

		// <DB Column Type> ::= time
		protected override object RuleDbcolumntypeTime(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= time");
			//return new
		}

		// <DB Column Type> ::= datetime
		protected override object RuleDbcolumntypeDatetime(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= datetime");
			//return new
		}

		// <DB Column Type> ::= daterange
		protected override object RuleDbcolumntypeDaterange(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= daterange");
			//return new
		}

		// <DB Column Type> ::= timerange
		protected override object RuleDbcolumntypeTimerange(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= timerange");
			//return new
		}

		// <DB Column Type> ::= datetimerange
		protected override object RuleDbcolumntypeDatetimerange(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Type> ::= datetimerange");
			//return new
		}

		// <DB Column Attr List> ::= <DB Column Attr List> <DB Column Attr>
		protected override object RuleDbcolumnattrlist(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Attr List> ::= <DB Column Attr List> <DB Column Attr>");
			//return new
		}

		// <DB Column Attr List> ::= 
		protected override object RuleDbcolumnattrlist2(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Attr List> ::= ");
			//return new
		}

		// <DB Column Attr> ::= unique
		protected override object RuleDbcolumnattrUnique(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Attr> ::= unique");
			//return new
		}

		// <DB Column Attr> ::= not null
		protected override object RuleDbcolumnattrNotNull(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Attr> ::= not null");
			//return new
		}

		// <DB Column Attr> ::= index
		protected override object RuleDbcolumnattrIndex(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Attr> ::= index");
			//return new
		}

		// <DB Column Attr> ::= default <Value>
		protected override object RuleDbcolumnattrDefault(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Attr> ::= default <Value>");
			//return new
		}

		// <DB Column Attr> ::= ID '=' <Value>
		protected override object RuleDbcolumnattrIdEq(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Attr> ::= ID '=' <Value>");
			//return new
		}

		// <DB Table Attr List> ::= <DB Table Attr List> <DB Table Attr>
		protected override object RuleDbtableattrlist(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr List> ::= <DB Table Attr List> <DB Table Attr>");
			//return new
		}

		// <DB Table Attr List> ::= 
		protected override object RuleDbtableattrlist2(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr List> ::= ");
			//return new
		}

		// <DB Table Attr> ::= index '(' <ID List> ')' ';'
		protected override object RuleDbtableattrIndexLparanRparanSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= index '(' <ID List> ')' ';'");
			//return new
		}

		// <DB Table Attr> ::= unique '(' <ID List> ')' ';'
		protected override object RuleDbtableattrUniqueLparanRparanSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= unique '(' <ID List> ')' ';'");
			//return new
		}

		// <DB Table Attr> ::= <DB Trigger Runs> position <NumLiteral> <Stm>
		protected override object RuleDbtableattrPosition(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= <DB Trigger Runs> position <NumLiteral> <Stm>");
			//return new
		}

		// <DB Table Attr> ::= ID '=' <Value> ';'
		protected override object RuleDbtableattrIdEqSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= ID '=' <Value> ';'");
			//return new
		}

		// <DB Trigger Runs> ::= <DB Trigger Runs> ',' <DB Trigger Run>
		protected override object RuleDbtriggerrunsComma(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Runs> ::= <DB Trigger Runs> ',' <DB Trigger Run>");
			//return new
		}

		// <DB Trigger Runs> ::= <DB Trigger Run>
		protected override object RuleDbtriggerruns(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Runs> ::= <DB Trigger Run>");
			//return new
		}

		// <DB Trigger Run> ::= before select
		protected override object RuleDbtriggerrunBeforeSelect(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Run> ::= before select");
			//return new
		}

		// <DB Trigger Run> ::= after select
		protected override object RuleDbtriggerrunAfterSelect(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Run> ::= after select");
			//return new
		}

		// <DB Trigger Run> ::= before insert
		protected override object RuleDbtriggerrunBeforeInsert(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Run> ::= before insert");
			//return new
		}

		// <DB Trigger Run> ::= after insert
		protected override object RuleDbtriggerrunAfterInsert(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Run> ::= after insert");
			//return new
		}

		// <DB Trigger Run> ::= before update
		protected override object RuleDbtriggerrunBeforeUpdate(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Run> ::= before update");
			//return new
		}

		// <DB Trigger Run> ::= after update
		protected override object RuleDbtriggerrunAfterUpdate(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Run> ::= after update");
			//return new
		}

		// <DB Trigger Run> ::= before delete
		protected override object RuleDbtriggerrunBeforeDelete(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Run> ::= before delete");
			//return new
		}

		// <DB Trigger Run> ::= after delete
		protected override object RuleDbtriggerrunAfterDelete(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Run> ::= after delete");
			//return new
		}

		// <DB Trigger Run> ::= before modified
		protected override object RuleDbtriggerrunBeforeModified(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Run> ::= before modified");
			//return new
		}

		// <DB Trigger Run> ::= after modified
		protected override object RuleDbtriggerrunAfterModified(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Trigger Run> ::= after modified");
			//return new
		}

		// <NumLiteral> ::= IntLiteral
		protected override object RuleNumliteralIntliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<NumLiteral> ::= IntLiteral");
			//return new
		}

		// <NumLiteral> ::= DecimalLiteral
		protected override object RuleNumliteralDecimalliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<NumLiteral> ::= DecimalLiteral");
			//return new
		}

		// <Function> ::= function ID '(' <Func args> ')' <Stm>
		protected override object RuleFunctionFunctionIdLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Function> ::= function ID '(' <Func args> ')' <Stm>");
			//return new
		}

		// <Function> ::= function '(' <Func args> ')' <Stm>
		protected override object RuleFunctionFunctionLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Function> ::= function '(' <Func args> ')' <Stm>");
			//return new
		}

		// <Widget> ::= window ID <WndParam List> <Layout Block>
		protected override object RuleWidgetWindowId(NonterminalToken token)
		{
			throw new NotImplementedException("<Widget> ::= window ID <WndParam List> <Layout Block>");
			//return new
		}

		// <Widget> ::= window <WndParam List> <Layout Block>
		protected override object RuleWidgetWindow(NonterminalToken token)
		{
			throw new NotImplementedException("<Widget> ::= window <WndParam List> <Layout Block>");
			//return new
		}

		// <Widget> ::= widget ID <Layout Block>
		protected override object RuleWidgetWidgetId(NonterminalToken token)
		{
			throw new NotImplementedException("<Widget> ::= widget ID <Layout Block>");
			//return new
		}

		// <Widget> ::= widget <Layout Block>
		protected override object RuleWidgetWidget(NonterminalToken token)
		{
			throw new NotImplementedException("<Widget> ::= widget <Layout Block>");
			//return new
		}

		// <WndParam List> ::= <WndParam> <WndParam List>
		protected override object RuleWndparamlist(NonterminalToken token)
		{
			throw new NotImplementedException("<WndParam List> ::= <WndParam> <WndParam List>");
			//return new
		}

		// <WndParam List> ::= 
		protected override object RuleWndparamlist2(NonterminalToken token)
		{
			throw new NotImplementedException("<WndParam List> ::= ");
			//return new
		}

		// <WndParam> ::= ID '=' <Expr> ';'
		protected override object RuleWndparamIdEqSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<WndParam> ::= ID '=' <Expr> ';'");
			//return new
		}

		// <WndParam> ::= ID ';'
		protected override object RuleWndparamIdSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<WndParam> ::= ID ';'");
			//return new
		}

		// <Layout List> ::= <Layout List> <Layout Block>
		protected override object RuleLayoutlist(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout List> ::= <Layout List> <Layout Block>");
			//return new
		}

		// <Layout List> ::= 
		protected override object RuleLayoutlist2(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout List> ::= ");
			//return new
		}

		// <Layout Block> ::= hbox <WndParam List> <Layout List> end
		protected override object RuleLayoutblockHboxEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= hbox <WndParam List> <Layout List> end");
			//return new
		}

		// <Layout Block> ::= vbox <WndParam List> <Layout List> end
		protected override object RuleLayoutblockVboxEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= vbox <WndParam List> <Layout List> end");
			//return new
		}

		// <Layout Block> ::= hbuttonbox <WndParam List> <Layout List> end
		protected override object RuleLayoutblockHbuttonboxEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= hbuttonbox <WndParam List> <Layout List> end");
			//return new
		}

		// <Layout Block> ::= vbuttonbox <WndParam List> <Layout List> end
		protected override object RuleLayoutblockVbuttonboxEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= vbuttonbox <WndParam List> <Layout List> end");
			//return new
		}

		// <Layout Block> ::= table <WndParam List> <Layout List> end
		protected override object RuleLayoutblockTableEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= table <WndParam List> <Layout List> end");
			//return new
		}

		// <Layout Block> ::= toolbar <WndParam List> <Layout List> end
		protected override object RuleLayoutblockToolbarEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= toolbar <WndParam List> <Layout List> end");
			//return new
		}

		// <Layout Block> ::= button <WndParam List> <Layout Block>
		protected override object RuleLayoutblockButton(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= button <WndParam List> <Layout Block>");
			//return new
		}

		// <Layout Block> ::= button <WndParam List> end
		protected override object RuleLayoutblockButtonEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= button <WndParam List> end");
			//return new
		}

		// <Layout Block> ::= toolbutton <WndParam List> <Layout Block>
		protected override object RuleLayoutblockToolbutton(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= toolbutton <WndParam List> <Layout Block>");
			//return new
		}

		// <Layout Block> ::= toolbutton <WndParam List> end
		protected override object RuleLayoutblockToolbuttonEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= toolbutton <WndParam List> end");
			//return new
		}

		// <Layout Block> ::= image <WndParam List>
		protected override object RuleLayoutblockImage(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= image <WndParam List>");
			//return new
		}

		// <Layout Block> ::= StringLiteral <WndParam List>
		protected override object RuleLayoutblockStringliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= StringLiteral <WndParam List>");
			//return new
		}

		// <Layout Block> ::= <Menu Block>
		protected override object RuleLayoutblock(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= <Menu Block>");
			//return new
		}

		// <Layout Block> ::= '[' <Expr> ']' <WndParam List>
		protected override object RuleLayoutblockLbracketRbracket(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= '[' <Expr> ']' <WndParam List>");
			//return new
		}

		// <Menu Block> ::= menu <WndParam List> <MenuItems List> end
		protected override object RuleMenublockMenuEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Menu Block> ::= menu <WndParam List> <MenuItems List> end");
			//return new
		}

		// <MenuItems List> ::= <Menu Item> <MenuItems List>
		protected override object RuleMenuitemslist(NonterminalToken token)
		{
			throw new NotImplementedException("<MenuItems List> ::= <Menu Item> <MenuItems List>");
			//return new
		}

		// <MenuItems List> ::= 
		protected override object RuleMenuitemslist2(NonterminalToken token)
		{
			throw new NotImplementedException("<MenuItems List> ::= ");
			//return new
		}

		// <Menu Item> ::= <Menu Block>
		protected override object RuleMenuitem(NonterminalToken token)
		{
			throw new NotImplementedException("<Menu Item> ::= <Menu Block>");
			//return new
		}

		// <Menu Item> ::= menuitem <WndParam List>
		protected override object RuleMenuitemMenuitem(NonterminalToken token)
		{
			throw new NotImplementedException("<Menu Item> ::= menuitem <WndParam List>");
			//return new
		}

		// <Menu Item> ::= Separator
		protected override object RuleMenuitemSeparator(NonterminalToken token)
		{
			throw new NotImplementedException("<Menu Item> ::= Separator");
			//return new
		}

		// <Expr List> ::= <Expr List> ',' <Expr>
		protected override object RuleExprlistComma(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr List> ::= <Expr List> ',' <Expr>");
			//return new
		}

		// <Expr List> ::= <Expr>
		protected override object RuleExprlist(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr List> ::= <Expr>");
			//return new
		}

		// <Dict List> ::= <Expr> ':' <Expr> ',' <Dict List>
		protected override object RuleDictlistColonComma(NonterminalToken token)
		{
			throw new NotImplementedException("<Dict List> ::= <Expr> ':' <Expr> ',' <Dict List>");
			//return new
		}

		// <Dict List> ::= <Expr> ':' <Expr>
		protected override object RuleDictlistColon(NonterminalToken token)
		{
			throw new NotImplementedException("<Dict List> ::= <Expr> ':' <Expr>");
			//return new
		}

		// <Dict List> ::= 
		protected override object RuleDictlist(NonterminalToken token)
		{
			throw new NotImplementedException("<Dict List> ::= ");
			//return new
		}

		// <Expr> ::= <Widget>
		protected override object RuleExpr(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Widget>");
			//return new
		}

		// <Expr> ::= <Database>
		protected override object RuleExpr2(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Database>");
			//return new
		}

		// <Expr> ::= <Op Assign>
		protected override object RuleExpr3(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op Assign>");
			//return new
		}

		// <Op Assign> ::= <Op If> '=' <Expr>
		protected override object RuleOpassignEq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Assign> ::= <Op If> '=' <Expr>");
			//return new
		}

		// <Op Assign> ::= <Op If> '+=' <Expr>
		protected override object RuleOpassignPluseq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Assign> ::= <Op If> '+=' <Expr>");
			//return new
		}

		// <Op Assign> ::= <Op If> '-=' <Expr>
		protected override object RuleOpassignMinuseq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Assign> ::= <Op If> '-=' <Expr>");
			//return new
		}

		// <Op Assign> ::= <Op If> '*=' <Expr>
		protected override object RuleOpassignTimeseq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Assign> ::= <Op If> '*=' <Expr>");
			//return new
		}

		// <Op Assign> ::= <Op If> '/=' <Expr>
		protected override object RuleOpassignDiveq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Assign> ::= <Op If> '/=' <Expr>");
			//return new
		}

		// <Op Assign> ::= <Op If> '<==' <Expr>
		protected override object RuleOpassignLteqeq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Assign> ::= <Op If> '<==' <Expr>");
			//return new
		}

		// <Op Assign> ::= <Op If> '<==>' <Expr>
		protected override object RuleOpassignLteqeqgt(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Assign> ::= <Op If> '<==>' <Expr>");
			//return new
		}

		// <Op Assign> ::= <Op If>
		protected override object RuleOpassign(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Assign> ::= <Op If>");
			//return new
		}

		// <Op If> ::= <Op Or> '?' <Op If> ':' <Op If>
		protected override object RuleOpifQuestionColon(NonterminalToken token)
		{
			throw new NotImplementedException("<Op If> ::= <Op Or> '?' <Op If> ':' <Op If>");
			//return new
		}

		// <Op If> ::= <Op Or>
		protected override object RuleOpif(NonterminalToken token)
		{
			throw new NotImplementedException("<Op If> ::= <Op Or>");
			//return new
		}

		// <Op Or> ::= <Op Or> or <Op And>
		protected override object RuleOporOr(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Or> ::= <Op Or> or <Op And>");
			//return new
		}

		// <Op Or> ::= <Op And>
		protected override object RuleOpor(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Or> ::= <Op And>");
			//return new
		}

		// <Op And> ::= <Op And> and <Op Equate>
		protected override object RuleOpandAnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Op And> ::= <Op And> and <Op Equate>");
			//return new
		}

		// <Op And> ::= <Op Equate>
		protected override object RuleOpand(NonterminalToken token)
		{
			throw new NotImplementedException("<Op And> ::= <Op Equate>");
			//return new
		}

		// <Op Equate> ::= <Op Equate> '==' <Op Compare>
		protected override object RuleOpequateEqeq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Equate> ::= <Op Equate> '==' <Op Compare>");
			//return new
		}

		// <Op Equate> ::= <Op Equate> '!=' <Op Compare>
		protected override object RuleOpequateExclameq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Equate> ::= <Op Equate> '!=' <Op Compare>");
			//return new
		}

		// <Op Equate> ::= <Op Equate> in <Op Compare>
		protected override object RuleOpequateIn(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Equate> ::= <Op Equate> in <Op Compare>");
			//return new
		}

		// <Op Equate> ::= <Op Compare>
		protected override object RuleOpequate(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Equate> ::= <Op Compare>");
			//return new
		}

		// <Op Compare> ::= <Op Compare> '<' <Op Add>
		protected override object RuleOpcompareLt(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Compare> ::= <Op Compare> '<' <Op Add>");
			//return new
		}

		// <Op Compare> ::= <Op Compare> '>' <Op Add>
		protected override object RuleOpcompareGt(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Compare> ::= <Op Compare> '>' <Op Add>");
			//return new
		}

		// <Op Compare> ::= <Op Compare> '<=' <Op Add>
		protected override object RuleOpcompareLteq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Compare> ::= <Op Compare> '<=' <Op Add>");
			//return new
		}

		// <Op Compare> ::= <Op Compare> '>=' <Op Add>
		protected override object RuleOpcompareGteq(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Compare> ::= <Op Compare> '>=' <Op Add>");
			//return new
		}

		// <Op Compare> ::= <Op Add>
		protected override object RuleOpcompare(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Compare> ::= <Op Add>");
			//return new
		}

		// <Op Add> ::= <Op Add> '+' <Op Mult>
		protected override object RuleOpaddPlus(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Add> ::= <Op Add> '+' <Op Mult>");
			//return new
		}

		// <Op Add> ::= <Op Add> '-' <Op Mult>
		protected override object RuleOpaddMinus(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Add> ::= <Op Add> '-' <Op Mult>");
			//return new
		}

		// <Op Add> ::= <Op Mult>
		protected override object RuleOpadd(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Add> ::= <Op Mult>");
			//return new
		}

		// <Op Mult> ::= <Op Mult> '*' <Op Unary>
		protected override object RuleOpmultTimes(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Mult> ::= <Op Mult> '*' <Op Unary>");
			//return new
		}

		// <Op Mult> ::= <Op Mult> '/' <Op Unary>
		protected override object RuleOpmultDiv(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Mult> ::= <Op Mult> '/' <Op Unary>");
			//return new
		}

		// <Op Mult> ::= <Op Mult> '%' <Op Unary>
		protected override object RuleOpmultPercent(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Mult> ::= <Op Mult> '%' <Op Unary>");
			//return new
		}

		// <Op Mult> ::= <Op Unary>
		protected override object RuleOpmult(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Mult> ::= <Op Unary>");
			//return new
		}

		// <Op Unary> ::= not <Op Unary>
		protected override object RuleOpunaryNot(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= not <Op Unary>");
			//return new
		}

		// <Op Unary> ::= '!' <Op Unary>
		protected override object RuleOpunaryExclam(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= '!' <Op Unary>");
			//return new
		}

		// <Op Unary> ::= '-' <Op Unary>
		protected override object RuleOpunaryMinus(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= '-' <Op Unary>");
			//return new
		}

		// <Op Unary> ::= cast '(' <Op Unary> as <QualifiedName> ')'
		protected override object RuleOpunaryCastLparanAsRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= cast '(' <Op Unary> as <QualifiedName> ')'");
			//return new
		}

		// <Op Unary> ::= '++' <Op Unary>
		protected override object RuleOpunaryPlusplus(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= '++' <Op Unary>");
			//return new
		}

		// <Op Unary> ::= -- <Op Unary>
		protected override object RuleOpunaryMinusminus(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= -- <Op Unary>");
			//return new
		}

		// <Op Unary> ::= <Op Pointer> '++'
		protected override object RuleOpunaryPlusplus2(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer> '++'");
			//return new
		}

		// <Op Unary> ::= <Op Pointer> --
		protected override object RuleOpunaryMinusminus2(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer> --");
			//return new
		}

		// <Op Unary> ::= <Op Pointer> is null
		protected override object RuleOpunaryIsNull(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer> is null");
			//return new
		}

		// <Op Unary> ::= <Op Pointer> not null
		protected override object RuleOpunaryNotNull(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer> not null");
			//return new
		}

		// <Op Unary> ::= <Op Pointer> is not null
		protected override object RuleOpunaryIsNotNull(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer> is not null");
			//return new
		}

		// <Op Unary> ::= <Op Pointer>
		protected override object RuleOpunary(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= <Op Pointer>");
			//return new
		}

		// <Op Pointer> ::= <Op Pointer> '.' <Value>
		protected override object RuleOppointerDot(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Pointer> ::= <Op Pointer> '.' <Value>");
			//return new
		}

		// <Op Pointer> ::= <Op Pointer> '->' <Value>
		protected override object RuleOppointerMinusgt(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Pointer> ::= <Op Pointer> '->' <Value>");
			//return new
		}

		// <Op Pointer> ::= <Op Pointer> '[' <Expr> ']'
		protected override object RuleOppointerLbracketRbracket(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Pointer> ::= <Op Pointer> '[' <Expr> ']'");
			//return new
		}

		// <Op Pointer> ::= <Op Pointer> '(' <Args> ')'
		protected override object RuleOppointerLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Pointer> ::= <Op Pointer> '(' <Args> ')'");
			//return new
		}

		// <Op Pointer> ::= <Value>
		protected override object RuleOppointer(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Pointer> ::= <Value>");
			//return new
		}

		// <Value> ::= IntLiteral
		protected override object RuleValueIntliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= IntLiteral");
			//return new
		}

		// <Value> ::= StringLiteral
		protected override object RuleValueStringliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= StringLiteral");
			//return new
		}

		// <Value> ::= DecimalLiteral
		protected override object RuleValueDecimalliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= DecimalLiteral");
			//return new
		}

		// <Value> ::= DateTimeLiteral
		protected override object RuleValueDatetimeliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= DateTimeLiteral");
			//return new
		}

		// <Value> ::= TimeSpanLiteral
		protected override object RuleValueTimespanliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= TimeSpanLiteral");
			//return new
		}

		// <Value> ::= type '(' <QualifiedName> ')'
		protected override object RuleValueTypeLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= type '(' <QualifiedName> ')'");
			//return new
		}

		// <Value> ::= new <QualifiedName> '(' <Args> ')'
		protected override object RuleValueNewLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= new <QualifiedName> '(' <Args> ')'");
			//return new
		}

		// <Value> ::= ID
		protected override object RuleValueId(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= ID");
			//return new
		}

		// <Value> ::= var ID
		protected override object RuleValueVarId(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= var ID");
			//return new
		}

		// <Value> ::= static ID
		protected override object RuleValueStaticId(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= static ID");
			//return new
		}

		// <Value> ::= '(' <Expr> ')'
		protected override object RuleValueLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= '(' <Expr> ')'");
			//return new
		}

		// <Value> ::= '[' <Expr List> ']'
		protected override object RuleValueLbracketRbracket(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= '[' <Expr List> ']'");
			//return new
		}

		// <Value> ::= '{' <Dict List> '}'
		protected override object RuleValueLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= '{' <Dict List> '}'");
			//return new
		}

		// <Value> ::= range '<' <Op Add> ';' <Op Add> '>'
		protected override object RuleValueRangeLtSemiGt(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= range '<' <Op Add> ';' <Op Add> '>'");
			//return new
		}

		// <Value> ::= range '<' <Op Add> ';' <Op Add> ')'
		protected override object RuleValueRangeLtSemiRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= range '<' <Op Add> ';' <Op Add> ')'");
			//return new
		}

		// <Value> ::= range '(' <Op Add> ';' <Op Add> '>'
		protected override object RuleValueRangeLparanSemiGt(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= range '(' <Op Add> ';' <Op Add> '>'");
			//return new
		}

		// <Value> ::= range '(' <Op Add> ';' <Op Add> ')'
		protected override object RuleValueRangeLparanSemiRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= range '(' <Op Add> ';' <Op Add> ')'");
			//return new
		}

		// <Value> ::= property <Expr> ';'
		protected override object RuleValuePropertySemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= property <Expr> ';'");
			//return new
		}

		// <Value> ::= property <Expr> get <Expr> ';'
		protected override object RuleValuePropertyGetSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= property <Expr> get <Expr> ';'");
			//return new
		}

		// <Value> ::= property <Expr> get <Expr> set <Expr> ';'
		protected override object RuleValuePropertyGetSetSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= property <Expr> get <Expr> set <Expr> ';'");
			//return new
		}

		// <Value> ::= <Function>
		protected override object RuleValue(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= <Function>");
			//return new
		}

		// <Value> ::= dict
		protected override object RuleValueDict(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= dict");
			//return new
		}

		// <Value> ::= list
		protected override object RuleValueList(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= list");
			//return new
		}

		// <Value> ::= null
		protected override object RuleValueNull(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= null");
			//return new
		}

		// <Value> ::= true
		protected override object RuleValueTrue(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= true");
			//return new
		}

		// <Value> ::= false
		protected override object RuleValueFalse(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= false");
			//return new
		}

		*/
		#endregion

		#region Test templates for user overrided rules functions
		/*
			Test(@"<QualifiedName> ::= <QualifiedName> '.' ID");
			Test(@"<QualifiedName> ::= ID");
			Test(@"<Stm> ::= if '(' <Expr> ')' <Stm>");
			Test(@"<Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>");
			Test(@"<Stm> ::= while '(' <Expr> ')' <Stm>");
			Test(@"<Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>");
			Test(@"<Stm> ::= foreach '(' ID in <Expr> ')' <Stm>");
			Test(@"<Stm> ::= using <QualifiedName> ';'");
			Test(@"<Stm> ::= using StringLiteral ';'");
			Test(@"<Stm> ::= using <QualifiedName> as ID ';'");
			Test(@"<Stm> ::= using StringLiteral as ID ';'");
			Test(@"<Stm> ::= using '(' <Expr> ')' <Stm>");
			Test(@"<Stm> ::= observed '(' <Expr List> ')' <Stm>");
			Test(@"<Stm> ::= <TryFinally>");
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
			Test(@"<Normal Stm> ::= throw <Expr> ';'");
			Test(@"<Normal Stm> ::= throw <Expr> ',' <Expr> ';'");
			Test(@"<Normal Stm> ::= ';'");
			Test(@"<TryFinally> ::= try <Normal Stm> <Catchs> finally <Normal Stm>");
			Test(@"<TryFinally> ::= try <Normal Stm> <Catchs>");
			Test(@"<Catchs> ::= <Catch> <Catchs>");
			Test(@"<Catchs> ::= ");
			Test(@"<Catch> ::= catch '(' <QualifiedName> ID ')' <Normal Stm>");
			Test(@"<Catch> ::= catch '(' type <QualifiedName> ')' <Normal Stm>");
			Test(@"<Catch> ::= catch '(' var ID ')' <Normal Stm>");
			Test(@"<Catch> ::= catch <Block>");
			Test(@"<Func args> ::= <Func args> ',' <Func Arg>");
			Test(@"<Func args> ::= <Func Arg>");
			Test(@"<Func args> ::= ");
			Test(@"<Func Arg> ::= ID");
			Test(@"<Func Arg> ::= ID '=' <Expr>");
			Test(@"<Args> ::= <Args> ',' <Arg>");
			Test(@"<Args> ::= <Arg>");
			Test(@"<Args> ::= ");
			Test(@"<Arg> ::= <Op If>");
			Test(@"<Arg> ::= ID '=' <Expr>");
			Test(@"<Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>");
			Test(@"<Case Stms> ::= default ':' <Stm List>");
			Test(@"<Case Stms> ::= ");
			Test(@"<Block> ::= '{' <Stm List> '}'");
			Test(@"<Stm List> ::= <Stm> <Stm List>");
			Test(@"<Stm List> ::= <Stm>");
			Test(@"<ID List> ::= <ID List> ',' ID");
			Test(@"<ID List> ::= ID");
			Test(@"<ID List> ::= ");
			Test(@"<Database> ::= database ID '{' <DB Tables> '}'");
			Test(@"<Database> ::= extends database ID '{' <DB Tables> '}'");
			Test(@"<DB Tables> ::= <DB Tables> <DB Table>");
			Test(@"<DB Tables> ::= ");
			Test(@"<DB Table> ::= template table ID '{' <DB Columns> '}' <DB Table Attr List>");
			Test(@"<DB Table> ::= template table ID template ID '{' <DB Columns> '}' <DB Table Attr List>");
			Test(@"<DB Table> ::= table ID '{' <DB Columns> '}' <DB Table Attr List>");
			Test(@"<DB Table> ::= table ID template ID '{' <DB Columns> '}' <DB Table Attr List>");
			Test(@"<DB Table> ::= extends table ID '{' <DB Columns> '}' <DB Table Attr List>");
			Test(@"<DB Columns> ::= <DB Columns> <DB Column>");
			Test(@"<DB Columns> ::= ");
			Test(@"<DB Column> ::= ID <DB Column Type> <DB Column Attr List> ';'");
			Test(@"<DB Column Type> ::= primary");
			Test(@"<DB Column Type> ::= foreign ID");
			Test(@"<DB Column Type> ::= foreign ID '(' ID ')'");
			Test(@"<DB Column Type> ::= many ID");
			Test(@"<DB Column Type> ::= many ID through ID");
			Test(@"<DB Column Type> ::= many ID through ID '(' ID ',' ID ')'");
			Test(@"<DB Column Type> ::= varchar");
			Test(@"<DB Column Type> ::= varchar '(' IntLiteral ')'");
			Test(@"<DB Column Type> ::= integer");
			Test(@"<DB Column Type> ::= bool");
			Test(@"<DB Column Type> ::= decimal '(' IntLiteral ',' IntLiteral ')'");
			Test(@"<DB Column Type> ::= date");
			Test(@"<DB Column Type> ::= time");
			Test(@"<DB Column Type> ::= datetime");
			Test(@"<DB Column Type> ::= daterange");
			Test(@"<DB Column Type> ::= timerange");
			Test(@"<DB Column Type> ::= datetimerange");
			Test(@"<DB Column Attr List> ::= <DB Column Attr List> <DB Column Attr>");
			Test(@"<DB Column Attr List> ::= ");
			Test(@"<DB Column Attr> ::= unique");
			Test(@"<DB Column Attr> ::= not null");
			Test(@"<DB Column Attr> ::= index");
			Test(@"<DB Column Attr> ::= default <Value>");
			Test(@"<DB Column Attr> ::= ID '=' <Value>");
			Test(@"<DB Table Attr List> ::= <DB Table Attr List> <DB Table Attr>");
			Test(@"<DB Table Attr List> ::= ");
			Test(@"<DB Table Attr> ::= index '(' <ID List> ')' ';'");
			Test(@"<DB Table Attr> ::= unique '(' <ID List> ')' ';'");
			Test(@"<DB Table Attr> ::= <DB Trigger Runs> position <NumLiteral> <Stm>");
			Test(@"<DB Table Attr> ::= ID '=' <Value> ';'");
			Test(@"<DB Trigger Runs> ::= <DB Trigger Runs> ',' <DB Trigger Run>");
			Test(@"<DB Trigger Runs> ::= <DB Trigger Run>");
			Test(@"<DB Trigger Run> ::= before select");
			Test(@"<DB Trigger Run> ::= after select");
			Test(@"<DB Trigger Run> ::= before insert");
			Test(@"<DB Trigger Run> ::= after insert");
			Test(@"<DB Trigger Run> ::= before update");
			Test(@"<DB Trigger Run> ::= after update");
			Test(@"<DB Trigger Run> ::= before delete");
			Test(@"<DB Trigger Run> ::= after delete");
			Test(@"<DB Trigger Run> ::= before modified");
			Test(@"<DB Trigger Run> ::= after modified");
			Test(@"<NumLiteral> ::= IntLiteral");
			Test(@"<NumLiteral> ::= DecimalLiteral");
			Test(@"<Function> ::= function ID '(' <Func args> ')' <Stm>");
			Test(@"<Function> ::= function '(' <Func args> ')' <Stm>");
			Test(@"<Widget> ::= window ID <WndParam List> <Layout Block>");
			Test(@"<Widget> ::= window <WndParam List> <Layout Block>");
			Test(@"<Widget> ::= widget ID <Layout Block>");
			Test(@"<Widget> ::= widget <Layout Block>");
			Test(@"<WndParam List> ::= <WndParam> <WndParam List>");
			Test(@"<WndParam List> ::= ");
			Test(@"<WndParam> ::= ID '=' <Expr> ';'");
			Test(@"<WndParam> ::= ID ';'");
			Test(@"<Layout List> ::= <Layout List> <Layout Block>");
			Test(@"<Layout List> ::= ");
			Test(@"<Layout Block> ::= hbox <WndParam List> <Layout List> end");
			Test(@"<Layout Block> ::= vbox <WndParam List> <Layout List> end");
			Test(@"<Layout Block> ::= hbuttonbox <WndParam List> <Layout List> end");
			Test(@"<Layout Block> ::= vbuttonbox <WndParam List> <Layout List> end");
			Test(@"<Layout Block> ::= table <WndParam List> <Layout List> end");
			Test(@"<Layout Block> ::= toolbar <WndParam List> <Layout List> end");
			Test(@"<Layout Block> ::= button <WndParam List> <Layout Block>");
			Test(@"<Layout Block> ::= button <WndParam List> end");
			Test(@"<Layout Block> ::= toolbutton <WndParam List> <Layout Block>");
			Test(@"<Layout Block> ::= toolbutton <WndParam List> end");
			Test(@"<Layout Block> ::= image <WndParam List>");
			Test(@"<Layout Block> ::= StringLiteral <WndParam List>");
			Test(@"<Layout Block> ::= <Menu Block>");
			Test(@"<Layout Block> ::= '[' <Expr> ']' <WndParam List>");
			Test(@"<Menu Block> ::= menu <WndParam List> <MenuItems List> end");
			Test(@"<MenuItems List> ::= <Menu Item> <MenuItems List>");
			Test(@"<MenuItems List> ::= ");
			Test(@"<Menu Item> ::= <Menu Block>");
			Test(@"<Menu Item> ::= menuitem <WndParam List>");
			Test(@"<Menu Item> ::= Separator");
			Test(@"<Expr List> ::= <Expr List> ',' <Expr>");
			Test(@"<Expr List> ::= <Expr>");
			Test(@"<Dict List> ::= <Expr> ':' <Expr> ',' <Dict List>");
			Test(@"<Dict List> ::= <Expr> ':' <Expr>");
			Test(@"<Dict List> ::= ");
			Test(@"<Expr> ::= <Widget>");
			Test(@"<Expr> ::= <Database>");
			Test(@"<Expr> ::= <Op Assign>");
			Test(@"<Op Assign> ::= <Op If> '=' <Expr>");
			Test(@"<Op Assign> ::= <Op If> '+=' <Expr>");
			Test(@"<Op Assign> ::= <Op If> '-=' <Expr>");
			Test(@"<Op Assign> ::= <Op If> '*=' <Expr>");
			Test(@"<Op Assign> ::= <Op If> '/=' <Expr>");
			Test(@"<Op Assign> ::= <Op If> '<==' <Expr>");
			Test(@"<Op Assign> ::= <Op If> '<==>' <Expr>");
			Test(@"<Op Assign> ::= <Op If>");
			Test(@"<Op If> ::= <Op Or> '?' <Op If> ':' <Op If>");
			Test(@"<Op If> ::= <Op Or>");
			Test(@"<Op Or> ::= <Op Or> or <Op And>");
			Test(@"<Op Or> ::= <Op And>");
			Test(@"<Op And> ::= <Op And> and <Op Equate>");
			Test(@"<Op And> ::= <Op Equate>");
			Test(@"<Op Equate> ::= <Op Equate> '==' <Op Compare>");
			Test(@"<Op Equate> ::= <Op Equate> '!=' <Op Compare>");
			Test(@"<Op Equate> ::= <Op Equate> in <Op Compare>");
			Test(@"<Op Equate> ::= <Op Compare>");
			Test(@"<Op Compare> ::= <Op Compare> '<' <Op Add>");
			Test(@"<Op Compare> ::= <Op Compare> '>' <Op Add>");
			Test(@"<Op Compare> ::= <Op Compare> '<=' <Op Add>");
			Test(@"<Op Compare> ::= <Op Compare> '>=' <Op Add>");
			Test(@"<Op Compare> ::= <Op Add>");
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
			Test(@"<Op Unary> ::= cast '(' <Op Unary> as <QualifiedName> ')'");
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
			Test(@"<Op Pointer> ::= <Op Pointer> '(' <Args> ')'");
			Test(@"<Op Pointer> ::= <Value>");
			Test(@"<Value> ::= IntLiteral");
			Test(@"<Value> ::= StringLiteral");
			Test(@"<Value> ::= DecimalLiteral");
			Test(@"<Value> ::= DateTimeLiteral");
			Test(@"<Value> ::= TimeSpanLiteral");
			Test(@"<Value> ::= type '(' <QualifiedName> ')'");
			Test(@"<Value> ::= new <QualifiedName> '(' <Args> ')'");
			Test(@"<Value> ::= ID");
			Test(@"<Value> ::= var ID");
			Test(@"<Value> ::= static ID");
			Test(@"<Value> ::= '(' <Expr> ')'");
			Test(@"<Value> ::= '[' <Expr List> ']'");
			Test(@"<Value> ::= '{' <Dict List> '}'");
			Test(@"<Value> ::= range '<' <Op Add> ';' <Op Add> '>'");
			Test(@"<Value> ::= range '<' <Op Add> ';' <Op Add> ')'");
			Test(@"<Value> ::= range '(' <Op Add> ';' <Op Add> '>'");
			Test(@"<Value> ::= range '(' <Op Add> ';' <Op Add> ')'");
			Test(@"<Value> ::= property <Expr> ';'");
			Test(@"<Value> ::= property <Expr> get <Expr> ';'");
			Test(@"<Value> ::= property <Expr> get <Expr> set <Expr> ';'");
			Test(@"<Value> ::= <Function>");
			Test(@"<Value> ::= dict");
			Test(@"<Value> ::= list");
			Test(@"<Value> ::= null");
			Test(@"<Value> ::= true");
			Test(@"<Value> ::= false");
		*/
		#endregion

    }
}
