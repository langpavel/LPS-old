using System;
using LPS.Client;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LPS.Util
{
	public class CommandCollection: IDisposable
	{
		public List<ICommand> Commands { get; private set; }
		private Dictionary<string, object> vars;

		public CommandCollection()
		{
			Commands = new List<ICommand>();
			vars = new Dictionary<string, object>();
		}

		public object GetVar(string name)
		{
			object result;
			if(!vars.TryGetValue(name, out result))
				throw new ApplicationException("Proměnná se jménem '"+name+"' nenalezena");
			return result;
		}

		public T GetVar<T>(string name)
		{
			return (T)GetVar(name);
		}

		public void CreateStandartCommands()
		{
			new HelpCommand(this, "help");
			new EchoCommand(this, "echo");
			new EchoCommand(this, "print");
			new LoginCommand(this, "login");
			new PingCommand(this, "ping");
			new SqlTablesCommand(this, "sqltab");
			new XtableCommand(this, "xtab");
			new RefreshCommand(this, "refresh");
			new ChangeDirCommand(this, "");
			new LsDirCommand(this, "");
			new LsDirCommand(this, "");
		}

		public ICommand GetCommand(string name)
		{
			foreach(ICommand cmd in this.Commands)
				if(cmd.Name == name)
					return cmd;
			return null;
		}

		public void Execute(string cmdline, TextWriter output)
		{
			string[] line_bits = cmdline.Split(new char[] {' ','(',',',';'}, 2, StringSplitOptions.RemoveEmptyEntries);
			ICommand cmd;
			if(Commands.TryGetValue(line_bits[0], out cmd))
			{
				try
				{
					if(line_bits.Length == 1)
						cmd.Execute(this, line_bits[0], "", output);
					else
						cmd.Execute(this, line_bits[0], line_bits[1], output);
				}
				catch(Exception err)
				{
					Log.Error("Příkaz '{0}' vyvolal vyjímku {1}", line_bits[0], err);
				}
			}
			else
			{
				Log.Error("Příkaz nenalezen: '{0}'", line_bits[0]);
			}
		}

		public virtual void Dispose()
		{
			if(Connection != null)
			{
				Connection.Dispose();
				Connection = null;
			}
		}
	}
}
