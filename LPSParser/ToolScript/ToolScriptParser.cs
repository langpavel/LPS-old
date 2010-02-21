using com.calitha.goldparser;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using LPS.ToolScript.Tokens;
using System.Collections.Generic;
using System.Diagnostics;

namespace LPS.ToolScript
{
    public class ToolScriptParser : ToolScriptParserBase
    {
		#region Terminaly
		protected override object TerminalStringliteral (TerminalToken token)
		{
			return new StringLiteral(token);
		}

		protected override object TerminalIntliteral (TerminalToken token)
		{
			return new IntLiteral(token);
		}

		protected override object TerminalDecimalliteral (TerminalToken token)
		{
			return new DecimalLiteral(token);
		}

		protected override object TerminalNull (TerminalToken token)
		{
			return new NullLiteral();
		}

		protected override object TerminalTrue (TerminalToken token)
		{
			return new BooleanLiteral(true);
		}

		protected override object TerminalFalse (TerminalToken token)
		{
			return new BooleanLiteral(false);
		}

		#endregion

		IStatement Statement(NonterminalToken token, int index)
		{
			object obj = CreateObject(token.Tokens[index]);
			try
			{
				return (IStatement)obj;
			}
			catch(InvalidCastException err)
			{
				StackFrame stack = new StackFrame(1);
				Console.WriteLine("Cannot cast from {0} to IStatement {1}", obj.GetType(), stack.GetMethod().Name);
				throw err;
			}
		}

		private IExpression Expr(NonterminalToken token, int index)
		{
			return (IExpression)CreateObject(token.Tokens[index]);
		}

		private string TText(NonterminalToken token, int index)
		{
			return ((TerminalToken)token.Tokens[index]).Text;
		}


		// <Stm> ::= if '(' <Expr> ')' <Stm>
		protected override object RuleStmIfLparanRparan(NonterminalToken token)
		{
			//CheckRule(token, Symbols.If, Symbols.Lparan, Symbols.Expr, Symbols.Rparan, Symbols.Stm);
			return new IfStatement(
				(IExpression)CreateObject(token.Tokens[2]),
			    (IStatement)CreateObject(token.Tokens[4]),
				null);
		}

		// <Stm> ::= if '(' <Expr> ')' <Then Stm> else <Stm>
		protected override object RuleStmIfLparanRparanElse(NonterminalToken token)
		{
			//CheckRule(token, Symbols.If, Symbols.Lparan, Symbols.Expr, Symbols.Rparan, Symbols.Thenstm, Symbols.Else, Symbols.Stm);
			return new IfStatement(
				(IExpression)CreateObject(token.Tokens[2]),
			    (IStatement)CreateObject(token.Tokens[4]),
				(IStatement)CreateObject(token.Tokens[6]));
		}

		// <Stm> ::= while '(' <Expr> ')' <Stm>
		protected override object RuleStmWhileLparanRparan(NonterminalToken token)
		{
			//CheckRule(token, Symbols.While, Symbols.Lparan, Symbols.Expr, Symbols.Rparan, Symbols.Expr);
			return new WhileStatement(
				(IExpression)CreateObject(token.Tokens[2]),
			    (IStatement)CreateObject(token.Tokens[4]));
		}

		// <Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>
		protected override object RuleStmForLparanSemiSemiRparan(NonterminalToken token)
		{
			//CheckRule(token, Symbols.For, Symbols.Lparan, Symbols.Expr, Symbols.Semi, Symbols.Expr, Symbols.Semi, Symbols.Expr, Symbols.Rparan, Symbols.Stm);
			throw new NotImplementedException("<Stm> ::= for '(' <Expr> ';' <Expr> ';' <Expr> ')' <Stm>");
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

		// <Stm> ::= using QualifiedName ';'
		protected override object RuleStmUsingQualifiednameSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using QualifiedName ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= using QualifiedName from StringLiteral ';'
		protected override object RuleStmUsingQualifiednameFromStringliteralSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using QualifiedName from StringLiteral ';'");
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

		// <Stm> ::= using QualifiedName as ID from StringLiteral ';'
		protected override object RuleStmUsingQualifiednameAsIdFromStringliteralSemi(NonterminalToken token)
		{
			throw new NotImplementedException("<Stm> ::= using QualifiedName as ID from StringLiteral ';'");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Stm> ::= <Normal Stm>
		protected override object RuleStm(NonterminalToken token)
		{
			return CreateObject(token.Tokens[0]);
		}

		// <Then Stm> ::= if '(' <Expr> ')' <Then Stm> else <Then Stm>
		protected override object RuleThenstmIfLparanRparanElse(NonterminalToken token)
		{
			return new IfStatement(
				(IExpression)CreateObject(token.Tokens[2]),
			    (IStatement)CreateObject(token.Tokens[4]),
				(IStatement)CreateObject(token.Tokens[6]));
		}

		// <Then Stm> ::= while '(' <Expr> ')' <Then Stm>
		protected override object RuleThenstmWhileLparanRparan(NonterminalToken token)
		{
			return new WhileStatement(
				(IExpression)CreateObject(token.Tokens[2]),
			    (IStatement)CreateObject(token.Tokens[4]));
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
			return CreateObject(token.Tokens[0]);
		}

		// <Normal Stm> ::= do <Stm> while '(' <Expr> ')'
		protected override object RuleNormalstmDoWhileLparanRparan(NonterminalToken token)
		{
			return new DoWhileStatement(
			    (IStatement)CreateObject(token.Tokens[1]),
				(IExpression)CreateObject(token.Tokens[4]));
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
			return new ReturnStatement((IExpression)CreateObject(token.Tokens[1]));
		}

		// <Normal Stm> ::= ';'
		protected override object RuleNormalstmSemi2(NonterminalToken token)
		{
			return new NoopStatement();
		}

		// <Args> ::= <Expr> ',' <Args>
		protected override object RuleArgsComma(NonterminalToken token)
		{
			List<IExpression> list = (List<IExpression>)CreateObject(token.Tokens[2]);
			list.Insert(0, (IExpression)CreateObject(token.Tokens[0]));
			return list;
		}

		// <Args> ::= <Expr>
		protected override object RuleArgs(NonterminalToken token)
		{
			List<IExpression> list = new List<IExpression>();
			list.Add((IExpression)CreateObject(token.Tokens[0]));
			return list;
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
			return new BlockStatement(true, (List<IStatement>)CreateObject(token.Tokens[1]));
		}

		// <Stm List> ::= <Stm> <Stm List>    -- start symbol
		protected override object RuleStmlist (NonterminalToken token)
		{
			List<IStatement> list = (List<IStatement>)CreateObject(token.Tokens[1]);
			list.Insert(0, Statement(token, 0));
			return list;
		}

		// <Stm List> ::=
		protected override object RuleStmlist2(NonterminalToken token)
		{
			return new List<IStatement>();
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

		// <Expr> ::= <Op If> '=' <Expr>
		protected override object RuleExprEq(NonterminalToken token)
		{
			return new AssignExpression(Expr(token,0), Expr(token,2));
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
			return Expr(token, 0);
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
			return Expr(token, 0);
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
			return Expr(token, 0);
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
			return Expr(token, 0);
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
			return Expr(token, 0);
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
			return Expr(token, 0);
		}

		// <Op In> ::= <Op In> in <Op Add>
		protected override object RuleOpinIn(NonterminalToken token)
		{
			throw new NotImplementedException("<Op In> ::= <Op In> in <Op Add>");
			//CheckRule(token, Symbols);
			//return new
		}

		// <Op In> ::= <Op Add>
		protected override object RuleOpin(NonterminalToken token)
		{
			return Expr(token, 0);
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
			return Expr(token, 0);
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
			return Expr(token, 0);
		}

		// <Op Unary> ::= not <Op Unary>
		protected override object RuleOpunaryNot(NonterminalToken token)
		{
			throw new NotImplementedException("<Op Unary> ::= not <Op Unary>");
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
			return Expr(token, 0);
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
			return Expr(token, 0);
		}

		// <Value> ::= IntLiteral
		protected override object RuleValueIntliteral(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Value> ::= StringLiteral
		protected override object RuleValueStringliteral(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Value> ::= DecimalLiteral
		protected override object RuleValueDecimalliteral(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Value> ::= type ID
		protected override object RuleValueTypeId(NonterminalToken token)
		{
			return Expr(token, 0);
		}

		// <Value> ::= type QualifiedName
		protected override object RuleValueTypeQualifiedname (NonterminalToken token)
		{
			return new TypeLiteral(TText(token, 1));
		}

		// <Value> ::= QualifiedName '(' <Args> ')'
		protected override object RuleValueQualifiednameLparanRparan(NonterminalToken token)
		{
			return new FunctionCall(TText(token,0), (List<IExpression>)CreateObject(token.Tokens[2]));
		}

		// <Value> ::= QualifiedName '(' ')'
		protected override object RuleValueQualifiednameLparanRparan2(NonterminalToken token)
		{
			return new FunctionCall(TText(token,0), null);
		}

		// <Value> ::= ID '(' <Args> ')'
		protected override object RuleValueIdLparanRparan(NonterminalToken token)
		{
			return new FunctionCall(TText(token,0), (List<IExpression>)CreateObject(token.Tokens[2]));
		}

		// <Value> ::= ID '(' ')'
		protected override object RuleValueIdLparanRparan2(NonterminalToken token)
		{
			return new FunctionCall(TText(token,0), null);
		}

		// <Value> ::= ID
		protected override object RuleValueId (NonterminalToken token)
		{
			return new Variable(TText(token, 0));
		}

		// <Value> ::= '(' <Expr> ')'
		protected override object RuleValueLparanRparan (NonterminalToken token)
		{
			return CreateObject(token.Tokens[1]);
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
			return new NullLiteral();
		}

		// <Value> ::= true
		protected override object RuleValueTrue(NonterminalToken token)
		{
			return new BooleanLiteral(false);
		}

		// <Value> ::= false
		protected override object RuleValueFalse(NonterminalToken token)
		{
			return new BooleanLiteral(false);
		}

    }
}
