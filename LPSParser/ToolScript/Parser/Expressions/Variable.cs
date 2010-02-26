using System;
using com.calitha.goldparser;
using System.Globalization;

namespace LPS.ToolScript.Parser
{
	public class Variable : ExpressionBase, IAssignable
	{
		public string Name { get; private set; }
		public bool IsInitializer { get; private set; }
		public Variable(string name, bool is_initializer)
		{
			this.Name = name;
			this.IsInitializer = is_initializer;
		}

		public override void Run (Context context)
		{
			if(IsInitializer)
			{
				context.InitVariable(Name);
			}
		}

		public override object Eval(Context context)
		{
			if(IsInitializer)
			{
				context.InitVariable(Name);
			}
			object result = context.GetVariable(Name);
			if(result == SpecialValue.VariableNotSet)
				throw new VariableNotInitialized(Name);
			return result;
		}

		public void AssignValue(Context context, object val)
		{
			if(IsInitializer)
				context.InitVariable(Name, val);
			else
				context.SetVariable(Name, val);
		}

		/*
		public bool IsUnset(Context context)
		{
			if(IsInitializer)
			{
				context.InitVariable(Name);
			}
			return SpecialValue.VariableNotSet == context.GetVariable(Name);
		}

		public void UnsetValue(Context context)
		{
			context.UnsetVariable(Name);
		}
		*/

		public override string ToString ()
		{
			return string.Format("{0}", Name);
		}

	}
}
