// DataTableStore.cs
//
// Copyright (C) 2008 Christian Hoff
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Data;
using Gtk;
using LPS;

namespace LPS.Client
{
		[Obsolete("Nejlepsi je paradoxne DataTableListStoreBinding")]
        public class DataTableStore : GLib.Object, Gtk.TreeModelImplementor
        {
                // A custom TreeModel to display DataTables
               
                private Gtk.TreeModelAdapter pAdapter;
                protected System.Data.DataTable table;
                public readonly System.Int32 Stamp;
               
                public DataTableStore (System.Data.DataTable tbl) : base() {
                        if(tbl == null)
                                throw (new System.ArgumentNullException ("tbl"));
                       
                        table = tbl;
                        table.RowChanged += this.Row_Changed;
                        table.RowDeleted += this.Row_Changed;
                        table.RowDeleting += this.Row_Changing;
                        table.TableCleared += new System.Data.DataTableClearEventHandler (this.Table_Cleared);
                       
                       
                        // Create a random stamp for the iters
                        System.Random RandomStampGen = new System.Random ();
                        Stamp = RandomStampGen.Next (System.Int32.MinValue, System.Int32.MaxValue);
                        pAdapter = new Gtk.TreeModelAdapter (this);
                }
               
                public Gtk.TreeModelAdapter Adapter {
                        get {
                                return pAdapter;
                        }
                }
               
                public System.Data.DataRow GetRowAtPath (Gtk.TreePath path) {
                        switch (path.Indices.Length) {
                        case 0:
                                return null;
                        case 1:
                                return table.Rows [path.Indices [0]];
                        default:
                                // Model is list-only; there are no child iters
                                throw (new System.ArgumentOutOfRangeException ("path"));
                        }
                }

                public Gtk.TreeModelFlags Flags {
                        get {
                                return Gtk.TreeModelFlags.ListOnly;
                                // return Gtk.TreeModelFlags.ItersPersist;
                        }
                }
               
                public System.Int32 NColumns {
                        get {
                                return table.Columns.Count;
                        }
                }
               
                // Problem: the conversion to a GType will fail with any non-value types derived from System.Object(except string)
                // Therefore, I added another function which returns a System.Type
                // TODO: Maybe file a bug report and provide a patch
                public GLib.GType GetColumnType (System.Int32 col) {
                        // System.Console.WriteLine("Column index: {0}, type: {1}", col.ToString(), type.ToString());
                        return (GLib.GType) table.Columns[col].DataType;
                }
               
                public System.Type GetColumnSystemType (System.Int32 col) {
                        return table.Columns[col].DataType;
                }
               
                public System.String GetColumnTitle(System.Int32 col) {
                        if(col < table.Columns.Count) {
                                return table.Columns[col].Caption;
                        } else {
                                throw(new System.ArgumentOutOfRangeException("col"));
                        }
                }
               
                public Gtk.TreeIter IterFromRow (System.Data.DataRow row) {
                        System.Runtime.InteropServices.GCHandle gch;

                        gch = System.Runtime.InteropServices.GCHandle.Alloc (row);
                        Gtk.TreeIter result = Gtk.TreeIter.Zero;
                        result.UserData = (System.IntPtr) gch;
                        result.Stamp = this.Stamp;
                       
                        return result;
                }
               
                public System.Data.DataRow RowFromIter (Gtk.TreeIter iter) {
                        if (iter.Stamp != this.Stamp)
                                throw (new System.InvalidOperationException (System.String.Format ("iter belongs to a different model; it's stamp is not equal to the stamp of this model({0})", this.Stamp.ToString ())));
                       
                        if (iter.UserData == System.IntPtr.Zero)
                                throw (new System.Exception ("iter is Gtk.TreeIter.Zero"));
                       
                        System.Runtime.InteropServices.GCHandle gch = (System.Runtime.InteropServices.GCHandle) iter.UserData;
                        return gch.Target as System.Data.DataRow;
                }
               
                private Gtk.TreePath PathFromRow (System.Data.DataRow row) {
                        if (row == null) {
                                return null;
                        } else {
                                Gtk.TreePath path = new Gtk.TreePath ();
                                path.AppendIndex (table.Rows.IndexOf (row));
                                return path;
                        }
                }
               
                public System.Boolean GetIter (out Gtk.TreeIter iter, Gtk.TreePath path) {
                        if (path == null)
                                throw new System.ArgumentNullException ("path");

                        iter = Gtk.TreeIter.Zero;
                        System.Data.DataRow row;
                        try {
                                row = GetRowAtPath (path);
                        } catch {
                                return false;
                        }
                        if (row == null)
                                return false;
                       
                        iter = IterFromRow (row);
                        return true;
                }

                public Gtk.TreePath GetPath (Gtk.TreeIter iter) {
                        System.Data.DataRow row = RowFromIter (iter);
                       
                        return PathFromRow (row);
                }
               
                public System.Data.DataColumn GetColumn(System.Int32 ColumnIndex) {
                        return table.Columns [ColumnIndex];
                }
               
                // col: zero-based index of the column
                public void GetValue (Gtk.TreeIter iter, System.Int32 col, ref GLib.Value val) {
                        val = new GLib.Value (GetValue (iter, col));
                }
               
                public System.Object GetValue(Gtk.TreeIter iter, System.Int32 col) {
                        System.Data.DataRow row = RowFromIter (iter);
                        if (row == null) {
                                throw(new System.ArgumentException ("The iter is pointing to a row that is NULL"));
                        } else {
                                return row [col];
                        }
                }
               
                public void SetValue (Gtk.TreeIter iter, System.Int32 column, System.Object val) {
                        RowFromIter (iter) [column] = val;
                }
               
                public System.Boolean GetIterFirst (out Gtk.TreeIter iter) {
                        if(table.Rows.Count == 0) {
                                iter = Gtk.TreeIter.Zero;
                                return false;
                        } else {
                                iter = IterFromRow (table.Rows[0]);
                                return true;
                        }
                }

                public System.Boolean IterNext (ref Gtk.TreeIter iter) {
                        System.Data.DataRow row = RowFromIter (iter);
                       
                        if (row == null) {
                                return false;
                        } else {
                                System.Int32 index = table.Rows.IndexOf (row);
                               
                                if(index + 1 < table.Rows.Count - 1) {
                                        // Return next row
                                        iter = IterFromRow (table.Rows [index + 1]);
                                        return true;
                                } else {
                                        // No rows remaining
                                        return false;
                                }
                        }
                }
               
                // DataTableStore is list-only
                public System.Int32 ChildCount (System.Data.DataRow row) {
                        return 0;
                }
               
                // Child: first child iter of parent
                // @return: failure: false, otherwise; true
                public System.Boolean IterChildren (out Gtk.TreeIter child, Gtk.TreeIter parent)
                {
                        child = Gtk.TreeIter.Zero;
                       
                        if (parent.UserData == System.IntPtr.Zero) {
                                if(table.Rows.Count == 0) {
                                        return false;
                                } else {
                                        child = IterFromRow (table.Rows[0]);
                                        return true;
                                }
                        } else {
                                // List-only model
                                return false;
                        }
                }

                public System.Boolean IterHasChild (Gtk.TreeIter iter)
                {
                        // List-only model
                        if (IterNChildren(iter) == 0) {
                                return false;
                        } else {
                                return true;
                        }
                }

                public System.Int32 IterNChildren (Gtk.TreeIter iter)
                {
                        // List-only model
                        if (iter.UserData == System.IntPtr.Zero) {
                                return table.Rows.Count;
                        } else {
                                return 0;
                        }
                }
               
                // According to the Mono docs, index should be zero-based
                public System.Boolean IterNthChild (out Gtk.TreeIter child, Gtk.TreeIter parent, System.Int32 index)
                {
                        if (parent.UserData == System.IntPtr.Zero) {
                                child = IterFromRow (table.Rows [index]);
                                return true;
                        } else {
                                // List-only model
                                child = Gtk.TreeIter.Zero;
                                return false;
                        }
                }
               
                public System.Boolean IterParent (out Gtk.TreeIter parent, Gtk.TreeIter child)
                {
                        // List-only model
                        parent = Gtk.TreeIter.Zero;
                        return false;
                }

                public void RefNode (Gtk.TreeIter iter) {
                }
               
                public void UnrefNode (Gtk.TreeIter iter) {
                }
               
                public System.Data.DataTable Table {
                        get {
                                return table;
                        }
                }
               
                public System.Data.DataColumn[] GetRelatedColumns(System.Int32[] ColIndices) {
                        System.Data.DataColumn[] SrcCols = new System.Data.DataColumn[ColIndices.GetLength(0)];
                        for(System.Int32 ColumnIndex = 0; ColumnIndex <= ColIndices.GetUpperBound(0); ColumnIndex++) {
                                SrcCols[ColumnIndex] = table.Columns[ColIndices[ColumnIndex]];
                        }
                        return GetRelatedColumns(SrcCols);
                }
               
                public System.Data.DataColumn[] GetRelatedColumns(System.Data.DataColumn[] SrcCols) {
                        System.Data.DataSet ds = table.DataSet;
                        System.Collections.Generic.List<System.Data.DataColumn> DestColumns = new System.Collections.Generic.List<System.Data.DataColumn>();
                       
                        foreach(System.Data.DataRelation rel in ds.Relations) {
                                foreach(System.Data.DataColumn col1 in rel.ChildColumns) {
                                        foreach(System.Data.DataColumn col2 in SrcCols) {
                                                if(col1 == col2) {
                                                        foreach(System.Data.DataColumn col in rel.ParentColumns) {
                                                                DestColumns.Add(col);
                                                        }
                                                }
                                        }
                                }
                               
                                foreach(System.Data.DataColumn col1 in rel.ParentColumns) {
                                        foreach(System.Data.DataColumn col2 in SrcCols) {
                                                if(col1 == col2) {
                                                        foreach(System.Data.DataColumn col in rel.ChildColumns) {
                                                                DestColumns.Add(col);
                                                        }
                                                }
                                        }
                                }
                        }
                        return DestColumns.ToArray();
                }
               
                // Event handlers
                private System.Collections.Generic.Dictionary<System.Data.DataRow, Gtk.TreePath> RowsToBeDeleted = new System.Collections.Generic.Dictionary<System.Data.DataRow, Gtk.TreePath> ();
               
                private void Row_Changing (System.Object o, System.Data.DataRowChangeEventArgs e) {
                        if ((e.Action & System.Data.DataRowAction.Delete) == System.Data.DataRowAction.Delete)
                                RowsToBeDeleted.Add (e.Row, PathFromRow (e.Row));
                        Log.Debug("Note: Action {0} is performed in DataTable", e.Action.ToString ());
                }
               
                private void Row_Changed(System.Object obj, System.Data.DataRowChangeEventArgs e) {
                        if ((e.Action & System.Data.DataRowAction.Add) == System.Data.DataRowAction.Add)
                                pAdapter.EmitRowInserted (PathFromRow (e.Row), IterFromRow (e.Row));
                        else if ((e.Action & System.Data.DataRowAction.Change) == System.Data.DataRowAction.Change)
                                pAdapter.EmitRowChanged (PathFromRow (e.Row), IterFromRow (e.Row));
                        else if ((e.Action & System.Data.DataRowAction.Delete) == System.Data.DataRowAction.Delete) {
                                Gtk.TreePath delPath = RowsToBeDeleted [e.Row];
                                Log.Debug("Removed row, path: {0}", delPath.Indices [0]);
                                RowsToBeDeleted.Remove (e.Row);
                                pAdapter.EmitRowDeleted (delPath);
                               
                                foreach (Gtk.TreePath currPath in RowsToBeDeleted.Values) {
                                        Log.Debug("Looking for a TreePath to be changed");
                                        if (currPath.Indices [0] > delPath.Indices [0]) {
                                                if (!currPath.Prev ())
                                                        throw (new System.ApplicationException ("Moving TreePath to previous element failed"));
                                               
                                        }
                                }
                                delPath.Dispose ();
                        } else
                                Log.Warning("Note: Unhandled action {0} performed in DataTable", e.Action.ToString ());
                }
               
                private void Table_Cleared(System.Object sender, System.Data.DataTableClearEventArgs e) {
                        pAdapter.EmitRowDeleted (new Gtk.TreePath ());
                }
		
				public static void AssignNew(TreeView view, DataTable dt, bool markup)
				{
					DataTableStore model = new DataTableStore(dt);
					//model.DataTable = dt;
					//model.RenderToMarkup = markup;
					view.BorderWidth = 0;
					view.EnableGridLines = TreeViewGridLines.Vertical;
					view.Reorderable = true;
					view.FixedHeightMode = true;
					
					for(int i = 0; i < dt.Columns.Count; i++)
					{
						DataColumn dc = dt.Columns[i];
						CellRenderer renderer;
						TreeViewColumn wc;
						string caption = dc.Caption.Replace('_',' ');
						if(dc.DataType == typeof(bool))
						{
							renderer = new CellRendererToggle();
							wc = new TreeViewColumn(caption, renderer, "active", i); //, "inconsistent", i+1000, "radio",i+1000);
						}
						else
						{
							renderer = new CellRendererText();
							if(markup)
								wc = new TreeViewColumn(caption, renderer, "markup", i);
							else
								wc = new TreeViewColumn(caption, renderer, "text", i);
						}
						wc.Reorderable = true;
						wc.MinWidth = 4;
						wc.MaxWidth = 1000;
						wc.Resizable = true;
						wc.Sizing = TreeViewColumnSizing.Fixed;
						wc.FixedWidth = 50;
						//wc.SortIndicator = true;
						//wc.SortOrder = SortType.Ascending;
						view.AppendColumn(wc);
					}
		
					view.Model = new TreeModelAdapter(model);
				}

        }
}
