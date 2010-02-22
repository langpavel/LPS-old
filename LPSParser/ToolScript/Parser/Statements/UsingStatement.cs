using System;

namespace LPS.ToolScript.Parser
{
	public class UsingStatement : StatementBase
	{
		/// <summary>
		/// if global:: prefix is present
		/// </summary>
		public bool IsGlobal { get; private set; }

		/// <summary>
		/// for example global::System.Data
		/// </summary>
		public string CompletePath { get; private set; }

		/// <summary>
		/// array of path components without 'global::' prefix
		/// </summary>
		public string[] Path { get; private set; }

		/// <summary>
		/// Alias or null
		/// </summary>
		public string Alias { get; private set; }

		public UsingStatement(string path)
			:this(path, null)
		{
		}

		public UsingStatement(string path, string alias)
		{
			this.CompletePath = path;
			this.IsGlobal = path.StartsWith("global::");
			if(IsGlobal)
				path = path.Substring(9);
			this.Path = path.Split('.');
			this.Alias = alias;
		}

		public override void Run (Context context)
		{
			throw new System.NotImplementedException ("Using");
		}
	}
}
