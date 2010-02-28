using System;

namespace LPS.ToolScript.Parser
{
	public interface IDBSchemaItem : IExpression, ICloneable
	{
		string Name { get; }
	}
}
