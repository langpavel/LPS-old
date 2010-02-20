using System;
using System.IO;
using LPS.Client;

namespace LPS.Util
{
	public class PingCommand : CommandBase
	{
		public PingCommand(CommandCollection Commands, string Name)
			: base(Commands, Name)
		{
		}

		public override string Help
		{
			get { return "otestuje spojení na server"; }
		}

		public override object Execute (TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			ServerConnection conn = Commands.Get<ServerConnection>("ServerConnection");
			if(conn.Ping())
			{
				Info.WriteLine("Ping OK");
				Out.WriteLine(true);
				return true;
			}
			else
			{
				Info.WriteLine("Ping selhal");
				Out.WriteLine(false);
				return false;
			}
		}
	}
}
