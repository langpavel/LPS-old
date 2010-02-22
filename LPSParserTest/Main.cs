using System;
using LPS.ToolScript.Parser;
using System.Collections.Generic;

namespace LPS.ToolScript.Test
{
	class MainClass
	{
		ToolScriptParser parser = new ToolScriptParser();

		public static void Main(string[] args)
		{
			(new MainClass()).Run(args);
		}

		public static void WriteVar(object o)
		{
			if(o == null)
			{
				Console.WriteLine("(null)");
				return;
			}

			Console.WriteLine("{0}: '{1}'", o.GetType(), o);
			if(o is Array)
				foreach(object x in (Array)o)
					WriteVar(x);

		}

		public void Test(string code)
		{
			try
			{
				Console.WriteLine("============================================================");
				Console.WriteLine("Test: {0}", code);
				StatementList result = parser.Parse(code);
				object o = result.Run(parser);
				if(o != null)
					Console.WriteLine("Result: {0}: {1}", o.GetType().Name, o);
				else
					Console.WriteLine("No result");

				/*
				foreach(IStatement statement in result)
				{
					Console.WriteLine(statement.GetType());
					if(statement is IExpression)
						Console.WriteLine(((IExpression)statement).Eval(context));
					else
						statement.Run(context);
				}
				*/
			}
			catch(Exception err)
			{
				Console.WriteLine(err);
			}
		}

		public void Run(string[] args)
		{
			Log.Add(new TextLogger(Console.Out, Verbosity.Debug));

			//Test("\"Ahojda!\";");
			//Test("\"Druhý text ěščřžýáíé!\";");
			//Test("1234.56789;");
			//Test("55667; \"Ahojda\"; null; true; false;");
			//Test("promenna = 123; promenna;");
			//Test("Format(\"Ahojda {0}\", \"svete!\");");
			//Test("if(null) 123; else 456;");
			//Test(@"for(var i=1;i<3;i++) for(var j=1;j<3;j++) { Print('Iterace {0}:{1}', i+1, j+1); }");

			Test(@"return 'Return works?';");
			Test(@"if(true) return 'If works?';");
			Test(@"if(false) return 'Oops'; return 'If - else works?';");
			//Test(@"if(1==2) return 'Error'; else return 'OK';");
			//Test(@"if(1>2) return 'Error'; else return 'OK';");
			//Test(@"if(1>=2) return 'Error'; else return 'OK';");
			//Test(@"if(2<1) return 'Error'; else return 'OK';");
			//Test(@"if(2<=1) return 'Error'; else return 'OK';");

/*
			Test(@"<Stm> ::= foreach '(' ID in <Expr> ')' <Stm>");
			Test(@"<Stm> ::= observed '(' <Expr> ')' <Stm>");
			Test(@"<Stm> ::= using QualifiedName ';'");
			Test(@"<Stm> ::= using QualifiedName from StringLiteral ';'");
			Test(@"<Stm> ::= using QualifiedName as ID ';'");
			Test(@"<Stm> ::= using QualifiedName as ID from StringLiteral ';'");
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
			Test(@"<Value> ::= QualifiedName '(' <Args> ')'");
			Test(@"<Value> ::= QualifiedName '(' ')'");
			Test(@"<Value> ::= ID '(' <Args> ')'");
			Test(@"<Value> ::= ID '(' ')'");
			Test(@"<Value> ::= ID");
			Test(@"<Value> ::= '(' <Expr> ')'");
			Test(@"<Value> ::= '{' <Expr List> '}'");
			Test(@"<Value> ::= null");
			Test(@"<Value> ::= true");
			Test(@"<Value> ::= false");
*/
		}
	}
}
