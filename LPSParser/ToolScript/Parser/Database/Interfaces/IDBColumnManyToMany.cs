using System;

namespace LPS.ToolScript.Parser
{
	public interface IDBColumnManyToMany : IDBColumn
	{
		IDBTable ReferencesTable { get; }
		IDBTable ThroughTable { get; }
		IDBColumnForeign ThroughColumnThis { get; }
		IDBColumnForeign ThroughColumnThat { get; }
	}
}
