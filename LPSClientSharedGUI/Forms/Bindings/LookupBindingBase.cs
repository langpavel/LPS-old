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

		protected DataTable LookupTable { get; set; }
		protected TableInfo LookupTableInfo { get; set; }
		protected ListStore Store { get; set; }

		protected virtual void InitLookupData()
		{
			LookupTable = ServerConnection.Instance.GetCachedDataSet(LookupInfo.LookupTable).Tables[0];
			LookupTableInfo = ResourceManager.Instance.GetTableInfo(LookupInfo.LookupTable);
			CreateListStore();
			FillListStore();
		}

		protected virtual void CreateListStore()
		{
			List<Type> types = new List<Type>(LookupInfo.LookupColumns.Length + 1);
			types.Add(typeof(long));
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

		protected virtual void AddListStoreRow(System.Data.DataRow r)
		{
			ArrayList data = new ArrayList(LookupInfo.LookupColumns.Length + 1);
			data.Add(r[0]);
			foreach(string col_name in LookupInfo.LookupColumns)
				data.Add(r[col_name]);
			Store.AppendValues(data.ToArray());
		}

		protected virtual void DisposeLookupData()
		{
			LookupTable = null;
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
