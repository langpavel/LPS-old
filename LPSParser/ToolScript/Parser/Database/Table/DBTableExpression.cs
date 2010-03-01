using System;
using System.Collections.Generic;
using System.Collections;

namespace LPS.ToolScript.Parser
{
	public class DBTableExpression : Dictionary<string, IDBColumn>, IDBTable
	{
		public string Name { get; private set; }

		/// <summary>
		/// If table should NOT be created
		/// </summary>
		public bool IsTemplate { get; private set; }

		/// <summary>
		/// If existing table with Name should be updated
		/// </summary>
		public bool IsExtension { get; private set; }
		public string ExtensionName { get; private set; }
		public EvaluatedAttributeList Attribs { get; private set; }
		public DBTableIndices Indices { get; private set; }

		public DBTableExpression(string Name, bool IsTemplate, bool IsExtension, string ExtensionName)
		{
			this.Name = Name;
			this.IsTemplate = IsTemplate;
			this.IsExtension = IsExtension;
			this.Attribs = new EvaluatedAttributeList();
			this.Indices = new DBTableIndices();
			this.ExtensionName = ExtensionName;
		}

		public void AddAttrib(object attrib)
		{
			if(attrib is KeyValuePair<string,EvaluatedAttribute>)
			{
				KeyValuePair<string,EvaluatedAttribute> kv = (KeyValuePair<string,EvaluatedAttribute>)attrib;
				Attribs.Add(kv.Key, kv.Value);
            }
			else if (attrib is IDBTableTrigger)
			{
				throw new NotImplementedException();
			}
			else if (attrib is DBTableIndex)
			{
				this.Indices.Add((DBTableIndex)attrib);
			}
			else
				throw new NotImplementedException();
		}

		public void AddAttribs(IEnumerable collection)
		{
			foreach(object o in collection)
				AddAttrib(o);
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
			DBTableExpression clone = new DBTableExpression(Name, IsTemplate, IsExtension, ExtensionName);
			clone.Attribs = Attribs.Clone();
			clone.Indices = this.Indices.Clone();
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
