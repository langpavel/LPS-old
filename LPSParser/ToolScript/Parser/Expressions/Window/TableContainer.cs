using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class TableContainer : ContainerWidgetBase
	{
		public TableContainer(string Name, WidgetParamList Params, LayoutList Childs)
			: base(Name, Params, Childs)
		{
		}

		private class WidgetTableInfo
		{
			public WidgetTableInfo()
			{
			}

			public Gtk.Widget Widget;
			public uint Left;
			public uint Right;
			public uint Top;
			public uint Bottom;
			public Gtk.AttachOptions XOptions;
			public Gtk.AttachOptions YOptions;
			public uint XPadding;
			public uint YPadding;
		}

		protected override Gtk.Widget CreateWidget()
		{
			uint rows = 0;
			uint columns = 0;
			uint wrow = 0;
			uint wcolumn = 0;
			uint startrow = 0;
			uint startcolumn = 0;
			bool rowmode = GetAttribute<bool>("rowmode", true);
			rowmode = !GetAttribute<bool>("colmode", !rowmode);
			List<WidgetTableInfo> wlist = new List<WidgetTableInfo>(Childs.Count);
			foreach(IWidgetBuilder b in Childs)
			{
				WidgetTableInfo w = new WidgetTableInfo();
				w.Widget = b.Build();

				startcolumn = b.GetAttribute<uint>("col", startcolumn);
				startrow = b.GetAttribute<uint>("row", startrow);
				wcolumn = b.GetAttribute<uint>("col", wcolumn);
				wrow = b.GetAttribute<uint>("row", wrow);

				if(b.GetAttribute<bool>("newcol", false))
					{ wrow = startrow; wcolumn = ++startcolumn; }
				else if(b.GetAttribute<bool>("newrow", false))
					{ wcolumn = startcolumn; wrow = ++startrow; }

				w.Left = wcolumn;
				w.Right = wcolumn + b.GetAttribute<uint>("colspan", 1u);
				w.Top = wrow;
				w.Bottom = wrow + b.GetAttribute<uint>("rowspan", 1u);

				if(!b.GetAttribute<bool>("xnoexpand", false)) w.XOptions |= Gtk.AttachOptions.Expand;
				if(!b.GetAttribute<bool>("xnofill", false)) w.XOptions |= Gtk.AttachOptions.Fill;
				if(b.GetAttribute<bool>("xshrink", false)) w.XOptions |= Gtk.AttachOptions.Shrink;

				if(!b.GetAttribute<bool>("ynoexpand", false)) w.YOptions |= Gtk.AttachOptions.Expand;
				if(!b.GetAttribute<bool>("ynofill", false)) w.YOptions |= Gtk.AttachOptions.Fill;
				if(b.GetAttribute<bool>("yshrink", false)) w.YOptions |= Gtk.AttachOptions.Shrink;

				w.XPadding = b.GetAttribute<uint>("xpadding", 0u);
				w.YPadding = b.GetAttribute<uint>("ypadding", 0u);
				wlist.Add(w);

				if(rows < w.Bottom) rows = (uint)w.Bottom;
				if(columns < w.Right) columns = (uint)w.Right;

				if(rowmode)
					wcolumn++;
				else
					wrow++;
			}
			Gtk.Table table = new Gtk.Table(rows, columns, GetAttribute<bool>("homogeneous", false));
			List<Gtk.Widget> focuschain = new List<Gtk.Widget>(wlist.Count);
			foreach(WidgetTableInfo w in wlist)
			{
				table.Attach(w.Widget,
					w.Left, w.Right, w.Top, w.Bottom,
					w.XOptions, w.YOptions, w.XPadding, w.YPadding);
				if(w.Widget.CanFocus)
					focuschain.Add(w.Widget);
			}
			table.FocusChain = focuschain.ToArray();
			return table;
		}

	}
}
