using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnBoolean : DBColumnBase
	{
		public DBColumnBoolean()
			: base(typeof(Boolean))
		{
		}

		protected override string GetDBTypeName ()
		{
			return "bool";
		}

	}
}
