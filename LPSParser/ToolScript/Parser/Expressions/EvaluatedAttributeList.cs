using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class EvaluatedAttributeList : Dictionary<string, EvaluatedAttribute>, IExpression, ICloneable
	{
		public EvaluatedAttributeList()
		{
		}

		public void Run (Context context)
		{
			Eval(context);
		}

		public object Eval(Context context)
		{
			foreach(KeyValuePair<string, EvaluatedAttribute> kv in this)
				kv.Value.Eval(context);
			return this;
		}

		public bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException();
		}

		public T Get<T>(string name, T default_val)
		{
			EvaluatedAttribute result;
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
			EvaluatedAttribute result;
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

		public virtual EvaluatedAttributeList Clone()
		{
			EvaluatedAttributeList clone = new EvaluatedAttributeList();
			foreach(KeyValuePair<string, EvaluatedAttribute> kv in this)
				clone.Add(kv.Key, kv.Value.Clone());
			return clone;
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
