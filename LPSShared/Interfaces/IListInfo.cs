using System;
using System.Collections.Generic;

namespace LPS
{
	public enum ListInfoKind
	{
		Table,
		Module
	}

	[Obsolete("Use IDBTable / IDBColumn")]
	public interface IListInfo
	{
		ListInfoKind Kind { get; }
		string Id { get; }
		string TableName { get; }
		string ListSql { get; }
		string DetailName { get; }
		IColumnInfo[] Columns { get; }
		IColumnInfo GetColumnInfo(string name);
	}
}
