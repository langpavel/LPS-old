using System;
using System.IO;

namespace LPS.Util
{
	public class EvalCommand : CommandBase
	{
		public EvalCommand(CommandCollection Commands, string Name)
			: base(Commands, Name)
		{
		}

		public override Type[] ParamTypes
		{
			get { return new Type[] { typeof(string) }; }
		}

		public override string Help
		{
			get { return "vyhodnotí obsah parametru jako příkaz"; }
		}

		public override object Execute(TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			string command = Get<string>(Params, 0);
			if(String.IsNullOrEmpty(command))
				return null;




		}
	}
}
