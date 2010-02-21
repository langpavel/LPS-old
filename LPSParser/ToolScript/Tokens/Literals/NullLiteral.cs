using com.calitha.goldparser;
namespace LPS.ToolScript.Tokens
{
	public sealed class NullLiteral : LiteralBase, IConstantValue
	{
		public NullLiteral()
		{
		}

		public override object Eval(Context context)
		{
			return null;
		}

		public override string ToString ()
		{
			return "null";
		}

		public override bool EvalAsBool(Context context)
		{
			return false;
		}

	}
}
