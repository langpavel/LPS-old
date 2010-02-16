using System;
using System.Data;
using System.IO;
using System.Collections.Generic;

namespace LPS.Util
{
	public class SqlTablesCommand : ICommand
	{
		public SqlTablesCommand()
		{
		}

		public static string[] GetSqlTableNames()
		{
			if(CommandConsumer.Connection == null)
				throw new Exception("login required");

			List<string> tables = new List<string>();
			using(DataSet ds = CommandConsumer.Connection.GetDataSetBySql(@"
				select distinct table_name from information_schema.columns where table_schema = 'public'"))
			{
				foreach(DataRow r in ds.Tables[0].Rows)
					tables.Add((string)r[0]);
			}
			tables.Sort();
			return tables.ToArray();
		}

		public void Execute (string cmd_name, string argline, TextWriter output)
		{
			List<string> tablenames = new List<string>(GetSqlTableNames());
			if(argline == "missing")
			{
				foreach(string table in tablenames.ToArray())
				{
					try
					{
						if(CommandConsumer.Connection.Resources.GetTableInfo(table) != null)
						{
							tablenames.Remove(table);
						}
					}
					catch
					{ }
				}
			}
			else if(argline == "mismod")
			{
				ModulesTreeInfo root = CommandConsumer.Connection.Resources.GetModulesInfo("root");
				RemoveByModuleInfo(tablenames, root);
			}

			foreach(string table in tablenames)
				output.WriteLine(table);
		}

		private void RemoveByModuleInfo(List<String> tablenames, ModulesTreeInfo module)
		{
			if(!String.IsNullOrEmpty(module.TableName))
				tablenames.Remove(module.TableName);
			foreach(ModulesTreeInfo info in module.Items)
				RemoveByModuleInfo(tablenames, info);
		}

		public string GetHelp ()
		{
			return "Zobrazí tabulky v databázi, přepínač missing a mismod";
		}
	}
}
