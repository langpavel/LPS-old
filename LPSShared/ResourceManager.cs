using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using System.IO;

namespace LPS
{
	public class ResourceManager: IDisposable
	{
		private static ResourceManager instance;
		public static ResourceManager Instance { get { return instance; } }
		
		#region Serialization helpers
		public static T DeserializeObject<T>(string data)
		{
			return (T)DeserializeObject(data, typeof(T));
		}
		
		public static object DeserializeObject(string data, Type type)
		{
			XmlSerializer ser = new XmlSerializer(type);
			using(StringReader sr = new StringReader(data))
			{
				return ser.Deserialize(sr);
			}
		}
		
		public T LoadAndDeserialize<T>(string path)
		{
			string data = server.GetTextResource(path);
			if(String.IsNullOrEmpty(data))
				throw new ResourceNotFoundException(String.Format("Nenalezeno resource {0}", path));
			return DeserializeObject<T>(data);
		}

		private T GetCachedObject<T>(string path_base, string name, Dictionary<string, T> cache)
		{
			T result;
			if(cache.TryGetValue(name, out result))
				return result;
			result = LoadAndDeserialize<T>(Path.Combine(path_base, name + ".xml"));
			cache[name] = result;
			return result;
		}
		
		#endregion
		
		private Dictionary<string, TableInfo> TableInfos;
		private Dictionary<string, ModulesTreeInfo> ModulesInfo;
		private IResourceStore server;
		
		public ResourceManager(IResourceStore server)
		{
			this.server = server;
			TableInfos = new Dictionary<string, TableInfo>();
			ModulesInfo = new Dictionary<string, ModulesTreeInfo>();
			instance = this;
		}
		
		public TableInfo GetTableInfo(string name)
		{
			return GetCachedObject<TableInfo>("tables", name, TableInfos);
		}
		
		private void PopulateModulesTree(ModulesTreeInfo info)
		{
			foreach(ModulesTreeInfo item in info.Items)
			{
				item.Parent = info;
				string id = item.Id;
				if(ModulesInfo.ContainsKey(id))
					throw new ApplicationException("tree.xml obsahuje duplicitní id");
				ModulesInfo[id] = item;
				PopulateModulesTree(item);
			}
		}
		
		public ModulesTreeInfo FindModulesInfo(string name)
		{
			ModulesTreeInfo result;
			if(ModulesInfo.Count == 0)
			{
				result = LoadAndDeserialize<ModulesTreeInfo>("tree.xml");
				ModulesInfo["root"] = result;
				PopulateModulesTree(result);
			}
			if(ModulesInfo.TryGetValue(name, out result))
				return result;
			return null;
		}

		public ModulesTreeInfo GetModulesInfo(string name)
		{
			ModulesTreeInfo result = FindModulesInfo(name);
			if(result == null)
				throw new ResourceNotFoundException(String.Format("Nenalezeno resource tree.xml/{0}", name));
			return result;
		}

		public IListInfo GetListInfo(string name)
		{
			return ((IListInfo)FindModulesInfo(name)) ?? ((IListInfo)GetTableInfo(name));
		}

		public virtual void Dispose()
		{
		}
	}
}
