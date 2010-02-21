using System;
using LPS.Client;
using System.IO;

namespace LPS.Util
{
	public class LoginCommand : CommandBase
	{
		public LoginCommand(CommandCollection Commands, string Name)
			: base(Commands, Name)
		{
		}

		public override Type[] ParamTypes
		{
			get
			{
				return new Type[] {
					typeof(string), typeof(string), typeof(string) };
			}
		}

		public override string Help
		{
			get { return "Přihlášení na server"; }
		}

		public override object Execute (TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			string url = Get<string>(ref Params, 0);
			ServerConnection conn = new ServerConnection(url);
			Info.WriteLine("Připojeno k {0}", url);
			conn.Login(Get<string>(ref Params, 1), Get<string>(ref Params, 2));
			Info.WriteLine("Uživatel {0} přihlášen", Get<string>(ref Params, 1));
			this.Commands.SetVar("ServerConnection", conn);
			return conn;
		}
	}
}
