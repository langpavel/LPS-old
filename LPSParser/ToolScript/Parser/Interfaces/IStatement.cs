using System;

namespace LPS.ToolScript.Parser
{
	public interface IStatement
	{
		void Run(Context context);
	}
}
