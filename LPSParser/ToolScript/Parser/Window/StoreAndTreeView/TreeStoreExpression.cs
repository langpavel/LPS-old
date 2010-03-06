using System;
using System.Collections.Generic;
using Gtk;

namespace LPS.ToolScript.Parser
{
	public class TreeStoreExpression : StoreExpressionBase
	{
		public TreeStoreExpression(EvaluatedAttributeList Params, List<StoreItemStatement> Items)
			:base(Params, Items)
		{
		}

		protected override TreeModel CreateStore ()
		{
			return new TreeStore(GetColumnTypes());
		}

		private void AppendChilds(TreeModel store, TreeIter iter, StoreItemStatement item)
		{
			if(item.Childs != null && item.Childs.Count != 0)
				foreach(StoreItemStatement child in item.Childs)
					AppendData(store, iter, child);
		}

		protected override TreeIter AppendData (TreeModel store, StoreItemStatement item)
		{
			TreeIter iter = ((TreeStore)store).AppendValues(item.Evaluated);
			AppendChilds(store, iter, item);
			return iter;
		}

		protected override TreeIter AppendData (TreeModel store, TreeIter parent, StoreItemStatement item)
		{
			TreeIter iter = ((TreeStore)store).AppendValues(parent, item.Evaluated);
			AppendChilds(store, iter, item);
			return iter;
		}

	}
}
