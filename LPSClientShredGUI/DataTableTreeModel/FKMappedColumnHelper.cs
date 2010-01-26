using System;
using System.Data;

namespace LPSClient
{
	public class FKMappedColumnHelper
	{
		private DataTable referencedTable;
		public string DisplayFormat { get; set; }
		
		public FKMappedColumnHelper(string table, string refCols)
		{
			string sql = String.Format("select id, {1} from {0}", table, refCols);
			this.referencedTable = ServerConnection.Instance.GetCachedDataSet(sql).Tables[0];
			this.DisplayFormat = "{1}";
			for(int i = 2; i < this.referencedTable.Columns.Count; i++)
			{
				this.DisplayFormat += "; {" + i.ToString() + "}";
		    }
		}
		
		public string GetDisplayValue(object val, DataRow row)
		{
			if(val == null || val is DBNull)
				return "";
			try
			{
				DataRow refrow = referencedTable.Rows.Find(val);
				return String.Format(this.DisplayFormat, refrow.ItemArray);
			}
			catch
			{
				return "<span color=\"#ff0000\">(err)</span>";
			}
		}
			
	}
}
