using System;
using LPS.Client;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LPS.Util
{
	public class CommandConsumer: IDisposable
	{
		public static ServerConnection Connection { get; set; }
		public static Dictionary<string, ICommand> Commands { get; private set; }
		static CommandConsumer()
		{
			Commands = new Dictionary<string, ICommand>();
			Commands["help"] = new HelpCommand();

			Commands["foreach"] = new ForeachCommand();

			Commands["echo"] = new EchoCommand();
			Commands["login"] = new LoginCommand();
			Commands["ping"] = new PingCommand();
			Commands["sqltab"] = new SqlTablesCommand();
			Commands["xtab"] = new XtableCommand();
			Commands["refresh"] = new RefreshCommand();
			Commands["cd"] = new ChangeDirCommand();
			Commands["ls"] = new LsDirCommand();
			Commands["dir"] = new LsDirCommand();
		}

		public CommandConsumer()
		{
		}

		public static void Main(string[] args)
		{
			Log.Add(new TextLogger(Console.Out, Verbosity.Debug));
			try
			{
				using(CommandConsumer consumer = new CommandConsumer())
				{
					consumer.Execute(String.Join(" ", args));
					Console.Write("LPS# ");
					string cmd = Console.ReadLine();
					while(cmd != "exit" && cmd != "quit")
					{
						switch(cmd)
						{
						case "clear":
						case "cls":
							Console.Clear();
							break;
						default:
							consumer.Execute(cmd);
							break;
						}
						Console.Write("LPS# ");
						cmd = Console.ReadLine();
					}
				}
			}
			finally
			{
				Log.DisposeLoggers();
			}
		}

		public void Execute(string cmds)
		{
			string[] cmd_bits = cmds.Split(new string[] {";;"}, StringSplitOptions.RemoveEmptyEntries);
			foreach(string cmdline in cmd_bits)
			{
				TextWriter output = Console.Out;
				string[] cmd_output = cmdline.Split(new string[] { ">>>" }, StringSplitOptions.None);
				if(cmd_output.Length > 1)
				{
					try
					{
						output = new StreamWriter(cmd_output[1], false, Encoding.UTF8);
					}
					catch(Exception err)
					{
						Log.Error(err);
						continue;
					}
				}
				Execute(cmd_output[0], output);
				if(output != Console.Out)
				{
					output.Close();
					output.Dispose();
				}
			}
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
