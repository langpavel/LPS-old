using System;
using System.Collections.Generic;
using System.Text;

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
			if(name == "Print")
			{
				if(args.Length < 1)
					throw new InvalidOperationException("minimum je 1 parametr typu string");
				if(args.Length == 1)
					Console.WriteLine(args[0]);
				else if (args[0] is string)
				{
					object[] o = new object[args.Length - 1];
					for(int i = 0; i < o.Length; i++)
						o[i] = args[i+1];
					Console.WriteLine((string)args[0], o);
				}
				else
				{
					StringBuilder sb = new StringBuilder("Pole: ");
					for(int i=0; i<args.Length; i++)
						sb.AppendFormat("[{0}]:{1}; ", i, args[i]);
					Console.WriteLine(sb.ToString());
				}
				return null;
			}
			else
				throw new InvalidOperationException("Funkce "+name+" nenalezena");
		}

		public void Dispose()
		{
			this.variables.Clear();
		}

		public void InitVariable(string name)
		{
			if(!this.variables.ContainsKey(name))
				this.variables[name] = SpecialValue.VariableNotSet;
		}

		public void InitVariable(string name, object val)
		{
			this.variables[name] = val;
		}

		public object GetVariable(string name)
		{
			object result;
			if(this.TryGetVariable(name, out result))
				return result;
			throw new VariableNotInitialized(name);
		}

		public void SetVariable(string name, object val)
		{
			if(!TrySetVariable(name, val))
				throw new VariableNotInitialized(name);
		}

		public void UnsetVariable(string name)
		{
			this.variables.Remove(name);
		}
	}
}