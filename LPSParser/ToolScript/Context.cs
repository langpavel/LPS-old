using System;
using System.Collections.Generic;

namespace LPS.ToolScript
{
	public sealed class Context : IDisposable
	{
		public Context GlobalContext { get; private set; }
		public Context ParentContext { get; private set; }

		private Dictionary<string, object> variables;

		private Context(Context ParentContext, Context GlobalContext)
		{
			this.GlobalContext = GlobalContext ?? this;
			this.ParentContext = ParentContext;
			variables = new Dictionary<string, object>();
		}

		public static Context CreateRootContext()
		{
			return new Context(null, null);
		}

		public Context CreateChildContext()
		{
			return new Context(this, this.GlobalContext ?? this);
		}

		private bool TryGetVariable(string name, out object val)
		{
			if(variables.TryGetValue(name, out val))
				return true;
			if(this.ParentContext != null)
				return this.ParentContext.TryGetVariable(name, out val);
			val = null;
			return false;
		}

		private bool TrySetVariable(string name, object val)
		{
			if(variables.ContainsKey(name))
			{
				variables[name] = val;
				return true;
			}
			if(this.ParentContext != null)
				return this.ParentContext.TrySetVariable(name, val);
			return false;
		}

		public object GetVariable(string name)
		{
			object result;
			if(this.TryGetVariable(name, out result))
				return result;
			throw new Exception(String.Format("Proměnná {0} nebyla nalezena", name));
		}

		public void SetVariable(string name, object val)
		{
			if(!TrySetVariable(name, val))
				variables.Add(name, val);
		}

		public object FunctionCall(string name, params object[] args)
		{
			if(name == "Format")
			{
				if(args.Length < 1)
					throw new InvalidOperationException("minimum je 1 parametr typu string");
				object[] o = new object[args.Length - 1];
				for(int i = 0; i < o.Length; i++)
					o[i] = args[i+1];
				return String.Format((string)args[0], o);
			}
			else
				throw new InvalidOperationException("Funkce "+name+" nenalezena");
		}

		public void Dispose()
		{
			this.variables.Clear();
		}
	}
}
