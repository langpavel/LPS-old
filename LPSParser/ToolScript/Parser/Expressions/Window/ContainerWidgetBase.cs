using System;

namespace LPS.ToolScript.Parser
{
	public abstract class ContainerWidgetBase : WidgetBase
	{
		public LayoutList Childs { get; private set; }

		public ContainerWidgetBase(string Name, EvaluatedAttributeList Params, LayoutList Childs)
			:base(Name, Params)
		{
			this.Childs = Childs;
		}

		public override object Eval (Context context)
		{
			object result = base.Eval(context);
			if(this.Childs != null)
				foreach(IWidgetBuilder child in Childs)
					child.Eval(context);
			return result;
		}
	}
}
