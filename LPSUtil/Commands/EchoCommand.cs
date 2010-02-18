using System;
using System.IO;

namespace LPS.Util
{
	public class EchoCommand : ICommand
	{
		public EchoCommand()
		{
		}

		public void Execute(CommandConsumer consumer, string cmd_name, string argline, TextWriter output)
		{
			output.WriteLine(argline);
		}

		public string GetHelp()
		{
			return "vypíše parametr na výstup";
		}
	}
}
