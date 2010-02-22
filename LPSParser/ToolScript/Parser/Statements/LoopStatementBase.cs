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
		public Context Context { get; private set; }
		public object ReturnValue { get; private set; }

		private IterationTermination(TerminationReason reason, Context context, object returnValue)
		{
			this.Reason = reason;
			this.Context = context;
			this.ReturnValue = returnValue;
		}

		public static void Break(Context context)
		{
			throw new IterationTermination(TerminationReason.Break, context, null);
		}

		public static void Continue(Context context)
		{
			throw new IterationTermination(TerminationReason.Continue, context, null);
		}

		public static void Return(Context context, object value)
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

		public TerminationReason ExecuteSingleIteration(Context context, bool create_child_context)
		{
			Context child_context = create_child_context ? context.CreateChildContext() : context;
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
