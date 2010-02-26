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

		public override object Eval (Context context)
		{
			return Activator.CreateInstance(this.ObjectType);
		}

	}
}
