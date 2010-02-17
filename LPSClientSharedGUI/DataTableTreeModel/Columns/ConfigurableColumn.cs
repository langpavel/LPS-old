using GLib;
using System;
using System.Data;
using Gtk;

namespace LPS.Client
{
	[TreeNode(ListOnly=true)]
	public abstract class ConfigurableColumn : TreeViewColumn, ITreeNode
	{
		public ListStoreMapping Mapping { get; private set; }
		public DataColumn DataColumn { get; private set; }
		public ColumnInfo ColumnInfo { get; private set; }

		public ConfigurableColumn(IntPtr raw)
			: base(raw)
		{
		}

		public ConfigurableColumn(ListStoreMapping mapping, ColumnInfo info, DataColumn column)
		{
			this.Mapping = mapping;
			this.ColumnInfo = info;
			this.DataColumn = column;

			this.Reorderable = true;
			this.Resizable = true;
			this.MinWidth = 10;
			this.FixedWidth = 80;
			this.Sizing = TreeViewColumnSizing.Fixed;
			this.Clickable = true;
			this.SortIndicator = false;
			this.SortOrder = SortType.Ascending;

			if(this.ColumnInfo != null)
			{
				this.Title = this.ColumnInfo.Caption;
				this.Visible = this.ColumnInfo.Visible;
			}
			else if (this.DataColumn != null)
			{
				this.Title = this.DataColumn.Caption;
			}

			CreateCellRenderers();

			this.Mapping.ColumnsStore.AddNode(this);
		}

		protected abstract void CreateCellRenderers();

		protected override void OnClicked ()
		{
			base.OnClicked();
			Mapping.OnColumnClicked(this);
		}

		protected object GetAsString(DataRow row)
		{
			object val = row[this.DataColumn];
			if(val == null || DBNull.Value.Equals(val))
				return "";
			return Convert.ToString(val);
		}

		public override void Dispose ()
		{
			this.Mapping.ColumnsStore.RemoveNode(this);
			base.Dispose ();
		}

		public void DoChanged()
		{
			if(Changed != null)
				Changed(this, EventArgs.Empty);
		}

		[TreeNodeValue(Column=0)]
		public int Conf_Index
		{
			get
			{
				if(this.TreeView == null)
					return 0;
				TreeView view = (TreeView)this.TreeView;
				for(int i=0; i<view.Columns.Length; i++)
					if(view.Columns[i] == this)
						return i+1;
				return 0;
			}
		}

		[TreeNodeValue(Column=1)]
		public string Conf_Caption { get { return Title; } set { Title = value; DoChanged(); } }

		[TreeNodeValue(Column=2)]
		public bool Conf_Visible { get { return Visible; } set { Visible = value; DoChanged(); } }

		public static void CreateNodeViewColumns(NodeView view)
		{
			view.AppendColumn("Poz.", new CellRendererText(), "text", 0);
			view.AppendColumn("Nadpis", new CellRendererText(), "text", 1);
			CellRendererToggle toggle_visible = new CellRendererToggle();
			toggle_visible.Activatable = true;
			toggle_visible.Toggled += delegate(object o, ToggledArgs args) {
				TreePath path = new TreePath(args.Path);
				ConfigurableColumn cc = ((ConfigurableColumn)view.NodeStore.GetNode(path));
				cc.Conf_Visible = !cc.Conf_Visible;
			};
			TreeViewColumn col = new TreeViewColumn("Viditelný", toggle_visible, "active", 2);
			view.AppendColumn(col);
		}

		#region ITreeNode implementation
		private event EventHandler Changed;
		event EventHandler ITreeNode.Changed
		{
			add { this.Changed += value; }
			remove { this.Changed -= value; }
		}
		event TreeNodeAddedHandler ITreeNode.ChildAdded { add {} remove {} }
		event TreeNodeRemovedHandler ITreeNode.ChildRemoved { add {} remove {} }

		int ITreeNode.IndexOf (object o)
		{
			return -1;
		}

		int ITreeNode.ID
		{
			get	{ return this.GetHashCode(); }
		}

		ITreeNode ITreeNode.Parent
		{
			get { return null; }
		}

		int ITreeNode.ChildCount
		{
			get { return 0; }
		}

		ITreeNode ITreeNode.this[int index]
		{
			get { return null; }
		}
		#endregion
	}
}
