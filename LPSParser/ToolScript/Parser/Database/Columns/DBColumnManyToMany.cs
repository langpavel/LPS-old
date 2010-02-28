using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnManyToMany : DBColumnBase
	{
		public DBColumnManyToMany()
			: base(typeof(Int64))
		{
			this.IsAsbtract = true;
			this.IsPrimary = false;
			this.IsUnique = false;
			this.IsNotNull = false;
		}
	}
}
