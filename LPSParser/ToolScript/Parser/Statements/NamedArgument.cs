using System;

namespace LPS.ToolScript.Parser
{
	public class NamedArgument
	{
		public string Name { get; private set; }
		public object Value { get; private set; }

		public NamedArgument(string name, object value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
