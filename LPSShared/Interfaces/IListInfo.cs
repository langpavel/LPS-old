using System;
using System.Collections.Generic;

namespace LPS
{
	public enum ListInfoKind
	{
		Table,
		Module
	}
	
	public interface IListInfo
	{
		ListInfoKind Kind { get; }
		string Id { get; }
		string TableName { get; }
		string ListSql { get; }
		string DetailName { get; }
		List<ColumnInfo> Columns { get; }
		ColumnInfo GetColumnInfo(string name);
	}
}
