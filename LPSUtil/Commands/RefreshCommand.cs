using System;
using LPS.Client;
using System.IO;

namespace LPS.Util
{
	public class RefreshCommand : CommandBase
	{
		public RefreshCommand(CommandCollection Commands, string Name)
			: base(Commands, Name)
		{
		}

		public override Type[] ParamTypes
		{
			get { return new Type[] { typeof(string) }; }
		}

		public override string Help
		{
			get { return "FlushModulesInfo v connection"; }
		}

		public override object Execute(TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			Commands.GetVar<ServerConnection>("ServerConnection").Resources.FlushModulesInfo();
			Info.WriteLine("FlushModulesInfo: OK");
			return true;
		}
	}
}
