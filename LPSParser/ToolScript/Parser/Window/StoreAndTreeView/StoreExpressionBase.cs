using System;
using System.Collections.Generic;
using Gtk;

namespace LPS.ToolScript.Parser
{
	public abstract class StoreExpressionBase : ExpressionBase
	{
		public EvaluatedAttributeList Params { get; private set; }
		public List<StoreItemStatement> Items { get; private set; }

		public StoreExpressionBase(EvaluatedAttributeList Params, List<StoreItemStatement> Items)
		{
			this.Params = Params;
			this.Items = Items;
		}

		protected Type[] GetColumnTypes()
		{
			throw new NotImplementedException();
		}

		protected abstract TreeModel CreateStore();
		protected abstract TreeIter AppendData(TreeModel store, StoreItemStatement item);
		protected abstract TreeIter AppendData(TreeModel store, TreeIter parent, StoreItemStatement item);

		public override object Eval (IExecutionContext context)
		{
			foreach(StoreItemStatement item in Items)
				item.Run(context);
			TreeModel store = CreateStore();
			foreach(StoreItemStatement item in Items)
				AppendData(store, item);
			return store;
		}
	}
}
