using System;

namespace LPS.ToolScript.Parser
{
	public class UnaryMinusExpression : UnaryExpression
	{
		public UnaryMinusExpression(IExpression Expr)
			:base(Expr)
		{
		}

		public override object Eval (Context context, object val)
		{
			if(val == null)
				return null;
			if(val is Int64)
			    return -((Int64)val);
			if(val is Decimal)
			    return -((Decimal)val);
			if(val is Int32)
			    return -((Int32)val);
			if(val is Byte)
			    return -((Byte)val);
			if(val is SByte)
			    return -((SByte)val);
			if(val is Char)
			    return -((Char)val);
			if(val is Double)
			    return -((Double)val);
			if(val is Single)
			    return -((Single)val);
			if(val is UInt32)
			    return -((UInt32)val);
			if(val is Int16)
			    return -((Int16)val);
			if(val is UInt16)
			    return -((UInt16)val);
			else
				throw new InvalidOperationException(String.Format(
					"Nelze provádět unární minus na objektu typu {0}", val.GetType().Name));
		}

		public override bool EvalAsBool (Context context, object val)
		{
			throw new InvalidOperationException("nelze převádět číslo na boolean");
		}

	}
}
