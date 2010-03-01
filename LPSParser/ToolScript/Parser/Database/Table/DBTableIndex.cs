using System;

namespace LPS.ToolScript.Parser
{
	public class DBTableIndex : ICloneable
	{
		public string[] ColumnNames { get; private set; }
		public bool IsUnique { get; private set; }

		public DBTableIndex(bool IsUnique, string[] ColumnNames)
		{
			this.IsUnique = IsUnique;
			this.ColumnNames = ColumnNames;
		}

		public DBTableIndex Clone()
		{
			DBTableIndex clone = (DBTableIndex)this.MemberwiseClone();
			return clone;
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
