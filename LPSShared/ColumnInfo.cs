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
	[XmlRoot("column")]
	public class ColumnInfo : ICloneable, ILookupInfo
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
		
		[XmlElement("ref-table")]
		public string FkReferenceTable { get; set; }
		
		[XmlElement("replace-columns")]
		public string FkReplaceColumns { get; set; }
		
		[XmlElement("combo-replace-columns")]
		public string FkComboReplaceColumns { get; set; }
		
		[XmlAttribute("display")]
		public string DisplayFormat { get; set; }
		
		[XmlElement("desc")]
		public string Description { get; set; }
		
		#region ICloneable implementation
		object ICloneable.Clone()
		{
			return this.Clone();
		}
		
		public virtual ColumnInfo Clone()
		{
			ColumnInfo clone = (ColumnInfo)this.MemberwiseClone();
			return clone;
		}
		#endregion

		#region ILookupInfo implementation
		string ILookupInfo.LookupTable
		{
			get { return this.FkReferenceTable; }
		}

		string[] ILookupInfo.LookupColumns
		{
			get
			{
				string str = this.FkComboReplaceColumns ?? this.FkReplaceColumns ?? (String.IsNullOrEmpty(this.FkReferenceTable)?"":"kod");
				return str.Split(
					new char[] {' ',',',';','[',']','{','}','(',')','\'','"','-',':'},
					StringSplitOptions.RemoveEmptyEntries);
			}
		}
		#endregion

      	}
}