using System;
using LPS.Client;
using System.IO;

namespace LPS.Util
{
	public class LoginCommand : ICommand
	{
		public LoginCommand()
		{
		}

		public virtual void Execute(string cmd_name, string argline, TextWriter output)
		{
			string[] p = argline.Split(new char[] {',',';'});
			if(p.Length != 3)
				throw new Exception("Neplatný počet parametrů");

			ServerConnection c = new LPS.Client.ServerConnection(p[0]);
			long user_id = c.Login(p[1], p[2]);
			Log.Write(Verbosity.Info, "", "User ID: {0}", user_id);
			if(user_id != 0L)
			{
				CommandConsumer.Connection = c;
			}
			else
			{
				Log.Error("User not logged in!");
				c.Dispose();
			}
		}

		public virtual string GetHelp()
		{
			return "Přihlášení na server";
		}
	}
}
