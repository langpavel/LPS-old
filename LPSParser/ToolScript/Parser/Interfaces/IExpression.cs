using System;

namespace LPS.ToolScript.Parser
{
	public interface IExpression: IStatement
	{
		object Eval(IExecutionContext context);
		bool EvalAsBool(IExecutionContext context);
	}
}
