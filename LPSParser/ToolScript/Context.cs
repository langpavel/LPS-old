using System;
using System.Collections.Generic;
using System.Text;
using LPS.ToolScript.Parser;

namespace LPS.ToolScript
{
	public sealed class Context : IDisposable
	{
		public Context GlobalContext { get; private set; }
		public Context ParentContext { get; private set; }

		/// <summary>
		/// Parser is user for evaluating eval function
		/// </summary>
		public ToolScriptParser Parser { get; set; }

		public Dictionary<string, object> LocalVariables { get; private set; }

		private Context(Context ParentContext, Context GlobalContext, ToolScriptParser Parser)
		{
			this.GlobalContext = GlobalContext ?? this;
			this.ParentContext = ParentContext;
			this.Parser = Parser;
			LocalVariables = new Dictionary<string, object>();
		}

		public static Context CreateRootContext()
		{
			return CreateRootContext(null);
		}

		public static Context CreateRootContext(ToolScriptParser parser)
		{
			Context root = new Context(null, null, parser);
			root.LocalVariables["eval"] = new ToolScriptFunction(EvalInvoked);
			return root;
		}

		private static void EvalInvoked (object sender, ToolScriptFunctionArgs e)
		{
			if(e.Args.Count != 1 || !(e.Args[0].Value is string))
				throw new Exception("Funkce eval přijímá jeden parametr typu string");
			e.ReturnValue = e.ParserContext.Eval((string)e.Args[0].Value);
		}

		public Context CreateChildContext()
		{
			return new Context(this, this.GlobalContext ?? this, Parser);
		}

		private bool TryGetVariable(string name, out object val)
		{
			if(LocalVariables.TryGetValue(name, out val))
				return true;
			if(this.ParentContext != null)
				return this.ParentContext.TryGetVariable(name, out val);
			val = null;
			return false;
		}

		private bool TrySetVariable(string name, object val)
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

		public void Dispose()
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
	}
}
