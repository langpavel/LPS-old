using System;

namespace LPS.ToolScript.Parser
{
	public class HButtonBoxContainer : BoxContainerBase
	{
		public HButtonBoxContainer(string Name, EvaluatedAttributeList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected override Gtk.Box CreateBoxWidget ()
		{
			Gtk.HButtonBox box = new Gtk.HButtonBox();
			box.Layout = Gtk.ButtonBoxStyle.Spread;
			return box;
		}

	}
}
