using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class ArrayExpression : ExpressionBase
	{
		public List<IExpression> Items { get; private set; }
		public ArrayExpression(List<IExpression> Items)
		{
			this.Items = Items;
		}

		public override object Eval (Context context)
		{
			if(Items == null)
				return null;
			object[] result = new object[Items.Count];
			for(int i=0; i < Items.Count; i++)
				result[i] = Items[i].Eval(context);
			return result;
		}

		public override bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException("Nelze přetypovat pole na boolean");
		}

	}
}
