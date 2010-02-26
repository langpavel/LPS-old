using System;

namespace LPS.ToolScript.Parser
{
	public class VBoxContainer : BoxContainerBase
	{
		public VBoxContainer(string Name, WidgetParamList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected override Gtk.Box CreateBoxWidget()
		{
			return new Gtk.VBox(
				Params.Get<bool>("homogeneous", false),
				Params.Get<int>("spacing", 0));
		}
	}
}
