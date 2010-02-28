using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnVarchar : DBColumnBase
	{
		public int MaxLength { get; set; }

		public DBColumnVarchar()
			: base(typeof(String))
		{
			this.MaxLength = int.MaxValue;
			this.IsAsbtract = false;
			this.IsPrimary = false;
			this.IsUnique = false;
			this.IsNotNull = false;
		}

	}
}
