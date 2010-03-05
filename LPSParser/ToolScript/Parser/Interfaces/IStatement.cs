using System;

namespace LPS.ToolScript.Parser
{
	public interface IStatement
	{
		void Run(IExecutionContext context);
	}
}
