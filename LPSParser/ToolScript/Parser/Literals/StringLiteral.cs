using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
{
	public sealed class StringLiteral : LiteralBase, IConstantValue
	{
		private string val;
		public StringLiteral(string val)
		{
			this.val = val;
		}

		public override object Eval(Context context)
		{
			return val;
		}

		public static explicit operator string(StringLiteral lit)
		{
			return lit.val;
		}

		public override string ToString ()
		{
			return string.Format("\"{0}\"", val.Replace("\"","\\\""));
		}

		public override bool EvalAsBool(Context context)
		{
			throw new Exception("Nelze vyhodnotit string jako boolean");
		}

		/// <summary>
		/// remove quotes etc...
		/// </summary>
		public static string Parse(string code)
		{
			return code.Substring(1, code.Length - 2);
		}
	}
}
