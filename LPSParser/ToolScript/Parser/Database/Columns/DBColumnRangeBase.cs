using System;

namespace LPS.ToolScript.Parser
{
	public abstract class DBColumnRangeBase : DBColumnBase
	{
		public Type RangeDBColumnType { get; protected set; }
		public DBColumnBase LowColumn { get; protected set; }
		public DBColumnBase HighColumn { get; protected set; }

		public DBColumnRangeBase(Type ValueType, Type RangeDBColumnType)
			: base(ValueType)
		{
			this.RangeDBColumnType = RangeDBColumnType;
			this.IsAsbtract = true;
		}
	}
}
