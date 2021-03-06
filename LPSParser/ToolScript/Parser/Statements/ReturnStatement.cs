using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public class ReturnStatement : StatementBase, IStatement
	{
		private IExpression Expression;
		public ReturnStatement(IExpression expr)
		{
			this.Expression = expr;
		}

		public override void Run(IExecutionContext context)
		{
			IterationTermination.Return(context, Expression.Eval(context));
		}
	}
}
