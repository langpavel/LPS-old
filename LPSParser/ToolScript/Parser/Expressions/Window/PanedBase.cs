using System;

namespace LPS.ToolScript.Parser
{
	public abstract class PanedBase : ContainerWidgetBase
	{
		public PanedBase(string Name, EvaluatedAttributeList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected abstract Gtk.Paned CreatePanedWidget();

		protected override Gtk.Widget CreateWidget()
		{
			if(Childs.Count != 2)
				throw new Exception("Paned widget musí mít právě dva prvky");

			Gtk.Paned paned = CreatePanedWidget();
			paned.Add1(Childs[0].Build());
			paned.Add2(Childs[1].Build());
			return paned;
		}

	}
}
