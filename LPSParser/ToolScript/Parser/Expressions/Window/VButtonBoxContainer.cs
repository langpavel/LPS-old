using System;

namespace LPS.ToolScript.Parser
{
	public class VButtonBoxContainer : BoxContainerBase
	{
		public VButtonBoxContainer(string Name, EvaluatedAttributeList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected override Gtk.Box CreateBoxWidget ()
		{
			return new Gtk.VButtonBox();
		}

	}
}
