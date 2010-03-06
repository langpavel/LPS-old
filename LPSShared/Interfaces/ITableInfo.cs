using System;
using System.Collections.Generic;

namespace LPS
{
	[Obsolete("Use IDBTable")]
	public interface ITableInfo : IListInfo, ICloneable, ILookupInfo
	{
		string Category { get; }
		string DetailCaption { get; }
		string Description { get; }
		string LookupReplaceFormat { get; }
	}
}
