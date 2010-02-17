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
		public DataTableListStoreBinding Binding { get; private set; }
		
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
			if(this.Binding != null)
			{
				this.Binding.Dispose();
				this.Binding = null;
			}
			if(this.dataset != null)
			{
				Connection.DisposeDataSet(this.dataset);
				this.dataset = null;
			}
			Destroy();
			base.Dispose();
		}

		protected override void OnColumnsChanged()
		{
			// when columns are reodered, added...
			base.OnColumnsChanged();
			if(Binding != null && Binding.Mapping != null)
				Binding.Mapping.ReorderColumns();

		}

		protected void LoadData()
		{
			this.dataset = Connection.GetDataSetByName(ListInfo.Id, "", this.parameters);
			this.table = this.dataset.Tables[0];
			
			Binding = new DataTableListStoreBinding(this, this.table, this.ListInfo);
			Binding.Bind();
		}

		protected override void OnRowActivated (TreePath path, TreeViewColumn column)
		{
			base.OnRowActivated (path, column);
			DataRow row = this.Binding.GetRow(path);
			FormManager.Instance.GetWindow(this.ListInfo.DetailName, Convert.ToInt64(row["id"]), this.ListInfo);
		}
		
		public IManagedWindow OpenDetail()
		{
			DataRow row = this.Binding.GetCurrentRow();
			if(row == null)
				return null;
			return FormManager.Instance.GetWindow(this.ListInfo.DetailName, Convert.ToInt64(row["id"]), this.ListInfo);
		}
		
		public IManagedWindow OpenNewDetail()
		{
			IManagedWindow result = FormManager.Instance.GetWindow(this.ListInfo.DetailName, 0L, this.ListInfo);
			result.NewItem();
			return result;
		}
		
		public bool OpenDetailAndDelete()
		{
			IManagedWindow wnd = OpenDetail();
			if(wnd == null)
				return false;
			return wnd.DeleteItem();
		}

		private string filter;
		public string Filter
		{
			get	{ return filter ?? ""; }
			set { filter = value; this.Binding.ApplyFilter(filter); }
		}
	}
}
