using System;
using System.IO;

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

		public override object Execute(LPS.ToolScript.Context context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			Out.WriteLine(Params[0]);
			return Params[0];
		}
	}
}
