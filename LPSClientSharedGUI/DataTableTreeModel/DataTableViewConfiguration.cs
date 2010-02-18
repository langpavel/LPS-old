using System;
using System.Collections.Generic;
using Gtk;

namespace LPS.Client
{
	[Serializable]
	public sealed class DataTableViewConfiguration
	{
		public DataTableViewConfiguration()
		{
		}

		public DataTableViewConfiguration(DataTableView view)
		{
			List<ColumnConfiguration> cols = new List<ColumnConfiguration>();
			foreach(ConfigurableColumn col in view.Columns)
				cols.Add(new ColumnConfiguration(col));
			this.Columns = cols.ToArray();
			this.Sorting = view.Sorting;
			this.Filter = view.Filter;
		}

		public string Sorting { get; set; }
		public string Filter { get; set; }
		public ColumnConfiguration[] Columns { get; set; }

		private ConfigurableColumn FindColumn(DataTableView view, string name)
		{
			foreach(ConfigurableColumn col in view.Columns)
			{
				if(col == null)
					continue;
				string colname = (col.ColumnInfo != null) ? col.ColumnInfo.Name : col.DataColumn.ColumnName;
				if(colname == name)
					return col;
			}
			return null;
		}

		public void ApplyTo(DataTableView view)
		{
			ConfigurableColumn prev = null;
			foreach(ColumnConfiguration confcol in Columns)
			{
				ConfigurableColumn col = FindColumn(view, confcol.Name);
				if(col == null)
					continue;
				view.MoveColumnAfter(col, prev);
				prev = col;
				confcol.ApplyTo(col);
			}
			view.Filter = this.Filter;
			view.Sorting = this.Sorting;
		}
	}
}
