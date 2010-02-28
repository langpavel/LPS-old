using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnTimeRange : DBColumnDateTimeRangeBase
	{
		public DBColumnTimeRange()
			: base(typeof(DBColumnTime))
		{
		}
	}
}
