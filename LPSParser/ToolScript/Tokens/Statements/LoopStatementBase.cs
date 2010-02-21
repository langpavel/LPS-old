using System;

namespace LPS.ToolScript.Tokens
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
			throw new IterationTermination(TerminationReason.Continue, context, value);
		}
	}

	public abstract class LoopStatementBase : StatementBase
	{
		protected IStatement IterationBody { get; private set; }
		public LoopStatementBase(IStatement IterationBody)
		{
			this.IterationBody = IterationBody;
		}

		public TerminationReason ExecuteSingleIteration(Context context)
		{
			try
			{
				IterationBody.Run(context);
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
			return TerminationReason.None;
		}
	}
}
