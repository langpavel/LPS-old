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
		public List<ParamDeclStatement> Parameters { get; private set; }
		public IStatement Body { get; private set; }
		public FunctionExpression(string name, List<ParamDeclStatement> parameters, IStatement body)
		{
			this.Name = name;
			this.Parameters = parameters;
			this.Body = body;
		}

		public override object Eval (Context context)
		{
			foreach(ParamDeclStatement p in Parameters)
				p.Run(context);
			if(this.Name != null)
				context.InitVariable(this.Name, this);
			return this;
		}

		/*
		public int CheckNamedArguments(object[] argumentValues)
		{
			int nonamed_count = 0;
			bool wasNamed = false;
			foreach(object o in argumentValues)
			{
				if(o is NamedArgument
			}
		}
		*/

		public object Execute(Context context, object[] argumentValues)
		{
			//Dictionary<string, Variable> vars = new Dictionary<string, Variable>();
			using(Context child_context = context.CreateChildContext())
			{
				try
				{
					for(int i = 0; i < Parameters.Count; i++)
					{
						ParamDeclStatement param = Parameters[i];
						Variable variable = new Variable(param.Name, true);
						//variable.Run(child_context);
						object val = (i < argumentValues.Length) ? argumentValues[i] : param.DefaultValue;
						variable.AssignValue(child_context, val);
					}
					this.Body.Run(child_context);
					return SpecialValue.Void;
				}
				catch(IterationTermination info)
				{
					if(info.Reason == TerminationReason.Return)
						return info.ReturnValue;
					throw new InvalidOperationException("Invalid: " + info.Reason.ToString());
				}
			}
		}

	}
}
