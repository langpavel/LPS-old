using System;
using LPS.ToolScript.Tokens;
using System.Collections.Generic;

namespace LPS.ToolScript.Test
{
	class MainClass
	{
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

		public static void ParseAndDisplay(string code)
		{
			try
			{
				Console.WriteLine("==============================");
				Console.WriteLine("Parsing: {0}", code);
				ToolScriptParser p = new ToolScriptParser();
				List<IStatement> result = p.Parse(code);
				Context context = Context.CreateRootContext();
				foreach(IStatement statement in result)
				{
					Console.WriteLine(statement.GetType());
					if(statement is IExpression)
						Console.WriteLine(((IExpression)statement).Eval(context));
					else
						statement.Run(context);
				}
			}
			catch(Exception err)
			{
				Console.WriteLine(err);
			}
		}

		public static void Main(string[] args)
		{
			Log.Add(new TextLogger(Console.Out, Verbosity.Debug));

			ParseAndDisplay("\"Ahojda!\";");
			ParseAndDisplay("\"Druhý text ěščřžýáíé!\";");
			ParseAndDisplay("1234.56789;");
			ParseAndDisplay("55667; \"Ahojda\"; null; true; false;");
			ParseAndDisplay("promenna = 123; promenna;");
			ParseAndDisplay("Format(\"Ahojda {0}\", \"svete!\");");
		}
	}
}
