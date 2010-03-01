using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class DBTableExpression : Dictionary<string, IDBColumn>, IDBTable
	{
		public string Name { get; private set; }
		public bool IsExtension { get; private set; }
		public string ExtensionName { get; private set; }
		public EvaluatedAttributeList Attribs { get; private set; }

		public DBTableExpression(string Name, bool IsExtension, EvaluatedAttributeList Attribs, string ExtensionName)
		{
			this.Name = Name;
			this.IsExtension = IsExtension;
			this.Attribs = Attribs;
			this.ExtensionName = ExtensionName;
		}

		public void Run (Context context)
		{
			Eval(context);
		}

		public object Eval (Context context)
		{
			foreach(IDBColumn column in this.Values)
				column.Eval(context);

			return this;
		}

		public bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException();
		}

		public void Resolve (IDatabaseSchema database)
		{
			foreach(IDBColumn column in this.Values)
				column.Resolve(database);
		}

		public virtual DBTableExpression Clone()
		{
			DBTableExpression clone = new DBTableExpression(Name, IsExtension, Attribs.Clone(), ExtensionName);
			foreach(KeyValuePair<string, IDBColumn> kv in this)
				clone.Add(kv.Key, (IDBColumn)kv.Value.Clone());
			return clone;
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
