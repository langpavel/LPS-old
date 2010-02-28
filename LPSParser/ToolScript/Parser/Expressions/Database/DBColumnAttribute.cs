using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnAttribute : ExpressionBase
	{
		public string Name { get; private set; }
		public IExpression Expr { get; private set; }
		public object Value { get; private set; }

		public DBColumnAttribute(string Name, IExpression Expr)
		{
		}

		public override object Eval (Context context)
		{
			this.Value = this.Expr.Eval(context);
		}

	}
}
