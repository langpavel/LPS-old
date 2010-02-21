using System;
using com.calitha.goldparser;
using System.Globalization;

namespace LPS.ToolScript.Tokens
{
	public class Variable : ExpressionBase, IAssignable
	{
		public string Name { get; private set; }
		public Variable(string name)
		{
			this.Name = name;
		}

		public override object Eval(Context context)
		{
			return context.GetVariable(Name);
		}

		public void Initialize(Context context, object val)
		{
			context.InitVariable(Name, val);
		}

		public void AssignValue(Context context, object val)
		{
			context.SetVariable(Name, val);
		}

		public void UnInitialize(Context context)
		{
			context.UnsetVariable(Name);
		}

		public override string ToString ()
		{
			return string.Format("{0}", Name);
		}

	}
}
