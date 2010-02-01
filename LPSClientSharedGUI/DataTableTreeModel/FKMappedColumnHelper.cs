using System;
using System.Data;

namespace LPS.Client
{
	public class FKMappedColumnHelper
	{
		private DataTable referencedTable;
		private string DisplayFormat { get; set; }
		
		public FKMappedColumnHelper(string table, string refCols)
		{
			//string sql = String.Format("select id, {1} from {0}", table, refCols);
			this.referencedTable = ServerConnection.Instance.GetCachedDataSet(table).Tables[0];
			string[] cols = refCols.Split(new char[] {',',';',' ',':','-','\'','"', '[', ']', '(', ')'}, StringSplitOptions.RemoveEmptyEntries);
			this.DisplayFormat = refCols;
			for(int i = 0; i < cols.Length; i++)
			{
				int idx = this.referencedTable.Columns.IndexOf(cols[i]);
				if(idx >= 0)
					this.DisplayFormat = this.DisplayFormat.Replace(cols[i], "{" + idx.ToString() + "}");
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
