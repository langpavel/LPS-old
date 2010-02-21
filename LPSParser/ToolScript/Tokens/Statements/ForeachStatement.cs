using System;
using System.Collections;

namespace LPS.ToolScript.Tokens
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

		public override void Run (Context context)
		{
			using(Context child_context = context.CreateChildContext())
			{
				foreach(object val in (IEnumerable)enumerable.Eval(child_context))
				{
					variable.AssignValue(child_context, val);
					if(ExecuteSingleIteration(child_context) == TerminationReason.Break)
						break;
				}
			}
		}

	}
}
