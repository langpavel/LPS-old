using System;

namespace LPS.ToolScript.Tokens
{
	public interface IStatement
	{
		void Run(Context context);
	}
}
