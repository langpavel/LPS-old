using System;

namespace LPS.ToolScript.Parser
{
	public interface IWidgetBuilder : IExpression
	{
		T GetAttribute<T>(string name, T default_value);
		Gtk.Widget Build();
	}
}
