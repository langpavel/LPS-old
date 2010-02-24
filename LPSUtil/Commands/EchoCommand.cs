using System;
using System.IO;
using LPS.ToolScript;
using System.Collections;
using System.Text;

namespace LPS.Util
{
	public class EchoCommand : CommandBase
	{
		public EchoCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "vypíše parametr na výstup"; }
		}

		public void GetStringRepr(object obj, StringBuilder sb)
		{
			if(obj is Hashtable)
			{
				foreach(DictionaryEntry de in ((Hashtable)obj))
					sb.AppendFormat("'{0}':{1}\t", de.Key, de.Value);
			}
			else if(obj is IEnumerable)
			{
				foreach(object item in (IEnumerable)obj)
				{
					GetStringRepr(item, sb);
					sb.AppendLine();
				}
			}
			else
				sb.Append(obj).Append("\t");
		}

		public override object Execute(LPS.ToolScript.Context context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			StringBuilder sb = new StringBuilder();
			GetStringRepr(Params[0], sb);
			Out.WriteLine(sb.ToString());
			return SpecialValue.Void;
		}
	}
}
