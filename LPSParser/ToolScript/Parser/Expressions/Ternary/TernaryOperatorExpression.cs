using System;

namespace LPS.ToolScript.Parser
{
	public class TernaryOperatorExpression : ExpressionBase
	{
		private IExpression ifexpr;
		private IExpression iftrue;
		private IExpression iffalse;
		public TernaryOperatorExpression(IExpression ifexpr, IExpression iftrue, IExpression iffalse)
		{
			this.ifexpr = ifexpr;
			this.iftrue = iftrue;
			this.iffalse = iffalse;
		}

		public override object Eval (IExecutionContext context)
		{
			if(ifexpr.EvalAsBool(context))
				return iftrue.Eval(context);
			else
				return iffalse.Eval(context);
		}

		public override bool EvalAsBool (IExecutionContext context)
		{
			return base.EvalAsBool (context);
		}
	}
}
