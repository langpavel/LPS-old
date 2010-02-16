using System;
using System.IO;

namespace LPS.Util
{
	public class PingCommand : ICommand
	{
		public PingCommand()
		{
		}

		public void Execute (string cmd_name, string argline, TextWriter output)
		{
			if(CommandConsumer.Connection == null)
				output.WriteLine("Not logged in!");
			else
				output.WriteLine(CommandConsumer.Connection.Ping());
		}

		public string GetHelp ()
		{
			return "Otestuje spojení se serverem";
		}
	}
}
