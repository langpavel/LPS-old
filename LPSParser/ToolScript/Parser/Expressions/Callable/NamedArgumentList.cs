using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public enum NamedArgumentListMode
	{
		DefaultArguments,
		CallArguments
	}

	public class NamedArgumentList : List<NamedArgument>, IStatement
	{
		public NamedArgumentListMode Mode { get; private set; }
		public NamedArgumentList(NamedArgumentListMode Mode)
		{
			this.Mode = Mode;
		}

		public void Run (Context context)
		{
			bool named = false;
			foreach(NamedArgument arg in this)
			{
				if(named && !arg.IsNamed)
					throw new Exception("Nelze umístit nepojmenovanou proměnnou za pojmenované");
				named = arg.IsNamed;
				arg.Run(context);
			}
		}

		public object GetValue(string name, int index)
		{
			if(name != null)
			{
				foreach(NamedArgument arg in this)
				{
					if(arg.Name == name)
						return arg.Value;
				}
			}
			if (index >= this.Count)
				return SpecialValue.VariableNotSet;
			else
			{
				return this[index].Value;
			}
		}

		/// <summary>
		/// run this on defaults instance with actual parameters in 'values' list
		/// </summary>
		public void InitVariables(Context context, NamedArgumentList values)
		{
			for(int i = 0; i < this.Count; i++)
			{
				NamedArgument arg = this[i];
				object val = values.GetValue(arg.Name, i);
				context.InitVariable(arg.Name,
					(val != SpecialValue.VariableNotSet) ? val : arg.Value);
			}
		}

		public override string ToString ()
		{
			return string.Format("[NamedArgumentList: Mode={0}] Count={1}", Mode, this.Count);
		}
	}
}
