using System;

namespace LPS.ToolScript.Parser
{
	public abstract class DBColumnRangeBase : DBColumnBase
	{
		public override bool IsAbstract { get { return true; } }

		public Type RangeDBColumnType { get; protected set; }
		public DBColumnBase LowColumn { get; protected set; }
		public DBColumnBase HighColumn { get; protected set; }

		public DBColumnRangeBase(Type ValueType, Type RangeDBColumnType)
			: base(ValueType)
		{
			this.RangeDBColumnType = RangeDBColumnType;
			this.LowColumn = (DBColumnBase)Activator.CreateInstance(RangeDBColumnType);
			this.HighColumn = (DBColumnBase)Activator.CreateInstance(RangeDBColumnType);
		}

		public override string Name
		{
			get { return base.Name; }
			set
			{
				base.Name = value;
				this.LowColumn.Name = value + "_min";
				this.HighColumn.Name = value + "_max";
			}
		}

		public override string[] CreateColumnsSQL (bool in_table)
		{
			return new string[] {
				LowColumn.CreateSQL(in_table),
				HighColumn.CreateSQL(in_table)
			};
		}
	}
}
