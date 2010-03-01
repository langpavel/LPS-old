using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnManyToMany : DBColumnBase
	{
		public override bool IsAsbtract { get { return true; } }

		public string ReferencesTableName { get; private set; }
		public string ThroughTableName { get; private set; }
		public string ThroughColumnNameThis { get; private set; }
		public string ThroughColumnNameThat { get; private set; }

		public DBColumnManyToMany(
			string ReferencesTableName,
			string ThroughTableName,
			string ThroughColumnNameThis,
			string ThroughColumnNameThat)
			: base(typeof(Int64))
		{
			this.ReferencesTableName = ReferencesTableName;
			this.ThroughTableName = ThroughTableName;
			this.ThroughColumnNameThis = ThroughColumnNameThis;
			this.ThroughColumnNameThat = ThroughColumnNameThat;
		}
	}
}
