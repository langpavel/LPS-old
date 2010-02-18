using System;
using System.Data;
using Gtk;
using System.Collections.Generic;

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
			this.FixedHeightMode = true;
			this.RulesHint = true;

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
			using(Log.Scope("Load {0}", this.ListInfo.Id))
			{
				this.dataset = Connection.GetDataSetByName(ListInfo.Id, "", this.parameters);
				this.table = this.dataset.Tables[0];
			
				Binding = new DataTableListStoreBinding(this, this.table, this.ListInfo);
				Binding.Bind();
				LoadConfiguration("__current");
			}
		}

		protected override void OnRowActivated (TreePath path, TreeViewColumn column)
		{
			using(Log.Scope("Row activated {0}", this.ListInfo.Id))
			{
				base.OnRowActivated (path, column);
				DataRow row = this.Binding.GetRow(path);
				FormManager.Instance.GetWindow(this.ListInfo.DetailName, Convert.ToInt64(row["id"]), this.ListInfo);
			}
		}
		
		public IManagedWindow OpenDetail()
		{
			using(Log.Scope("Open {0}", this.ListInfo.DetailName))
			{
				DataRow row = this.Binding.GetCurrentRow();
				if(row == null)
					return null;
				return FormManager.Instance.GetWindow(this.ListInfo.DetailName, Convert.ToInt64(row["id"]), this.ListInfo);
			}
		}
		
		public IManagedWindow OpenNewDetail()
		{
			using(Log.Scope("New {0}", this.ListInfo.DetailName))
			{
				IManagedWindow result = FormManager.Instance.GetWindow(this.ListInfo.DetailName, 0L, this.ListInfo);
				result.NewItem();
				return result;
			}
		}
		
		public bool OpenDetailAndDelete()
		{
			using(Log.Scope("Open and delete {0}", this.Binding.ListInfo.DetailName))
			{
				IManagedWindow wnd = OpenDetail();
				if(wnd == null)
					return false;
				return wnd.DeleteItem();
			}
		}

		private string filter;
		public string Filter
		{
			get	{ return filter ?? ""; }
			set { filter = value; this.Binding.ApplyFilter(filter); }
		}

		private string conf_path;
		public string ConfigurationPath
		{
			get
			{
				return conf_path ??
					(conf_path = String.Format("DataTableView/{0}", this.ListInfo.Id));
			}
			set { conf_path = value; }
		}

		public string[] GetAvailableConfigurations()
		{
			List<string> result = Connection.Configuration.GetAvailableConfigurations(ConfigurationPath, true);
			result.Remove("__current");
			return result.ToArray();
		}

		private string conf_name;
		public string ConfigurationName
		{
			get { return conf_name; }
			set { LoadConfiguration(conf_name = value); }
		}

		public void SaveConfiguration(string name)
		{
			Connection.Configuration.SaveConfiguration(ConfigurationPath, name,
				new DataTableViewConfiguration(this));
		}

		public void LoadConfiguration(string name)
		{
			DataTableViewConfiguration conf = Connection.Configuration.GetConfiguration<DataTableViewConfiguration>(
				ConfigurationPath, name, null);
			if(conf != null)
				conf.ApplyTo(this);
		}

		public ConfigurableColumn GetColumn(string name)
		{
			foreach(ConfigurableColumn col in this.Columns)
			{
				if(col == null)
					continue;
				if(col.ColumnInfo != null && col.ColumnInfo.Name == name)
					return col;
				if(col.DataColumn != null && col.DataColumn.ColumnName == name)
					return col;
			}
			return null;
		}

		public ConfigurableColumn GetColumn(DataColumn dc)
		{
			foreach(ConfigurableColumn col in this.Columns)
			{
				if(col == null)
					continue;
				if(col.DataColumn == dc)
					return col;
			}
			return null;
		}

		public string Sorting
		{
			get { return Binding.Sorting; }
			set { Binding.Sorting = value; }
		}
	}
}
