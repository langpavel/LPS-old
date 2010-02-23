using System;
using System.IO;
using System.Text;

namespace LPS.Util
{
	public class ReadFileCommand : CommandBase
	{
		public ReadFileCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "otevře soubor pro zápis"; }
		}

		public override object Execute(LPS.ToolScript.Context context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			return new StreamReader(Get<string>(Params, 0), Encoding.UTF8, true);
		}
	}
}
