using System;
using LPS.ToolScript.Parser;
using System.Collections.Generic;

namespace LPS.ToolScript
{
	public class StatementList : List<IStatement>, IStatement
	{
		public StatementList()
		{
		}

		public void Run (IExecutionContext context)
		{
			foreach(IStatement s in this)
			{
				s.Run(context);
			}
		}

		public object RunAsMain(IExecutionContext context)
		{
			try
			{
				foreach(IStatement s in this)
				{
					s.Run(context);
				}
				return SpecialValue.Void;
			}
			catch(IterationTermination info)
			{
				return (info.Reason == TerminationReason.Return) ? info.ReturnValue : null;
			}
			finally
			{
				context.Dispose();
			}
		}

		public object Run(ToolScriptParser parser)
		{
			IExecutionContext context = ExecutionContext.CreateRootContext(parser);
			return RunAsMain(context);
		}
	}
}
