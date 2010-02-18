using System;

namespace LPS.Client
{
	[Serializable]
	public sealed class ColumnConfiguration
	{
		public ColumnConfiguration()
		{
		}

		public ColumnConfiguration(ConfigurableColumn col)
		{
			this.Name = (col.ColumnInfo != null) ? col.ColumnInfo.Name : col.DataColumn.ColumnName;
			this.Visible = col.Visible;
			this.Width = col.Width;
			this.Title = col.Title;
		}

		public string Name { get; set; }
		public string Title { get; set; }
		public int Width { get; set; }
		public bool Visible { get; set; }

		public void ApplyTo(ConfigurableColumn col)
		{
			string colname = (col.ColumnInfo != null) ? col.ColumnInfo.Name : col.DataColumn.ColumnName;
			if(colname != this.Name)
				throw new InvalidOperationException("Konfigurace sloupečku nepatří sloupci");
			if(this.Title != null) col.Title = this.Title;
			if(this.Width > 0) col.FixedWidth = this.Width;
			col.Visible = Visible;
		}
	}
}
