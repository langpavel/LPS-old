using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace LPS
{
	[Serializable]
	[XmlRoot("table")]
	public class TableInfo : IListInfo
	{
		public TableInfo ()
		{
			Columns = new List<ColumnInfo>();
		}
		
		[XmlIgnore]
		public ListInfoKind Kind { get { return ListInfoKind.Table; } }

		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlAttribute("name")]
		public string TableName { get; set; }

		[XmlElement("detail-caption")]
		public string DetailCaption { get; set; }
		
		[XmlElement("desc")]
		public string Description { get; set; }
		
		[XmlElement("list-sql")]
		public string ListSql { get; set; }
		
		[XmlElement("detail-name")]
		public string DetailName { get; set; }
		
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
