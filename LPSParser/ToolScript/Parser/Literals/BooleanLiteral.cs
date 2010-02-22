using System;

namespace LPS.ToolScript.Parser
{
	public sealed class BooleanLiteral : LiteralBase, IConstantValue
	{
		private bool val;
		public BooleanLiteral(bool value)
		{
			val = value;
		}

		public override object Eval(Context context)
		{
			return val;
		}

		public static explicit operator bool(BooleanLiteral lit)
		{
			return lit.val;
		}

		public override string ToString ()
		{
			return string.Format("{0}", val);
		}
		
		public override bool EvalAsBool(Context context)
		{
			return val;
		}
	}
}
