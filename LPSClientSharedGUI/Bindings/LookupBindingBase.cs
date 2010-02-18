using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public abstract class LookupBindingBase: BindingBase
	{
		public LookupBindingBase()
		{
		}

		public LookupBindingBase(ILookupInfo LookupInfo)
		{
			this.LookupInfo = LookupInfo;
		}

		private ILookupInfo lookupInfo;
		public ILookupInfo LookupInfo
		{
			get { return lookupInfo; }
			set
			{
				if(lookupInfo != null)
					DisposeLookupData();
				lookupInfo = value;
				if(lookupInfo != null)
					InitLookupData();
			}
		}

		protected DataSet LookupData { get; set; }
		protected DataTable LookupTable { get { return LookupData.Tables[0]; } }
		protected TableInfo LookupTableInfo { get; set; }
		protected ListStore Store { get; set; }

		protected virtual void InitLookupData()
		{
			LookupData = ServerConnection.Instance.GetCachedDataSet(LookupInfo.LookupTable);
			LookupTableInfo = ResourceManager.Instance.GetTableInfo(LookupInfo.LookupTable);
			CreateListStore();
			FillListStore();
			LookupTable.RowChanged += LookupTableRowChanged;
		}

		protected virtual void CreateListStore()
		{
			List<Type> types = new List<Type>(LookupInfo.LookupColumns.Length + 1);
			types.Add(typeof(long));
			types.Add(typeof(DataRow));
			foreach(string col_name in LookupInfo.LookupColumns)
				types.Add(LookupTable.Columns[col_name].DataType);
			Store = new ListStore(types.ToArray());
			//Store.DefaultSortFunc = SortCompareFunc;
		}

		//protected int SortCompareFunc(TreeModel model, TreeIter tia, TreeIter tib)
		//{
		//	object va = model.GetValue(tia, 1);
		//	object vb = model.GetValue(tib, 1);
		//}

		protected virtual void FillListStore()
		{
			foreach(DataRow r in LookupTable.Rows)
				AddListStoreRow(r);
			//if(Store.NColumns > 2)
			//	Store.Reorder(new int[] {1, 2});
			//else
			//	Store.Reorder(new int[] {1});
		}

		protected object[] GetValuesForStore(DataRow r)
		{
			ArrayList data = new ArrayList(LookupInfo.LookupColumns.Length + 1);
			data.Add(r[0]);
			data.Add(r);
			foreach(string col_name in LookupInfo.LookupColumns)
				data.Add(r[col_name]);
			return data.ToArray();
		}

		protected long GetIdFromIter(TreeIter iter)
		{
			return (long)Store.GetValue(iter, 0);
		}

		protected DataRow GetRowFromIter(TreeIter iter)
		{
			return (DataRow)Store.GetValue(iter, 1);
		}

		protected virtual void AddListStoreRow(DataRow r)
		{
			//Log.Debug("Lookup {0} add id {1}", LookupInfo.LookupTable, r[0]);
			Store.AppendValues(GetValuesForStore(r));
		}

		protected virtual void UpdateListStoreRow(TreeIter iter, DataRow r)
		{
			//Log.Debug("Lookup {0} update id {1}", LookupInfo.LookupTable, r[0]);
			Store.SetValues(iter, GetValuesForStore(r));
		}

		protected virtual void DeleteListStoreRow(DataRow r)
		{
			//Log.Debug("Lookup {0} delete id {1}", LookupInfo.LookupTable, r[0, DataRowVersion.Original]);
			TreeIter? iter = FindRow(r);
			if(iter != null)
			{
				TreeIter i2 = (TreeIter) iter;
				Store.Remove(ref i2);
			}
		}

		protected TreeIter? FindRow(DataRow row)
		{
			if(Store == null)
				throw new InvalidOperationException("Store is not assigned");
			TreeIter iter;
			if(!Store.GetIterFirst(out iter))
				return null;
			do
			{
				if(GetRowFromIter(iter) == row)
					return iter;
			}
			while(Store.IterNext(ref iter));
			return null;
		}

		protected virtual void LookupTableRowChanged (object sender, DataRowChangeEventArgs e)
		{
			if(Store == null)
				return;
			if(e.Action == DataRowAction.Add)
			{
				AddListStoreRow(e.Row);
			}
			else if(e.Action == DataRowAction.Delete
				|| e.Row.RowState == DataRowState.Deleted
				|| e.Row.RowState == DataRowState.Detached)
			{
				DeleteListStoreRow(e.Row);
			}
			else
			{
				TreeIter? iter = FindRow(e.Row);
				if(iter != null)
					UpdateListStoreRow((TreeIter)iter, e.Row);
				else
					AddListStoreRow(e.Row);
			}
		}

		protected virtual void DisposeLookupData()
		{
			LookupTable.RowChanged -= LookupTableRowChanged;
			ServerConnection.Instance.DisposeDataSet(LookupData);
			LookupData = null;
			LookupTableInfo = null;
			Store.Clear();
			Store.Dispose();
			Store = null;
		}

		public override void Dispose ()
		{
			if(lookupInfo != null)
				DisposeLookupData();
			base.Dispose();
		}

	}
}
