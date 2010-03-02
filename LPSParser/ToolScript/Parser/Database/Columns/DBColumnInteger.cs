using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnInteger : DBColumnBase
	{
		public DBColumnInteger()
			: base(typeof(Int64))
		{
		}

		protected override string GetDBTypeName ()
		{
			return "bigint";
		}
	}
}
