using System;

namespace LPS.ToolScript.Parser
{
	public class TreeViewColumnStatement : StatementBase
	{
		public EvaluatedAttributeList Params { get; private set; }

		public TreeViewColumnStatement(EvaluatedAttributeList Params)
		{
			this.Params = Params;
		}

		public override void Run (IExecutionContext context)
		{
			Params.Run(context);
		}

	}
}
