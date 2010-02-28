using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnDateRange : DBColumnDateTimeRangeBase
	{
		public DBColumnDateRange()
			: base(typeof(DBColumnDate))
		{
		}
	}
}
