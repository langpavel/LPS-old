using GLib;
using System;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public class Int64Column : ConfigurableColumn
	{
		public Int64Column(IntPtr raw)
			: base(raw)
		{
		}

		public Int64Column(ListStoreMapping Mapping, IColumnInfo ColumnInfo, DataColumn DataColumn)
			: base(Mapping, ColumnInfo, DataColumn)
		{
		}

		protected override void CreateCellRenderers()
		{
			CellRendererText renderer = new CellRendererText();
			renderer.Alignment = Pango.Alignment.Right;
			this.PackStart(renderer, false);
			this.AddAttribute(renderer, "text", Mapping.AddValueMapping(GType.String, GetValue));
		}

		private object GetValue(DataRow row)
		{
			object val = row[this.DataColumn];
			if(val == null || DBNull.Value.Equals(val))
				return "";
			Int64 num = Convert.ToInt64(val);
			if(this.ColumnInfo != null && !String.IsNullOrEmpty(this.ColumnInfo.DisplayFormat))
				return num.ToString(this.ColumnInfo.DisplayFormat);
			return num.ToString();
		}
	}
}
