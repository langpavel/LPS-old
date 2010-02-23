using System;
using System.Collections;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class DictionaryExpression : ExpressionBase
	{
		public struct DistionaryItem
		{
			public DistionaryItem(IExpression Key, IExpression Value)
			{
				this.Key = Key;
				this.Value = Value;
			}
			public IExpression Key;
			public IExpression Value;
		}

		public List<DistionaryItem> Items { get; private set; }
		public DictionaryExpression()
		{
			this.Items = new List<DistionaryItem>();
		}

		public void Add(IExpression Key, IExpression Value)
		{
			Items.Add(new DistionaryItem(Key, Value));
		}

		public override object Eval (Context context)
		{
			if(Items == null)
				return null;
			Hashtable result = new Hashtable(Items.Count * 2);
			for(int i=0; i < Items.Count; i++)
				result.Add(Items[i].Key.Eval(context), Items[i].Value.Eval(context));
			return result;
		}

		public override bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException("Nelze přetypovat slovník na boolean");
		}

	}
}
