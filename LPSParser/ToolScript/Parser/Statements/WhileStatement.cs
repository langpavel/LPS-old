using System;

namespace LPS.ToolScript.Parser
{
	public class WhileStatement : LoopStatementBase
	{
		private IExpression expr;
		public WhileStatement(IExpression expr, IStatement statement)
			: base(statement)
		{
			this.expr = expr;
		}

		public override void Run (IExecutionContext context)
		{
			while(expr.EvalAsBool(context))
			{
				if(ExecuteSingleIteration(context, true) == TerminationReason.Break)
					return;
			}
		}
	}
}
