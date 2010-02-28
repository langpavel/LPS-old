using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnDateTimeRange : DBColumnDateTimeRangeBase
	{
		public DBColumnDateTimeRange()
			: base(typeof(DBColumnDateTime))
		{
		}
	}
}
