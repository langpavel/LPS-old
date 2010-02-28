using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnPrimary : DBColumnBase
	{
		public DBColumnPrimary()
			: base(typeof(Int64))
		{
			this.IsAsbtract = false;
			this.IsPrimary = true;
			this.IsUnique = true;
			this.IsNotNull = true;
		}
	}
}
