using System;

namespace LPS.ToolScript
{
	[Serializable]
	public class VariableNotInitialized : VariableException
	{
		public VariableNotInitialized(string VariableName)
			:base(String.Format("Proměnná {0} nebyla inicializována", VariableName))
		{
		}
	}
}
