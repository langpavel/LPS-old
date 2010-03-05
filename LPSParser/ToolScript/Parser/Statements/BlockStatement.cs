using System;
using System.Collections.Generic;
using System.Collections;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public class BlockStatement: StatementBase
	{
		public bool CreateChildContext { get; set; }
		public StatementList Statements { get; private set; }
		public BlockStatement(bool CreateChildContext, StatementList Statements)
		{
			this.CreateChildContext = CreateChildContext;
			this.Statements = Statements ?? new StatementList();
		}

		public override void Run (IExecutionContext context)
		{
			IExecutionContext child_context =
				CreateChildContext ? context.CreateChildContext() : context;
			try
			{
				Statements.Run(child_context);
			}
			finally
			{
				if(CreateChildContext)
					child_context.Dispose();
			}
		}
	}
}
