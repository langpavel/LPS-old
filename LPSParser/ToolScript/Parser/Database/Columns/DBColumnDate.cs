using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnDate : DBColumnDateTimeBase
	{
		public override bool HasTimePart { get { return false; } }

		public DBColumnDate()
		{
		}
	}
}
