using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using Gtk;

namespace LPS.Client
{
	/// <summary>
	/// Vytvori ListStore pomoci ListStoreMapping
	/// </summary>
	public class DataTableListStoreBinding: IDisposable
	{
		public DataTableListStoreBinding(DataTableView view, DataTable dt, IListInfo listinfo)
		{
			this.Mapping = new ListStoreMapping();
			this.DataTableView = view;
			this.DataTable = dt;
			this.ListInfo = listinfo;
			this.Mapping.ColumnClicked += ToggleSort;
		}

		private Dictionary<DataRow, TreeIter> row_iters;
		public ListStoreMapping Mapping { get; private set; }
		public DataTableView DataTableView { get; set; }
		public DataTable DataTable { get; set; }
		public ListStore ListStore { get; set; }
		public TreeModelFilter Filter { get; set; }
		public IListInfo ListInfo { get; set; }

		public void Dispose()
		{
			Unbind();
		}

		public void Bind()
		{
			List<DataColumn> added = new List<DataColumn>();
			foreach(IColumnInfo colinfo in ListInfo.Columns)
			{
				DataColumn datacol = null;
				if(DataTable.Columns.Contains(colinfo.Name))
				{
					datacol = DataTable.Columns[colinfo.Name];
					added.Add(datacol);
				}
				this.Mapping.CreateColumn(colinfo, datacol);
			}
			foreach(DataColumn datacol in this.DataTable.Columns)
			{
				if(!added.Contains(datacol))
				{
					this.Mapping.CreateColumn(null, datacol);
				}
			}
			foreach(TreeViewColumn col in this.Mapping.GetColumns())
			{
				this.DataTableView.AppendColumn(col);
			}
			this.ListStore = new ListStore(this.Mapping.GetStoreTypes());
			FillDataList();
			this.Filter = new TreeModelFilter(this.ListStore, null);
			this.Filter.VisibleFunc = FilterFunc;
			this.DataTableView.Model = this.Filter;
			this.DataTable.RowChanged += HandleDataTableRowChanged;
			this.DataTable.RowDeleted += HandleDataTableRowDeleted;
		}

		void ToggleSort(object sender, EventArgs e)
		{
			ConfigurableColumn col = (ConfigurableColumn)sender;
			if(!col.SortIndicator || col.SortOrder == SortType.Descending)
				Sorting = col.DataColumn.ColumnName;
			else
				Sorting = col.DataColumn.ColumnName + " desc";
		}

		public void Unbind()
		{
			this.Mapping.Clear();
			if(this.DataTable != null)
			{
				this.DataTable.RowChanged -= HandleDataTableRowChanged;
				this.DataTable.RowDeleted -= HandleDataTableRowDeleted;
				this.DataTable = null;
			}
			if(this.DataTableView != null)
			{
				this.DataTableView.Model = null;
				while(this.DataTableView.Columns.Length != 0)
				{
					this.DataTableView.RemoveColumn(this.DataTableView.Columns[this.DataTableView.Columns.Length-1]);
				}
			}
			if(this.Filter != null)
			{
				this.Filter.Dispose();
				this.Filter = null;
			}
			if(this.ListStore != null)
			{
				this.ListStore.Clear();
				this.ListStore.Dispose();
				this.ListStore = null;
			}
		}
		
		void HandleDataTableRowChanged (object sender, DataRowChangeEventArgs e)
		{
			TreeIter iter;
			if(row_iters.TryGetValue(e.Row, out iter))
			{
				if(e.Row.RowState == DataRowState.Deleted || e.Row.RowState == DataRowState.Detached)
				{
					iter = row_iters[e.Row];
					this.ListStore.Remove(ref iter);
					row_iters.Remove(e.Row);
				}
				else
				{
					iter = row_iters[e.Row];
					this.ListStore.SetValues(iter, Mapping.GetValuesForStore(e.Row));
				}
			}
			else
			{
				if(e.Row.RowState == DataRowState.Deleted || e.Row.RowState == DataRowState.Detached)
					return;
				row_iters[e.Row] = this.ListStore.AppendValues(Mapping.GetValuesForStore(e.Row));
			}
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

		public void FillDataList(DataRowCollection rows)
		{
			row_iters = new Dictionary<DataRow, TreeIter>(this.DataTable.Rows.Count);
			this.ListStore.Clear();
			foreach(DataRow row in this.DataTable.Rows)
			{
				row_iters[row] = this.ListStore.AppendValues(Mapping.GetValuesForStore(row));
			}
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
			using(Log.Scope("filter: {0}", (filter == null)?"disable":"'"+filter+"'"))
			{
				if(!String.IsNullOrEmpty(filter))
					filter_str = filter.ToLower();
				else
					filter_str = filter;
				this.Filter.Refilter();
			}
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
				foreach(TreeViewColumn tw_col in this.DataTableView.Columns)
					tw_col.SortIndicator = false;

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
	
					TreeViewColumn tw_col = this.DataTableView.GetColumn(col);
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
		
		public DataRow GetCurrentRow()
		{
			TreeModel model;
			TreeIter iter;
			if(this.DataTableView.Selection.GetSelected(out model, out iter))
			{
				return model.GetValue(iter, 0) as DataRow;
			}
			return null;
		}
	}
}
