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

		public override object CreateObjectFromTerminal (TerminalToken token)
		{
			StackFrame stack = new StackFrame(3);
			Console.WriteLine("Cannot create terminal {0} at {1}", token, stack.GetMethod().Name);
			throw new InvalidOperationException();
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

		// <QualifiedName> ::= <QualifiedName> '.' ID
		protected override object RuleQualifiednameDotId(NonterminalToken token)
		{
			QualifiedName names = Get<QualifiedName>(token, 0);
			names.Names.Add(TText(token,2));
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

		// <Stm> ::= <TryFinally>
		protected override object RuleStm(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Stm> ::= <Normal Stm>
		protected override object RuleStm2(NonterminalToken token)
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

		// <Normal Stm> ::= throw <Expr> ';'
		protected override object RuleNormalstmThrowSemi(NonterminalToken token)
		{
			return new ThrowStatement(Expr(token,1),null);
		}

		// <Normal Stm> ::= throw <Expr> ',' <Expr> ';'
		protected override object RuleNormalstmThrowCommaSemi(NonterminalToken token)
		{
			return new ThrowStatement(Expr(token,1),Expr(token,3));
		}

		// <Normal Stm> ::= ';'
		protected override object RuleNormalstmSemi2(NonterminalToken token)
		{
			return new NoopStatement();
		}

		// <TryFinally> ::= try <Normal Stm> <Catchs> finally <Normal Stm>
		protected override object RuleTryfinallyTryFinally(NonterminalToken token)
		{
			return new TryBlockStatement(
				Statement(token,1),
				Get<List<CatchStatement>>(token,2),
				Statement(token,4));
		}

		// <TryFinally> ::= try <Normal Stm> <Catchs>
		protected override object RuleTryfinallyTry(NonterminalToken token)
		{
			return new TryBlockStatement(
				Statement(token,1),
				Get<List<CatchStatement>>(token,2),
				null);
		}

		// <Catchs> ::= <Catch> <Catchs>
		protected override object RuleCatchs(NonterminalToken token)
		{
			List<CatchStatement> list = Get<List<CatchStatement>>(token,1);
			list.Insert(0, Get<CatchStatement>(token,0));
			return list;
		}

		// <Catchs> ::= 
		protected override object RuleCatchs2(NonterminalToken token)
		{
			return new List<CatchStatement>();
		}

		// <Catch> ::= catch '(' <QualifiedName> ID ')' <Normal Stm>
		protected override object RuleCatchCatchLparanIdRparan(NonterminalToken token)
		{
			return new CatchStatement(
				Get<QualifiedName>(token,2),
				TText(token,3),
				Statement(token,5));
		}

		// <Catch> ::= catch '(' type <QualifiedName> ')' <Normal Stm>
		protected override object RuleCatchCatchLparanTypeRparan(NonterminalToken token)
		{
			return new CatchStatement(
				Get<QualifiedName>(token,3),
				null,
				Statement(token,5));
		}

		// <Catch> ::= catch '(' var ID ')' <Normal Stm>
		protected override object RuleCatchCatchLparanVarIdRparan(NonterminalToken token)
		{
			return new CatchStatement(
				null,
				TText(token,3),
				Statement(token,5));
		}

		// <Catch> ::= catch <Block>
		protected override object RuleCatchCatch(NonterminalToken token)
		{
			return new CatchStatement(
				null,
				null,
				Statement(token,1));
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

		// <Expr> ::= <Database>
		protected override object RuleExpr2(NonterminalToken token)
		{
			return Expr(token,0);
		}

		// <Expr> ::= liststore <WndParam List> <Store Items> end
		protected override object RuleExprListstoreEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= liststore <WndParam List> <Store Items> end");
			//return new
		}

		// <Expr> ::= treestore <WndParam List> <Store Items> end
		protected override object RuleExprTreestoreEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= treestore <WndParam List> <Store Items> end");
			//return new
		}

		// <Expr> ::= <Op Assign>
		protected override object RuleExpr3(NonterminalToken token)
		{
			return Expr(token,0);
		}

		// <Op Assign> ::= <Op If> '=' <Expr>
		protected override object RuleOpassignEq (NonterminalToken token)
		{
			return new AssignExpression(Expr(token,0), Expr(token,2));
		}

		// <Op Assign> ::= <Op If> '+=' <Expr>
		protected override object RuleOpassignPluseq(NonterminalToken token)
		{
			IExpression op1 = Expr(token, 0);
			return new AssignExpression(op1, new AddExpression(op1, Expr(token, 2)));
		}

		// <Op Assign> ::= <Op If> '-=' <Expr>
		protected override object RuleOpassignMinuseq(NonterminalToken token)
		{
			IExpression op1 = Expr(token, 0);
			return new AssignExpression(op1, new SubstractExpression(op1, Expr(token, 2)));
		}

		// <Op Assign> ::= <Op If> '*=' <Expr>
		protected override object RuleOpassignTimeseq(NonterminalToken token)
		{
			IExpression op1 = Expr(token, 0);
			return new AssignExpression(op1, new MultiplyExpression(op1, Expr(token, 2)));
		}

		// <Op Assign> ::= <Op If> '/=' <Expr>
		protected override object RuleOpassignDiveq(NonterminalToken token)
		{
			IExpression op1 = Expr(token, 0);
			return new AssignExpression(op1, new DivideExpression(op1, Expr(token, 2)));
		}

		// <Op Assign> ::= <Op If> '<==' <Expr>
		protected override object RuleOpassignLteqeq(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op If> '<==' <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Assign> ::= <Op If> '<==>' <Expr>
		protected override object RuleOpassignLteqeqgt(NonterminalToken token)
		{
			throw new NotImplementedException("<Expr> ::= <Op If> '<==>' <Expr>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op Assign> ::= <Op If>
		protected override object RuleOpassign(NonterminalToken token)
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

		// <Op Equate> ::= <Op Equate> 'in' <Op Compare>
		protected override object RuleOpequateIn (NonterminalToken token)
		{
			return new InExpression(Expr(token, 0), Expr(token, 2));
		}

		// <Op Equate> ::= <Op Compare>
		protected override object RuleOpequate(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Op Compare> ::= <Op Compare> '<' <Op Add>
		protected override object RuleOpcompareLt(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.Less, Expr(token, 0), Expr(token, 2));
		}

		// <Op Compare> ::= <Op Compare> '>' <Op Add>
		protected override object RuleOpcompareGt(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.Greater, Expr(token, 0), Expr(token, 2));
		}

		// <Op Compare> ::= <Op Compare> '<=' <Op Add>
		protected override object RuleOpcompareLteq(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.LessOrEqual, Expr(token, 0), Expr(token, 2));
		}

		// <Op Compare> ::= <Op Compare> '>=' <Op Add>
		protected override object RuleOpcompareGteq(NonterminalToken token)
		{
			return new CompareExpression(ComparisonType.GreaterOrEqual, Expr(token, 0), Expr(token, 2));
		}

		// <Op Compare> ::= <Op Add>
		protected override object RuleOpcompare(NonterminalToken token)
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

		// <Op Unary> ::= cast '(' <Op Unary> as <QualifiedName> ')'
		protected override object RuleOpunaryCastLparanAsRparan (NonterminalToken token)
		{
			return new CastExpression(Expr(token, 2), Get<QualifiedName>(token,4));
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

		// <Value> ::= type '(' <QualifiedName> ')'
		protected override object RuleValueTypeLparanRparan (NonterminalToken token)
		{
			throw new NotImplementedException("<Value> ::= type '(' <QualifiedName> ')'");
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

		// <Value> ::= range '<' <Op Add> ';' <Op Add> '>'
		protected override object RuleValueRangeLtSemiGt(NonterminalToken token)
		{
			return new RangeExpression(RangeType.Closed, Expr(token,2), Expr(token,4));
		}

		// <Value> ::= range '<' <Op Add> ';' <Op Add> ')'
		protected override object RuleValueRangeLtSemiRparan(NonterminalToken token)
		{
			return new RangeExpression(RangeType.LeftClosed, Expr(token,2), Expr(token,4));
		}

		// <Value> ::= range '(' <Op Add> ';' <Op Add> '>'
		protected override object RuleValueRangeLparanSemiGt(NonterminalToken token)
		{
			return new RangeExpression(RangeType.RightClosed, Expr(token,2), Expr(token,4));
		}

		// <Value> ::= range '(' <Op Add> ';' <Op Add> ')'
		protected override object RuleValueRangeLparanSemiRparan(NonterminalToken token)
		{
			return new RangeExpression(RangeType.Open, Expr(token,2), Expr(token,4));
		}

		// <Value> ::= <Function>
		protected override object RuleValue(NonterminalToken token)
		{
			return Expr(token,0);
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
		protected override object RuleExpr(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Widget> ::= WINDOW ID <WndParam List> <Layout Block>
		protected override object RuleWidgetWindowId(NonterminalToken token)
		{
			return new WindowExpression(
				TText(token, 1),
				Get<EvaluatedAttributeList>(token, 2),
				Get<IWidgetBuilder>(token, 3));
		}

		// <Widget> ::= WINDOW <WndParam List> <Layout Block>
		protected override object RuleWidgetWindow(NonterminalToken token)
		{
			return new WindowExpression(
				null,
				Get<EvaluatedAttributeList>(token, 1),
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
			EvaluatedAttributeList list = Get<EvaluatedAttributeList>(token,1);
			KeyValuePair<string, IExpression> param = Get<KeyValuePair<string, IExpression>>(token,0);
			list.Add(param.Key, new EvaluatedAttribute(param.Value));
			return list;
		}

		// <WndParam List> ::= 
		protected override object RuleWndparamlist2(NonterminalToken token)
		{
			return new EvaluatedAttributeList();
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
			return new HBoxContainer(null, Get<EvaluatedAttributeList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= VBOX <WndParam List> <Layout List> END
		protected override object RuleLayoutblockVboxEnd(NonterminalToken token)
		{
			return new VBoxContainer(null, Get<EvaluatedAttributeList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= hbuttonbox <WndParam List> <Layout List> end
		protected override object RuleLayoutblockHbuttonboxEnd(NonterminalToken token)
		{
			return new HButtonBoxContainer(null, Get<EvaluatedAttributeList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= vbuttonbox <WndParam List> <Layout List> end
		protected override object RuleLayoutblockVbuttonboxEnd(NonterminalToken token)
		{
			return new VButtonBoxContainer(null, Get<EvaluatedAttributeList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= hpaned <WndParam List> <Layout List> end
		protected override object RuleLayoutblockHpanedEnd(NonterminalToken token)
		{
			return new HPanedContainer(null, Get<EvaluatedAttributeList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= vpaned <WndParam List> <Layout List> end
		protected override object RuleLayoutblockVpanedEnd(NonterminalToken token)
		{
			return new VPanedContainer(null, Get<EvaluatedAttributeList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= TABLE <WndParam List> <TabRow Block> END
		protected override object RuleLayoutblockTableEnd(NonterminalToken token)
		{
			return new TableContainer(null, Get<EvaluatedAttributeList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= toolbar <WndParam List> <Layout List> end
		protected override object RuleLayoutblockToolbarEnd(NonterminalToken token)
		{
			return new ToolbarExpression(null, Get<EvaluatedAttributeList>(token, 1), Get<LayoutList>(token,2));
		}

		// <Layout Block> ::= button <WndParam List> <Layout Block>
		protected override object RuleLayoutblockButton(NonterminalToken token)
		{
			return new ButtonExpression(null, Get<EvaluatedAttributeList>(token, 1), Get<IWidgetBuilder>(token,2));
		}

		// <Layout Block> ::= button <WndParam List> end
		protected override object RuleLayoutblockButtonEnd(NonterminalToken token)
		{
			return new ButtonExpression(null, Get<EvaluatedAttributeList>(token, 1), null);
		}

		// <Layout Block> ::= toolbutton <WndParam List> <Layout Block>
		protected override object RuleLayoutblockToolbutton(NonterminalToken token)
		{
			return new ToolButtonExpression(null, Get<EvaluatedAttributeList>(token, 1), Get<IWidgetBuilder>(token,2));
		}

		// <Layout Block> ::= toolbutton <WndParam List> end
		protected override object RuleLayoutblockToolbuttonEnd(NonterminalToken token)
		{
			return new ToolButtonExpression(null, Get<EvaluatedAttributeList>(token, 1), null);
		}

		// <Layout Block> ::= align <WndParam List> <Layout Block>
		protected override object RuleLayoutblockAlign(NonterminalToken token)
		{
			return new AlignExpression(null, Get<EvaluatedAttributeList>(token, 1), Get<IWidgetBuilder>(token,2));
		}

		// <Layout Block> ::= scrolled <WndParam List> <Layout Block>
		protected override object RuleLayoutblockScrolled (NonterminalToken token)
		{
			return new ScrolledExpression(null, Get<EvaluatedAttributeList>(token, 1), Get<IWidgetBuilder>(token,2));
		}

		// <Layout Block> ::= image <WndParam List>
		protected override object RuleLayoutblockImage(NonterminalToken token)
		{
			return new ImageExpression(null, Get<EvaluatedAttributeList>(token, 1));
		}

		// <Layout Block> ::= treeview <WndParam List> <TreeView Columns> end
		protected override object RuleLayoutblockTreeviewEnd(NonterminalToken token)
		{
			throw new NotImplementedException("<Layout Block> ::= treeview <WndParam List> <TreeView Columns> end");
			//return new
		}

		// <Layout Block> ::= StringLiteral <WndParam List>
		protected override object RuleLayoutblockStringliteral(NonterminalToken token)
		{
			return new LabelExpression(null, StringLiteral.Parse(TText(token,0)), Get<EvaluatedAttributeList>(token, 1));
		}

		// <Layout Block> ::= <Menu Block>
		protected override object RuleLayoutblock(NonterminalToken token)
		{
			MenuExpression menu = Get<MenuExpression>(token,0);
			menu.Kind = MenuExpressionKind.MenuBar;
			return menu;
		}

		// <Layout Block> ::= '[' <Expr> ']' <WndParam List>
		protected override object RuleLayoutblockLbracketRbracket(NonterminalToken token)
		{
			return new WidgetFromExpression(Expr(token, 1), Get<EvaluatedAttributeList>(token,3));
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
			return new MenuExpression(null, MenuExpressionKind.Menu, Get<EvaluatedAttributeList>(token,1), Get<List<MenuExpression>>(token,2));
		}

		// <MenuItems List> ::= <MenuItems List> <Menu Item>
		protected override object RuleMenuitemslist(NonterminalToken token)
		{
			List<MenuExpression> list = Get<List<MenuExpression>>(token,0);
			list.Add(Get<MenuExpression>(token,1));
			return list;
		}

		// <MenuItems List> ::= 
		protected override object RuleMenuitemslist2(NonterminalToken token)
		{
			return new List<MenuExpression>();
		}

		// <Menu Item> ::= <Menu Block>
		protected override object RuleMenuitem(NonterminalToken token)
		{
			MenuExpression menu = Get<MenuExpression>(token,0);
			menu.Kind = MenuExpressionKind.Menu;
			return menu;
		}

		// <Menu Item> ::= menuitem <WndParam List>
		protected override object RuleMenuitemMenuitem(NonterminalToken token)
		{
			return new MenuExpression(null, MenuExpressionKind.MenuItem, Get<EvaluatedAttributeList>(token,1), null);
		}

		// <Menu Item> ::= Separator
		protected override object RuleMenuitemSeparator(NonterminalToken token)
		{
			return new MenuExpression(null, MenuExpressionKind.ItemSeparator, new EvaluatedAttributeList(), null);
		}

		// <TreeView Columns> ::= <TreeView Columns> <TreeView Column>
		protected override object RuleTreeviewcolumns(NonterminalToken token)
		{
			List<TreeViewColumnStatement> list = Get<List<TreeViewColumnStatement>>(token,0);
			list.Add(Get<TreeViewColumnStatement>(token,1));
			return list;
		}

		// <TreeView Columns> ::=
		protected override object RuleTreeviewcolumns2(NonterminalToken token)
		{
			return new List<TreeViewColumnStatement>();
		}

		// <TreeView Column> ::= column <WndParam List>
		protected override object RuleTreeviewcolumnColumn(NonterminalToken token)
		{
			return new TreeViewColumnStatement(Get<EvaluatedAttributeList>(token,1));
		}

		// <Store Items> ::= <Store Items> <Store Item>
		protected override object RuleStoreitems(NonterminalToken token)
		{
			List<StoreItemStatement> list = Get<List<StoreItemStatement>>(token,0);
			list.Add(Get<StoreItemStatement>(token,1));
			return list;
		}

		// <Store Items> ::=
		protected override object RuleStoreitems2(NonterminalToken token)
		{
			return new List<StoreItemStatement>();
		}

		// <Store Item> ::= item '[' <Expr List> ']' <Store Items> end
		protected override object RuleStoreitemItemLbracketRbracketEnd(NonterminalToken token)
		{
			return new StoreItemStatement(
				Get<List<IExpression>>(token,2),
				Get<List<StoreItemStatement>>(token,4));
		}

		#endregion

		#region Database support
		// <Column Name> ::= ID
		protected override object RuleColumnnameId(NonterminalToken token)
		{
			return TText(token,0);
		}

		// <Column Name> ::= StringLiteral
		protected override object RuleColumnnameStringliteral(NonterminalToken token)
		{
			return StringLiteral.Parse(TText(token,0));
		}

		// <Column List> ::= <Column List> ',' <Column Name>
		protected override object RuleColumnlistComma(NonterminalToken token)
		{
			List<string> list = Get<List<string>>(token,0);
			list.Add(Get<string>(token,2));
			return list;
		}

		// <Column List> ::= <Column Name>
		protected override object RuleColumnlist(NonterminalToken token)
		{
			List<string> list = new List<string>();
			list.Add(Get<string>(token,0));
			return list;
		}

		// <Column List> ::=
		protected override object RuleColumnlist2(NonterminalToken token)
		{
			return new List<string>();
		}

		// <Database> ::= database ID '{' <DB Tables> '}'
		protected override object RuleDatabaseDatabaseIdLbraceRbrace(NonterminalToken token)
		{
			DatabaseExpression db = new DatabaseExpression(TText(token,1), false);
			foreach(IDBTable table in Get<IEnumerable>(token,3))
				db.Add(table.Name, table);
			return db;
		}

		// <Database> ::= extends database ID '{' <DB Tables> '}'
		protected override object RuleDatabaseExtendsDatabaseIdLbraceRbrace(NonterminalToken token)
		{
			DatabaseExpression db = new DatabaseExpression(TText(token,1), true);
			foreach(IDBTable table in Get<IEnumerable>(token,3))
				db.Add(table.Name, table);
			return db;
		}

		// <DB Tables> ::= <DB Tables> <DB Table>
		protected override object RuleDbtables(NonterminalToken token)
		{
			List<IDBTable> list = Get<List<IDBTable>>(token,0);
			list.Add(Get<IDBTable>(token,1));
			return list;
		}

		// <DB Tables> ::= 
		protected override object RuleDbtables2(NonterminalToken token)
		{
			return new List<IDBTable>();
		}

		// <DB Table> ::= template table ID '{' <DB Columns> '}' <DB Table Attr List>
		protected override object RuleDbtableTemplateTableIdLbraceRbrace(NonterminalToken token)
		{
			DBTableExpression table = new DBTableExpression(
				TText(token,2), true, false, null);
			table.AddAttribs(Get<IEnumerable>(token,6));
			foreach(IDBColumn column in Get<IEnumerable>(token,4))
				table.Add(column.Name, column);
			return table;
		}

		// <DB Table> ::= template table ID template ID '{' <DB Columns> '}' <DB Table Attr List>
		protected override object RuleDbtableTemplateTableIdTemplateIdLbraceRbrace(NonterminalToken token)
		{
			DBTableExpression table = new DBTableExpression(
				TText(token,2), true, false, TText(token,4));
			table.AddAttribs(Get<IEnumerable>(token,8));
			foreach(IDBColumn column in Get<IEnumerable>(token,6))
				table.Add(column.Name, column);
			return table;
		}

		// <DB Table> ::= table ID '{' <DB Columns> '}' <DB Table Attr List>
		protected override object RuleDbtableTableIdLbraceRbrace(NonterminalToken token)
		{
			DBTableExpression table = new DBTableExpression(
				TText(token,1), false, false, null);
			table.AddAttribs(Get<IEnumerable>(token,5));
			foreach(IDBColumn column in Get<IEnumerable>(token,3))
				table.Add(column.Name, column);
			return table;
		}

		// <DB Table> ::= table ID template ID '{' <DB Columns> '}' <DB Table Attr>
		protected override object RuleDbtableTableIdTemplateIdLbraceRbrace(NonterminalToken token)
		{
			DBTableExpression table = new DBTableExpression(
				TText(token,1), false, false, TText(token,3));
			table.AddAttribs(Get<IEnumerable>(token,7));
			foreach(IDBColumn column in Get<IEnumerable>(token,5))
				table.Add(column.Name, column);
			return table;
		}

		// <DB Table> ::= extends table ID '{' <DB Columns> '}' <DB Table Attr List>
		protected override object RuleDbtableExtendsTableIdLbraceRbrace(NonterminalToken token)
		{
			DBTableExpression table = new DBTableExpression(
				TText(token,2), false, true, TText(token,4));
			table.AddAttribs(Get<IEnumerable>(token,8));
			foreach(IDBColumn column in Get<IEnumerable>(token,6))
				table.Add(column.Name, column);
			return table;
		}

		// <DB Columns> ::= <DB Columns> <DB Column>
		protected override object RuleDbcolumns(NonterminalToken token)
		{
			List<IDBColumn> list = Get<List<IDBColumn>>(token,0);
			list.Add(Get<IDBColumn>(token,1));
			return list;
		}

		// <DB Columns> ::= 
		protected override object RuleDbcolumns2(NonterminalToken token)
		{
			return new List<IDBColumn>();
		}

		// <DB Column> ::= <Column Name> <DB Column Type> <DB Column Attr List> ';'
		protected override object RuleDbcolumnSemi(NonterminalToken token)
		{
			DBColumnBase col = Get<DBColumnBase>(token, 1);
			col.Name = Get<string>(token, 0);
			col.Attribs = Get<EvaluatedAttributeList>(token, 2);
			return col;
		}

		// <DB Column Type> ::= primary
		protected override object RuleDbcolumntypePrimary(NonterminalToken token)
		{
			return new DBColumnPrimary();
		}

		// <DB Column Type> ::= foreign ID
		protected override object RuleDbcolumntypeForeignId(NonterminalToken token)
		{
			return new DBColumnForeign(TText(token,1));
		}

		// <DB Column Type> ::= foreign ID '(' ID ')'
		protected override object RuleDbcolumntypeForeignIdLparanIdRparan(NonterminalToken token)
		{
			return new DBColumnForeign(TText(token,1), TText(token,3));
		}

		// <DB Column Type> ::= many ID
		protected override object RuleDbcolumntypeManyId(NonterminalToken token)
		{
			return new DBColumnManyToMany(
				TText(token,1),  // referenced table
				null,  // through table
				null,  // through table this foreign key name
				null); // through table referenced foreign key name
		}

		// <DB Column Type> ::= many ID through ID
		protected override object RuleDbcolumntypeManyIdThroughId(NonterminalToken token)
		{
			return new DBColumnManyToMany(
				TText(token,1),  // referenced table
				TText(token,3),  // through table
				null,  // through table this foreign key name
				null); // through table referenced foreign key name
		}

		// <DB Column Type> ::= many ID through ID '(' ID ',' ID ')'
		protected override object RuleDbcolumntypeManyIdThroughIdLparanIdCommaIdRparan(NonterminalToken token)
		{
			return new DBColumnManyToMany(
				TText(token,1),  // referenced table
				TText(token,3),  // through table
				TText(token,5),  // through table this foreign key name
				TText(token,7)); // through table referenced foreign key name
		}

		// <DB Column Type> ::= varchar
		protected override object RuleDbcolumntypeVarchar(NonterminalToken token)
		{
			return new DBColumnVarchar();
		}

		// <DB Column Type> ::= varchar '(' IntLiteral ')'
		protected override object RuleDbcolumntypeVarcharLparanIntliteralRparan(NonterminalToken token)
		{
			return new DBColumnVarchar(Int32.Parse(TText(token, 2)));
		}

		// <DB Column Type> ::= integer
		protected override object RuleDbcolumntypeInteger(NonterminalToken token)
		{
			return new DBColumnInteger();
		}

		// <DB Column Type> ::= bool
		protected override object RuleDbcolumntypeBool(NonterminalToken token)
		{
			return new DBColumnBoolean();
		}

		// <DB Column Type> ::= decimal '(' IntLiteral ',' IntLiteral ')'
		protected override object RuleDbcolumntypeDecimalLparanIntliteralCommaIntliteralRparan(NonterminalToken token)
		{
			return new DBColumnDecimal(Int32.Parse(TText(token, 2)), Int32.Parse(TText(token, 4)));
		}

		// <DB Column Type> ::= date
		protected override object RuleDbcolumntypeDate(NonterminalToken token)
		{
			return new DBColumnDate();
		}

		// <DB Column Type> ::= time
		protected override object RuleDbcolumntypeTime(NonterminalToken token)
		{
			return new DBColumnTime();
		}

		// <DB Column Type> ::= datetime
		protected override object RuleDbcolumntypeDatetime(NonterminalToken token)
		{
			return new DBColumnDateTime();
		}

		// <DB Column Type> ::= daterange
		protected override object RuleDbcolumntypeDaterange(NonterminalToken token)
		{
			return new DBColumnDateRange();
		}

		// <DB Column Type> ::= timerange
		protected override object RuleDbcolumntypeTimerange(NonterminalToken token)
		{
			return new DBColumnTimeRange();
		}

		// <DB Column Type> ::= datetimerange
		protected override object RuleDbcolumntypeDatetimerange(NonterminalToken token)
		{
			return new DBColumnDateTimeRange();
		}

		// <DB Column Attr List> ::= <DB Column Attr List> <DB Column Attr>
		protected override object RuleDbcolumnattrlist(NonterminalToken token)
		{
			EvaluatedAttributeList list = Get<EvaluatedAttributeList>(token,0);
			KeyValuePair<string, EvaluatedAttribute> kv = Get<KeyValuePair<string, EvaluatedAttribute>>(token,1);
			list.Add(kv.Key, kv.Value);
			return list;
		}

		// <DB Column Attr List> ::= 
		protected override object RuleDbcolumnattrlist2(NonterminalToken token)
		{
			return new EvaluatedAttributeList();
		}

		// <DB Column Attr> ::= unique
		protected override object RuleDbcolumnattrUnique(NonterminalToken token)
		{
			return new KeyValuePair<string, EvaluatedAttribute>(
				"UNIQUE", new EvaluatedAttribute(new BooleanLiteral(true)));
		}

		// <DB Column Attr> ::= not null
		protected override object RuleDbcolumnattrNotNull(NonterminalToken token)
		{
			return new KeyValuePair<string, EvaluatedAttribute>(
				"NOT NULL", new EvaluatedAttribute(new BooleanLiteral(true)));
		}

		// <DB Column Attr> ::= index
		protected override object RuleDbcolumnattrIndex(NonterminalToken token)
		{
			return new KeyValuePair<string, EvaluatedAttribute>(
				"INDEX", new EvaluatedAttribute(new BooleanLiteral(true)));
		}

		// <DB Column Attr> ::= default <Value>
		protected override object RuleDbcolumnattrDefault(NonterminalToken token)
		{
			return new KeyValuePair<string, EvaluatedAttribute>(
				"default", new EvaluatedAttribute(Expr(token,1)));
		}

		// <DB Column Attr> ::= ID '=' <Value>
		protected override object RuleDbcolumnattrIdEq(NonterminalToken token)
		{
			return new KeyValuePair<string, EvaluatedAttribute>(
				TText(token,0), new EvaluatedAttribute(Expr(token,2)));
		}

		// <DB Table Attr List> ::= <DB Table Attr List> <DB Table Attr>
		protected override object RuleDbtableattrlist(NonterminalToken token)
		{
			ArrayList list = Get<ArrayList>(token,0);
			list.Add(CreateObject(token.Tokens[1]));
			return list;
		}

		// <DB Table Attr List> ::= 
		protected override object RuleDbtableattrlist2(NonterminalToken token)
		{
			return new ArrayList();
		}

		// <DB Table Attr> ::= index '(' <ID List> ')' ';'
		protected override object RuleDbtableattrIndexLparanRparanSemi(NonterminalToken token)
		{
			return new DBTableIndex(false, Get<List<string>>(token,2).ToArray());
		}

		// <DB Table Attr> ::= unique '(' <ID List> ')' ';'
		protected override object RuleDbtableattrUniqueLparanRparanSemi(NonterminalToken token)
		{
			return new DBTableIndex(true, Get<List<string>>(token,2).ToArray());
		}

		// <DB Table Attr> ::= <DB Trigger Runs> position DecimalLiteral <Stm>
		protected override object RuleDbtableattrPosition(NonterminalToken token)
		{
			return new DBTableTrigger(
				Get<DBTriggerPosition>(token,0),
				Get<Decimal>(token,2),
				Statement(token,3));
		}

		// <DB Table Attr> ::= ID '=' <Expr> ';'
		protected override object RuleDbtableattrIdEqSemi(NonterminalToken token)
		{
			return new KeyValuePair<string, EvaluatedAttribute>(
				TText(token,0), new EvaluatedAttribute(Expr(token,2)));
		}

		// <DB Trigger Runs> ::= <DB Trigger Runs> ',' <DB Trigger Run>
		protected override object RuleDbtriggerrunsComma(NonterminalToken token)
		{
			DBTriggerPosition curpos = Get<DBTriggerPosition>(token,0);
			DBTriggerPosition newpos = Get<DBTriggerPosition>(token,2);
			if((int)(newpos & curpos) != 0 && curpos != DBTriggerPosition.None)
				throw new Exception("Pozice triggeru byla nastavena duplicitně");
			return curpos | newpos;
		}

		// <DB Trigger Runs> ::= <DB Trigger Run>
		protected override object RuleDbtriggerruns(NonterminalToken token)
		{
			return Get<DBTriggerPosition>(token,0);
		}

		// <DB Trigger Run> ::= before select
		protected override object RuleDbtriggerrunBeforeSelect(NonterminalToken token)
		{
			return DBTriggerPosition.BeforeSelect;
		}

		// <DB Trigger Run> ::= after select
		protected override object RuleDbtriggerrunAfterSelect(NonterminalToken token)
		{
			return DBTriggerPosition.AfterSelect;
		}

		// <DB Trigger Run> ::= before insert
		protected override object RuleDbtriggerrunBeforeInsert(NonterminalToken token)
		{
			return DBTriggerPosition.BeforeInsert;
		}

		// <DB Trigger Run> ::= after insert
		protected override object RuleDbtriggerrunAfterInsert(NonterminalToken token)
		{
			return DBTriggerPosition.AfterInsert;
		}

		// <DB Trigger Run> ::= before update
		protected override object RuleDbtriggerrunBeforeUpdate(NonterminalToken token)
		{
			return DBTriggerPosition.BeforeUpdate;
		}

		// <DB Trigger Run> ::= after update
		protected override object RuleDbtriggerrunAfterUpdate(NonterminalToken token)
		{
			return DBTriggerPosition.AfterUpdate;
		}

		// <DB Trigger Run> ::= before delete
		protected override object RuleDbtriggerrunBeforeDelete(NonterminalToken token)
		{
			return DBTriggerPosition.BeforeDelete;
		}

		// <DB Trigger Run> ::= after delete
		protected override object RuleDbtriggerrunAfterDelete(NonterminalToken token)
		{
			return DBTriggerPosition.AfterDelete;
		}

		// <DB Trigger Run> ::= before modified
		protected override object RuleDbtriggerrunBeforeModified(NonterminalToken token)
		{
			return DBTriggerPosition.BeforeModify;
		}

		// <DB Trigger Run> ::= after modified
		protected override object RuleDbtriggerrunAfterModified(NonterminalToken token)
		{
			return DBTriggerPosition.AfterModify;
		}

		// <NumLiteral> ::= IntLiteral
		protected override object RuleNumliteralIntliteral(NonterminalToken token)
		{
			return DecimalLiteral.Parse(TText(token,0));
		}

		// <NumLiteral> ::= DecimalLiteral
		protected override object RuleNumliteralDecimalliteral(NonterminalToken token)
		{
			return DecimalLiteral.Parse(TText(token,0));
		}

		#endregion

		#endregion
    }
}
