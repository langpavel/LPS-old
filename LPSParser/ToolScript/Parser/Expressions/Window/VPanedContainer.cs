using System;

namespace LPS.ToolScript.Parser
{
	public class VPanedContainer : PanedBase
	{
		public VPanedContainer(string Name, EvaluatedAttributeList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected override Gtk.Paned CreatePanedWidget()
		{
			return new Gtk.VPaned();
		}
	}
}
