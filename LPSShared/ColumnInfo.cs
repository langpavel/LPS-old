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

		[XmlAttribute("unique")]
		public bool Unique { get; set; }

		[XmlAttribute("width")]
		public int Width { get; set; }

		[XmlAttribute("max-length")]
		public int? MaxLength { get; set; }
		
		[XmlAttribute("default")]
		public string Default { get; set; }

		[XmlElement("ref-table")]
		public string FkReferenceTable { get; set; }
		
		[XmlElement("replace-columns")]
		public string FkListReplaceFormat { get; set; }
		
		[XmlElement("combo-replace-columns")]
		public string FkComboReplaceColumns { get; set; }

		[XmlElement("lookup-method")]
		public string LookupMethod { get; set; }

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
				if(!String.IsNullOrEmpty(this.FkComboReplaceColumns))
				{
					return this.FkComboReplaceColumns.Split(
						new char[] {',',';',' ',':','-','\'','"', '[', ']', '(', ')', '{', '}', '<', '>'},
						StringSplitOptions.RemoveEmptyEntries);
				}
				if(!String.IsNullOrEmpty(this.FkReferenceTable))
				{
					TableInfo tableInfo = ResourceManager.Instance.GetTableInfo(this.FkReferenceTable);
					ILookupInfo look = (ILookupInfo)tableInfo;
					return look.LookupColumns;
				}
				Log.Warning("Sloupec {0} ({1} - {2}): Nenalezena hodnota", this.Name, this.Caption, this.Description);
				return new string[] { };
			}
		}

		string ILookupInfo.FkListReplaceFormat
		{
			get
			{
				if(!String.IsNullOrEmpty(this.FkListReplaceFormat))
					return this.FkListReplaceFormat;
				if(!String.IsNullOrEmpty(this.FkReferenceTable))
				{
					TableInfo tableInfo = ResourceManager.Instance.GetTableInfo(this.FkReferenceTable);
					ILookupInfo look = (ILookupInfo)tableInfo;
					string fmt = look.FkListReplaceFormat;
					if(!String.IsNullOrEmpty(fmt))
						return fmt;
				}
				Log.Warning("Sloupec {0} ({1} - {2}): Nenalezena hodnota, poziti id", this.Name, this.Caption, this.Description);
				return "{id}";
			}
		}

		string ILookupInfo.LookupMethod
		{
			get
			{
				if(!String.IsNullOrEmpty(this.LookupMethod))
					return this.LookupMethod;
				if(!String.IsNullOrEmpty(this.FkReferenceTable))
				{
					TableInfo tableInfo = ResourceManager.Instance.GetTableInfo(this.FkReferenceTable);
					return tableInfo.LookupMethod;
				}
				return null;
			}
		}
		#endregion

		public bool IsForeignKey
		{
			get { return !String.IsNullOrEmpty(this.FkReferenceTable); }
		}
   	}
}