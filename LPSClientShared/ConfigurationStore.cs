using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace LPS.Client
{
	public class ConfigurationStore : IDisposable
	{
		public ServerConnection Connection { get { return ServerConnection.Instance; } }

		public ConfigurationStore()
		{
		}

		private DataSet config_ds;
		public DataSet ConfigDs
		{
			get
			{
				return config_ds ??
					(config_ds = Connection.GetDataSetByName("sys_user_preferences", "id_user=0 or id_user=:id_user", "id_user", Connection.UserId));
			}
		}
		public DataTable ConfigTable
		{
			get { return ConfigDs.Tables[0]; }
		}

		public List<string> GetAvailableConfigurations(string path, bool include_systemwide)
		{
			List<string> result = new List<string>();
			foreach(DataRow r in ConfigTable.Rows)
			{
				if(path.Equals(r["path"])
					&& (include_systemwide || Connection.UserId.Equals(r["id_user"])))
				{
					if(result.IndexOf((string)r["name"]) < 0)
						result.Add((string)r["name"]);
				}
			}
			result.Sort();
			return result;
		}

		private bool HasInterface(Type type, Type interface_type)
		{
			foreach(Type intf in type.GetInterfaces())
				if(intf == interface_type)
					return true;
			return false;
		}

		public void DeleteConfiguration(string path, string name)
		{
			DataRow r = FindConfigurationRow(path, name);
			if(r != null)
			{
				r.Delete();
				Connection.SaveDataSet(this.ConfigDs);
			}
		}

		public void SaveConfiguration(string path, string name, object val)
		{
			if(val == null)
				throw new ArgumentNullException("val");
			Type type = val.GetType();
			string type_name = type.Name;
			string str_val;
			if(val is IConfiguration)
			{
				type_name = "conf:" + type_name;
				str_val = ((IConfiguration)val).Save();
			}
			else if(val is IConvertible)
			{
				type_name = "simple:" + type_name;
				str_val = ((IConvertible)val).ToString(CultureInfo.InvariantCulture);
			}
			else // try xml serialize
			{
				type_name = "xml:" + type_name;
				StringBuilder sb = new StringBuilder();
				using(StringWriter tw = new StringWriter(sb))
				using(XmlTextWriter writer = new XmlTextWriter(tw))
				{
					XmlSerializer xser = new XmlSerializer(type);
					xser.Serialize(writer, val);
				}
				str_val = sb.ToString();
			}
			DataRow r = FindConfigurationRow(path, name);
			if(r == null)
			{
				r = this.ConfigTable.NewRow();
				r["id"] = this.Connection.NextSeqValue("sys_user_preferences_id_seq");
				r["path"] = path;
				r["name"] = name;
				r["id_user"] = Connection.UserId;
				this.ConfigTable.Rows.Add(r);
			}
			r["type"] = type_name;
			r["value"] = str_val;
			Connection.SaveDataSet(this.ConfigDs);
		}

		private DataRow FindConfigurationRow(string path, string name)
		{
			foreach(DataRow r in ConfigTable.Rows)
			{
				if(path.Equals(r["path"]) && name.Equals(r["name"]) && Connection.UserId.Equals(r["id_user"]))
					return r;
			}
			return null;
		}

		public T GetConfiguration<T>(string path, string name, T default_val)
		{
			return (T)GetConfiguration(path, name, typeof(T), default_val);
		}

		public object GetConfiguration(string path, string name, Type type, object default_val)
		{
			try
			{
				Connection.CheckChanges();
				string g_val = null;
				string g_type = null;
				string usr_val = null;
				string usr_type = null;
				foreach(DataRow r in ConfigTable.Rows)
				{
					if(path.Equals(r["path"]) && name.Equals(r["name"]))
					{
						long idusr = Convert.ToInt64(r["id_user"]);
						if(idusr == 0)
						{
							g_val = r["value"] as string;
							g_type = r["type"] as string;
						}
						else
						{
							usr_val = r["value"] as string;
							usr_type = r["type"] as string;
						}
					}
				}
				if(usr_val != null)
					return RestoreObject(type, usr_type, usr_val);
				else if(g_val != null)
					return RestoreObject(type, g_type, g_val);
				else return default_val;
			}
			catch(Exception ex)
			{
				throw new ApplicationException(
					String.Format("Nelzdařilo se načíst konfiguraci '{0}', hodnotu {1}", path, name),
					ex);
			}
		}

		private object RestoreObject(Type type, string stored_type, string val)
		{
			if(stored_type == ("xml:" + type.Name))
			{
				using(StringReader sr = new StringReader(val))
				{
					XmlSerializer xser = new XmlSerializer(type);
					return xser.Deserialize(sr);
				}
			}
			if(stored_type == ("conf:" + type.Name) && HasInterface(type, typeof(IConfiguration)))
			{
				object result = Activator.CreateInstance(type);
				IConfiguration conf = (IConfiguration)result;
				conf.Load(val);
				return result;
			}
			if(stored_type == ("simple:" + type.Name) && HasInterface(type, typeof(IConvertible)))
			{
				return Convert.ChangeType(val, type, CultureInfo.InvariantCulture);
			}
			throw new ApplicationException(String.Format("Nelze obnovit hodnotu typu {0} z uložené hodnoty typu {1}",
				type.Name, stored_type));
		}

		public void Dispose ()
		{
			if(config_ds != null)
			{
				Connection.DisposeDataSet(config_ds);
				config_ds = null;
			}
		}

	}
}
