using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnTime : DBColumnDateTimeBase
	{
		public override bool HasTimePart { get { return false; } }

		public DBColumnTime()
		{
		}
	}
}
