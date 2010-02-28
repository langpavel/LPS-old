using System;

namespace LPS.ToolScript.Parser
{
	public interface IDBTable
	{
		IDBColumn[] Columns { get; }
	}
}
