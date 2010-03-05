using System;

namespace LPS.ToolScript.Parser
{
	public class UsingStatement : StatementBase
	{
		public string[] Path { get; private set; }

		/// <summary>
		/// Alias or null
		/// </summary>
		public string Alias { get; private set; }

		public UsingStatement(string[] path)
			:this(path, null)
		{
		}

		public UsingStatement(string[] path, string alias)
		{
			this.Path = path;
			this.Alias = alias;
		}

		public override void Run (IExecutionContext context)
		{
			throw new System.NotImplementedException ("Using");
		}

		public override string ToString ()
		{
			return string.Format("using ", Path);
		}
	}
}
