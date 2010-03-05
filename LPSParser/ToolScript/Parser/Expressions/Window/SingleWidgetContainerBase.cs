using System;

namespace LPS.ToolScript.Parser
{
	public abstract class SingleWidgetContainerBase : WidgetBase
	{
		public IWidgetBuilder Child { get; private set; }

		public SingleWidgetContainerBase(string Name, EvaluatedAttributeList Params, IWidgetBuilder Child)
			:base(Name, Params)
		{
			this.Child = Child;
		}

		public override object Eval (IExecutionContext context)
		{
			object result = base.Eval(context);
			if(this.Child != null)
				this.Child.Eval(context);
			return result;
		}
	}
}
