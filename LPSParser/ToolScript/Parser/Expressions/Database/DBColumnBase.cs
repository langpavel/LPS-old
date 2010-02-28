using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnBase : ExpressionBase
	{
		public DBColumnBase()
		{
		}

		public override object Eval (Context context)
		{
			return this;
		}

		public override bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException("Nelze vyhodnocovat odkaz na databázový sloupec jako boolean");
		}

	}
}
