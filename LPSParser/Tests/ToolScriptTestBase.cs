using System;

namespace LPS.ToolScript.Tests
{
	public class ToolScriptTestBase
	{
		protected ToolScriptParser parser;

		public ToolScriptTestBase()
		{
			parser = new ToolScriptParser();
			Log.Add(new TextLogger(Console.Out, Verbosity.Debug));
		}

		public object Run(string prog)
		{
			StatementList result = parser.Parse(prog);
			if(result == null)
				throw new Exception(String.Format("Parse err: {0}", prog));
			IExecutionContext context = ExecutionContext.CreateRootContext(parser);
			context.InitVariable("this", this);
			return result.RunAsMain(context);
		}

	}
}
