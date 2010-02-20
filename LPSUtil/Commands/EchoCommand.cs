using System;
using System.IO;

namespace LPS.Util
{
	public class EchoCommand : CommandBase
	{
		public EchoCommand(CommandCollection Commands, string Name)
			: base(Commands, Name)
		{
		}

		public override Type[] ParamTypes
		{
			get { return new Type[] { typeof(string) }; }
		}

		public override string Help
		{
			get { return "vypíše parametr na výstup"; }
		}

		public override object Execute(TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			Out.WriteLine(Params[0]);
			return Params[0];
		}
	}
}
