using System;
using System.Collections.Generic;

namespace LPS.ToolScript
{
	public interface IExecutionContext : IDisposable, ICloneable
	{
		IExecutionContext GlobalContext { get; }
		IExecutionContext ParentContext { get; }
		ToolScriptParser Parser { get; }
		IDictionary<string, object> LocalVariables { get; }

		IExecutionContext CreateChildContext();
		bool TryGetVariable(string name, out object val);
		bool TrySetVariable(string name, object val);
		object Eval(string code);
		void InitVariable(string name);
		void InitVariable(string name, object val);
		void InitVariable(string name, object val, bool canOverwrite);
		object GetVariable(string name);
		void SetVariable(string name, object val);
		void UnsetVariable(string name);
	}
}
