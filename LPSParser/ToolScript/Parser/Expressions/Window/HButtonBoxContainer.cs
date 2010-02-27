using System;

namespace LPS.ToolScript.Parser
{
	public class HButtonBoxContainer : BoxContainerBase
	{
		public HButtonBoxContainer(string Name, WidgetParamList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected override Gtk.Box CreateBoxWidget ()
		{
			return new Gtk.HButtonBox();
		}

	}
}
