using System;

namespace LPS.ToolScript.Parser
{
	public interface IDBColumn : IExpression, ICloneable
	{
		//IDBTable Table { get; }
		bool IsPrimary { get; }
		bool IsUnique { get; }
		bool IsNotNull { get; }
		bool IsIndex { get; }
		bool HasAttribute(string name);
		T GetAttribute<T>(string name);
		object GetAttribute(Type type, string name);
	}
}
