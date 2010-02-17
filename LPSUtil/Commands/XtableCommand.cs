using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace LPS.Util
{
	public class XtableCommand : ICommand
	{
		public XtableCommand()
		{
		}

		public static DataSet GetTableInfoDataSet(string table_name)
		{
			if(CommandConsumer.Connection == null)
				throw new Exception("login required");

			DataSet ds = CommandConsumer.Connection.GetDataSetBySql(String.Format(
				@"SELECT cols.ordinal_position, cols.column_name, cols.udt_name, cols.is_nullable,
					cols.character_maximum_length, cols.numeric_precision, cols.numeric_precision_radix, cols.numeric_scale,
					tc.constraint_type, tc.constraint_name, tc.table_name, kcu.column_name,
					ccu.table_name AS references_table,
					ccu.column_name AS references_field
				FROM information_schema.columns cols
				LEFT JOIN information_schema.key_column_usage kcu
					ON cols.column_name = kcu.column_name
					AND cols.table_schema = kcu.table_schema
					AND cols.table_name = kcu.table_name
				LEFT JOIN information_schema.table_constraints tc
					ON tc.constraint_catalog = kcu.constraint_catalog
					AND tc.constraint_schema = kcu.constraint_schema
					AND tc.constraint_name = kcu.constraint_name
				LEFT JOIN information_schema.referential_constraints rc
					ON tc.constraint_catalog = rc.constraint_catalog
					AND tc.constraint_schema = rc.constraint_schema
					AND tc.constraint_name = rc.constraint_name
				LEFT JOIN information_schema.constraint_column_usage ccu
					ON rc.unique_constraint_catalog = ccu.constraint_catalog
					AND rc.unique_constraint_schema = ccu.constraint_schema
					AND rc.unique_constraint_name = ccu.constraint_name
				WHERE cols.table_schema = 'public'
					and cols.table_name = '{0}'
				ORDER BY cols.ordinal_position", table_name));
			return ds;
		}

		public static string Capitalize(string text, bool remove_underscores, params string[] remove_start)
		{
			string s = text;
			foreach(string str in remove_start)
			{
				if(s.StartsWith(str))
				{
					s = s.Substring(str.Length);
					break;
				}
			}
			if(remove_underscores)
				s = s.Replace('_', ' ');
			return s[0].ToString().ToUpper() + s.Substring(1);
		}

		public void Execute (string cmd_name, string argline, TextWriter output)
		{
			string[] args = argline.Split(new char[] {' ',',',';'}, StringSplitOptions.RemoveEmptyEntries);
			foreach(string tabname in args)
			{
				string UserFriendTitle = Capitalize(tabname, true, "sys_", "c_");
				using(DataSet ds = GetTableInfoDataSet(tabname))
				{
					TableInfo info;
					try
					{
						info = CommandConsumer.Connection.Resources.GetTableInfo(tabname);
						info = info.Clone();
						Log.Info("Updating TableInfo name {0}", tabname);
					}
					catch
					{
						info = new TableInfo();
						Log.Info("Creating new TableInfo name {0}", tabname);
						info.TableName = tabname;
						info.ListSql = "select * from " + tabname + " {where}";
						info.Description = "Tabulka " + UserFriendTitle;
						info.DetailCaption = UserFriendTitle;
						info.DetailName = "generic";
						info.Id = tabname;
						info.Text = UserFriendTitle;
					}

					List<ColumnInfo> cols = new List<ColumnInfo>();
					foreach(DataRow r in ds.Tables[0].Rows)
					{
						ColumnInfo colinfo = info.GetColumnInfo((string)r["column_name"]);
						if(colinfo == null)
						{
							colinfo = new ColumnInfo();
							colinfo.Name = (string)r["column_name"];
							colinfo.Caption = Capitalize((string)r["column_name"], true, "id_");
							colinfo.Visible = true;
							colinfo.Description = colinfo.Caption;
							colinfo.Editable = true;
						}
						else
						{
							colinfo = colinfo.Clone();
						}
						colinfo.Required = "NO".Equals(r["is_nullable"]);
						colinfo.Unique = ("UNIQUE".Equals(r["constraint_type"]) || "PRIMARY KEY".Equals(r["constraint_type"]));
						colinfo.FkReferenceTable = r["references_table"] as string;
						switch(colinfo.Name)
						{
						case "ts":
						case "id_user_create":
						case "dt_create":
						case "id_user_modify":
						case "dt_modify":
							colinfo.Editable = false;
							break;
						}
						cols.Add(colinfo);
					}
					info.Columns.Clear();
					info.Columns.AddRange(cols);
					XmlSerializer xser = new XmlSerializer(typeof(TableInfo));
					using(XmlTextWriter writer = new XmlTextWriter(output))
					{
						writer.Formatting = Formatting.Indented;
						writer.Indentation = 1;
						writer.IndentChar = '\t';
						xser.Serialize(writer, info);
					}
				}
			}
		}

		public string GetHelp ()
		{
			return "args: [new] {table_name}: create updated or new xml file from table name";
		}
	}
}
