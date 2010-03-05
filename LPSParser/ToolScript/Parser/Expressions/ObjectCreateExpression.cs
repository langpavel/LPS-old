using System;

namespace LPS.ToolScript.Parser
{
	public class ObjectCreateExpression : ExpressionBase
	{
		public Type ObjectType { get; private set; }
		public ObjectCreateExpression(Type ObjectType)
		{
			this.ObjectType = ObjectType;
		}

		public override object Eval (IExecutionContext context)
		{
			return Activator.CreateInstance(this.ObjectType);
		}

	}
}
