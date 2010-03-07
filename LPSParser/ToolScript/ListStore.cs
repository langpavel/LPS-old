using System;
using Gtk;
using GLib;

namespace LPS.ToolScript
{
	public class ListStore : Gtk.ListStore
	{
		[Obsolete]
		protected ListStore(GType gtype)
			: base (gtype)
		{
		}

		public ListStore(IntPtr raw)
			: base (raw)
		{
		}

		protected ListStore()
			: base ()
		{
		}

		public ListStore(params GLib.GType[] types)
			: base (types)
		{
		}

		public ListStore(params Type[] types)
			: base (types)
		{
		}

		public TreeIter GetIter(TreeView view, TreePath path)
		{
			TreeIter iter;
			if(!GetIter(out iter, path))
				throw new InvalidOperationException("Cesta ve stromu není platná");
			return iter;
		}

		public object GetValue(TreeView view, TreePath path, long index)
		{
			return this.GetValue(GetIter(view, path), (int)index);
		}
	}
}
