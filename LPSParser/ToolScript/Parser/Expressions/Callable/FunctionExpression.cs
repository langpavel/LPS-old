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

		private IExecutionContext Context;

		public override object Eval (IExecutionContext context)
		{
			this.Context = context.CreateChildContext();

			Parameters.Run(context);

			if(this.Name != null)
				context.InitVariable(this.Name, this);

			return this;
		}

		public object Execute(NamedArgumentList arguments)
		{
			try
			{
				Parameters.InitVariables(this.Context, arguments);
				Body.Run(this.Context);
				return SpecialValue.Void;
			}
			catch(IterationTermination info)
			{
				if(info.Reason == TerminationReason.Return)
					return info.ReturnValue;
				else
					throw new InvalidOperationException("volání funkce bylo přerušeno jiným důvodem než return: " + info.Reason.ToString());
			}
		}

		private class EventHandlerHelper
		{
			private FunctionExpression Func;
			private IExecutionContext CustomContext;

			public EventHandlerHelper(FunctionExpression Func, IExecutionContext CustomContext)
			{
				this.Func = Func;
				this.CustomContext = CustomContext;
			}

			public void Invoke(object sender, EventArgs args)
			{
				IExecutionContext context;
				if(CustomContext != null)
					context = CustomContext.CreateChildContext();
				else
					context = Func.Context.CreateChildContext();
				try
				{
					context.InitVariable("sender", sender);
					context.InitVariable("args", args);
					Func.Body.Run(context);
				}
				catch(IterationTermination info)
				{
					if(info.Reason != TerminationReason.Return)
						throw new InvalidOperationException("volání funkce bylo přerušeno jiným důvodem než return: " + info.Reason.ToString());
				}
				finally
				{
					context.Dispose();
				}
			}
		}

		public Delegate GetEventHandler(Type EventHandlerType, IExecutionContext CustomContext)
		{
			EventHandlerHelper helper = new EventHandlerHelper(this, CustomContext);
			return Delegate.CreateDelegate(EventHandlerType, helper, typeof(EventHandlerHelper).GetMethod("Invoke"));
		}

		public override string ToString ()
		{
			return string.Format("function({0})", Parameters.ToString());
		}
	}
}
