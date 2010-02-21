using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public class ReturnStatement : StatementBase, IStatement
	{
		private IExpression Expression;
		public ReturnStatement(IExpression expr)
		{
			this.Expression = expr;
		}

		public override void Run(Context context)
		{
			IterationTermination.Return(context, Expression.Eval(context));
		}
	}
}
