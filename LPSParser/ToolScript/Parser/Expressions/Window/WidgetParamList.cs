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

		public bool TryGet<T>(string name, out T value)
		{
			object result;
			if(TryGet(typeof(T), name, out result))
			{
				value = (T)result;
				return true;
			}
			value = default(T);
			return false;
		}

		public bool TryGet(Type type, string name, out object value)
		{
			EvaluatedExpression result;
			if(TryGetValue(name, out result))
			{
				if(result.Value != null)
				{
					Type t = result.Value.GetType();
					if(t != type || t.IsSubclassOf(type))
					{
						value = Convert.ChangeType(result.Value, type);
						return true;
					}
				}
				value = result.Value;
				return true;
			}
			value = null;
			return false;
		}
	}
}
