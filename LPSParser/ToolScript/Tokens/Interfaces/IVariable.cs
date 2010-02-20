using System;

namespace LPS.ToolScript.Tokens
{
	public interface IVariable : IValue
	{
		void SetValue(Context context, object val);
	}
}
