using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class DBTableIndices : List<DBTableIndex>, ICloneable
	{
		public DBTableIndices()
		{
		}

		public DBTableIndices Clone()
		{
			DBTableIndices clone = new DBTableIndices();
			foreach(DBTableIndex index in this)
				clone.Add(index.Clone());
			return clone;
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
