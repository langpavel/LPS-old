using System;

namespace LPS.ToolScript.Parser
{
	public class HBoxContainer : BoxContainerBase
	{
		public HBoxContainer(string Name, EvaluatedAttributeList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected override Gtk.Box CreateBoxWidget()
		{
			return new Gtk.HBox(
				Params.Get<bool>("homogeneous", false),
				Params.Get<int>("spacing", 0));
		}
	}
}
