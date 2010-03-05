using System;
using LPS.ToolScript.Parser;
using System.Collections.Generic;
using System.Reflection;

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

			Type t = null;
			foreach(Assembly a in AppDomain.CurrentDomain.GetAssemblies())
			{
				t = a.GetType(typename, false);
				if(t != null)
					break;
			}
			if(t == null)
				throw new Exception("Typ '" + typename + "' nebyl nalezen");

			return Activator.CreateInstance(t, Arguments.ValuesToArray());
		}
	}
}
