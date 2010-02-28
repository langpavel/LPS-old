using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnDateTimeRangeBase : DBColumnRangeBase
	{
		public DBColumnDateTimeRangeBase(Type RangeDBColumnType)
			: base(typeof(DateTime), RangeDBColumnType)
		{
		}
	}
}
