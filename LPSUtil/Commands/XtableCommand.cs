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

		public void Execute (CommandConsumer consumer, string cmd_name, string argline, TextWriter output)
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
					catch(Exception err)
					{
						Log.Error(err);
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
							Log.Info("Vytvořen sloupec {0}", colinfo.Name);
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
						switch(r["udt_name"] as string)
						{
						case "date":
							colinfo.DisplayFormat = "dd.MM.yyyy";
							break;
						case "timestamp":
							colinfo.DisplayFormat = "dd.MM.yyyy hh':'mm':'ss";
							break;
						case "varchar":
							int length;
							if(r["character_maximum_length"].IsNotNull(out length))
								colinfo.MaxLength = length;
							break;
						}
						switch(colinfo.Name)
						{
						case "id":
							colinfo.Caption = "ID";
							colinfo.Description = "Identifikátor";
							colinfo.Unique = true;
							colinfo.Visible = false;
							colinfo.Editable = false;
							break;
						case "ts":
							colinfo.Caption = "Časová značka";
							colinfo.Description = "Časová značka poslední změny";
							colinfo.Visible = false;
							colinfo.Editable = false;
							colinfo.DisplayFormat = "yyyy-MM-dd hh:mm:ss.ffffff";
							break;
						case "id_user_create":
							colinfo.Editable = false;
							colinfo.Caption = "Vytvořil";
							colinfo.Description = "Vytvořil uživatel";
							break;
						case "dt_create":
							colinfo.Editable = false;
							colinfo.Caption = "Vytvořeno";
							colinfo.Description = "Vytvořeno dne";
							break;
						case "id_user_modify":
							colinfo.Editable = false;
							colinfo.Caption = "Změnil";
							colinfo.Description = "Změněno uživatelem";
							break;
						case "dt_modify":
							colinfo.Editable = false;
							colinfo.Caption = "Změněno";
							colinfo.Description = "Změněno dne";
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
