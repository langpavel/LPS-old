using System;
using System.Globalization;

namespace LPS.ToolScript.Parser
{
	public abstract class DBColumnBase : ExpressionBase, IDBColumn
	{
		public string Name { get; set; }
		public EvaluatedAttributeList Attribs { get; set; }
		public IDBTable Table { get; set; }
		public Type DataType { get; private set; }
		public virtual bool IsAsbtract { get { return false; } }
		public virtual bool IsPrimary { get { return false; } }
		public bool IsUnique { get; protected set; }
		public bool IsNotNull { get; protected set; }
		public string DisplayNull { get; protected set; }
		public string DisplayNullWithTags { get; protected set; }
		public string DisplayFormat { get; protected set; }
		public string DisplayFormatWithTags { get; protected set; }

		/// <summary>
		/// If column is in one-column index
		/// </summary>
		public bool IsIndex { get; protected set; }

		public DBColumnBase(Type DataType)
		{
			this.DataType = DataType;
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

		public virtual string DisplayValue(object value, string format, string ifnull)
		{
			if(value == null || value is DBNull)
				return ifnull;
			if(String.IsNullOrEmpty(format) || !(value is IFormattable))
				return value.ToString();
			return ((IFormattable)value).ToString(format, CultureInfo.CurrentCulture);
		}

		public string DisplayValue(object value, bool allow_tags)
		{
			if(allow_tags && !String.IsNullOrEmpty(this.DisplayFormatWithTags))
				return DisplayValue(value, this.DisplayFormatWithTags, this.DisplayNullWithTags);
			else
				return DisplayValue(value, this.DisplayFormat, this.DisplayNull);
		}

		public override object Eval (Context context)
		{
			Attribs.Eval(context);
			this.IsNotNull = Attribs.Get<bool>("NOT NULL", false);
			this.IsUnique = Attribs.Get<bool>("UNIQUE", false);
			this.IsIndex = Attribs.Get<bool>("INDEX", false);
			this.DisplayFormat = Attribs.Get<string>("Display", "");
			this.DisplayFormatWithTags = Attribs.Get<string>("DisplayTags", this.DisplayFormat);
			this.DisplayNull = Attribs.Get<string>("DisplayNull", "");
			this.DisplayNullWithTags = Attribs.Get<string>("DisplayNullTags", this.DisplayNull);
			return this;
		}

		public override bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException("Nelze vyhodnocovat odkaz na databázový sloupec jako boolean");
		}

		public virtual void Resolve (IDatabaseSchema database)
		{
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
