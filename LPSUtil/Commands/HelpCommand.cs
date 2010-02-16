using System;
using System.Collections.Generic;
using System.IO;

namespace LPS.Util
{
	public class HelpCommand : ICommand
	{
		public HelpCommand()
		{
		}

		public void Execute (string cmd_name, string argline, TextWriter output)
		{
			foreach(KeyValuePair<string, ICommand> kw in CommandConsumer.Commands)
			{
				output.WriteLine("{0} \t- {1}", kw.Key, kw.Value.GetHelp());
			}
		}

		public string GetHelp()
		{
			return "vypíše tento seznam na výstup";
		}
	}
}
