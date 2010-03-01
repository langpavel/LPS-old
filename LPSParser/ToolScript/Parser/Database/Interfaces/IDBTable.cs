using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public interface IDBTable: IDBSchemaItem, IDictionary<string, IDBColumn>
	{
		void Resolve(IDatabaseSchema database);
	}
}
