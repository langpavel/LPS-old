using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public class BreakStatement : StatementBase
	{
		public BreakStatement()
		{
		}

		public override void Run(Context context)
		{
			IterationTermination.Break(context);
		}
	}
}
