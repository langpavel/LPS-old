using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public class ContinueStatement : StatementBase
	{
		public ContinueStatement()
		{
		}

		public override void Run(IExecutionContext context)
		{
			IterationTermination.Continue(context);
		}
	}
}
