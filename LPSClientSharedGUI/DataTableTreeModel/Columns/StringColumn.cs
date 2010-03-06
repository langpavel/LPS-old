using GLib;
using System;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public class StringColumn : ConfigurableColumn
	{
		public StringColumn(IntPtr raw)
			: base(raw)
		{
		}

		public StringColumn(ListStoreMapping Mapping, IColumnInfo ColumnInfo, DataColumn DataColumn)
			: base(Mapping, ColumnInfo, DataColumn)
		{
		}

		protected override void CreateCellRenderers()
		{
			CellRendererText renderer = new CellRendererText();
			renderer.Alignment = Pango.Alignment.Left;
			this.PackStart(renderer, false);
			this.AddAttribute(renderer, "text", Mapping.AddValueMapping(GType.String, GetAsString));
		}
	}
}
