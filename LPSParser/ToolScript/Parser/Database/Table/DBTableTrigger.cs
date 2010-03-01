using System;
using System.Data;

namespace LPS.ToolScript.Parser
{
	public class DBTableTrigger: IDBTableTrigger
	{
		public DBTriggerPosition Actions { get; private set; }
		public Decimal Position { get; private set; }
		public IStatement Statement { get; private set; }

		public DBTableTrigger(DBTriggerPosition Actions, Decimal Position, IStatement Statement)
		{
			this.Actions = Actions;
			this.Position = Position;
			this.Statement = Statement;
		}

		public void Execute(DBTriggerPosition action, IDatabaseSchema database, IDBTable table, string query, DataRow row)
		{
			throw new NotImplementedException();
		}

	}
}
