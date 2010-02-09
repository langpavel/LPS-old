using System;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public class DataTableView: TreeView
	{
		private DataSet dataset;
		private DataTable table;
		private object[] parameters;
		private DataTableListStoreBinding binding;
		
		public DataTable Table
		{
			get { return table; }
			set { table = value; }
		}
		
		public IListInfo ListInfo { get; private set; }
		
		protected ServerConnection Connection { get { return ServerConnection.Instance; } }
		protected ResourceManager Resources { get { return this.Connection.Resources; } }
		
		public DataTableView(IListInfo info, params object[] parameters)
		{
			this.ListInfo = info;
			this.parameters = parameters;
			
			this.LoadData();
		}
		
		public override void Dispose ()
		{
			if(this.dataset != null)
			{
				this.dataset.Dispose();
				this.dataset = null;
			}
			if(this.binding != null)
			{
				this.binding.Dispose();
				this.binding = null;
			}
			base.Dispose();
		}
		
		protected void LoadData()
		{
			this.dataset = Connection.GetDataSetByTableName(ListInfo.TableName, this.parameters);
			this.table = this.dataset.Tables[0];
			
			binding = new DataTableListStoreBinding(this, this.table, this.ListInfo);
			binding.Bind();
		}

		protected override void OnRowActivated (TreePath path, TreeViewColumn column)
		{
			base.OnRowActivated (path, column);
			DataRow row = binding.GetRow(path);
			FormManager.Instance.GetWindow(this.ListInfo.DetailName, this.ListInfo, Convert.ToInt64(row["id"]));
		}
		
		public AutobindWindow OpenDetail()
		{
			DataRow row = binding.GetCurrentRow();
			if(row == null)
				return null;
			return FormManager.Instance.GetWindow(this.ListInfo.DetailName, this.ListInfo, Convert.ToInt64(row["id"]));
		}
		
		public AutobindWindow OpenNewDetail()
		{
			return FormManager.Instance.GetWindow(this.ListInfo.DetailName, this.ListInfo, 0L);
		}
		
		public AutobindWindow OpenDetailAndDelete()
		{
			AutobindWindow wnd = OpenDetail();
			if(wnd == null)
				return null;
			wnd.DeleteQuery();
			return wnd;
		}

		private string filter;
		public string Filter
		{
			get	{ return filter ?? ""; }
			set { filter = value; binding.ApplyFilter(filter); }
		}
	}
}
