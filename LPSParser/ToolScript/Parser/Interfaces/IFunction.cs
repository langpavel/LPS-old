using System;

namespace LPS.ToolScript.Parser
{
	public interface IFunction
	{
		object Execute(NamedArgumentList arguments);
	}
}
