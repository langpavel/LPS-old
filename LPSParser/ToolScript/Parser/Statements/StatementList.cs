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

		public void Run (Context context)
		{
			foreach(IStatement s in this)
			{
				s.Run(context);
			}
		}

		public object Run()
		{
			Context context = Context.CreateRootContext();
			try
			{
				foreach(IStatement s in this)
				{
					s.Run(context);
				}
				return null;
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
	}
}
