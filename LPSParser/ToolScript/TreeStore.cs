using System;
using GLib;
using Gtk;

namespace LPS.ToolScript
{
	public class TreeStore : Gtk.TreeStore
	{
		[Obsolete]
		protected TreeStore(GType gtype)
			: base (gtype)
		{
		}

		public TreeStore(IntPtr raw)
			: base (raw)
		{
		}

		protected TreeStore()
			: base ()
		{
		}

		public TreeStore(params GLib.GType[] types)
			: base (types)
		{
		}

		public TreeStore(params Type[] types)
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
