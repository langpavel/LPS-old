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
			Gtk.VButtonBox box = new Gtk.VButtonBox();
			box.Layout = Gtk.ButtonBoxStyle.Start;
			return box;
		}

	}
}
