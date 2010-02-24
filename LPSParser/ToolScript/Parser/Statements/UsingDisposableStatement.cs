using System;
using System.Collections.Generic;
using System.Collections;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public class UsingDisposableStatement: IStatement
	{
		public IExpression Expression { get; private set; }
		public IStatement Statement { get; private set; }
		public UsingDisposableStatement(IExpression Expression, IStatement Statement)
		{
			this.Expression = Expression;
			this.Statement = Statement;
		}

		public void Run (Context context)
		{
			IDisposable disposable = Expression.Eval(context) as IDisposable;
			try
			{
				Statement.Run(context);
			}
			finally
			{
				if(disposable != null)
					disposable.Dispose();
			}
		}
	}
}
