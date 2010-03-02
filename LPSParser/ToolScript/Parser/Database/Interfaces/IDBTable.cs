using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public interface IDBTable: IDBSchemaItem, IDictionary<string, IDBColumn>
	{
		bool IsTemplate { get; }
		string TemplateName { get; }
		void Resolve(IDatabaseSchema database);
		IDBColumnPrimary PrimaryKey { get; }
		IDBColumnForeign[] FindTiesTo(IDBTable table);
		string CreateTableSQL();
		string CreateForeignKeysSQL();
	}
}
