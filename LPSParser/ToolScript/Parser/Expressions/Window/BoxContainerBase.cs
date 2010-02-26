using System;

namespace LPS.ToolScript.Parser
{
	public abstract class BoxContainerBase : ContainerWidgetBase
	{
		public BoxContainerBase(string Name, WidgetParamList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		protected abstract Gtk.Box CreateBoxWidget();

		protected override Gtk.Widget CreateWidget()
		{
			Gtk.Box box = CreateBoxWidget();
			foreach(IWidgetBuilder builder in Childs)
			{
				if(builder.GetAttribute<bool>("packend", false))
					box.PackEnd(builder.Build(),
						builder.GetAttribute<bool>("expand", true),
						builder.GetAttribute<bool>("fill", true),
						builder.GetAttribute<uint>("padding", 0));
				else
					box.PackStart(builder.Build(),
						builder.GetAttribute<bool>("expand", true),
						builder.GetAttribute<bool>("fill", true),
						builder.GetAttribute<uint>("padding", 0));
			}
			return box;
		}

	}
}
