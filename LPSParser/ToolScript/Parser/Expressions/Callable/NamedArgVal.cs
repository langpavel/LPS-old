using System;

namespace LPS.ToolScript.Parser
{
	public struct NamedArgVal
	{
		public string Name;
		public object Value;
		public bool IsNamed { get { return ! (Name == null); } }
	}
}
