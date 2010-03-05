using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public class BreakStatement : StatementBase
	{
		public BreakStatement()
		{
		}

		public override void Run(IExecutionContext context)
		{
			IterationTermination.Break(context);
		}
	}
}
