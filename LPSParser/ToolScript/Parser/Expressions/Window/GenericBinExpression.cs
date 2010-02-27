using System;

namespace LPS.ToolScript.Parser
{
	public class GenericBinExpression : SingleWidgetContainerBase
	{
		public Type BinType { get; private set; }
		public GenericBinExpression(string Name, WidgetParamList Params, IWidgetBuilder Child, Type BinType)
			: base(Name, Params, Child)
		{
			this.BinType = BinType;
		}

		public virtual Gtk.Bin CreateBinWidget()
		{
			return (Gtk.Bin)Activator.CreateInstance(this.BinType);
		}

		protected override Gtk.Widget CreateWidget()
		{
			Gtk.Bin bin = CreateBinWidget();
			if(Child != null)
			{
				bin.Child = Child.Build();
			}
			return bin;
		}
	}
}
