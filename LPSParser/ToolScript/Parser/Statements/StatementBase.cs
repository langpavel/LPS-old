using System;

namespace LPS.ToolScript.Parser
{
	public abstract class StatementBase : IStatement
	{
		public StatementBase()
		{
		}

		public abstract void Run(Context context);
	}
}
