using System;
using System.Collections.Generic;
using Gtk;

namespace LPS.ToolScript.Parser
{
	public class ListStoreExpression : StoreExpressionBase
	{
		public ListStoreExpression(EvaluatedAttributeList Params, List<StoreItemStatement> Items)
			:base(Params, Items)
		{
		}

		protected override TreeModel CreateStore ()
		{
			return new ListStore(GetColumnTypes());
		}

		protected override TreeIter AppendData (TreeModel store, StoreItemStatement item)
		{
			if(item.Childs != null && item.Childs.Count != 0)
				throw new InvalidOperationException("ListStore nemůže obsahovat vnořené položky");
			return ((ListStore)store).AppendValues(item.Evaluated);
		}

		protected override TreeIter AppendData (TreeModel store, TreeIter parent, StoreItemStatement item)
		{
			throw new InvalidOperationException("ListStore nemůže obsahovat vnořené položky");
		}

	}
}
