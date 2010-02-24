using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	/// <summary>
	/// This is not function call,
	/// this is function declaration % definition
	/// </summary>
	public class FunctionExpression: ExpressionBase, IFunction
	{
		/// <summary>
		/// can be null
		/// </summary>
		public string Name { get; private set; }
		public NamedArgumentList Parameters { get; private set; }
		public IStatement Body { get; private set; }
		public FunctionExpression(string name, NamedArgumentList parameters, IStatement body)
		{
			this.Name = name;
			this.Parameters = parameters;
			this.Body = body;
		}

		public override object Eval (Context context)
		{
			Parameters.Run(context);

			if(this.Name != null)
				context.InitVariable(this.Name, this);

			return this;
		}

		public object Execute(Context context, NamedArgumentList arguments)
		{
			context = context.CreateChildContext();
			try
			{
				Parameters.InitVariables(context, arguments);
				Body.Run(context);
				return SpecialValue.Void;
			}
			catch(IterationTermination info)
			{
				if(info.Reason == TerminationReason.Return)
					return info.ReturnValue;
				else
					throw new InvalidOperationException("volání funkce bylo přerušeno jiným důvodem než return: " + info.Reason.ToString());
			}
			finally
			{
				context.Dispose();
			}
		}

		public override string ToString ()
		{
			return string.Format("function({0})", Parameters.ToString());
		}
	}
}
