using System;

namespace LPS.ToolScript.Parser
{
	public class HPanedContainer : PanedBase
	{
		public HPanedContainer(string Name, EvaluatedAttributeList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected override Gtk.Paned CreatePanedWidget(WindowContext context)
		{
			return new Gtk.HPaned();
		}
	}
}
