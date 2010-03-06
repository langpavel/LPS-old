using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace LPS
{
	[Serializable]
	[XmlRoot("table")]
	[Obsolete("Use IDBTable")]
	public class TableInfo : ITableInfo
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
		public string Category { get; set; }

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

		[XmlElement("lookup-method")]
		public string LookupMethod { get; set; }

		[XmlArray("columns")]
		[XmlArrayItem("column")]
		public List<ColumnInfo> Columns { get; set; }

		IColumnInfo[] IListInfo.Columns { get { return Columns.ToArray(); } }

		public IColumnInfo GetColumnInfo(string name)
		{
			foreach(IColumnInfo col in this.Columns)
			{
				if(col.Name == name)
					return col;
			}
			return null;
		}

		public virtual object Clone()
		{
			TableInfo result = (TableInfo)this.MemberwiseClone();
			result.Columns = new List<ColumnInfo>();
			foreach(IColumnInfo column in this.Columns)
				result.Columns.Add((ColumnInfo)column.Clone());
			return result;
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
