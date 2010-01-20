using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using Gtk;

namespace LPSClient
{

	public class DataTableListStoreBinding: IDisposable
	{

		public DataTableListStoreBinding()
		{
		}
		
		public DataTableListStoreBinding(TreeView view, DataTable dt)
		{
			this.TreeView = view;
			this.DataTable = dt;
		}

		public TreeView TreeView { get; set; }
		public DataTable DataTable { get; set; }
		public ListStore ListStore { get; set; }
		public bool UseMarkup { get; set; }

		public void Dispose()
		{
			this.ListStore.Clear();
			this.ListStore.Dispose();
		}
		
		public void Bind()
		{
			CellRendererToggle rendererToggle = new CellRendererToggle();
			CellRendererText rendererText = new CellRendererText2();
			rendererText.FixedHeightFromFont = 1;
			
			List<Type> types = new List<Type>();
			for(int i = 0; i < this.DataTable.Columns.Count; i++)
			{
				DataColumn dc = this.DataTable.Columns[i];
				TreeViewColumn wc;
				string caption = dc.Caption.Replace('_',' ');
				if(dc.DataType == typeof(bool))
				{
					wc = new TreeViewColumn(caption, rendererToggle, "active", i); //, "inconsistent", i+1000, "radio",i+1000);
					types.Add(typeof(bool));
				}
				else
				{
					types.Add(typeof(string));
					if(UseMarkup)
						wc = new TreeViewColumn(caption, rendererText, "markup", i);
					else
						wc = new TreeViewColumn(caption, rendererText, "text", i);
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
			
			object[] data = new object[this.DataTable.Columns.Count];
			foreach(DataRow row in this.DataTable.Rows)
			{
				for(int i=0; i < this.DataTable.Columns.Count; i++)
				{
					DataColumn col = this.DataTable.Columns[i];
					object val = row[i];
					if(col.DataType == typeof(bool))
					{
						if(val == null || val == DBNull.Value)
							data[i] = false;
						else
							data[i] = val;
					}
					else
					{
						if(val == null || val == DBNull.Value)
							data[i] = "";
						else
							data[i] = val.ToString();
					}
				}
				this.ListStore.AppendValues(data);
			}

			this.TreeView.Model = this.ListStore;
		}
	}
}
