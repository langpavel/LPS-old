using System;
using System.IO;
using System.Text;

namespace LPS.Util
{
	public class WriteFileCommand : CommandBase
	{
		public WriteFileCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "otevře soubor pro zápis"; }
		}

		public override object Execute(LPS.ToolScript.IExecutionContext context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			return new StreamWriter(Get<string>(Params, 0), false, Encoding.UTF8);
		}
	}
}
