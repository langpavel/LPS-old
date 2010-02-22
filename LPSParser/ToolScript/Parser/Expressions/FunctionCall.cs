using System;
using LPS.ToolScript.Parser;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class FunctionCall : ExpressionBase
	{
		protected string Name { get; private set; }
		protected List<IExpression> Args { get; private set; }

		public FunctionCall(string Name, List<IExpression> Args)
		{
			this.Name = Name;
			this.Args = Args; // can be null
		}

		public object[] EvalArguments(Context context)
		{
			if(Args == null || Args.Count == 0)
				return new object[] { };
			object[] result = new object[Args.Count];
			for(int i = 0; i < Args.Count; i++)
			{
				result[i] = Args[i].Eval(context);
			}
			return result;
		}

		public override object Eval (Context context)
		{
			return this.Execute(context, EvalArguments(context));
		}

		public virtual object Execute(Context context, object[] args)
		{
			return context.FunctionCall(Name, args);
		}
	}
}
