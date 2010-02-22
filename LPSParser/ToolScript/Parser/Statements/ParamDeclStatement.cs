using System;

namespace LPS.ToolScript.Parser
{
	public class ParamDeclStatement : StatementBase
	{
		public string Name { get; private set; }

		/// <summary>
		/// vyhodnocovano pri definici
		/// </summary>
		public IExpression DefaultValueExpr { get; private set; }
		public object DefaultValue { get; set; }

		public ParamDeclStatement(string argument_name, IExpression default_value)
		{
			this.Name = argument_name;
			this.DefaultValueExpr = default_value;
		}

		public override void Run(Context context)
		{
			if(DefaultValue == null)
				this.DefaultValue = SpecialValue.VariableNotSet;
			else
				this.DefaultValue = DefaultValueExpr.Eval(context);
		}
	}
}
