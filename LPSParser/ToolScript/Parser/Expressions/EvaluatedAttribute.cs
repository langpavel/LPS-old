using System;

namespace LPS.ToolScript.Parser
{
	public class EvaluatedAttribute : ExpressionBase, ICloneable
	{
		public IExpression Expression { get; private set; }
		public object Value  { get; private set; }

		public EvaluatedAttribute(IExpression Expression)
		{
			this.Expression = Expression;
			this.Value = null;
		}

		public override object Eval (Context context)
		{
			this.Value = this.Expression.Eval(context);
			return this;
		}

		public override bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException();
		}

		public virtual EvaluatedAttribute Clone()
		{
			return (EvaluatedAttribute)this.MemberwiseClone();
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
