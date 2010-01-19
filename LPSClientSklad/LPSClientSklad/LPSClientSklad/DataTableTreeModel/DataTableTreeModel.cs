using System;
using System.Collections;
using System.Data;
using System.Runtime.InteropServices;
using Gtk;

// Ispirated by http://anonsvn.mono-project.com/viewvc/trunk/gtk-sharp/sample/TreeModelDemo.cs?view=co

namespace LPSClientSklad
{
	public class DataTableTreeModel : GLib.Object, TreeModelImplementor
	{
		public DataTableTreeModel ()
		{
		}
		
		private DataTable _DataTable;
		public DataTable DataTable
		{
			get { return _DataTable; }
			set { _DataTable = value; }
		}
		
		/// <summary>
		/// Returns row on one-level path
		/// </summary>
		object GetNodeAtPath (TreePath path) 
		{
			try
			{
				if(path.Indices.Length == 1)
					return this.DataTable.Rows[path.Indices[0]];
				return null;
			}
			catch(IndexOutOfRangeException)
			{
				return null;
			}
		}
		
		public TreeModelFlags Flags {
			get { return TreeModelFlags.ItersPersist | TreeModelFlags.ListOnly; }
		}

		public int NColumns {
			get { return this.DataTable.Columns.Count; }
		}
		
		public GLib.GType GetColumnType (int col)
		{
			Type managedType = this.DataTable.Columns[col].DataType;
			//Console.WriteLine(managedType.ToString());
			if(managedType == typeof(Int64))
				return GLib.GType.Int64;
			if(managedType == typeof(Int32))
				return GLib.GType.Int;
			return GLib.GType.FromName(managedType.Name);
			/*
			if(managedType == typeof(String))
				return GLib.GType.String;
			if(managedType == typeof(Int64))
				return GLib.GType.Int64;
			if(managedType == typeof(Int32))
				return GLib.GType.Int;
			if(managedType == typeof(Int32))
			*/
		} 
		
		Hashtable node_hash = new Hashtable (); 
		
		TreeIter IterFromNode (object node)
		{
			GCHandle gch;
			if (node_hash [node] != null)
				gch = (GCHandle) node_hash [node];
			else
				gch = GCHandle.Alloc (node);
			TreeIter result = TreeIter.Zero;
			result.UserData = (IntPtr) gch;
			return result;
		} 		
		
		object NodeFromIter (TreeIter iter)
		{
			GCHandle gch = (GCHandle) iter.UserData;
			return gch.Target;
		} 
		
		TreePath PathFromNode (object node)
		{
			int[] indices = new int[]{ this.DataTable.Rows.IndexOf((DataRow)node) };
			return new TreePath(indices);
		}
		
		public bool GetIter (out TreeIter iter, TreePath path)
		{
			if (path == null)
				throw new ArgumentNullException ("path");

			iter = TreeIter.Zero;

			object node = GetNodeAtPath (path);
			if (node == null)
				return false;

			iter = IterFromNode (node);
			return true;
		}
		
		public TreePath GetPath (TreeIter iter)
		{
			object node = NodeFromIter (iter);
			if (node == null)
				throw new ArgumentException ("iter");

			return PathFromNode (node);
		}
		
		public void GetValue (TreeIter iter, int col, ref GLib.Value val)
		{
			DataRow row = NodeFromIter (iter) as DataRow;
			if (row == null)
				return;
			val = new GLib.Value(row[col]);
		}
		
		public bool IterNext (ref TreeIter iter)
		{
			DataRow row = NodeFromIter (iter) as DataRow;
			if (row == null)
				return false;
			try
			{
				int idx = this.DataTable.Rows.IndexOf(row);
				row = this.DataTable.Rows[idx + 1];
				iter = IterFromNode(row);
				return true;
			}
			catch(IndexOutOfRangeException)
			{
				return false;
			}
		}
		
		public bool IterChildren (out TreeIter child, TreeIter parent)
		{
			child = TreeIter.Zero;
			return false;
		}
		
		public bool IterHasChild (TreeIter iter)
		{
			return false;
		}

		public int IterNChildren (TreeIter iter)
		{
			// root?
			if (iter.UserData == IntPtr.Zero)
				return this.DataTable.Rows.Count;

			return 0;
		}

		public bool IterNthChild (out TreeIter child, TreeIter parent, int n)
		{
			child = TreeIter.Zero;

			// root?
			if (parent.UserData == IntPtr.Zero) 
			{
				if (this.DataTable.Rows.Count <= n)
					return false;
				child = IterFromNode (this.DataTable.Rows[n]);
				return true;
			}
			return false;
		}

		public bool IterParent (out TreeIter parent, TreeIter child)
		{
			parent = TreeIter.Zero;
			return false;
		}

		public void RefNode (TreeIter iter)
		{
		}

		public void UnrefNode (TreeIter iter)
		{
		}
		
		public DataRow GetRow(TreeIter iter)
		{
			return NodeFromIter(iter) as DataRow;
		}
		
		public DataRow GetRow(TreePath path)
		{
			return GetNodeAtPath(path) as DataRow;
		}
		
		public static void AssignNew(TreeView view, DataTable dt)
		{
			DataTableTreeModel model = new DataTableTreeModel();
			model.DataTable = dt;
			view.Model = new TreeModelAdapter(model);
			for(int i = 0; i < dt.Columns.Count; i++)
			{
				DataColumn dc = dt.Columns[i];
				string caption = dc.Caption;
				CellRendererText renderer = new CellRendererText();
				TreeViewColumn wc = new TreeViewColumn(caption, renderer, "text", i);
				view.AppendColumn(wc);
			}

		}
	}
}
