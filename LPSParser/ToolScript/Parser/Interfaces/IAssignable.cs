using System;

namespace LPS.ToolScript.Parser
{
	public interface IAssignable : IValue
	{
		void AssignValue(Context context, object val);
	}
}
