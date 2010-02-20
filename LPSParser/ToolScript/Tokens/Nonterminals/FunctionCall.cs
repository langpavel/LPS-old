using System;
using com.calitha.goldparser;
using System.Collections.Generic;
using System.Collections;

namespace LPS.ToolScript.Tokens
{
	public sealed class FunctionCall : NonterminalBase, IValue
	{
		private string function_name;
		private IList<IExpression> arguments;

		public FunctionCall(NonterminalToken Token, IList<IExpression> args)
			: base(Token)
		{
			if(IsRule(Symbols.Id, Symbols.Lparan, Symbols.Rparan))
			{
				function_name = GetT(0).Text;
				arguments = null;
			}
			else if(IsRule(Symbols.Id, Symbols.Lparan, Symbols.Args, Symbols.Rparan))
			{
				function_name = GetT(0).Text;
				arguments = args;
			}
		}

		public object Eval(Context context)
		{
			if(arguments == null || arguments.Count == 0)
			{
				return context.FunctionCall(function_name);
			}
			else
			{
				ArrayList p = new ArrayList();
				foreach(IValue val in arguments)
					p.Add(val.Eval(context));
				return context.FunctionCall(function_name, p.ToArray());
			}
		}
	}
}