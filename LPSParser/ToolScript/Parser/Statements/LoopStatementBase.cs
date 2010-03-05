using System;

namespace LPS.ToolScript.Parser
{
	public enum TerminationReason
	{
		None,
		Continue,
		Break,
		Return
	}

	public class IterationTermination : Exception
	{
		public TerminationReason Reason { get; private set; }
		public IExecutionContext Context { get; private set; }
		public object ReturnValue { get; private set; }

		private IterationTermination(TerminationReason reason, IExecutionContext context, object returnValue)
		{
			this.Reason = reason;
			this.Context = context;
			this.ReturnValue = returnValue;
		}

		public static void Break(IExecutionContext context)
		{
			throw new IterationTermination(TerminationReason.Break, context, null);
		}

		public static void Continue(IExecutionContext context)
		{
			throw new IterationTermination(TerminationReason.Continue, context, null);
		}

		public static void Return(IExecutionContext context, object value)
		{
			throw new IterationTermination(TerminationReason.Return, context, value);
		}
	}

	public abstract class LoopStatementBase : StatementBase
	{
		protected IStatement IterationBody { get; private set; }
		public LoopStatementBase(IStatement IterationBody)
		{
			this.IterationBody = IterationBody;
		}

		public TerminationReason ExecuteSingleIteration(IExecutionContext context, bool create_child_context)
		{
			IExecutionContext child_context = create_child_context ? context.CreateChildContext() : context;
			try
			{
				IterationBody.Run(child_context);
			}
			catch(IterationTermination info)
			{
				switch(info.Reason)
				{
				case TerminationReason.Continue:
				case TerminationReason.Break:
					return info.Reason;
				default:
					throw info;
				}
			}
			finally
			{
				if(create_child_context)
					child_context.Dispose();
			}
			return TerminationReason.None;
		}
	}
}
