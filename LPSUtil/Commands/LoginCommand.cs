using System;
using LPS.Client;
using System.IO;
using LPS.ToolScript;

namespace LPS.Util
{
	public class LoginCommand : CommandBase
	{
		public LoginCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "Přihlášení na server"; }
		}

		public override object Execute(IExecutionContext context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			string url = Get<string>(Params, 0);
			ServerConnection conn = new ServerConnection(url);
			Info.WriteLine("Připojeno k {0}", url);
			conn.Login(Get<string>(Params, 1), Get<string>(Params, 2));
			Info.WriteLine("Uživatel {0} přihlášen", Get<string>(Params, 1));
			return conn;
		}
	}
}
