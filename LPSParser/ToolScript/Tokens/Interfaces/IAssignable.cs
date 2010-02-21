using System;

namespace LPS.ToolScript.Tokens
{
	public interface IAssignable : IValue
	{
		void Initialize(Context context, object val);
		void AssignValue(Context context, object val);
		void UnInitialize(Context context);
	}
}
