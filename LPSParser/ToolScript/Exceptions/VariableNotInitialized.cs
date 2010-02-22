using System;

namespace LPS.ToolScript
{
	[Serializable]
	public class VariableNotInitialized : Exception
	{
		public VariableNotInitialized(string VariableName)
			:base(String.Format("Proměnná {0} nebyla inicializována", VariableName))
		{
		}
	}
}
