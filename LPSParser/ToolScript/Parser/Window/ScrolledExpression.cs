using System;
using Gtk;
using System.Reflection;
using GLib;

namespace LPS.ToolScript.Parser
{
	public class ScrolledExpression : SingleWidgetContainerBase
	{
		public Type BinType { get; private set; }
		public ScrolledExpression(string Name, EvaluatedAttributeList Params, IWidgetBuilder Child)
			: base(Name, Params, Child)
		{
		}

		private static bool SignalFilter(MemberInfo member, object filter)
		{
			foreach(SignalAttribute signal in member.GetCustomAttributes(typeof(SignalAttribute), true))
			{
				if(signal.CName == (string)filter)
					return true;
			}
			return false;
		}

		public static bool IsNativelyScrolled(Type widgettype)
		{
			MemberInfo[] infos = widgettype.FindMembers(
				MemberTypes.Event,
				BindingFlags.Instance | BindingFlags.Public,
				SignalFilter,
				"set_scroll_adjustment");
			return infos.Length > 0;
		}

		protected override Widget CreateWidget(WindowContext context)
		{
			if(Child == null)
				throw new Exception("Scrolled musí obsahovat widget");
			ScrolledWindow sw = new ScrolledWindow();
			Widget child = Child.Build(context);
			if(IsNativelyScrolled(child.GetType()))
				sw.Add(child);
			else
				sw.AddWithViewport(child);
			return sw;
		}
	}
}
