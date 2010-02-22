using System;

namespace LPS.ToolScript.Parser
{
	public interface IFunction
	{
		object Execute(Context context, object[] argumentValues);
	}
}
