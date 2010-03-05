using System;
using System.Collections;

namespace LPS.ToolScript.Parser
{
	public class ForeachStatement : LoopStatementBase
	{
		private IAssignable variable;
		private IExpression enumerable;
		public ForeachStatement(IAssignable variable, IExpression enumerable, IStatement statement)
			: base(statement)
		{
			this.variable = variable;
			this.enumerable = enumerable;
		}

		public override void Run (IExecutionContext context)
		{
			foreach(object val in (IEnumerable)enumerable.Eval(context))
			{
				using(IExecutionContext child_context = context.CreateChildContext())
				{
					variable.Run(context);
					variable.AssignValue(context, val);
					if(ExecuteSingleIteration(child_context, false) == TerminationReason.Break)
						break;
				}
			}
		}

	}
}
