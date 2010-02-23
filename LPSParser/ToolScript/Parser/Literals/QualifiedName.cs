using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class QualifiedName
	{
		public List<string> Names {get; private set;}

		public QualifiedName()
		{
			Names = new List<string>();
		}

		public QualifiedName(string name)
		{
			Names = new List<string>();
			Names.Add(name);
		}

		public override string ToString ()
		{
			return string.Join(".", Names.ToArray());
		}
	}
}
