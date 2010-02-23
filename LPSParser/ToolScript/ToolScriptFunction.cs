using System;
using LPS.ToolScript.Parser;

namespace LPS.ToolScript
{

	public class ToolScriptFunctionArgs : EventArgs
	{
		public NamedArgumentList Args { get; private set; }
		public Context ParserContext { get; private set; }
		public object ReturnValue { get; set; }

		public ToolScriptFunctionArgs(Context context, NamedArgumentList Args)
		{
			this.ParserContext = context;
			this.Args = Args;
			this.ReturnValue = SpecialValue.Void;
		}
	}

	public class ToolScriptFunction : IFunction
	{
		public event EventHandler<ToolScriptFunctionArgs> Invoked;

		public ToolScriptFunction()
		{
		}

		public ToolScriptFunction(EventHandler<ToolScriptFunctionArgs> handler)
		{
			this.Invoked += handler;
		}

		public virtual object Execute (Context context, NamedArgumentList arguments)
		{
			ToolScriptFunctionArgs args = new ToolScriptFunctionArgs(context, arguments);
			if(Invoked != null)
				Invoked(this, args);
			return args.ReturnValue;
		}
	}
}
