using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Npgsql;

namespace ImportHelper
{
	public class MainClass
	{
		public static void Main (string[] args)
		{
			new MainClass(args);
		}

		public static string GetSHA1String(string data, string salt)
		{
			SHA1 sha1 = new SHA1Managed();
			byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data + salt));
			return Convert.ToBase64String(hash);
		}

		public NpgsqlConnection Connection { get; set; }
		
		private static int ID_DRUH_ADRESY_FA;
		private static LPSClient.ServerConnection _LPS;
		public static LPSClient.ServerConnection LPS
		{ 
			get
			{	
				if(_LPS != null)
					return _LPS;
				try
				{
					_LPS = new LPSClient.ServerConnection("http://localhost:8080/Server.asmx");
					_LPS.Login("langpa", "michala");
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
				return _Adresa = LPS.GetDataSet("select * from adresa", true);
			}
		}
		
		public MainClass(string[] atgs)
		{
			Console.WriteLine ("Hello World!");
			Connection = new NpgsqlConnection();
			Connection.ConnectionString = "Server=127.0.0.1;Port=5432;Database=filmarena;Userid=filmarena;Password=filmArena3095;Protocol=3;Pooling=true;MinPoolSize=1;MaxPoolSize=20;ConnectionLifeTime=15;";
			Connection.Open();
			
			ProcessAddresses();
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
				Console.WriteLine(parts.Length.ToString() + ": " + packed);
				break;
			}
		}
		
		private void WriteStrArray(string[] parts)
		{
			WriteStrArray(parts, 0, 1);
		}
		
		private void WriteStrArray(string[] parts, int offset, int step)
		{
			for(int i = offset; i < parts.Length; i += step)
			{
				Console.WriteLine("{0}:\t{1}", i, parts[i]);
			}
		}
		
		private bool WriteStrArray(string[] parts, int offset, int step, string compare)
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
				Console.WriteLine("SAVE: {0}", LPS.SaveDataSet(Adresa));
			}
		}

		private void Addr116(string objed, string packed, string[] parts)
		{
			//WriteStrArray(parts, 4, 6); // keys
			Dictionary<string, string> data = GetDict(parts);
			string hash = GetSHA1String(packed, "");
			DataTable adr = Adresa.Tables[0];
		}

	}
}
