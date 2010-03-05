using System;

namespace LPS.ToolScript.Parser
{
	public abstract class LiteralBase : IValue
	{
		public LiteralBase()
		{
		}

		public virtual void Run (IExecutionContext context)
		{
		}

		public abstract object Eval (IExecutionContext context);
		public virtual bool EvalAsBool (IExecutionContext context)
		{
			throw new Exception(String.Format("{0} Nelze přetypovat na boolean", this.GetType().Name));
		}
	}
}
