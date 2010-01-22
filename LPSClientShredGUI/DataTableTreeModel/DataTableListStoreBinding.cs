using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using Gtk;

namespace LPSClient
{
	/// <summary>
	/// Vytvori ListStore, kde prvni hodnota je row, druha je rezevovana
	/// a dale to je vzy column, is_null, takze step == 2
	/// </summary>
	public class DataTableListStoreBinding: IDisposable
	{

		public DataTableListStoreBinding()
			: this(null, null)
		{
		}
		
		public DataTableListStoreBinding(TreeView view, DataTable dt)
		{
			this.TreeView = view;
			this.DataTable = dt;
			this.MappedColumns = new Dictionary<string, GetMappedColumnValue>();
			MappedColumns["id_user_create"] = new DataTableListStoreBinding.GetMappedColumnValue(GetUserName);
			MappedColumns["id_user_modify"] = new DataTableListStoreBinding.GetMappedColumnValue(GetUserName);
		}

		public TreeView TreeView { get; set; }
		public DataTable DataTable { get; set; }
		public ListStore ListStore { get; set; }
		public TreeModelFilter Filter { get; set; }
		//public TreeModelSort Sort { get; set; }
		public bool UseMarkup { get; set; }

		public delegate string GetMappedColumnValue(object val, DataRow row);
		public Dictionary<string, GetMappedColumnValue> MappedColumns;
		
		public void Dispose()
		{
			//this.Sort.Dispose();
			this.Filter.Dispose();
			this.ListStore.Clear();
			this.ListStore.Dispose();
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
		
		public void Bind()
		{
			getValFuncs = new GetMappedColumnValue[this.DataTable.Columns.Count];
				
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
				string caption = dc.Caption.Replace('_',' ');
				GetMappedColumnValue getValFunc = null;
				getValFuncs[i] = null;
				if(this.MappedColumns.TryGetValue(dc.ColumnName, out getValFunc))
				{
					getValFuncs[i] = getValFunc;
					types.Add(typeof(string));
					types.Add(typeof(bool));
					wc = new TreeViewColumn(caption, rendererText, "markup", (i+1)*2);
				}
				else if(dc.DataType == typeof(bool))
				{
					wc = new TreeViewColumn(caption, rendererToggle, "active", (i+1)*2, "inconsistent", (i+1)*2+1); 
					types.Add(typeof(bool));
					types.Add(typeof(bool));
				}
				else
				{
					types.Add(typeof(string));
					types.Add(typeof(bool));
					if(UseMarkup)
						wc = new TreeViewColumn(caption, rendererText, "markup", (i+1)*2);
					else
						wc = new TreeViewColumn(caption, rendererText, "text", (i+1)*2);
				}
				wc.Reorderable = true;
				wc.MinWidth = 4;
				wc.MaxWidth = 1000;
				wc.Resizable = true;
				wc.FixedWidth = 50;
				wc.Sizing = TreeViewColumnSizing.Autosize;
				//wc.SortIndicator = true;
				//wc.SortOrder = SortType.Ascending;
				this.TreeView.AppendColumn(wc);

			}
			this.ListStore = new ListStore(types.ToArray());
			//this.ListStore.Data["_BINDING"] = this;
			FillDataList();
			//this.Sort = new TreeModelSort(this.ListStore);
			//this.Filter = new TreeModelFilter(this.Sort, null);
			this.Filter = new TreeModelFilter(this.ListStore, null);
			this.Filter.Data["_BINDING"] = this;
			this.TreeView.Model = this.Filter;
		}

		public void FillDataList()
		{
			FillDataList(this.DataTable.Rows);
		}
		
		public void FillDataList(DataRowCollection rows)
		{
			this.ListStore.Clear();
			object[] data = new object[(this.DataTable.Columns.Count + 1) * 2];
			foreach(DataRow row in this.DataTable.Rows)
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
				this.ListStore.AppendValues(data);
			}
		}
		
		public static DataTableListStoreBinding Get(TreeView view)
		{
			return (view.Model as TreeModelFilter).Data["_BINDING"] as DataTableListStoreBinding;
		}
		
		public DataRow GetRow(TreePath path)
		{
			TreeIter iter;
			if(!this.ListStore.GetIter(out iter, path))
				return null;
			return this.ListStore.GetValue(iter, 0) as DataRow;
		}
	}
}
