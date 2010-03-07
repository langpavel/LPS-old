using System;

namespace LPS.ToolScript.Parser
{
	public interface IFunction
	{
		object Execute(NamedArgumentList arguments);
		Delegate GetEventHandler(Type EventHandlerType, IExecutionContext CustomContext);
	}
}
