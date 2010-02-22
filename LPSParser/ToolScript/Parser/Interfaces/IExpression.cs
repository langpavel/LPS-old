using System;

namespace LPS.ToolScript.Parser
{
	public interface IExpression: IStatement
	{
		object Eval(Context context);
		bool EvalAsBool(Context context);
	}
}
