
using System;
using System.Globalization;

namespace LPS.ToolScript.Parser
{
	public abstract class ExpressionBase : StatementBase, IExpression
	{
		public ExpressionBase()
		{
		}

		public override void Run (IExecutionContext context)
		{
			Eval(context);
		}

		public abstract object Eval (IExecutionContext context);

		public virtual bool EvalAsBool (IExecutionContext context)
		{
			return AsBoolean(Eval(context));
		}

		protected static bool AsBoolean(object val)
		{
			if(val == null)
				return false;
			else if(val is bool)
				return (bool)val;
			else if(val is IConvertible)
				return ((IConvertible)val).ToBoolean(CultureInfo.InvariantCulture);
			else
				throw new Exception(String.Format("Nelze převést hodnotu typu {0} typ boolean", val.GetType().Name));
		}

		public static bool IsNumeric(Type t)
		{
			return t == typeof(Int64)
				|| t == typeof(Decimal)
				|| t == typeof(Int32)
				|| t == typeof(Byte)
				|| t == typeof(SByte)
				|| t == typeof(Char)
				|| t == typeof(Double)
				|| t == typeof(Single)
				|| t == typeof(UInt32)
				|| t == typeof(UInt64)
				|| t == typeof(Int16)
				|| t == typeof(UInt16);
		}

		public static bool IsNumeric(object o)
		{
			if(o == null)
				return false;
			return IsNumeric(o.GetType());
		}

		public static bool IsDecimal(Type t)
		{
			return t == typeof(Decimal)
				|| t == typeof(Double)
				|| t == typeof(Single);
		}

		public static bool IsDecimal(object o)
		{
			if(o == null)
				return false;
			return IsDecimal(o.GetType());
		}

		public static bool IsInteger(Type t)
		{
			return t == typeof(Int64)
				|| t == typeof(Int32)
				|| t == typeof(Byte)
				|| t == typeof(SByte)
				|| t == typeof(Char)
				|| t == typeof(UInt32)
				|| t == typeof(UInt64)
				|| t == typeof(Int16)
				|| t == typeof(UInt16);
		}

		public static bool IsInteger(object o)
		{
			if(o == null)
				return false;
			return IsInteger(o.GetType());
		}

		public static bool IsEqual(object val1, object val2)
		{
			if(IsInteger(val1) && IsInteger(val2))
				return Convert.ToInt64(val1) == Convert.ToInt64(val2);
			else if(IsNumeric(val1) && IsNumeric(val2))
				return Convert.ToDecimal(val1) == Convert.ToDecimal(val2);
			else if(val1 == null && val2 == null)
				return true;
			else if(val1 == null || val2 == null)
				return false;
			else if(Object.ReferenceEquals(val1, val2))
				return true;
			else if(val1.Equals(val2) || val2.Equals(val1))
				return true;
			else if(val1 is string && val2 is string)
				return (string)val1 == (string)val2;
			else throw new Exception(String.Format("Nelze porovnat hodnoty '{0}' a '{1}' typu {2} a {3}",
				val1, val2,
				(val1 == null)?"null":val1.GetType().Name,
			    (val2 == null)?"null":val2.GetType().Name));
		}

	}
}
