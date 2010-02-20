using System;

namespace LPS.ToolScript.Tokens
{
	public interface IExpression
	{
		object Eval(Context context);
	}
}
