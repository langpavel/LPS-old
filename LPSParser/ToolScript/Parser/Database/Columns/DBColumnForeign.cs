using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class DBColumnForeign : DBColumnBase, IDBColumnForeign
	{
		public string ReferencesTableName { get; private set; }
		public string ReferencesColumnName { get; private set; }

		public IDBTable ReferencesTable { get; private set; }
		public IDBColumn ReferencesColumn { get; private set; }

		public DBColumnForeign(string ReferencesTableName)
			: base(typeof(Int64))
		{
			this.ReferencesTableName = ReferencesTableName;
			this.ReferencesColumnName = null;
		}

		public DBColumnForeign(string ReferencesTableName, string ReferencesColumnName)
			: base(typeof(Int64))
		{
			this.ReferencesTableName = ReferencesTableName;
			this.ReferencesColumnName = ReferencesColumnName;
		}

		public override void Resolve (IDatabaseSchema database, IDBTable table)
		{
			base.Resolve (database, table);
			try
			{
				ReferencesTable = database[ReferencesTableName];
			}
			catch(KeyNotFoundException ex)
			{
				throw new Exception("Tabulka "+ReferencesTableName+" nebyla nalezena", ex);
			}
			if(ReferencesColumnName != null)
				ReferencesColumn = ReferencesTable[ReferencesColumnName];
			else
				ReferencesColumn = ReferencesTable.PrimaryKey;
		}

		protected override string GetDBTypeName ()
		{
			return "bigint";
		}
	}
}
