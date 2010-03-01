using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class DatabaseExpression : Dictionary<string, IDBTable>, IDatabaseSchema
	{
		public string Name { get; private set; }
		public bool IsExtension { get; private set; }

		public DatabaseExpression(string Name, bool IsExtension)
		{
			this.Name = Name;
			this.IsExtension = IsExtension;
		}

		public void Run (Context context)
		{
			Eval(context);
		}

		public object Eval (Context context)
		{
			IDatabaseSchema db = this;
			if(IsExtension)
				db = (IDatabaseSchema)context.GlobalContext.GetVariable(Name);

			foreach(IDBTable table in this.Values)
				table.Eval(context);

			Resolve(db);

			if(this.Name != null && !IsExtension)
				context.GlobalContext.InitVariable(this.Name, db, false);
			return db;
		}

		public bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException();
		}

		public void Resolve (IDatabaseSchema db)
		{
			foreach(IDBTable table in this.Values)
				table.Resolve(db);
		}

		public virtual DatabaseExpression Clone()
		{
			DatabaseExpression clone = new DatabaseExpression(Name, IsExtension);
			foreach(KeyValuePair<string, IDBTable> kv in this)
				clone.Add(kv.Key, (IDBTable)kv.Value.Clone());
			return clone;
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
