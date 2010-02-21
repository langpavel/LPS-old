using System;

namespace LPS.ToolScript.Tokens
{
	public abstract class StatementBase : IStatement
	{
		public StatementBase()
		{
		}

		public abstract void Run(Context context);
	}
}
