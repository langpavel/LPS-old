
using System;
using System.Globalization;

namespace LPS.ToolScript.Tokens
{
	public abstract class ExpressionBase : StatementBase, IExpression
	{
		public ExpressionBase()
		{
		}

		public override void Run (Context context)
		{
			Eval(context);
		}

		public abstract object Eval (Context context);

		public virtual bool EvalAsBool (Context context)
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

	}
}
