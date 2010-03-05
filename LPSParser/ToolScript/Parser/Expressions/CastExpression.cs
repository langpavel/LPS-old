using System;

namespace LPS.ToolScript.Parser
{
	public class CastExpression : ExpressionBase
	{
		public IExpression Expr { get; private set; }
		public QualifiedName TypeName { get; private set; }
		public CastExpression(IExpression Expr, QualifiedName TypeName)
		{
			this.Expr = Expr;
			this.TypeName = TypeName;
		}

		public override object Eval (IExecutionContext context)
		{
			Type t = Type.GetType(TypeName.ToString());
			return Convert.ChangeType(Expr.Eval(context), t);
		}

	}
}
