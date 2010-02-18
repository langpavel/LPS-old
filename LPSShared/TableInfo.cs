using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace LPS
{
	[Serializable]
	[XmlRoot("table")]
	public class TableInfo : IListInfo, ICloneable, ILookupInfo
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

		[XmlAttribute("text")]
		public string Text { get; set; }

		[XmlElement("detail-caption")]
		public string DetailCaption { get; set; }
		
		[XmlElement("desc")]
		public string Description { get; set; }
		
		[XmlElement("list-sql")]
		public string ListSql { get; set; }
		
		[XmlElement("detail-name")]
		public string DetailName { get; set; }

		/// <summary>
		/// sloupce pro zobrazeni v comboboxu
		/// </summary>
		[XmlArray("lookup-columns")]
		[XmlArrayItem("column")]
		public string[] LookupColumns { get; set; }

		/// <summary>
		/// Text pro vlozeni
		/// </summary>
		[XmlElement("lookup-replace-format")]
		public string LookupReplaceFormat { get; set; }

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

		public virtual TableInfo Clone()
		{
			TableInfo result = (TableInfo)this.MemberwiseClone();
			result.Columns = new List<ColumnInfo>();
			foreach(ColumnInfo column in this.Columns)
			{
				result.Columns.Add(column.Clone());
			}
			return result;
		}

		object ICloneable.Clone ()
		{
			return this.Clone();
		}

		#region ILookupInfo implementation
		string ILookupInfo.LookupTable
		{
			get	{ return this.TableName; }
		}

		string[] ILookupInfo.LookupColumns
		{
			get
			{
				if(this.LookupColumns != null && this.LookupColumns.Length > 0)
					return this.LookupColumns;
				Log.Warning("Tabulka {0} nemá hodnoty pro lookup", this.Id);
				List<string> l = new List<string>();
				if(this.GetColumnInfo("kod") != null) l.Add("kod");
				if(this.GetColumnInfo("popis") != null) l.Add("popis");
				if(l.Count == 0) l.Add("id");
				return l.ToArray();
			}
		}

		string ILookupInfo.FkListReplaceFormat
		{
			get { return this.LookupReplaceFormat; }
		}
		#endregion
	}
}
