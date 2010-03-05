using System;
using LPS.ToolScript.Parser;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class NewExpression : ExpressionBase
	{
		public QualifiedName TypeName { get; private set; }
		public NamedArgumentList Arguments { get; private set; }
		public NewExpression(QualifiedName TypeName, NamedArgumentList Arguments)
		{
			this.TypeName = TypeName;
			this.Arguments = Arguments;
		}

		public override object Eval (IExecutionContext context)
		{
			Arguments.Run(context);

			string typename = TypeName.ToString();

			Type t = Type.GetType(typename);
			if(t == null)
				throw new Exception("Typ " + TypeName.ToString() + " nebyl nalezen");

			return Activator.CreateInstance(t, Arguments.ValuesToArray());
		}
	}
}
