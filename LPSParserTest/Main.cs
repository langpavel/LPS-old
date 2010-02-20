using System;
using LPS.ToolScript.Tokens;

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

		public static void Main(string[] args)
		{
			ToolScriptParser p = new ToolScriptParser();

			Console.WriteLine("Parsing:");
			object result = p.Parse("\"Ahojda!\"; // funkce(parametr, 1, 2.2, 'Ahojda');");
			//Console.WriteLine("Executing:");
			WriteVar(result);
		}
	}
}
