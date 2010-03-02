using System;

namespace LPS.ToolScript.Parser
{
	public abstract class DBColumnDateTimeBase : DBColumnBase
	{
		public virtual bool HasDatePart { get { return true; } }
		public virtual bool HasTimePart { get { return true; } }

		public DBColumnDateTimeBase()
			:base(typeof(DateTime))
		{
		}

		protected override string GetDBTypeName ()
		{
			if(HasDatePart && HasTimePart)
				return "datetime";
			else if(HasDatePart)
				return "date";
			else
				return "time";
		}

	}
}
