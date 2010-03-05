using System;

namespace LPS.ToolScript.Parser
{
	public class ForStatement : LoopStatementBase
	{
		IExpression initialization;
		IExpression condition;
		IExpression step;

		public ForStatement(IExpression initialization, IExpression condition, IExpression step, IStatement statement)
			: base(statement)

		{
			this.initialization = initialization;
			this.condition = condition;
			this.step = step;
		}

		public override void Run (IExecutionContext context)
		{
			initialization.Run(context);
			while(condition.EvalAsBool(context))
			{
				if(ExecuteSingleIteration(context, true) == TerminationReason.Break)
					return;
				step.Run(context);
			}
		}

	}
}
