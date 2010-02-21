using System;

namespace LPS.ToolScript.Tokens
{
	public class WhileStatement : LoopStatementBase
	{
		private IExpression expr;
		public WhileStatement(IExpression expr, IStatement statement)
			: base(statement)
		{
			this.expr = expr;
		}

		public override void Run (Context context)
		{
			while(expr.EvalAsBool(context))
			{
				if(ExecuteSingleIteration(context) == TerminationReason.Break)
					return;
			}
		}
	}
}
