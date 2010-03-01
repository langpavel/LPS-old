using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnPrimary : DBColumnBase
	{
		public override bool IsPrimary { get { return true; } }

		public DBColumnPrimary()
			: base(typeof(Int64))
		{
			this.IsUnique = true;
			this.IsNotNull = true;
		}
	}
}
