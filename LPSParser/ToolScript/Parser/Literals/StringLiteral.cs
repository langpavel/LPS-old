using System;
using com.calitha.goldparser;
using System.Text;

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
			StringBuilder sb = new StringBuilder(code.Length-2);
			int state = 0;
			for(int i=1; i < code.Length - 1; i++)
			{
				char ch = code[i];
				if(ch == '\\' && state == 0)
				{
					state = 1;
					continue;
				}
				else if (state == 1)
				{
					switch(ch)
					{
					case '\\':
						state = 0;
						sb.Append('\\');
						continue;
					case 'n':
						state = 0;
						sb.Append('\n');
						continue;
					case 'r':
						state = 0;
						sb.Append('\r');
						continue;
					case 't':
						state = 0;
						sb.Append('\t');
						continue;
					case 'f':
						state = 0;
						sb.Append('\f');
						continue;
					case 'a':
						state = 0;
						sb.Append('\a');
						continue;
					case 'b':
						state = 0;
						sb.Append('\b');
						continue;
					case 'v':
						state = 0;
						sb.Append('\v');
						continue;
					case '\'':
						state = 0;
						sb.Append('\'');
						continue;
					case '"':
						state = 0;
						sb.Append('"');
						continue;
					}
					throw new Exception("Neočekávaný znak v escape sekvenci: \\"+ch);
				}
				else
					sb.Append(ch);
			}
			return sb.ToString();
		}
	}
}
