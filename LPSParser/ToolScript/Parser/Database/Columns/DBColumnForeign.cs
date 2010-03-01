using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnForeign : DBColumnBase
	{
		public string ReferencesTableName { get; private set; }
		public string ReferencesColumnName { get; private set; }

		public DBColumnForeign(string ReferencesTableName)
			: base(typeof(Int64))
		{
			this.ReferencesTableName = ReferencesTableName;
			this.ReferencesColumnName = null;
		}

		public DBColumnForeign(string ReferencesTableName, string ReferencesColumnName)
			: base(typeof(Int64))
		{
			this.ReferencesTableName = ReferencesTableName;
			this.ReferencesColumnName = ReferencesColumnName;
		}

	}
}
