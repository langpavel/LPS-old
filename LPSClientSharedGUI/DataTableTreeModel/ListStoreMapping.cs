using GLib;
using Gtk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace LPS.Client
{
	public delegate object GetStoreValueCallback(DataRow row);

	public class ListStoreMapping : IDisposable
	{
		private List<GType> store_types;
		private List<GetStoreValueCallback> callbacks;
		public NodeStore ColumnsStore { get; private set; }

		public ListStoreMapping()
		{
			store_types = new List<GType>();
			callbacks = new List<GetStoreValueCallback>();
			store_types.Add((GType)typeof(DataRow));
			ColumnsStore = new NodeStore(typeof(ConfigurableColumn));
		}

		public int AddValueMapping(GType store_type, GetStoreValueCallback callback)
		{
			store_types.Add(store_type);
			callbacks.Add(callback);
			return callbacks.Count;
		}

		public GType[] GetStoreTypes()
		{
			return store_types.ToArray();
		}

		public object[] GetValuesForStore(DataRow row)
		{
			object[] vals = new object[store_types.Count];
			vals[0] = row;
			for(int i=0; i<callbacks.Count; i++)
				vals[i+1] = callbacks[i](row);
			return vals;
		}

		public ConfigurableColumn CreateColumn(IColumnInfo info, DataColumn column)
		{
			if(info != null)
			{
				if(!String.IsNullOrEmpty(info.FkReferenceTable))
					return new LookupColumn(this, info, column);
			}
			if(column != null)
			{
				Type type = column.DataType;
				if(type == typeof(DateTime))
					return new DateTimeColumn(this, info, column);
				else if(type == typeof(string))
					return new StringColumn(this, info, column);
				else if(type == typeof(bool))
					return new CheckBoxColumn(this, info, column);
				else if(type == typeof(Decimal))
					return new DecimalColumn(this, info, column);
				else if(type == typeof(Int64))
					return new Int64Column(this, info, column);
				else if(type == typeof(Int32))
					return new Int64Column(this, info, column);
				throw new Exception("Nelze vytvořit sloupeček pro typ " + type.ToString());
			}
			throw new Exception("Nelze vytvořit sloupeček");
		}

		public event EventHandler ColumnClicked;
		public void OnColumnClicked(ConfigurableColumn column)
		{
			if(ColumnClicked != null)
				ColumnClicked(column, EventArgs.Empty);
		}

		public ConfigurableColumn[] GetColumns()
		{
			List<ConfigurableColumn> result = new List<ConfigurableColumn>();
			foreach(ConfigurableColumn col in ColumnsStore)
			{
				result.Add(col);
			}
			return result.ToArray();
		}

		public void Clear()
		{
			foreach(ConfigurableColumn col in GetColumns())
				col.Dispose();
			ColumnsStore.Clear();
			store_types.Clear();
			callbacks.Clear();
			store_types.Add((GType)typeof(DataRow));
		}

		private int CompareConfigurableColumnPosition(ConfigurableColumn x, ConfigurableColumn y)
		{
			return x.Conf_Index - y.Conf_Index;
		}

		public void ReorderColumns()
		{
			List<ConfigurableColumn> result = new List<ConfigurableColumn>();
			foreach(ConfigurableColumn col in ColumnsStore)
				result.Add(col);
			result.Sort(CompareConfigurableColumnPosition);
			ColumnsStore.Clear();
			foreach(ConfigurableColumn col in result)
				ColumnsStore.AddNode(col);
		}

		public void Dispose ()
		{
			Clear();
			ColumnsStore.Dispose();
		}
	}
}
