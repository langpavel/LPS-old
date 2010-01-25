using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace LPSClient
{
	[Serializable]
	[XmlRoot("column")]
	public class ColumnInfo
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("caption")]
		public string Caption { get; set; }

		[XmlAttribute("visible")]
		public bool Visible { get; set; }

		[XmlAttribute("editable")]
		public bool Editable { get; set; }

		[XmlAttribute("required")]
		public bool Required { get; set; }

		[XmlAttribute("width")]
		public int Width { get; set; }
		
		[XmlAttribute("ref-table")]
		public string FkReferenceTable { get; set; }
	}
}