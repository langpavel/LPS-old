using com.calitha.goldparser;
namespace LPS.ToolScript.Parser
{
	public sealed class NullLiteral : LiteralBase, IConstantValue
	{
		public NullLiteral()
		{
		}

		public override object Eval(IExecutionContext context)
		{
			return null;
		}

		public override string ToString ()
		{
			return "null";
		}

		public override bool EvalAsBool(IExecutionContext context)
		{
			return false;
		}

	}
}
