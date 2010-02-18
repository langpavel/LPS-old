using System;
using System.IO;
using System.Text;

namespace LPS.Util
{
	public class ForeachCommand : ICommand
	{
		public ForeachCommand()
		{
		}

		public void Execute(CommandConsumer consumer, string cmd_name, string argline, TextWriter output)
		{
			string[] args = argline.Split(new char[] {';'}, 2);
			StringBuilder sb = new StringBuilder();
			TextWriter resultset = new StringWriter(sb);
			consumer.Execute(args[0], resultset);
			if(args.Length == 1)
			{
				output.WriteLine(sb.ToString());
				return;
			}
			TextReader reader = new StringReader(sb.ToString());
			string line;
			string cmdt = args[1].Replace(">>", ">>>");
			while((line = reader.ReadLine()) != null)
			{
				consumer.Execute(String.Format(cmdt, line));
			}
		}

		public string GetHelp()
		{
			return "vypíše parametr na výstup";
		}
	}
}
