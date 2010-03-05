using System;
using System.Collections.Generic;
using System.Text;
using LPS.ToolScript.Parser;

namespace LPS.ToolScript
{
	public class ExecutionContext : IExecutionContext
	{
		public IExecutionContext GlobalContext { get; private set; }
		public IExecutionContext ParentContext { get; private set; }

		/// <summary>
		/// Parser is user for evaluating eval function
		/// </summary>
		public ToolScriptParser Parser { get; set; }

		public Dictionary<string, object> LocalVariables { get; private set; }
		IDictionary<string, object> IExecutionContext.LocalVariables { get { return LocalVariables; } }

		protected ExecutionContext(IExecutionContext ParentContext, IExecutionContext GlobalContext, ToolScriptParser Parser)
		{
			this.GlobalContext = GlobalContext ?? this;
			this.ParentContext = ParentContext;
			this.Parser = Parser;
			LocalVariables = new Dictionary<string, object>();
		}

		public static ExecutionContext CreateRootContext()
		{
			return CreateRootContext(null);
		}

		public static ExecutionContext CreateRootContext(ToolScriptParser parser)
		{
			ExecutionContext root = new ExecutionContext(null, null, parser);
			root.LocalVariables["eval"] = new ToolScriptFunction(root.EvalInvoked);
			return root;
		}

		private void EvalInvoked (object sender, ToolScriptFunctionArgs e)
		{
			if(e.Args.Count != 1 || !(e.Args[0].Value is string))
				throw new Exception("Funkce eval přijímá jeden parametr typu string");
			e.ReturnValue = this.Eval((string)e.Args[0].Value);
		}

		public IExecutionContext CreateChildContext()
		{
			return new ExecutionContext(this, this.GlobalContext ?? this, Parser);
		}

		public bool TryGetVariable(string name, out object val)
		{
			if(LocalVariables.TryGetValue(name, out val))
				return true;
			if(this.ParentContext != null)
				return this.ParentContext.TryGetVariable(name, out val);
			val = null;
			return false;
		}

		public bool TrySetVariable(string name, object val)
		{
			if(LocalVariables.ContainsKey(name))
			{
				LocalVariables[name] = val;
				return true;
			}
			if(this.ParentContext != null)
				return this.ParentContext.TrySetVariable(name, val);
			return false;
		}

		public object Eval(string code)
		{
			if(Parser == null)
				throw new Exception("Parser must be assigned while executing evals");
			code = code.Trim().TrimEnd(';') + ';';
			StatementList list = this.Parser.Parse(code);
			if(list == null)
				throw new Exception("Parse errror: Output is null");
			if(list.Count == 1 && (list[0] is IExpression))
				return ((IExpression)list[0]).Eval(this);
			else
			{
				try
				{
					list.Run(this);
				}
				catch(IterationTermination info)
				{
					if(info.Reason == TerminationReason.Return)
						return info.ReturnValue;
				}
			}
			return SpecialValue.Void;
		}

		public virtual void Dispose()
		{
			this.LocalVariables.Clear();
		}

		public void InitVariable(string name)
		{
			if(!this.LocalVariables.ContainsKey(name))
				this.LocalVariables[name] = SpecialValue.VariableNotSet;
		}

		public void InitVariable(string name, object val)
		{
			this.LocalVariables[name] = val;
		}

		public void InitVariable(string name, object val, bool canOverwrite)
		{
			if(!canOverwrite && this.LocalVariables.ContainsKey(name))
				throw new VariableException("Proměnná "+name+" byla již inicializována");
			this.LocalVariables[name] = val;
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
			this.LocalVariables.Remove(name);
		}

		public object this[string name]
		{
			get { return GetVariable(name); }
			set { SetVariable(name, value); }
		}

		public virtual object Clone()
		{
			ExecutionContext clone = (ExecutionContext)this.MemberwiseClone();
			clone.LocalVariables = new Dictionary<string, object>();
			foreach(KeyValuePair<string, object> kv in this.LocalVariables)
			{
				if(kv.Value is ICloneable)
					clone.LocalVariables.Add(kv.Key, ((ICloneable)kv.Value).Clone());
				else
					clone.LocalVariables.Add(kv.Key, kv.Value);
			}
			return clone;
		}
	}
}
