using System;

namespace LPS.ToolScript.Parser
{
	public class DoWhileStatement : LoopStatementBase
	{
		private IExpression expr;
		public DoWhileStatement(IStatement statement, IExpression expr)
			: base(statement)
		{
			this.expr = expr;
		}

		public override void Run (Context context)
		{
			TerminationReason reason;
			while(true)
			{
				while((reason = ExecuteSingleIteration(context, true)) == TerminationReason.Continue) ;
				if(reason == TerminationReason.Break)
					return;
				if(!expr.EvalAsBool(context))
					return;
			}
		}
	}
}
