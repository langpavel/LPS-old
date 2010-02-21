using System;
using LPS.ToolScript.Tokens;
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

		public void Run()
		{
			Context context = Context.CreateRootContext();
			try
			{
				foreach(IStatement s in this)
				{
					s.Run(context);
				}
			}
			finally
			{
				context.Dispose();
			}
		}
	}
}
