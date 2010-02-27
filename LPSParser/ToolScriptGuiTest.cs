using System;
using LPS.ToolScript.Parser;
using NUnit.Framework;

namespace LPS.ToolScript.Tests
{
	[TestFixture]
	public class ToolScriptGuiTest
	{
		ToolScriptParser parser;

		public ToolScriptGuiTest()
		{
			parser = new ToolScriptParser();
			Log.Add(new TextLogger(Console.Out, Verbosity.Debug));
		}

		public object Run(string prog)
		{
			StatementList result = parser.Parse(prog);
			if(result == null)
				throw new Exception(String.Format("Parse err: {0}", prog));
			Context context = Context.CreateRootContext(parser);
			context.InitVariable("this", this);
			return result.RunAsMain(context);
		}

		[Test]
		public void TestWindow()
		{
			Assert.IsInstanceOfType(
				typeof(WindowExpression),
				Run(@"return
					WINDOW
						VBOX
							['AAA']
							HBOX spacing=5; padding=5; ['BBB'] ['CCC'] packend; ['DDD'] END
							['EEE']
						END;
				"));
		}
	}
}
