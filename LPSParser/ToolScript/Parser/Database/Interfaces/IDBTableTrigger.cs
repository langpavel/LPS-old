using System;
using System.Data;

namespace LPS.ToolScript.Parser
{
	public interface IDBTableTrigger
	{
		DBTriggerPosition Actions { get; }
		Decimal Position { get; }
		void Execute(DBTriggerPosition action, IDatabaseSchema database, IDBTable table, string query, DataRow row);
	}
}
