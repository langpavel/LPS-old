using GLib;
using System;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public class LookupColumn : ConfigurableColumn
	{
		private DataSet ds;
		private string format;

		public LookupColumn(IntPtr raw)
			: base(raw)
		{
		}

		public LookupColumn(ListStoreMapping Mapping, ColumnInfo ColumnInfo, DataColumn DataColumn)
			: base(Mapping, ColumnInfo, DataColumn)
		{
			ILookupInfo linfo = (ILookupInfo)this.ColumnInfo;
			ds = ServerConnection.Instance.GetCachedDataSet(linfo.LookupTable);
			string[] cols = linfo.FkListReplaceFormat.Split(
				new char[] {',',';',' ',':','-','\'','"', '[', ']', '(', ')', '{', '}', '<', '>'}, StringSplitOptions.RemoveEmptyEntries);
			format = linfo.FkListReplaceFormat;
			for(int i = 0; i < cols.Length; i++)
			{
				int idx = ds.Tables[0].Columns.IndexOf(cols[i]);
				if(idx >= 0)
					format = format.Replace(cols[i], "{" + idx.ToString() + "}");
		    }
		}

		protected override void CreateCellRenderers()
		{
			CellRendererText renderer = new CellRendererText();
			renderer.Alignment = Pango.Alignment.Left;

			this.PackStart(renderer, false);
			this.AddAttribute(renderer, "markup", Mapping.AddValueMapping(GType.String, GetDisplayValue));
		}

		public object GetDisplayValue(DataRow row)
		{
			object val = row[this.DataColumn];
			if(val == null || DBNull.Value.Equals(val))
				return "";
			try
			{
				DataRow refrow = ds.Tables[0].Rows.Find(val);
				return String.Format(this.format, refrow.ItemArray);
			}
			catch
			{
				return "<span color=\"#ff0000\">(err)</span>";
			}
		}


		public override void Dispose ()
		{
			if(this.ds != null)
			{
				ServerConnection.Instance.DisposeDataSet(this.ds);
				this.ds = null;
			}
			base.Dispose ();
		}
	}
}
