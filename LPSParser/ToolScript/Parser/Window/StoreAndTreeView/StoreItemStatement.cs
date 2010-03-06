using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class StoreItemStatement : StatementBase
	{
		public List<IExpression> Expressions { get; private set; }
		public List<StoreItemStatement> Childs { get; private set; }

		public object[] Evaluated {get; private set; }

		public StoreItemStatement(List<IExpression> Expressions, List<StoreItemStatement> Childs)
		{
			this.Expressions = Expressions;
			this.Childs = Childs;
		}

		public override void Run (IExecutionContext context)
		{
			Evaluated = new object[Expressions.Count];
			for(int i=0; i < Expressions.Count; i++)
				Evaluated[i] = Expressions[i].Eval(context);
			if(Childs != null)
				foreach(StoreItemStatement child in Childs)
					child.Run(context);
		}
	}
}
