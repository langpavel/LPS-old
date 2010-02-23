using System;
using LPS.ToolScript.Parser;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class NewExpression : FunctionCall
	{
		public NewExpression(IExpression NewType, NamedArgumentList Args)
			: base(NewType, Args)
		{
		}

		public override object Eval (Context context)
		{
			Args.Run(context);
			throw new NotImplementedException("new keyword");
		}
	}
}
