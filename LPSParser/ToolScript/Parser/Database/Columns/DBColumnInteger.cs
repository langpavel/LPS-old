using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnInteger : DBColumnBase
	{
		public DBColumnInteger()
			: base(typeof(Int64))
		{
			this.IsAsbtract = false;
			this.IsPrimary = false;
			this.IsUnique = false;
			this.IsNotNull = false;
		}

	}
}
