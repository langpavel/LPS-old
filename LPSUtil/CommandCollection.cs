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

		public ICommand FindCommand(string name)
		{
			foreach(ICommand cmd in Commands)
			{
				if(cmd.Name == name)
					return cmd;
			}
			return null;
		}

		public void SetVar(string name, object value)
		{
			vars[name] = value;
		}

		public object GetVar(string name)
		{
			object result;
			if(!vars.TryGetValue(name, out result))
				throw new ApplicationException("Proměnná se jménem '"+name+"' nenalezena");
			return result;
		}

		public bool TryGetVar<T>(string name, out T value)
		{
			object result = null;
			if(vars.TryGetValue(name, out result))
			{
				value = (T)result;
				return true;
			}
			else
			{
				value = default(T);
				return false;
			}
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

		public void Execute(string cmdname, TextWriter Out, TextWriter Info, TextWriter Err, params object[] Params)
		{
			ICommand cmd = FindCommand(cmdname);
			if(cmd != null)
			{
				try
				{
					cmd.Execute(Out, Info, Err, Params);
				}
				catch(Exception err)
				{
					Err.WriteLine(err);
					Log.Error("Příkaz '{0}' vyvolal vyjímku {1}", cmdname, err);
				}
			}
			else
			{
				Log.Error("Příkaz nenalezen: '{0}'", cmdname);
			}
		}

		public virtual void Dispose()
		{
			foreach(IDisposable obj in this.vars.Values)
			{
				if(obj != null)
					obj.Dispose();
			}
			this.vars.Clear();
		}
	}
}
