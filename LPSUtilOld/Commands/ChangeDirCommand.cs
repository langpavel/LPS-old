using System;
using System.IO;

namespace LPS.Util
{
	public class ChangeDirCommand : ICommand
	{
		public ChangeDirCommand()
		{
		}

		public void Execute(CommandConsumer consumer, string cmd_name, string argline, TextWriter output)
		{
			string p = argline.Trim();
			if(p != "")
				System.IO.Directory.SetCurrentDirectory(p);
			else
				output.WriteLine(System.IO.Directory.GetCurrentDirectory());
		}

		public string GetHelp()
		{
			return "změní pracovní adresář";
		}
	}
}
