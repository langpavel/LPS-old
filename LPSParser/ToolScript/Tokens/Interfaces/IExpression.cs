using System;

namespace LPS.ToolScript.Tokens
{
	public interface IExpression: IStatement
	{
		object Eval(Context context);
		bool EvalAsBool(Context context);
	}
}
