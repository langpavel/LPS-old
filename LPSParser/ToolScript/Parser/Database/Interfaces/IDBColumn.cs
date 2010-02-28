using System;

namespace LPS.ToolScript.Parser
{
	public interface IDBColumn : IDBSchemaItem
	{
		IDBTable Table { get; }
		Type DataType { get; }
		bool IsPrimary { get; }
		bool IsUnique { get; }
		bool IsNotNull { get; }
		bool IsIndex { get; }
		bool HasAttribute(string name);
		T GetAttribute<T>(string name);
		object GetAttribute(Type type, string name);

		object NormalizeValue(object value);
		string DisplayValue(object value);
	}
}
