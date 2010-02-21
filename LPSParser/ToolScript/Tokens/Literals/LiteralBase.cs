using System;

namespace LPS.ToolScript.Tokens
{
	public abstract class LiteralBase : IValue
	{
		public LiteralBase()
		{
		}

		public virtual void Run (Context context)
		{
		}

		public abstract object Eval (Context context);
		public virtual bool EvalAsBool (Context context)
		{
			throw new Exception(String.Format("{0} Nelze přetypovat na boolean", this.GetType().Name));
		}
	}
}
