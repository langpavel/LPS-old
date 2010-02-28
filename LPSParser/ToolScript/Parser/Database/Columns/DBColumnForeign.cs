using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnForeign : DBColumnBase
	{
		public string ReferencesTableName { get; set; }
		public string ReferencesColumnName { get; set; }

		public DBColumnForeign()
			: base(typeof(Int64))
		{
			this.IsAsbtract = false;
			this.IsPrimary = false;
			this.IsUnique = false;
			this.IsNotNull = false;
		}

	}
}
