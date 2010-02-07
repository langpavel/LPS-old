using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace LPS
{
	[Serializable]
	[XmlRoot("table")]
	public class TableInfo
	{
		public TableInfo ()
		{
			Columns = new List<ColumnInfo>();
		}
		
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlElement("detail-caption")]
		public string DetailCaption { get; set; }
		
		[XmlElement("desc")]
		public string Description { get; set; }
		
		[XmlElement("list-sql")]
		public string ListSql { get; set; }
		
		[XmlElement("edit-sql")]
		public string EditSql { get; set; }
		
		[XmlArray("columns")]
		[XmlArrayItem("column")]
		public List<ColumnInfo> Columns { get; set; }
		
		public ColumnInfo GetColumnInfo(string name)
		{
			foreach(ColumnInfo col in this.Columns)
			{
				if(col.Name == name)
					return col;
			}
			return null;
		}
		
	}
}
