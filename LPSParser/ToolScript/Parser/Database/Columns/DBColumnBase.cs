using System;

namespace LPS.ToolScript.Parser
{
	public abstract class DBColumnBase : ExpressionBase, IDBColumn
	{
		public string Name { get; set; }
		public EvaluatedAttributeList Attribs { get; set; }
		public IDBTable Table { get; set; }
		public bool IsAsbtract { get; protected set; }
		public Type DataType { get; private set; }
		public bool IsPrimary { get; protected set; }
		public bool IsUnique { get; protected set; }
		public bool IsNotNull { get; protected set; }
		public string DisplayFormat { get; protected set; }
		public string DisplayFormatWithTags { get; protected set; }

		/// <summary>
		/// If column is in one-column index
		/// </summary>
		public virtual bool IsIndex { get { return false; } }

		public DBColumnBase(Type DataType)
		{
			this.DataType = DataType;
			this.IsAsbtract = false;
			this.IsPrimary = false;
			this.IsUnique = false;
			this.IsNotNull = false;
		}

		public virtual object NormalizeValue (object value)
		{
			if(value == null || value is DBNull)
				return DBNull.Value;
			Type t = value.GetType();
			if(t == this.DataType || t.IsSubclassOf(this.DataType))
				return value;
			else
				return Convert.ChangeType(value, this.DataType);
		}

		public virtual string DisplayValue(object value, string format)
		{
			if(value == null || value is DBNull)
				return String.Empty;
			if(String.IsNullOrEmpty(format))
				return value.ToString();
			return String.Format("{0:" + format +"}", value);
		}

		public string DisplayValue(object value, bool allow_tags)
		{
			if(allow_tags && !String.IsNullOrEmpty(this.DisplayFormatWithTags))
				return DisplayValue(value, this.DisplayFormatWithTags);
			else
				return DisplayValue(value, this.DisplayFormat);
		}

		public override object Eval (Context context)
		{
			return this;
		}

		public override bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException("Nelze vyhodnocovat odkaz na databázový sloupec jako boolean");
		}

		public bool HasAttribute (string name)
		{
			return Attribs.ContainsKey(name);
		}

		public object GetAttribute (Type type, string name)
		{
			object value;
			if(!Attribs.TryGet(type, name, out value))
				throw new InvalidOperationException("Sloupec nemá atribut "+ name);
			return value;
		}

		public T GetAttribute<T>(string name)
		{
			T value;
			if(!Attribs.TryGet<T>(name, out value))
				throw new InvalidOperationException("Sloupec nemá atribut "+ name);
			return value;
		}

		public virtual DBColumnBase Clone()
		{
			DBColumnBase clone = (DBColumnBase)this.MemberwiseClone();
			this.Attribs = Attribs.Clone();
			return clone;
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
