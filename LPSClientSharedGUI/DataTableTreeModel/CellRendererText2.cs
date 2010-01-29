using System;
using Gtk;
using Gdk;

namespace LPS.Client
{
	public class CellRendererText2 : CellRendererText
	{
		public CellRendererText2 ()
		{
		}

		public override void GetSize (Widget widget, ref Rectangle cell_area, out int x_offset, out int y_offset, out int width, out int height)
		{
			base.GetSize (widget, ref cell_area, out x_offset, out y_offset, out width, out height);
			y_offset -= 3;
			height -= 5;
		}
	}
}
