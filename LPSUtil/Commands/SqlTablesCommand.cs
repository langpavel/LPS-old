using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using LPS.Client;

namespace LPS.Util
{
	public class SqlTablesCommand : CommandBase
	{
		public SqlTablesCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "zobrazí tabulky v databázi, přepínač missing a mismod"; }
		}

		public string[] GetSqlTableNames(ServerConnection conn)
		{
			List<string> tables = new List<string>();
			using(DataSet ds = conn.GetDataSetBySql(@"
				select distinct table_name from information_schema.columns where table_schema = 'public'"))
			{
				foreach(DataRow r in ds.Tables[0].Rows)
					tables.Add((string)r[0]);
			}
			tables.Sort();
			return tables.ToArray();
		}

		public override object Execute(LPS.ToolScript.IExecutionContext context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			ServerConnection conn = (ServerConnection)context.LocalVariables["ServerConnection"];
			List<string> tablenames = new List<string>(GetSqlTableNames(conn));
			if(Get<string>(Params, 0) == "missing")
			{
				foreach(string table in tablenames.ToArray())
				{
					try
					{
						if(conn.Resources.GetTableInfo(table) != null)
						{
							tablenames.Remove(table);
						}
					}
					catch
					{
					}
				}
			}
			else if(Get<string>(Params, 0) == "mismod")
			{
				ModulesTreeInfo root = conn.Resources.GetModulesInfo("root");
				RemoveByModuleInfo(tablenames, root);
			}

			foreach(string table in tablenames)
				Out.WriteLine(table);
			return tablenames.ToArray();
		}

		private void RemoveByModuleInfo(List<String> tablenames, ModulesTreeInfo module)
		{
			if(!String.IsNullOrEmpty(module.TableName))
				tablenames.Remove(module.TableName);
			foreach(ModulesTreeInfo info in module.Items)
				RemoveByModuleInfo(tablenames, info);
		}
	}
}
