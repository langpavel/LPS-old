using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace LPS
{
	[Serializable]
	public class TableInfo
	{
		public TableInfo ()
		{
			Columns = new List<ColumnInfo>();
		}
		
		public string Name { get; set; }

		[XmlArray("columns")]
		[XmlArrayItem("column")]
		public List<ColumnInfo> Columns { get; set; }
		
	}
}
