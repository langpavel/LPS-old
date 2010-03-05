using System;
using System.Collections.Generic;
using System.Collections;

namespace LPS.ToolScript.Parser
{
	public class ArrayExpression : ExpressionBase
	{
		public List<IExpression> Items { get; private set; }
		public ArrayExpression(List<IExpression> Items)
		{
			this.Items = Items;
		}

		public override object Eval (IExecutionContext context)
		{
			if(Items == null)
				return null;
			Type t = null;
			ArrayList list = new ArrayList(Items.Count);
			bool has_nulls = false;
			for(int i=0; i < Items.Count; i++)
			{
				object obj = Items[i].Eval(context);
				Type tobj = (obj == null) ? null : obj.GetType();
				if(obj == null)
					has_nulls = true;
				if(t == null && tobj != null)
					t = tobj;
				else if(t != tobj && tobj != null)
					t = typeof(object);

				list.Add(obj);
			}

			if(t.IsValueType && has_nulls)// && t != typeof(string))
				t = typeof(object);

			return list.ToArray(t ?? typeof(object));
		}

		public override bool EvalAsBool (IExecutionContext context)
		{
			throw new InvalidOperationException("Nelze přetypovat pole na boolean");
		}

	}
}
