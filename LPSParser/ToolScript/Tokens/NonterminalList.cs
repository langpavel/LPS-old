using System;
using com.calitha.goldparser;
using System.Collections.Generic;

namespace LPS.ToolScript.Tokens
{
	public class NonterminalList: NonterminalBase
	{
		public List<TokenBase> Items { get; private set; }
		public Symbols[] ItemSymbols { get; private set; }

		public NonterminalList(NonterminalToken token, params Symbols[] ItemSymbols)
			: base(token)
		{
			this.Items = new List<TokenBase>();
			this.ItemSymbols = ItemSymbols;
		}

		/*
		public override object Execute(Context context)
		{
			object[] result = new object[Items.Count];
			for(int i = 0; i < Items.Count; i++)
				result[i] = Items[i].Execute(context);
			return result;
		}
		*/
	}
}
