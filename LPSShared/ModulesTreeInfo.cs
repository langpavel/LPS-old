using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace LPS
{

	[Serializable]
	[XmlRoot("modules-tree")]
	public class ModulesTreeInfo
	{
		public ModulesTreeInfo ()
		{
			Items = new List<ModulesTreeInfo>();
			Columns = new List<ColumnInfo>();
			Data = new Hashtable();
		}
		
		public ModulesTreeInfo (string id, string text, string detailName, string iconName, string listSql, string desc)
			:this()
		{
			Id = id;
			Text = text;
			DetailName = detailName;
			IconName = iconName;
			ListSql = listSql;
			Description = desc;
		}
		
		[XmlAttribute("id")]
		public string Id { get; set; }
		
		[XmlAttribute("text")]
		public string Text { get; set; }
		
		[XmlElement("desc")]
		public string Description { get; set; }

		[XmlElement("detail-name")]
		public string DetailName { get; set; }

		[XmlElement(ElementName="icon")]
		public string IconName { get; set; }
		
		[XmlElement("list-sql")]
		public string ListSql { get; set; }
		
		[XmlArray("columns")]
		[XmlArrayItem("column")]
		public List<ColumnInfo> Columns { get; set; }
		
		[XmlElement("item")]
		public List<ModulesTreeInfo> Items { get; set; }
		
		public void SaveToFile(string filename)
		{
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
			settings.IndentChars = "\t";
            settings.Encoding = Encoding.UTF8;
            using (XmlWriter xwriter = XmlWriter.Create(filename, settings))
            {
                XmlSerializer xserializer = new XmlSerializer(typeof(ModulesTreeInfo));
                xserializer.Serialize(xwriter, this);
            }
        }

		public static ModulesTreeInfo Load(XmlReader xreader)
        {
            XmlSerializer xserializer = new XmlSerializer(typeof(ModulesTreeInfo));
            ModulesTreeInfo result = (ModulesTreeInfo)xserializer.Deserialize(xreader);
            return result;
        }

		public static ModulesTreeInfo LoadFromFile(string filename)
		{
			using(XmlReader reader = XmlReader.Create(filename))
				return Load(reader);
		}
		
		public static ModulesTreeInfo LoadFromString(string data)
		{
			using(StringReader reader = new StringReader(data))
			using(XmlReader xreader = XmlReader.Create(reader))
				return Load(xreader);
		}
		
		public override string ToString ()
		{
			return string.Format("[ModulesTreeInfo: Id={0}, Text={1}, Description={2}, DetailName={3}", Id, Text, Description, DetailName);
		}
		
		public ColumnInfo GetColumnInfo(string name)
		{
			foreach(ColumnInfo col in this.Columns)
			{
				if(col.Name == name)
					return col;
			}
			return null;
		}
		
		[XmlIgnore]
		public Hashtable Data { get; set; }
		
		[XmlIgnore]
		public ModulesTreeInfo Parent { get; set; }
	}
}
