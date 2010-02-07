using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using Gtk;

namespace LPS.Client
{
	/// <summary>
	/// Vytvori ListStore, kde prvni hodnota je row, druha je rezevovana
	/// a dale to je vzy column, is_null, takze step == 2
	/// </summary>
	public class DataTableListStoreBinding: IDisposable
	{

		public DataTableListStoreBinding()
			: this(null, null, null)
		{
		}
		
		public DataTableListStoreBinding(TreeView view, DataTable dt, TableInfo tableinfo)
		{
			this.TreeView = view;
			this.DataTable = dt;
			this.TableInfo = tableinfo;
			this.MappedColumns = new Dictionary<string, GetMappedColumnValue>();
			//MappedColumns["id_user_create"] = new DataTableListStoreBinding.GetMappedColumnValue(GetUserName);
			//MappedColumns["id_user_modify"] = new DataTableListStoreBinding.GetMappedColumnValue(GetUserName);
		}

		public TreeView TreeView { get; set; }
		public DataTable DataTable { get; set; }
		public ListStore ListStore { get; set; }
		public TreeModelFilter Filter { get; set; }
		public ListStore ColumnList { get; set; }
		public TableInfo TableInfo { get; set; }
		//public TreeModelSort Sort { get; set; }
		public bool UseMarkup { get; set; }

		public delegate string GetMappedColumnValue(object val, DataRow row);
		public Dictionary<string, GetMappedColumnValue> MappedColumns;
		
		public void Dispose()
		{
			Unbind();
		}
		
		public string GetUserName(object val, DataRow row)
		{
			try
			{
				long userid = Convert.ToInt64(val);
				return ServerConnection.Instance.GetUserName(userid);
			}
			catch
			{
				return "<span color=\"#ff0000\">(?)</span>";
			}
		}

		private	GetMappedColumnValue[] getValFuncs;
		private Dictionary<DataRow, TreeIter> row_iters;
		
		public ColumnInfo GetColumnInfo(string name)
		{
			foreach(ColumnInfo col in this.TableInfo.Columns)
			{
				if(col.Name == name)
					return col;
			}
			ColumnInfo result = new ColumnInfo();
			result.Caption = name.Replace('_',' ');
			result.Description = "(automaticky vytvořeno)";
			this.TableInfo.Columns.Add(result);
			return result;
		}
		
		public void Bind()
		{
			getValFuncs = new GetMappedColumnValue[this.DataTable.Columns.Count];
			this.ColumnList = new ListStore(typeof(object), typeof(string), typeof(string), typeof(string), typeof(bool));
				
			CellRendererToggle rendererToggle = new CellRendererToggle();
			CellRendererText rendererText = new CellRendererText2();
			rendererText.FixedHeightFromFont = 1;
			
			List<Type> types = new List<Type>();
			types.Add(typeof(object));
			types.Add(typeof(object));
			for(int i = 0; i < this.DataTable.Columns.Count; i++)
			{
				DataColumn dc = this.DataTable.Columns[i];
				TreeViewColumn wc;
				ColumnInfo colinfo = GetColumnInfo(dc.ColumnName);
				GetMappedColumnValue getValFunc = null;
				getValFuncs[i] = null;
				if(this.MappedColumns.TryGetValue(dc.ColumnName, out getValFunc))
				{
					getValFuncs[i] = getValFunc;
					types.Add(typeof(string));
					types.Add(typeof(bool));
					wc = new TreeViewColumn(colinfo.Caption, rendererText, "markup", (i+1)*2);
				}
				else if(!String.IsNullOrEmpty(colinfo.FkReferenceTable) && !String.IsNullOrEmpty(colinfo.FkReplaceColumns))
				{
					FKMappedColumnHelper helper = new FKMappedColumnHelper(colinfo.FkReferenceTable, colinfo.FkReplaceColumns);
					//if(!String.IsNullOrEmpty(colinfo.DisplayFormat))
					//	helper.DisplayFormat = colinfo.DisplayFormat;
					getValFuncs[i] = helper.GetDisplayValue;
					types.Add(typeof(string));
					types.Add(typeof(bool));
					wc = new TreeViewColumn(colinfo.Caption, rendererText, "markup", (i+1)*2);
				}
				else if(dc.DataType == typeof(bool))
				{
					wc = new TreeViewColumn(colinfo.Caption, rendererToggle, "active", (i+1)*2, "inconsistent", (i+1)*2+1); 
					types.Add(typeof(bool));
					types.Add(typeof(bool));
				}
				else
				{
					types.Add(typeof(string));
					types.Add(typeof(bool));
					if(UseMarkup)
						wc = new TreeViewColumn(colinfo.Caption, rendererText, "markup", (i+1)*2);
					else
						wc = new TreeViewColumn(colinfo.Caption, rendererText, "text", (i+1)*2);
				}
				wc.Reorderable = true;
				wc.MinWidth = 4;
				wc.MaxWidth = 1000;
				wc.Resizable = true;
				wc.Sizing = TreeViewColumnSizing.GrowOnly;
				wc.Clickable = true;
				wc.SortIndicator = false;
				wc.SortOrder = SortType.Ascending;
				wc.Data["TABLE_COLUMN"] = dc;
				wc.Clicked += ToggleSort;
				wc.Visible = colinfo.Visible;
				if(colinfo.Width > 0)
					wc.FixedWidth = colinfo.Width;

				ColumnList.AppendValues(
					new object[] {colinfo, wc },
					colinfo.Name,
					colinfo.Caption ?? "",
					colinfo.Description ?? "",
				    colinfo.Visible
				);

				
				this.TreeView.AppendColumn(wc);

			}
			this.ListStore = new ListStore(types.ToArray());
			//this.ListStore.Data["_BINDING"] = this;
			//this.ListStore.DefaultSortFunc = SortCompareFunc;
			FillDataList();
			//this.Sort = new TreeModelSort(this.ListStore);
			//this.Filter = new TreeModelFilter(this.Sort, null);
			this.Filter = new TreeModelFilter(this.ListStore, null);
			this.Filter.Data["_BINDING"] = this;
			this.Filter.VisibleFunc = FilterFunc;
			this.TreeView.Model = this.Filter;
			
			this.DataTable.RowChanged += HandleDataTableRowChanged;
			this.DataTable.RowDeleted += HandleDataTableRowDeleted;
			
			this.Sorting = this.Sorting;
		}

		void ToggleSort(object sender, EventArgs e)
		{
			TreeViewColumn col = (TreeViewColumn)sender;
			DataColumn dc = col.Data["TABLE_COLUMN"] as DataColumn;
			if(!col.SortIndicator || col.SortOrder == SortType.Descending)
				Sorting = dc.ColumnName;
			else
				Sorting = dc.ColumnName + " desc";
		}

		public void Unbind()
		{
			if(this.DataTable != null)
			{
				this.DataTable.RowChanged -= HandleDataTableRowChanged;
				this.DataTable.RowDeleted -= HandleDataTableRowDeleted;
				this.DataTable = null;
			}
			if(this.TreeView != null)
			{
				this.TreeView.Model = null;
				while(this.TreeView.Columns.Length != 0)
				{
					this.TreeView.RemoveColumn(this.TreeView.Columns[this.TreeView.Columns.Length-1]);
				}
			}
			if(this.Filter != null)
			{
				this.Filter.Dispose();
				this.Filter = null;
			}
//			if(this.Sort != null)
//			{
//				this.Sort.Dispose();
//				this.Sort = null;
//			}
			if(this.ListStore != null)
			{
				this.ListStore.Clear();
				this.ListStore.Dispose();
				this.ListStore = null;
			}
			if(this.ColumnList != null)
			{
				this.ColumnList.Clear();
				this.ColumnList.Dispose();
				this.ColumnList = null;
			}
		}
		
		void HandleDataTableRowChanged (object sender, DataRowChangeEventArgs e)
		{
			TreeIter iter = row_iters[e.Row];
			object[] data = new object[(this.DataTable.Columns.Count + 1) * 2];
			GetColumnValues(e.Row, ref data);
			this.ListStore.SetValues(iter, data);
		}

		void HandleDataTableRowDeleted (object sender, DataRowChangeEventArgs e)
		{
			TreeIter iter = row_iters[e.Row];
			this.ListStore.Remove(ref iter);
			row_iters.Remove(e.Row);
		}

		public void FillDataList()
		{
			FillDataList(this.DataTable.Rows);
		}
		
		protected void GetColumnValues(DataRow row, ref object[] data)
		{
			data[0] = row;
			for(int i=0; i < this.DataTable.Columns.Count; i++)
			{
				DataColumn col = this.DataTable.Columns[i];
				object val = row[i];
				GetMappedColumnValue getValFunc = getValFuncs[i];
				if(getValFunc != null)
				{
					data[(i+1)*2] = getValFunc(val, row);
					data[(i+1)*2+1] = false;
				}
				else if(col.DataType == typeof(bool))
				{
					if(val == null || val == DBNull.Value)
					{
						//data[i+1] = null;
						data[(i+1)*2] = false;
						data[(i+1)*2+1] = true; // null
					}
					else
					{
						data[(i+1)*2] = val;
						data[(i+1)*2+1] = false;
					}
				}
				else
				{
					if(val == null || val == DBNull.Value)
					{
						data[(i+1)*2] = "";
						data[(i+1)*2+1] = true;
					}
					else
					{
						data[(i+1)*2] = val.ToString();
						data[(i+1)*2+1] = false;
					}
				}
			}
		}
			
		public void FillDataList(DataRowCollection rows)
		{
			row_iters = new Dictionary<DataRow, TreeIter>(this.DataTable.Rows.Count);
			this.ListStore.Clear();
			object[] data = new object[(this.DataTable.Columns.Count + 1) * 2];
			foreach(DataRow row in this.DataTable.Rows)
			{
				GetColumnValues(row, ref data);
				row_iters[row] = this.ListStore.AppendValues(data);
			}
		}
		
		[Obsolete]
		public static DataTableListStoreBinding Get(TreeView view)
		{
			return (view.Model as TreeModelFilter).Data["_BINDING"] as DataTableListStoreBinding;
		}
		
		public DataRow GetRow(TreePath path)
		{
			TreeIter iter;
			if(!this.Filter.GetIter(out iter, path))
				return null;
			return this.Filter.GetValue(iter, 0) as DataRow;
		}
		
		private string filter_str;
		public void ApplyFilter(string filter)
		{
			if(!String.IsNullOrEmpty(filter))
				filter_str = filter.ToLower();
			else
				filter_str = filter;
			this.Filter.Refilter();
		}
		
		private bool FilterFunc(TreeModel model, TreeIter iter)
		{
			if(String.IsNullOrEmpty(filter_str))
				return true;
			DataRow row = model.GetValue(iter, 0) as DataRow;
			for(int i=0; i < row.Table.Columns.Count; i++)
			{
				object o = row[i];
				if(o == null || o is DBNull || o is Boolean)
					continue;
				string rowVal = Convert.ToString(o);
				if(rowVal.ToLowerInvariant().Contains(filter_str))
					return true;
			}
			return false;
		}

		private int[] comparison_cols;
		private int SortCompareFunc(TreeModel model, TreeIter tia, TreeIter tib)
		{
			if(comparison_cols.Length == 0)
				return 0;
			DataRow r1 = model.GetValue(tia, 0) as DataRow;
			DataRow r2 = model.GetValue(tib, 0) as DataRow;
			for(int i = 0; i < comparison_cols.Length; i += 2)
			{
				int col_idx = comparison_cols[i];
				object o1 = r1[col_idx];
				object o2 = r2[col_idx];
				if(o1 == o2 || ((o1 == null || o1 is DBNull) && (o2 == null || o2 is DBNull)))
					continue;
				int ordering = comparison_cols[i+1];
				int result = 0;
				Type type = this.DataTable.Columns[col_idx].DataType;
				if(type == typeof(Boolean))
				{
					int b1 = (o1 == null || o1 is DBNull) ? 0 : (((bool)o1) ? 1 : -1);
					int b2 = (o2 == null || o2 is DBNull) ? 0 : (((bool)o2) ? 1 : -1);
					result = (b1 - b2) * ordering;
				}
				else if(o1 == null || o1 is DBNull)
				{
					return 1;
				}
				else if(o2 == null || o2 is DBNull)
				{
					return -1;
				}
				else if(type == typeof(Int32) || type == typeof(Int64))
				{
					long i1 = Convert.ToInt64(o1);
					long i2 = Convert.ToInt64(o2);
					if(i1 == i2)
						continue;
					result = ((i1 - i2) > 0) ? ordering : -ordering;
				}
				else if(type == typeof(DateTime))
				{
					DateTime t1 = Convert.ToDateTime(o1);
					DateTime t2 = Convert.ToDateTime(o2);
					if(t1 == t2)
						continue;
					if(t1 < t2)
						result = -ordering;
					else
						result = ordering;
				}
				else
				{
					string s1 = o1.ToString();
					string s2 = o2.ToString();
					result = String.Compare(s1, s2, StringComparison.CurrentCultureIgnoreCase) * ordering;
				}
				if(result != 0)
				{
					return result;
				}
			}
			return 0;	                         
		}
		
		private string sorting;
		public string Sorting
		{
			get { return sorting ?? ""; }
			set 
			{
				foreach(TreeViewColumn tw_col in this.TreeView.Columns)
				{
					tw_col.SortIndicator = false;
				}
					
				if(String.IsNullOrEmpty(value))
				{
					comparison_cols = new int[] { };
					sorting = value;
					this.ListStore.SetSortFunc(0, SortCompareFunc);
					this.ListStore.SetSortColumnId(0, SortType.Ascending);
					return;
				}
				string[] cols = value.Split(';', ',');
				List<int> result = new List<int>();
				foreach(string _col in cols)
				{
					string col = _col;
					int order = 1;
					if(col.Trim().ToLower().EndsWith(" desc"))
					{
						order = -1;
						col = col.Substring(0, col.Length - 5);
					}
					col = col.Trim();
					int index = this.DataTable.Columns[col].Ordinal;
					result.Add(index);
					result.Add(order);
	
					TreeViewColumn tw_col = this.TreeView.Columns[index];
					tw_col.SortIndicator = true;
					if(order == -1)
						tw_col.SortOrder = SortType.Descending;
					else
						tw_col.SortOrder = SortType.Ascending;
				}
				comparison_cols = result.ToArray();
				sorting = value;
				this.ListStore.SetSortFunc(0, SortCompareFunc);
				this.ListStore.SetSortColumnId(0, SortType.Ascending);
			}
		}
		
		public static void CreateColumnTreeViewColumns(TreeView view)
		{
			CellRendererText textCellRenderer = new CellRendererText();
			CellRendererToggle toggleCellRenderer = new CellRendererToggle();
			toggleCellRenderer.Activatable = true;
			toggleCellRenderer.Toggled += HandleToggleColumnVisible;
			toggleCellRenderer.Data["VIEW"] = view;
			
			TreeViewColumn col;
			col = new TreeViewColumn("Zobr.", toggleCellRenderer, "active", 4);
			view.AppendColumn(col);

			col = new TreeViewColumn("Sloupec", textCellRenderer, "text", 2);
			col.Resizable = true;
			view.AppendColumn(col);
			
			col = new TreeViewColumn("Id", textCellRenderer, "text", 1);
			col.Resizable = true;
			view.AppendColumn(col);
			
			col = new TreeViewColumn("Popis", textCellRenderer, "text", 3);
			col.Resizable = true;
			view.AppendColumn(col);
		}

		static void HandleToggleColumnVisible (object o, ToggledArgs args)
		{
			CellRendererToggle renderer = o as CellRendererToggle;
			TreeView view = renderer.Data["VIEW"] as TreeView;
			ListStore store = view.Model as ListStore;
			TreeIter iter;
			if (store.GetIter (out iter, new TreePath(args.Path))) {
				bool val = !(bool) store.GetValue(iter, 4);
				store.SetValue(iter,4,val);
				object[] data = (object[]) store.GetValue(iter, 0);
				TreeViewColumn col = data[1] as TreeViewColumn;
				col.Visible = val;
			}
		}

		/*
		public void UpdateFromActualTable(DataTable table)
		{
			using(DataTable changestable = this.DataTable.GetChanges())
			{
				if(changestable != null && changestable.Rows.Count != 0)
					throw new Exception("Only unchanged tables can be updated");
			}
			bool updated = false;	
			foreach(DataRow actual_row in table.Rows)
			{
				DataRow old_row = this.DataTable.Rows.Find(actual_row[0]);
				if(old_row == null)
				{
					old_row = this.DataTable.NewRow();
					for(int i=0; i<table.Columns.Count; i++)
					{
						string col_name = table.Columns[i].ColumnName;
						old_row[col_name] = actual_row[i];
					}
					updated = true;
					this.DataTable.Rows.Add(old_row);
				}
				else
				{
					for(int i=0; i<table.Columns.Count; i++)
					{
						string col_name = table.Columns[i].ColumnName;
						if(old_row[col_name] != actual_row[i])
						{
							old_row[col_name] = actual_row[i];
							updated = true;
						}
					}
				}
			}
			if(updated)
				this.DataTable.AcceptChanges();
		}
		*/
	}
}
