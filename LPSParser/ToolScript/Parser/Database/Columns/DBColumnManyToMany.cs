using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnManyToMany : DBColumnBase, IDBColumnManyToMany
	{
		public override bool IsAbstract { get { return true; } }

		public string ReferencesTableName { get; private set; }
		public string ThroughTableName { get; private set; }
		public string ThroughColumnNameThis { get; private set; }
		public string ThroughColumnNameThat { get; private set; }

		public IDBTable ReferencesTable { get; private set; }
		public IDBTable ThroughTable { get; private set; }
		public IDBColumnForeign ThroughColumnThis { get; private set; }
		public IDBColumnForeign ThroughColumnThat { get; private set; }

		public DBColumnManyToMany(
			string ReferencesTableName,
			string ThroughTableName,
			string ThroughColumnNameThis,
			string ThroughColumnNameThat)
			: base(typeof(Int64))
		{
			this.ReferencesTableName = ReferencesTableName;
			this.ThroughTableName = ThroughTableName;
			this.ThroughColumnNameThis = ThroughColumnNameThis;
			this.ThroughColumnNameThat = ThroughColumnNameThat;
		}

		public override void Resolve (IDatabaseSchema database, IDBTable table)
		{
			base.Resolve (database, table);
			ReferencesTable = database[ReferencesTableName];
			ThroughTable = database[ThroughTableName];
			ThroughColumnThis = FindFk(this.Table, ThroughColumnNameThis);
			ThroughColumnThat = FindFk(ReferencesTable, ThroughColumnNameThat);
		}

		private IDBColumnForeign FindFk(IDBTable reftable, string fkname)
		{
			IDBColumnForeign[] fks;
			fks = ThroughTable.FindTiesTo(reftable);
			if(ThroughColumnNameThis == null)
			{
				if(fks.Length != 1)
					throw new Exception("Nebyl nalezen právě jeden klíč");
				return fks[0];
			}
			else
			{
				foreach(IDBColumnForeign fk in fks)
					if(fk.Name == fkname)
						return fk;
				throw new Exception("Nebyl nalezen klíč pro vazbu many to many");
			}
		}

	}
}
