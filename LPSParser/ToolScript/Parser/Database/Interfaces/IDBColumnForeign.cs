using System;

namespace LPS.ToolScript.Parser
{
	public interface IDBColumnForeign : IDBColumn
	{
		IDBTable ReferencesTable { get; }
		IDBColumn ReferencesColumn { get; }
	}
}
