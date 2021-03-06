using System;

namespace LPS.ToolScript.Parser
{
	public interface IWidgetBuilder : IExpression
	{
		bool HasAttribute(string name);
		T GetAttribute<T>(string name);
		T GetAttribute<T>(string name, T default_value);
		bool TryGetAttribute<T>(string name, out T value);
		bool TryGetAttribute(Type type, string name, out object value);
		Gtk.Widget Build(WindowContext context);
	}
}
