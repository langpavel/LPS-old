using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public class ContinueStatement : StatementBase
	{
		public ContinueStatement()
		{
		}

		public override void Run(Context context)
		{
			IterationTermination.Continue(context);
		}
	}
}
