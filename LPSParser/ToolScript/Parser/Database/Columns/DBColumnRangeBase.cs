using System;

namespace LPS.ToolScript.Parser
{
	public abstract class DBColumnRangeBase : DBColumnBase
	{
		public override bool IsAsbtract { get { return true; } }

		public Type RangeDBColumnType { get; protected set; }
		public DBColumnBase LowColumn { get; protected set; }
		public DBColumnBase HighColumn { get; protected set; }

		public DBColumnRangeBase(Type ValueType, Type RangeDBColumnType)
			: base(ValueType)
		{
			this.RangeDBColumnType = RangeDBColumnType;
		}
	}
}
