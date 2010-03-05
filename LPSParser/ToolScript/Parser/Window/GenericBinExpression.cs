using System;

namespace LPS.ToolScript.Parser
{
	public class GenericBinExpression : SingleWidgetContainerBase
	{
		public Type BinType { get; private set; }
		public GenericBinExpression(string Name, EvaluatedAttributeList Params, IWidgetBuilder Child, Type BinType)
			: base(Name, Params, Child)
		{
			this.BinType = BinType;
		}

		public virtual Gtk.Bin CreateBinWidget(WindowContext context)
		{
			return (Gtk.Bin)Activator.CreateInstance(this.BinType);
		}

		protected override Gtk.Widget CreateWidget(WindowContext context)
		{
			Gtk.Bin bin = CreateBinWidget(context);
			if(Child != null)
			{
				bin.Child = Child.Build(context);
			}
			return bin;
		}
	}
}
