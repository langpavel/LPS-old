using System;
using com.calitha.goldparser;
using System.Collections.Generic;
using System.Collections;

namespace LPS.ToolScript.Parser
{
	public sealed class IfStatement : StatementBase, IStatement
	{
		private IExpression IfExpression;
		private IStatement ThenStatement;
		private IStatement ElseStatement;

		public IfStatement(IExpression IfExpression, IStatement ThenStatement, IStatement ElseStatement)
		{
			if(IfExpression is IConstantValue)
				Log.Warning("Podmínka má vždy stejnou hodnotu");

			this.IfExpression = IfExpression;
			this.ThenStatement = ThenStatement;
			this.ElseStatement = ElseStatement;
		}

		public override void Run(Context context)
		{
			if(IfExpression.EvalAsBool(context))
			{
				if(ThenStatement != null) ThenStatement.Run(context);
			}
			else
			{
				if(ElseStatement != null) ElseStatement.Run(context);
			}
		}
	}
}