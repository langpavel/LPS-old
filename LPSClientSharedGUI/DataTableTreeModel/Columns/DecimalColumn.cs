using GLib;
using System;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public class DecimalColumn : ConfigurableColumn
	{
		public DecimalColumn(IntPtr raw)
			: base(raw)
		{
		}

		public DecimalColumn(ListStoreMapping Mapping, IColumnInfo ColumnInfo, DataColumn DataColumn)
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
			Decimal num = Convert.ToDecimal(val);
			if(this.ColumnInfo != null && !String.IsNullOrEmpty(this.ColumnInfo.DisplayFormat))
				return num.ToString(this.ColumnInfo.DisplayFormat);
			return num.ToString();
		}
	}
}
