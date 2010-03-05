using System;

namespace LPS.ToolScript.Parser
{
	public interface IAssignable : IValue
	{
		void AssignValue(IExecutionContext context, object val);
	}
}
