using GLib;
using System;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public class CheckBoxColumn : ConfigurableColumn
	{
		public CheckBoxColumn(IntPtr raw)
			: base(raw)
		{
		}

		public CheckBoxColumn(ListStoreMapping Mapping, ColumnInfo ColumnInfo, DataColumn DataColumn)
			: base(Mapping, ColumnInfo, DataColumn)
		{
			this.FixedWidth = 40;
		}

		protected override void CreateCellRenderers()
		{
			CellRendererToggle renderer = new CellRendererToggle();
			this.PackStart(renderer, false);
			this.AddAttribute(renderer, "active", Mapping.AddValueMapping(GType.Boolean, GetActive));
			this.AddAttribute(renderer, "inconsistent", Mapping.AddValueMapping(GType.Boolean, GetInconsistent));
		}

		private object GetActive(DataRow row)
		{
			return true.Equals(row[this.DataColumn]);
		}

		private object GetInconsistent(DataRow row)
		{
			object val = row[this.DataColumn];
			return val == null || DBNull.Value.Equals(val);
		}
	}
}
