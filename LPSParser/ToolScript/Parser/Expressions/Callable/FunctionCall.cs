using System;
using LPS.ToolScript.Parser;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class FunctionCall : ExpressionBase
	{
		public IExpression Function { get; private set; }
		public NamedArgumentList Args { get; private set; }

		public FunctionCall(IExpression Function, NamedArgumentList Args)
		{
			this.Function = Function;
			this.Args = Args; // can be null
		}

		public override object Eval (Context context)
		{
			Args.Run(context);
			object func = Function.Eval(context);
			if(func == null)
			{
				throw new InvalidOperationException("Nelze volat metodu na null hodnotě");
			}
			if(func is IFunction)
			{
				return ((IFunction)func).Execute(context, Args);
			}
			else
			{
				Type t = func.GetType();
				throw new InvalidOperationException(
					String.Format("Nelze volat metodu na hodnotě typu {0}", t));
			}
		}
	}
}
