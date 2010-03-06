/*
using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using LPS.Client;

namespace LPS.Util
{
	public class XtableCommand : CommandBase
	{
		public XtableCommand(string Name)
			: base(Name)
		{
		}

		public static DataSet GetTableInfoDataSet(ServerConnection conn, string table_name)
		{
			DataSet ds = conn.GetDataSetBySql(String.Format(
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

		public override object Execute (LPS.ToolScript.IExecutionContext context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			string tabname = Get<string>(Params, 0);
			string UserFriendTitle = Capitalize(tabname, true, "sys_", "c_");
			ServerConnection conn = (ServerConnection)context.LocalVariables["ServerConnection"];
			using(DataSet ds = GetTableInfoDataSet(conn, tabname))
			{
				ITableInfo info;
				try
				{
					info = conn.Resources.GetTableInfo(tabname);
					info = info.Clone();
					Log.Info("Updating TableInfo name {0}", tabname);
					if(String.IsNullOrEmpty(info.Id))
						info.Id = tabname;
				}
				catch(Exception err)
				{
					Log.Error(err);
					info = new ITableInfo();
					Log.Info("Creating new TableInfo name {0}", tabname);
					info.TableName = tabname;
					info.ListSql = "select * from " + tabname + " {where}";
					info.Description = "Tabulka " + UserFriendTitle;
					info.DetailCaption = UserFriendTitle;
					info.DetailName = "generic";
					info.Id = tabname;
					info.Text = UserFriendTitle;
				}
	
				bool has_kod = false;
				bool has_popis = false;
	
				List<IColumnInfo> cols = new List<IColumnInfo>();
				foreach(DataRow r in ds.Tables[0].Rows)
				{
					string column_name = (string)r["column_name"];
					if(column_name == "kod")
						has_kod = true;
					if(column_name == "popis")
						has_popis = true;
					ColumnInfo colinfo = info.GetColumnInfo(column_name);
					if(colinfo == null)
					{
						colinfo = new ColumnInfo();
						colinfo.Name = column_name;
						Log.Info("Vytvořen sloupec {0}", colinfo.Name);
						colinfo.Caption = Capitalize(column_name, true, "id_");
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
	
				if(String.IsNullOrEmpty(info.LookupReplaceFormat) && has_kod)
					info.LookupReplaceFormat = "kod";
				if((info.LookupColumns == null || info.LookupColumns.Length == 0) && (has_kod || has_popis))
				{
					List<string> l = new List<string>();
					if(has_kod) l.Add("kod");
					if(has_popis) l.Add("popis");
					info.LookupColumns = l.ToArray();
				}
				if(String.IsNullOrEmpty(info.DetailName))
					info.DetailName = "generic";
	
				XmlSerializer xser = new XmlSerializer(typeof(ITableInfo));
				using(XmlTextWriter writer = new XmlTextWriter(Out))
				{
					writer.Formatting = Formatting.Indented;
					writer.Indentation = 1;
					writer.IndentChar = '\t';
					xser.Serialize(writer, info);
				}
				return info;
			}
		}

		public override string Help
		{
			get { return "vrátí aktualizované TableInfo XML"; }
		}
	}
}
*/