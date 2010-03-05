using System;
using System.Collections.Generic;
using System.Text;

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
			{
				throw new NotImplementedException();
				//db = (IDatabaseSchema)context.GlobalContext.GetVariable(Name);
			}

			if(this.Name != null && !IsExtension)
				context.GlobalContext.InitVariable(this.Name, db);

			foreach(IDBTable table in this.Values)
				table.Eval(context);

			Resolve(db);

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

		public string CreateSQL()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("CREATE DATABASE {0};\n", this.Name);

			sb.AppendLine();
			sb.AppendLine("------------");
			sb.AppendLine("-- Tables --");
			sb.AppendLine("------------");

			foreach(IDBTable table in this.Values)
				if(!table.IsTemplate)
					sb.Append(table.CreateTableSQL());

			sb.AppendLine();
			sb.AppendLine("------------------");
			sb.AppendLine("-- Foreign keys --");
			sb.AppendLine("------------------");
			sb.AppendLine();

			foreach(IDBTable table in this.Values)
				if(!table.IsTemplate)
					sb.Append(table.CreateForeignKeysSQL());

			return sb.ToString();
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public DatabaseConnection Connect(string url)
		{
			return new DatabaseConnection(this, url);
		}
	}
}
