using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class TryBlockStatement : StatementBase
	{
		public IStatement TryStatement { get; private set; }
		public List<CatchStatement> Catches { get; private set; }
		public IStatement FinallyStatement { get; private set; }

		public TryBlockStatement(IStatement TryStatement, List<CatchStatement> Catches, IStatement FinallyStatement)
		{
			this.TryStatement = TryStatement;
			this.Catches = Catches;
			this.FinallyStatement = FinallyStatement;
		}

		public override void Run (IExecutionContext context)
		{
			try
			{
				TryStatement.Run(context);
			}
			catch(Exception ex)
			{
				if(Catches != null)
				{
					foreach(CatchStatement Catch in Catches)
					{
						if(Catch.Handle(context, ex))
							return;
					}
				}
				throw ex;
			}
			finally
			{
				if(FinallyStatement != null)
					FinallyStatement.Run(context);
			}
		}

	}
}
