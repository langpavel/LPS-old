using System;
using System.IO;

namespace LPS.Util
{
	public class LsDirCommand : ICommand
	{
		public LsDirCommand()
		{
		}

		public void Execute(CommandConsumer consumer, string cmd_name, string argline, TextWriter output)
		{
			string p = argline.Trim();
			if(p == "")
				p = "*";
			string dirname = Directory.GetCurrentDirectory();
			Console.WriteLine("Výpis adresáře {0}", dirname);
			DirectoryInfo info = new DirectoryInfo(dirname);
			foreach(DirectoryInfo dir in info.GetDirectories(p))
			{
				output.WriteLine("{0}/", dir.Name);
			}
			foreach(FileInfo file in info.GetFiles(p))
			{
				output.WriteLine("{0}", file.Name);
			}
		}

		public string GetHelp()
		{
			return "vypíše adresář";
		}
	}
}
