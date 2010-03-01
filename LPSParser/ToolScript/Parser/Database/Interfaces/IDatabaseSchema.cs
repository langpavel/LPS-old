using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public interface IDatabaseSchema: IDBSchemaItem, IDictionary<string, IDBTable>
	{
	}
}
