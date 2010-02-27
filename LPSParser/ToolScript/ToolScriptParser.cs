using com.calitha.goldparser;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using LPS.ToolScript.Parser;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Collections;

namespace LPS.ToolScript
{
    public class ToolScriptParser : ToolScriptParserBase
    {

		public object ParseAndRun(string source)
		{
			StatementList list = this.Parse(source);
			return list.Run(this);
		}

		private T Get<T>(NonterminalToken token, int index)
		{
			object obj = CreateObject(token.Tokens[index]);
			try
			{
				return (T)obj;
			}
			catch(InvalidCastException err)
			{
				StackFrame stack = new StackFrame(1);
				Console.WriteLine("Cannot cast from {0} to {1} at {2}", obj.GetType(), typeof(T).Name, stack.GetMethod().Name);
				throw err;
			}
		}

		private IStatement Statement(NonterminalToken token, int index)
		{
			return Get<IStatement>(token, index);
		}

		private IExpression Expr(NonterminalToken token, int index)
		{
			return Get<IExpression>(token, index);
		}

		private string TText(NonterminalToken token, int index)
		{
			return ((TerminalToken)token.Tokens[index]).Text;
		}

		#region Rules

		// <QualifiedName> ::= ID '.' <QualifiedName>
		protected override object RuleQualifiednameIdDot(NonterminalToken token)
		{
			QualifiedName names = Get<QualifiedName>(token, 2);
			names.Names.Insert(0, TText(token,0));
			return names;
		}

		// <QualifiedName> ::= ID
		protected override object RuleQualifiednameId(NonterminalToken token)
		{
			return new QualifiedName(TText(token, 0));
		}

		// <Stm> ::= if '(' <Expr> ')' <Stm>
		protected override object RuleStmIfLparanRparan(NonterminalToken token)
		{
			return new IfStatement(Expr(token, 2), Statement(token, 4), null);
		}

		// <Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>
		protected override object RuleStmIfLparanRparanElse(NonterminalToken token)
		{
			return new IfStatement(Expr(token, 2), Statement(token, 4), Statement(token, 6));
		}

		// <Stm> ::= while '(' <Expr> ')' <Stm>
		protected override object RuleStmWhileLparanRparan(NonterminalToken token)
		{
			return new WhileStatement(Expr(token, 2), Statement(token, 4));
		}

		// <Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>
		protected override object RuleStmForLparanSemiSemiRparan(NonterminalToken token)
		{
			return new ForStatement(Expr(token, 2), Expr(token, 4), Expr(token, 6), Statement(token, 8));
		}

		// <Stm> ::= foreach '(' ID in <Expr> ')' <Stm>
		protected override object RuleStmForeachLparanIdInRparan(NonterminalToken token)
		{
			return new ForeachStatement(
				new Variable(TText(token, 2), true),
				Expr(token, 4), Statement(token, 6));
		}

		// <Stm> ::= observed '(' <Expr> ')' <Stm>
		protected override object RuleStmObservedLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= observed '(' <Expr> ')' <Stm>");
		}

		// <Stm> ::= using <QualifiedName> ';'
		protected override object RuleStmUsingSemi(NonterminalToken token)
		{
			return new UsingStatement(Get<QualifiedName>(token,1).Names.ToArray());
		}

		// <Stm> ::= using StringLiteral ';'
		protected override object RuleStmUsingStringliteralSemi(NonterminalToken token)
		{
			return new UsingStatement(StringLiteral.Parse(TText(token, 1)).Split('.'));
		}

		// <Stm> ::= using <QualifiedName> as ID ';'
		protected override object RuleStmUsingAsIdSemi(NonterminalToken token)
		{
			return new UsingStatement(Get<QualifiedName>(token,1).Names.ToArray(), TText(token,3));
		}

		// <Stm> ::= using StringLiteral 'as' ID ';'
		protected override object RuleStmUsingStringliteralAsIdSemi(NonterminalToken token)
		{
			return new UsingStatement(StringLiteral.Parse(TText(token, 1)).Split('.'), TText(token,3));
		}

		// <Stm> ::= using '(' <Expr> ')' <Stm>
		protected override object RuleStmUsingLparanRparan(NonterminalToken token)
		{
			return new UsingDisposableStatement(Expr(token,2), Statement(token,4));
		}

		// <Stm> ::= <Normal Stm>
		protected override object RuleStm(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>
		protected override object RuleThenstmIfLparanRparanElse(NonterminalToken token)
		{
			return new IfStatement(Expr(token,2), Statement(token, 4),Statement(token, 6));
		}

		// <Then Stm> ::= while '(' <Expr> ')' <Then Stm>
		protected override object RuleThenstmWhileLparanRparan(NonterminalToken token)
		{
			return new WhileStatement(Expr(token, 2), Statement(token, 4));
		}

		// <Then Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Then Stm>
		protected override object RuleThenstmForLparanSemiSemiRparan(NonterminalToken token)
		{
			return new ForStatement(Expr(token, 2), Expr(token, 4), Expr(token, 6), Statement(token, 8));
		}

		// <Then Stm> ::= <Normal Stm>
		protected override object RuleThenstm(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Normal Stm> ::= do <Stm> while '(' <Expr> ')'
		protected override object RuleNormalstmDoWhileLparanRparan(NonterminalToken token)
		{
			return new DoWhileStatement(Statement(token, 1), Expr(token, 4));
		}

		// <Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'
		protected override object RuleNormalstmSwitchLparanRparanLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<Normal Stm> ::= switch '(' <Expr> ')' '{' <Case Stms> '}'");
		}

		// <Normal Stm> ::= <Block>
		protected override object RuleNormalstm(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Normal Stm> ::= <Expr> ';'
		protected override object RuleNormalstmSemi(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Normal Stm> ::= break ';'
		protected override object RuleNormalstmBreakSemi(NonterminalToken token)
		{
			return new BreakStatement();
		}

		// <Normal Stm> ::= continue ';'
		protected override object RuleNormalstmContinueSemi(NonterminalToken token)
		{
			return new ContinueStatement();
		}

		// <Normal Stm> ::= return <Expr> ';'
		protected override object RuleNormalstmReturnSemi(NonterminalToken token)
		{
			return new ReturnStatement(Expr(token, 1));
		}

		// <Normal Stm> ::= ';'
		protected override object RuleNormalstmSemi2(NonterminalToken token)
		{
			return new NoopStatement();
		}

		// <Func args> ::= <Func args> ',' <Func Arg>
		protected override object RuleFuncargsComma(NonterminalToken token)
		{
			NamedArgumentList list = (NamedArgumentList)CreateObject(token.Tokens[0]);
			list.Add((NamedArgument)CreateObject(token.Tokens[2]));
			return list;
		}

		// <Func args> ::= <Func Arg>
		protected override object RuleFuncargs(NonterminalToken token)
		{
			NamedArgumentList list = new NamedArgumentList(NamedArgumentListMode.DefaultArguments);
			list.Add((NamedArgument)CreateObject(token.Tokens[0]));
			return list;
		}

		// <Func args> ::=
		protected override object RuleFuncargs2(NonterminalToken token)
		{
			return new NamedArgumentList(NamedArgumentListMode.DefaultArguments);
		}

		// <Func Arg> ::= ID
		protected override object RuleFuncargId(NonterminalToken token)
		{
			return new NamedArgument(TText(token,0), null);
		}

		// <Func Arg> ::= ID '=' <Expr>
		protected override object RuleFuncargIdEq(NonterminalToken token)
		{
			return new NamedArgument(TText(token,0), Expr(token,2));
		}

		// <Args> ::= <Args> ',' <Arg>
		protected override object RuleArgsComma(NonterminalToken token)
		{
			NamedArgumentList list = (NamedArgumentList)CreateObject(token.Tokens[0]);
			list.Add(Get<NamedArgument>(token, 2));
			return list;
		}

		// <Args> ::= <Arg>
		protected override object RuleArgs(NonterminalToken token)
		{
			NamedArgumentList list = new NamedArgumentList(NamedArgumentListMode.CallArguments);
			list.Add((NamedArgument)CreateObject(token.Tokens[0]));
			return list;
		}

		// <Args> ::=
		protected override object RuleArgs2(NonterminalToken token)
		{
			return new NamedArgumentList(NamedArgumentListMode.CallArguments);
		}

		// <Arg> ::= <Op If>
		protected override object RuleArg(NonterminalToken token)
		{
			return new NamedArgument(null, Expr(token, 0));
		}

		// <Arg> ::= ID '=' <Expr>
		protected override object RuleArgIdEq(NonterminalToken token)
		{
			return new NamedArgument(TText(token,0), Expr(token, 2));
		}

		// <Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>
		protected override object RuleCasestmsCaseColon(NonterminalToken token)
		{
			throw new NotImplementedException("<Case Stms> ::= case <Value> ':' <Stm List> <Case Stms>");
		}

		// <Case Stms> ::= default ':' <Stm List>
		protected override object RuleCasestmsDefaultColon(NonterminalToken token)
		{
			throw new NotImplementedException("<Case Stms> ::= default ':' <Stm List>");
		}

		// <Case Stms> ::= 
		protected override object RuleCasestms(NonterminalToken token)
		{
			throw new NotImplementedException("<Case Stms> ::= ");
		}

		// <Block> ::= '{' <Stm List> '}'
		protected override object RuleBlockLbraceRbrace(NonterminalToken token)
		{
			return new BlockStatement(true, Get<StatementList>(token,1));
		}

		// <Stm List> ::= <Stm> <Stm List>    -- start symbol
		protected override object RuleStmlist (NonterminalToken token)
		{
			StatementList list = (StatementList)CreateObject(token.Tokens[1]);
			list.Insert(0, Statement(token, 0));
			return list;
		}

		// <Stm List> ::= <Stm>
		protected override object RuleStmlist2(NonterminalToken token)
		{
			StatementList list = new StatementList();
			list.Add(Statement(token, 0));
			return list;
		}

		// <Function> ::= function ID '(' <Func Args> ')' <Stm>
		protected override object RuleFunctionFunctionIdLparanRparan(NonterminalToken token)
		{
			return new FunctionExpression(
				TText(token, 1),
				(NamedArgumentList)CreateObject(token.Tokens[3]),
				Statement(token, 5));
		}

		// <Function> ::= function '(' <Func Args> ')' <Stm>
		protected override object RuleFunctionFunctionLparanRparan(NonterminalToken token)
		{
			return new FunctionExpression(
				null,
				(NamedArgumentList)CreateObject(token.Tokens[2]),
				Statement(token, 4));
		}

		// <Expr List> ::= <Expr List> ',' <Expr>
		protected override object RuleExprlistComma(NonterminalToken token)
		{
			List<IExpression> list = (List<IExpression>)CreateObject(token.Tokens[0]);
			list.Add(Expr(token, 2));
			return list;
		}

		// <Expr List> ::= <Expr>
		protected override object RuleExprlist(NonterminalToken token)
		{
			List<IExpression> list = new List<IExpression>();
			list.Add(Expr(token, 0));
			return list;
		}

		// <Dict List> ::= <Expr> ':' <Expr> ',' <Dict List>
		protected override object RuleDictlistColonComma(NonterminalToken token)
		{
			DictionaryExpression dict = Get<DictionaryExpression>(token,4);
			dict.Add(Expr(token,0), Expr(token,2));
			return dict;
		}

		// <Dict List> ::= <Expr> ':' <Expr>
		protected override object RuleDictlistColon(NonterminalToken token)
		{
			DictionaryExpression dict = new DictionaryExpression();
			dict.Add(Expr(token,0), Expr(token,2));
			return dict;
		}

		// <Dict List> ::=
		protected override object RuleDictlist(NonterminalToken token)
		{
			DictionaryExpression dict = new DictionaryExpression();
			return dict;
		}

		// <Expr> ::= <Op If> '=' <Expr>
		protected override object RuleExprEq(NonterminalToken token)
		{
			return new AssignExpression(Expr(token,0), Expr(token,2));
		}

		// <Expr> ::= <Op If> '+=' <Expr>
		protected override object RuleExprPluseq(NonterminalToken token)
		{
			IExpression op1 = Expr(token, 0);
			return new AssignExpression(op1, new AddExpression(op1, Expr(token, 2)));
		}

		// <Expr> ::= <Op If> '-=' <Expr>
		protected override object RuleExprMinuseq(NonterminalToken token)
		{
			IExpression op1 = Expr(token, 0);
			return new AssignExpression(op1, new SubstractExpression(op1, Expr(token, 2)));
		}

		// <Expr> ::= <Op If> '*=' <Expr>
		protected override object RuleExprTimeseq(NonterminalToken token)
		{
			IExpression op1 = Expr(token, 0);
			return new AssignExpression(op1, new MultiplyExpression(op1, Expr(token, 2)));
		}

		// <Expr> ::= <Op If> '/=' <Expr>
		protected override object RuleExprDiveq(NonterminalToken token)
		{
			IExpression op1 = Expr(token, 0);
			return new AssignExpression(op1, new DivideExpression(op1, Expr(token, 2)));
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
			return Expr(token, 0);
		}

		// <Op If> ::= <Op Or> '?' <Op If> ':' <Op If>
		protected override object RuleOpifQuestionColon(NonterminalToken token)
		{
			return new TernaryOperatorExpression(Expr(token, 0), Expr(token, 2), Expr(token, 4));
		}

		// <Op If> ::= <Op Or>
		protected override object RuleOpif(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Op Or> ::= <Op Or> or <Op And>
		protected override object RuleOporOr(NonterminalToken token)
		{
			return new OrExpression(Expr(token, 0), Expr(token, 2));
		}

		// <Op Or> ::= <Op And>
		protected override object RuleOpor(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Op And> ::= <Op And> and <Op Equate>
		protected override object RuleOpandAnd(NonterminalToken token)
		{
			return new AndExpression(Expr(token, 0), Expr(token, 2));
		}

		// <Op And> ::= <Op Equate>
		protected override object RuleOpand(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Op Equate> ::= <Op Equate> '==' <Op Compare>
		protected override object RuleOpequateEqeq(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.Equal, Expr(token, 0), Expr(token, 2));
		}

		// <Op Equate> ::= <Op Equate> '!=' <Op Compare>
		protected override object RuleOpequateExclameq(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.NonEqual, Expr(token, 0), Expr(token, 2));
		}

		// <Op Equate> ::= <Op Compare>
		protected override object RuleOpequate(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Op Compare> ::= <Op Compare> '<' <Op In>
		protected override object RuleOpcompareLt(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.Less, Expr(token, 0), Expr(token, 2));
		}

		// <Op Compare> ::= <Op Compare> '>' <Op In>
		protected override object RuleOpcompareGt(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.Greater, Expr(token, 0), Expr(token, 2));
		}

		// <Op Compare> ::= <Op Compare> '<=' <Op In>
		protected override object RuleOpcompareLteq(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.LessOrEqual, Expr(token, 0), Expr(token, 2));
		}

		// <Op Compare> ::= <Op Compare> '>=' <Op In>
		protected override object RuleOpcompareGteq(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.GreaterOrEqual, Expr(token, 0), Expr(token, 2));
		}

		// <Op Compare> ::= <Op In>
		protected override object RuleOpcompare(NonterminalToken token)
		{
			return Expr(token, 0);
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
			return new InRangeExpression(
				Expr(token,0), Expr(token,3), Expr(token,5),
				true, true);
		}

		// <Op In> ::= <Op In> in '<' <Op Add> ',' <Op Add> ')'
		protected override object RuleOpinInLtCommaRparan(NonterminalToken token)
		{
			return new InRangeExpression(
				Expr(token,0), Expr(token,3), Expr(token,5),
				true, false);
		}

		// <Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> '>'
		protected override object RuleOpinInLparanCommaGt(NonterminalToken token)
		{
			return new InRangeExpression(
				Expr(token,0), Expr(token,3), Expr(token,5),
				false, true);
		}

		// <Op In> ::= <Op In> in '(' <Op Add> ',' <Op Add> ')'
		protected override object RuleOpinInLparanCommaRparan(NonterminalToken token)
		{
			return new InRangeExpression(
				Expr(token,0), Expr(token,3), Expr(token,5),
				false, false);
		}

		// <Op In> ::= <Op Add>
		protected override object RuleOpin(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Op Add> ::= <Op Add> '+' <Op Mult>
		protected override object RuleOpaddPlus(NonterminalToken token)
		{
			return new AddExpression(Expr(token, 0), Expr(token, 2));
		}

		// <Op Add> ::= <Op Add> '-' <Op Mult>
		protected override object RuleOpaddMinus(NonterminalToken token)
		{
			return new SubstractExpression(Expr(token, 0), Expr(token, 2));
		}

		// <Op Add> ::= <Op Mult>
		protected override object RuleOpadd(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Op Mult> ::= <Op Mult> '*' <Op Unary>
		protected override object RuleOpmultTimes(NonterminalToken token)
		{
			return new MultiplyExpression(Expr(token, 0), Expr(token, 2));
		}

		// <Op Mult> ::= <Op Mult> '/' <Op Unary>
		protected override object RuleOpmultDiv(NonterminalToken token)
		{
			return new DivideExpression(Expr(token, 0), Expr(token, 2));
		}

		// <Op Mult> ::= <Op Mult> '%' <Op Unary>
		protected override object RuleOpmultPercent(NonterminalToken token)
		{
			return new ModuloExpression(Expr(token, 0), Expr(token, 2));
		}

		// <Op Mult> ::= <Op Unary>
		protected override object RuleOpmult(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Op Unary> ::= not <Op Unary>
		protected override object RuleOpunaryNot(NonterminalToken token)
		{
			return new NotExpression(Expr(token,1));
		}

		// <Op Unary> ::= '!' <Op Unary>
		protected override object RuleOpunaryExclam(NonterminalToken token)
		{
			return new NotExpression(Expr(token,1));
		}

		// <Op Unary> ::= '-' <Op Unary>
		protected override object RuleOpunaryMinus(NonterminalToken token)
		{
			return new UnaryMinusExpression(Expr(token,1));
		}

		// <Op Unary> ::= '++' <Op Unary>
		protected override object RuleOpunaryPlusplus(NonterminalToken token)
		{
			return new UnaryIncDec(Expr(token, 1), true, false);
		}

		// <Op Unary> ::= -- <Op Unary>
		protected override object RuleOpunaryMinusminus(NonterminalToken token)
		{
			return new UnaryIncDec(Expr(token, 1), false, false);
		}

		// <Op Unary> ::= <Op Pointer> '++'
		protected override object RuleOpunaryPlusplus2(NonterminalToken token)
		{
			return new UnaryIncDec(Expr(token, 0), true, true);
		}

		// <Op Unary> ::= <Op Pointer> --
		protected override object RuleOpunaryMinusminus2(NonterminalToken token)
		{
			return new UnaryIncDec(Expr(token, 0), false, true);
		}

		// <Op Unary> ::= <Op Pointer> is null
		protected override object RuleOpunaryIsNull(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.Equal, Expr(token, 0), new NullLiteral());
		}

		// <Op Unary> ::= <Op Pointer> not null
		protected override object RuleOpunaryNotNull(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.NonEqual, Expr(token, 0), new NullLiteral());
		}

		// <Op Unary> ::= <Op Pointer> is not null
		protected override object RuleOpunaryIsNotNull(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.NonEqual, Expr(token, 0), new NullLiteral());
		}

		// <Op Unary> ::= cast <Op Unary> as <QualifiedName>
		protected override object RuleOpunaryCastAs(NonterminalToken token)
		{
			return new CastExpression(Expr(token, 1), Get<QualifiedName>(token,3));
		}

		// <Op Unary> ::= <Op Pointer>
		protected override object RuleOpunary(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Op Pointer> ::= <Op Pointer> '.' <Value>
		protected override object RuleOppointerDot(NonterminalToken token)
		{
			return new OpMember(Expr(token, 0), Expr(token, 2), false);
		}

		// <Op Pointer> ::= <Op Pointer> '->' <Value>
		protected override object RuleOppointerMinusgt(NonterminalToken token)
		{
			return new OpMember(Expr(token, 0), Expr(token, 2), true);
		}

		// <Op Pointer> ::= <Op Pointer> '[' <Expr> ']'
		protected override object RuleOppointerLbracketRbracket(NonterminalToken token)
		{
			return new ArrayMember(Expr(token,0), Expr(token,2));
		}

		// <Op Pointer> ::= <Op Pointer> '(' <Args> ')'
		protected override object RuleOppointerLparanRparan(NonterminalToken token)
		{
			return new FunctionCall(Expr(token, 0), (NamedArgumentList)CreateObject(token.Tokens[2]));
		}

		// <Op Pointer> ::= <Value>
		protected override object RuleOppointer(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Value> ::= IntLiteral
		protected override object RuleValueIntliteral(NonterminalToken token)
		{
			return new IntLiteral(Int64.Parse(TText(token, 0), CultureInfo.InvariantCulture));
		}

		// <Value> ::= StringLiteral
		protected override object RuleValueStringliteral(NonterminalToken token)
		{
			return new StringLiteral(StringLiteral.Parse(TText(token, 0)));
		}

		// <Value> ::= DecimalLiteral
		protected override object RuleValueDecimalliteral(NonterminalToken token)
		{
			return new DecimalLiteral(Decimal.Parse(TText(token, 0), CultureInfo.InvariantCulture));
		}

		// <Value> ::= DateTimeLiteral
		protected override object RuleValueDatetimeliteral(NonterminalToken token)
		{
			return DatetimeLiteral.Create(TText(token, 0));
		}

		// <Value> ::= TimeSpanLiteral
		protected override object RuleValueTimespanliteral(NonterminalToken token)
		{
			return new TimeSpanLiteral(TimeSpanLiteral.Parse(TText(token, 0)));
		}

		// <Value> ::= <Function>
		protected override object RuleValue(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Value> ::= <Database>
		protected override object RuleValue3(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Value> ::= type <QualifiedName>
		protected override object RuleValueType(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= type <QualifiedName>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= new <QualifiedName> '(' <Args> ')'
		protected override object RuleValueNewLparanRparan(NonterminalToken token)
		{
			return new NewExpression(Get<QualifiedName>(token,1),Get<NamedArgumentList>(token,3));
		}

		// <Value> ::= ID
		protected override object RuleValueId (NonterminalToken token)
		{
			return new Variable(TText(token, 0), false);
		}

		// <Value> ::= var ID
		protected override object RuleValueVarId(NonterminalToken token)
		{
			return new Variable(TText(token, 1), true);
		}

		// <Value> ::= static ID
		protected override object RuleValueStaticId(NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= static ID");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Value> ::= '(' <Expr> ')'
		protected override object RuleValueLparanRparan (NonterminalToken token)
		{
			return CreateObject(token.Tokens[1]);
		}

		// <Value> ::= '[' <Expr List> ']'
		protected override object RuleValueLbracketRbracket(NonterminalToken token)
		{
			return new ArrayExpression((List<IExpression>)CreateObject(token.Tokens[1]));
		}

		// <Value> ::= '{' <Dict List> '}'
		protected override object RuleValueLbraceRbrace(NonterminalToken token)
		{
			return Get<DictionaryExpression>(token, 1);
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

		// <Value> ::= dict
		protected override object RuleValueDict(NonterminalToken token)
		{
			return new DictionaryExpression();
		}

		// <Value> ::= list
		protected override object RuleValueList(NonterminalToken token)
		{
			return new ObjectCreateExpression(typeof(ArrayList));
		}

		// <Value> ::= null
		protected override object RuleValueNull(NonterminalToken token)
		{
			return new NullLiteral();
		}

		// <Value> ::= true
		protected override object RuleValueTrue(NonterminalToken token)
		{
			return new BooleanLiteral(true);
		}

		// <Value> ::= false
		protected override object RuleValueFalse(NonterminalToken token)
		{
			return new BooleanLiteral(false);
		}

		#region Widgets

		// <Value> ::= <Widget>
		protected override object RuleValue2(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Widget> ::= WINDOW ID <WndParam List> <Layout Block>
		protected override object RuleWidgetWindowId(NonterminalToken token)
		{
			return new WindowExpression(
				TText(token, 1),
				Get<WidgetParamList>(token, 2),
				Get<IWidgetBuilder>(token, 3));
		}

		// <Widget> ::= WINDOW <WndParam List> <Layout Block>
		protected override object RuleWidgetWindow(NonterminalToken token)
		{
			return new WindowExpression(
				null,
				Get<WidgetParamList>(token, 1),
				Get<IWidgetBuilder>(token, 2));
		}

		// <Widget> ::= widget ID <Layout Block>
		protected override object RuleWidgetWidgetId(NonterminalToken token)
		{
			WidgetBase w = Get<WidgetBase>(token, 2);
			w.Name = TText(token, 1);
			return w;
		}

		// <Widget> ::= widget <Layout Block>
		protected override object RuleWidgetWidget(NonterminalToken token)
		{
			return CreateObject(token.Tokens[1]);
		}

		// <WndParam List> ::= <WndParam> <WndParam List>
		protected override object RuleWndparamlist(NonterminalToken token)
		{
			WidgetParamList list = Get<WidgetParamList>(token,1);
			KeyValuePair<string, IExpression> param = Get<KeyValuePair<string, IExpression>>(token,0);
			list.Add(param.Key, new EvaluatedExpression(param.Value));
			return list;
		}

		// <WndParam List> ::= 
		protected override object RuleWndparamlist2(NonterminalToken token)
		{
			return new WidgetParamList();
		}

		// <WndParam> ::= ID '=' <Expr> ';'
		protected override object RuleWndparamIdEqSemi(NonterminalToken token)
		{
			return new KeyValuePair<string, IExpression>(TText(token,0), Expr(token, 2));
		}

		// <WndParam> ::= ID ';'
		protected override object RuleWndparamIdSemi (NonterminalToken token)
		{
			return new KeyValuePair<string, IExpression>(TText(token,0), new BooleanLiteral(true));
		}

		// <Layout Block> ::= HBOX <WndParam List> <Layout List> END
		protected override object RuleLayoutblockHboxEnd(NonterminalToken token)
		{
			return new HBoxContainer(null, Get<WidgetParamList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= VBOX <WndParam List> <Layout List> END
		protected override object RuleLayoutblockVboxEnd(NonterminalToken token)
		{
			return new VBoxContainer(null, Get<WidgetParamList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= hbuttonbox <WndParam List> <Layout List> end
		protected override object RuleLayoutblockHbuttonboxEnd(NonterminalToken token)
		{
			return new HButtonBoxContainer(null, Get<WidgetParamList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= vbuttonbox <WndParam List> <Layout List> end
		protected override object RuleLayoutblockVbuttonboxEnd(NonterminalToken token)
		{
			return new VButtonBoxContainer(null, Get<WidgetParamList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= TABLE <WndParam List> <TabRow Block> END
		protected override object RuleLayoutblockTableEnd(NonterminalToken token)
		{
			return new TableContainer(null, Get<WidgetParamList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= toolbar <WndParam List> <Layout List> end
		protected override object RuleLayoutblockToolbarEnd(NonterminalToken token)
		{
			return new ToolbarExpression(null, Get<WidgetParamList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= button <WndParam List> <Layout Block>
		protected override object RuleLayoutblockButton(NonterminalToken token)
		{
			return new GenericBinExpression(null, Get<WidgetParamList>(token, 1), Get<IWidgetBuilder>(token,2), typeof(Gtk.Button));
		}

		// <Layout Block> ::= button <WndParam List> end
		protected override object RuleLayoutblockButtonEnd(NonterminalToken token)
		{
			return new GenericBinExpression(null, Get<WidgetParamList>(token, 1), null, typeof(Gtk.Button));
		}

		// <Layout Block> ::= toolbutton <WndParam List> <Layout Block>
		protected override object RuleLayoutblockToolbutton(NonterminalToken token)
		{
			return new ToolButtonExpression(null, Get<WidgetParamList>(token, 1), Get<IWidgetBuilder>(token,2));
		}

		// <Layout Block> ::= toolbutton <WndParam List> end
		protected override object RuleLayoutblockToolbuttonEnd(NonterminalToken token)
		{
			return new ToolButtonExpression(null, Get<WidgetParamList>(token, 1), null);
		}

		// <Layout Block> ::= image <WndParam List>
		protected override object RuleLayoutblockImage(NonterminalToken token)
		{
			return new ImageExpression(null, Get<WidgetParamList>(token, 1));
		}

		// <Layout Block> ::= StringLiteral <WndParam List>
		protected override object RuleLayoutblockStringliteral(NonterminalToken token)
		{
			return new LabelExpression(null, StringLiteral.Parse(TText(token,0)), Get<WidgetParamList>(token, 1));
		}

		// <Layout Block> ::= <Menu Block>
		protected override object RuleLayoutblock(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Layout Block> ::= ref <QualifiedName> <WndParam List>
		protected override object RuleLayoutblockRef(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= ref <QualifiedName> <WndParam List>");
			//return new
		}

		// <Layout Block> ::= ref StringLiteral <WndParam List>
		protected override object RuleLayoutblockRefStringliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= ref StringLiteral <WndParam List>");
			//return new
		}

		// <Layout Block> ::= '[' <Expr> ']' <WndParam List>
		protected override object RuleLayoutblockLbracketRbracket(NonterminalToken token)
		{
			return new WidgetFromExpression(Expr(token, 1), Get<WidgetParamList>(token,3));
		}

		// <Layout List> ::= <Layout List> <Layout Block>
		protected override object RuleLayoutlist(NonterminalToken token)
		{
			LayoutList list = Get<LayoutList>(token,0);
			list.Add(Get<IWidgetBuilder>(token,1));
			return list;
		}

		// <Layout List> ::=
		protected override object RuleLayoutlist2(NonterminalToken token)
		{
			return new LayoutList();
		}

		// <Menu Block> ::= MENU <WndParam List> <MenuItems List> END
		protected override object RuleMenublockMenuEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Menu Block> ::= MENU <WndParam List> <MenuItems List> END");
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
			throw new NotImplementedException("<Menu Item> ::= ITEM <WndParam List>");
			//return new
		}

		// <Menu Item> ::= Separator
		protected override object RuleMenuitemSeparator(NonterminalToken token)
		{
			throw new NotImplementedException("<Menu Item> ::= Separator");
			//return new
		}

		#endregion

		#region Database support
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

		// <DB Tables> ::= <DB Table> <DB Tables>
		protected override object RuleDbtables(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Tables> ::= <DB Table> <DB Tables>");
			//return new
		}

		// <DB Tables> ::= 
		protected override object RuleDbtables2(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Tables> ::= ");
			//return new
		}

		// <DB Table> ::= table ID '{' <DB Columns> '}' <DB Table Attr>
		protected override object RuleDbtableTableIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table> ::= table ID '{' <DB Columns> '}' <DB Table Attr>");
			//return new
		}

		// <DB Table> ::= template table ID '{' <DB Columns> '}'
		protected override object RuleDbtableTemplateTableIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table> ::= template table ID '{' <DB Columns> '}'");
			//return new
		}

		// <DB Table> ::= table ID template ID '{' <DB Columns> '}' <DB Table Attr>
		protected override object RuleDbtableTableIdTemplateIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table> ::= table ID template ID '{' <DB Columns> '}' <DB Table Attr>");
			//return new
		}

		// <DB Table> ::= template table ID template ID '{' <DB Columns> '}'
		protected override object RuleDbtableTemplateTableIdTemplateIdLbraceRbrace(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table> ::= template table ID template ID '{' <DB Columns> '}'");
			//return new
		}

		// <DB Columns> ::= <DB Columns> ',' <DB Column>
		protected override object RuleDbcolumnsComma(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Columns> ::= <DB Columns> ',' <DB Column>");
			//return new
		}

		// <DB Columns> ::= <DB Column>
		protected override object RuleDbcolumns(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Columns> ::= <DB Column>");
			//return new
		}

		// <DB Columns> ::= 
		protected override object RuleDbcolumns2(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Columns> ::= ");
			//return new
		}

		// <DB Column> ::= ID <DB Column Type> <DB Column Attr>
		protected override object RuleDbcolumnId(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column> ::= ID <DB Column Type> <DB Column Attr>");
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

		// <DB Column Attr> ::= ID '=' <Expr>
		protected override object RuleDbcolumnattrIdEq(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Column Attr> ::= ID '=' <Expr>");
			//return new
		}

		// <DB Table Attr> ::= index '(' <ID List> ')'
		protected override object RuleDbtableattrIndexLparanRparan(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= index '(' <ID List> ')'");
			//return new
		}

		// <DB Table Attr> ::= before insert position DecimalLiteral <Expr>
		protected override object RuleDbtableattrBeforeInsertPositionDecimalliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= before insert position DecimalLiteral <Expr>");
			//return new
		}

		// <DB Table Attr> ::= after insert position DecimalLiteral <Expr>
		protected override object RuleDbtableattrAfterInsertPositionDecimalliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= after insert position DecimalLiteral <Expr>");
			//return new
		}

		// <DB Table Attr> ::= before update position DecimalLiteral <Expr>
		protected override object RuleDbtableattrBeforeUpdatePositionDecimalliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= before update position DecimalLiteral <Expr>");
			//return new
		}

		// <DB Table Attr> ::= after update position DecimalLiteral <Expr>
		protected override object RuleDbtableattrAfterUpdatePositionDecimalliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= after update position DecimalLiteral <Expr>");
			//return new
		}

		// <DB Table Attr> ::= before delete position DecimalLiteral <Expr>
		protected override object RuleDbtableattrBeforeDeletePositionDecimalliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= before delete position DecimalLiteral <Expr>");
			//return new
		}

		// <DB Table Attr> ::= after delete position DecimalLiteral <Expr>
		protected override object RuleDbtableattrAfterDeletePositionDecimalliteral(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= after delete position DecimalLiteral <Expr>");
			//return new
		}

		// <DB Table Attr> ::= ID '=' <Expr>
		protected override object RuleDbtableattrIdEq(NonterminalToken token)
		{
			throw new NotImplementedException("<DB Table Attr> ::= ID '=' <Expr>");
			//return new
		}
		#endregion

		#endregion
    }
}
