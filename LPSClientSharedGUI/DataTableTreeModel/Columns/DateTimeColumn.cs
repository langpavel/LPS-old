using GLib;
using System;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public class DateTimeColumn : ConfigurableColumn
	{
		public DateTimeColumn(IntPtr raw)
			: base(raw)
		{
		}

		public DateTimeColumn(ListStoreMapping Mapping, ColumnInfo ColumnInfo, DataColumn DataColumn)
			: base(Mapping, ColumnInfo, DataColumn)
		{
			this.FixedWidth = 130;
		}

		protected override void CreateCellRenderers()
		{
			CellRendererText renderer = new CellRendererText();
			renderer.Alignment = Pango.Alignment.Left;
			this.PackStart(renderer, false);
			this.AddAttribute(renderer, "text", Mapping.AddValueMapping(GType.String, GetValue));
		}

		private object GetValue(DataRow row)
		{
			object val = row[this.DataColumn];
			if(val == null || DBNull.Value.Equals(val))
				return "";
			DateTime dt = Convert.ToDateTime(val);
			if(this.ColumnInfo != null && !String.IsNullOrEmpty(this.ColumnInfo.DisplayFormat))
				return dt.ToString(this.ColumnInfo.DisplayFormat);
			return dt.ToString();
		}
	}
}
