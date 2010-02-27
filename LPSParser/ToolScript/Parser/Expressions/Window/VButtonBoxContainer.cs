using System;

namespace LPS.ToolScript.Parser
{
	public class VButtonBoxContainer : BoxContainerBase
	{
		public VButtonBoxContainer(string Name, WidgetParamList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected override Gtk.Box CreateBoxWidget ()
		{
			return new Gtk.VButtonBox();
		}

	}
}
