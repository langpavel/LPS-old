using System;

namespace LPS.Util
{
	public class RefreshCommand : ICommand
	{
		public RefreshCommand()
		{
		}

		public void Execute (string cmd_name, string argline, System.IO.TextWriter output)
		{
			CommandConsumer.Connection.Resources.FlushModulesInfo();
		}

		public string GetHelp ()
		{
			return "Obnoví Connection";
		}

	}
}
