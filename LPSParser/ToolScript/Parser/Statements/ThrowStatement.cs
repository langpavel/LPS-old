using System;

namespace LPS.ToolScript.Parser
{
	public class ThrowStatement: StatementBase
	{
		public IExpression Expression1 { get; private set; }
		public IExpression Expression2 { get; private set; }

		public ThrowStatement(IExpression Expression1, IExpression Expression2)
		{
			this.Expression1 = Expression1;
			this.Expression2 = Expression2;
		}

		public override void Run (Context context)
		{
			object e2 = (Expression2 == null) ? null : Expression2.Eval(context);
			object e1 = (Expression1 == null) ? null : Expression1.Eval(context);
			Exception inner = null;
			if(e2 == null)
				inner = null;
			else if (e2 is Exception)
				inner = (Exception)e2;
			else
				inner = new ScriptCustomException(
					e2,
					(e2 == null) ? "Skript vyvolal vyjímku" : e2.ToString(),
					null);
			throw new ScriptCustomException(
				e1,
				(e1 == null) ? "Skript vyvolal vyjímku" : e1.ToString(),
				inner);
		}
	}
}
