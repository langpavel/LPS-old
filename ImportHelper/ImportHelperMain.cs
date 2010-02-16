using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;
using Npgsql;
using LPS;
using LPS.Client;

namespace ImportHelper
{
	public class MainClass
	{
		public static void Main (string[] args)
		{
			MainClass app = new MainClass(args);

			//app.MakeTreeTemplate("tree.xml", false);
			
			
			app.Connection.ConnectionString = "Server=127.0.0.1;Port=5432;Database=filmarena;Userid=filmarena;Password=filmArena3095;Protocol=3;Pooling=true;MinPoolSize=1;MaxPoolSize=20;ConnectionLifeTime=15;";
			app.Connection.Open();
			app.ProcessAddresses();
			app.Connection.Close();
			

		}

		public static string GetSHA1String(string data, string salt)
		{
			SHA1 sha1 = new SHA1Managed();
			byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data + salt));
			return Convert.ToBase64String(hash);
		}

		public NpgsqlConnection Connection { get; set; }
		
		private static int ID_DRUH_ADRESY_FA;
		private static LPS.Client.ServerConnection _LPS;
		public static LPS.Client.ServerConnection LPS
		{ 
			get
			{	
				if(_LPS != null)
					return _LPS;
				try
				{
					_LPS = new LPS.Client.ServerConnection("http://localhost/LPS/Server.asmx");
					_LPS.Login("langpa", "");
					object o = _LPS.ExecuteScalar("select id from c_druh_adresy where kod=:kod", "kod", "FA");
					ID_DRUH_ADRESY_FA = Convert.ToInt32(o);
					return _LPS;
				}
				catch
				{
					_LPS = null;
					throw;
				}
			}
		}
		
		private DataSet _Adresa;
		public DataSet Adresa
		{
			get
			{
				if(_Adresa != null)
					return _Adresa;
				return _Adresa = LPS.GetDataSetByName("adresa", "");
			}
		}
		
		public MainClass(string[] atgs)
		{
			Connection = new NpgsqlConnection();
		}
		
		public NpgsqlCommand GetCommand(string sql)
		{
			NpgsqlCommand cmd = Connection.CreateCommand();
			cmd.CommandText = sql;
			return cmd;
		}

		public void ProcessAddresses()
		{
			using(NpgsqlCommand cmd = GetCommand("select cislo,adresa from objednavky"))
			{
				NpgsqlDataReader reader = cmd.ExecuteReader();
				while(reader.Read())
				{
					string objed = reader[0] as string;
					string data = reader[1] as string;
					ProcessAddressRow(objed, data);
				}
			}
			int cnt = LPS.SaveDataSet(Adresa);
			Log.Debug("SAVE: {0}", cnt);
		}
		
		public void ProcessAddressRow(string objed, string packed)
		{
			string[] parts = packed.Split(new char[]{':','{','}',';'}, StringSplitOptions.RemoveEmptyEntries);
			switch(parts.Length)
			{
			case 68:
				Addr68(objed, packed, parts);
				break;
			case 116:
				Addr116(objed, packed, parts);
				break;
			default:
				Log.Debug(parts.Length.ToString() + ": " + packed);
				break;
			}
		}
		
		protected void WriteStrArray(string[] parts)
		{
			WriteStrArray(parts, 0, 1);
		}
		
		protected void WriteStrArray(string[] parts, int offset, int step)
		{
			for(int i = offset; i < parts.Length; i += step)
			{
				Console.WriteLine("{0}:\t{1}", i, parts[i]);
			}
		}
		
		protected bool WriteStrArray(string[] parts, int offset, int step, string compare)
		{
			bool result = true;
			for(int i = offset; i < parts.Length; i += step)
			{
				if(parts[i] != compare)
				{
					result = false;
					Console.WriteLine("{0}:\t{1}", i, parts[i]);
				}
			}
			return result;
		}

		private bool CheckStrArray(string[] parts, int offset, int step, string compare)
		{
			for(int i = offset; i < parts.Length; i += step)
				if(parts[i] != compare)
					return false;
			return true;
		}
		
		private Dictionary<string, string> GetDict(string[] parts)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			//WriteStrArray(parts, 4, 6); // keys
			if(CheckStrArray(parts, 2, 3, "s"))
			{
				for(int i=4; i<parts.Length; i+=6)
				{
					string key = parts[i].Trim('"');
					string val = parts[i+3].Trim('"');
					//Console.WriteLine("{0}: {1}", key, val);
					result[key] = val;
				}
			}
			return result;
		}

		private object StrOrDbNull(Dictionary<string, string> data, string key)
		{
			if(!data.ContainsKey(key))
				return DBNull.Value;
			string str = data[key];
			if(str == null || str.Trim() == "")
				return DBNull.Value;
			return str;
		}
		
		private void Addr68(string objed, string packed, string[] parts)
		{
			//WriteStrArray(parts, 0, 1); // keys
			Dictionary<string, string> data = GetDict(parts);
			string hash = GetSHA1String(packed, "");
			DataTable adr = Adresa.Tables[0];
			DataRow[] rows = adr.Select("import_php_str_hash='" + hash + "'");
			if(rows.Length == 0)
			{
				DataRow row = adr.NewRow();
				
				row["id"] = LPS.NextSeqValue("adresa_id_seq");
				row["import_php_str_hash"] = hash;
				row["import_objed_cislo"] = objed;

				row["id_druh_adresy"] = ID_DRUH_ADRESY_FA;
				row["nazev1"] = StrOrDbNull(data, "jmeno");
				row["nazev2"] = StrOrDbNull(data, "firma");
				row["prijmeni"] = StrOrDbNull(data, "prijmeni");
				row["jmeno"] = StrOrDbNull(data, "jmeno2");
				//row["jmeno2"] = StrOrDbNull(data, "jmeno");
				row["ulice"] = StrOrDbNull(data, "ulice");
				row["mesto"] = StrOrDbNull(data, "mesto");
				row["psc"] = StrOrDbNull(data, "psc");
				row["zeme"] = StrOrDbNull(data, "zeme");
				row["ico"] = StrOrDbNull(data, "ico");
				row["dic"] = StrOrDbNull(data, "dic");
				row["email"] = StrOrDbNull(data, "email");
				row["telefon1"] = StrOrDbNull(data, "telefon");
				
				row["aktivni"] = true;
				row["fakturacni"] = true;
				row["dodaci"] = true;
				row["dodavatel"] = false;
				row["odberatel"] = true;
				
				adr.Rows.Add(row);
			}
		}

		private void Addr116(string objed, string packed, string[] parts)
		{
			Addr68(objed, packed, parts);
			//WriteStrArray(parts, 4, 6); // keys
			//Dictionary<string, string> data = GetDict(parts);
			//string hash = GetSHA1String(packed, "");
			//DataTable adr = Adresa.Tables[0];
		}

		public void UpdateItemsColumns(ModulesTreeInfo items, bool recreate_cols)
		{
			foreach(ModulesTreeInfo item in items.Items)
			{
				if(!String.IsNullOrEmpty(item.ListSql))
				{
					if(recreate_cols)
						item.Columns.Clear();
					DataSet ds = null;
					try 
					{
						try
						{
							ds = LPS.GetDataSetSimple(item.ListSql + " where 1=0");
						}
						catch
						{
							ds = LPS.GetDataSetSimple(item.ListSql + " and 1=0");
						}
						Console.WriteLine(item.ListSql);
						foreach(DataColumn column in ds.Tables[0].Columns)
						{
							if(item.GetColumnInfo(column.ColumnName) != null)
								continue;
							ColumnInfo ci = new ColumnInfo();
							ci.Name = column.ColumnName;
							if(ci.Name.StartsWith("id_"))
							   ci.Caption = ci.Name.Substring(3).Replace('_', ' ');
							else
								ci.Caption = ci.Name.Replace('_', ' ');
							ci.Caption = ci.Caption[0].ToString().ToUpper() + ci.Caption.Substring(1);
							ci.Editable = true;
							ci.Visible = (ci.Name != "id");
							ci.Required = false;
							
							switch(ci.Name)
							{
							case "id_user_create":
								ci.Caption = "Vytvořil";
								ci.Editable = false;
								break;
							case "dt_create":
								ci.Caption = "Vytvořeno";
								ci.Editable = false;
								break;
							case "id_user_modify":
								ci.Caption = "Změnil";
								ci.Editable = false;
								break;
							case "dt_modify":
								ci.Caption = "Změněno";
								ci.Editable = false;
								break;
							}
							
							if(ci.Name == "id_group")
							{
								ci.Visible = false;
							}
							else if(ci.Name.StartsWith("id_user"))
							{
								ci.FkReferenceTable = "sys_user";
								ci.FkReplaceColumns = "surname, first_name";
								ci.DisplayFormat = "{1}, {2}";
							}
							else if(ci.Name.StartsWith("id_"))
							{
								ci.FkReferenceTable = ci.Name.Substring(3);
								ci.FkReplaceColumns = "kod";
								try
								{
									//FKMappedColumnHelper helper = new FKMappedColumnHelper(ci.FkReferenceTable, ci.FkReplaceColumns);
									//ci.DisplayFormat = helper.DisplayFormat;
								}
								catch 
								{
									try
									{
										string t2 = "c_" + ci.FkReferenceTable;
										ci.FkReferenceTable = t2;
										//FKMappedColumnHelper helper = new FKMappedColumnHelper(t2, ci.FkReplaceColumns);
										//ci.DisplayFormat = helper.DisplayFormat;
									}
									catch 
									{ 
										ci.FkReferenceTable = null;
										ci.FkReplaceColumns = null;
									}
								}
							}
							
							item.Columns.Add(ci);
						}
					}
					finally 
					{ 
						if(ds != null)
							ds.Dispose();
					}
				}
				UpdateItemsColumns(item, recreate_cols);
			}
		}
		
		public void MakeTreeTemplate(string filename, bool recreate_cols)
		{
			try
			{
				Console.WriteLine("Loading from server...");
				ModulesTreeInfo info = LPS.Resources.GetModulesInfo("root");
				UpdateItemsColumns(info, recreate_cols);
				info.SaveToFile(filename);
				
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
				
				ModulesTreeInfo root = new ModulesTreeInfo("root", null,null,null,null,null);
				ModulesTreeInfo adresar = new ModulesTreeInfo("adresar", "Adresář", "adresa", null, "select * from adresa", "Adresář");
				root.Items.Add(adresar);
				adresar.Items.Add(new ModulesTreeInfo("adresar_odb", "Odběratelé", "adresa", null, "select * from adresa", "Adresář odběratelů"));
				adresar.Items.Add(new ModulesTreeInfo("adresar_dod", "Dodavatelé", "adresa", null, "select * from adresa", "Adresář dodavatelů"));
				
				ModulesTreeInfo cisel = new ModulesTreeInfo("ciselniky", "Číselníky", null, null, null, null);
				root.Items.Add(cisel);
				cisel.Items.Add(new ModulesTreeInfo("c_dph", "DPH", "c_dph", null, "select * c_dph", "Hodnoty DPH"));
	
				root.SaveToFile(filename);
			}
		}
		
	}
}
