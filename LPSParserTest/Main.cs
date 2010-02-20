using System;

namespace LPS.ToolScript.Test
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			ToolScriptParser p = new ToolScriptParser();

			p.Parse("funkce(parametr, 1, 2.2, 'Ahojda');");

		}
	}
}
