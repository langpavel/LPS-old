using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class EvaluatedExpression
	{
		public EvaluatedExpression(IExpression Expression)
		{
			this.Expression = Expression;
			this.Value = null;
		}

		public IExpression Expression;
		public object Value;
	}

	public class WidgetParamList : Dictionary<string, EvaluatedExpression>, IExpression
	{
		public WidgetParamList()
		{
		}

		public void Run (Context context)
		{
			Eval(context);
		}

		public object Eval(Context context)
		{
			foreach(KeyValuePair<string, EvaluatedExpression> kv in this)
			{
				kv.Value.Value = kv.Value.Expression.Eval(context);
			}
			return this;
		}

		public bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException ();
		}

		public T Get<T>(string name, T default_val)
		{
			EvaluatedExpression result;
			if(TryGetValue(name, out result))
			{
				if(result.Value != null)
				{
					Type t = result.Value.GetType();
					if(t != typeof(T) || t.IsSubclassOf(typeof(T)))
						return (T)Convert.ChangeType(result.Value, typeof(T));
				}
				return (T) result.Value;
			}
			return default_val;
		}
	}
}
