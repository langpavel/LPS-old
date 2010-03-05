using System;

namespace LPS.ToolScript.Parser
{
	public class NamedArgument : IStatement
	{
		public string Name { get; private set; }
		public IExpression DefaultValue { get; private set; }
		public object Value { get; private set; }
		public bool IsNamed { get { return Name != null; } }

		public NamedArgument(string Name, IExpression DefaultValue)
		{
			this.Name = Name;
			this.DefaultValue = DefaultValue;
			this.Value = SpecialValue.VariableNotSet;
		}

		public void Run(IExecutionContext context)
		{
			if(this.DefaultValue == null)
				this.Value = SpecialValue.VariableNotSet;
			else
				this.Value = this.DefaultValue.Eval(context);
		}
	}
}
