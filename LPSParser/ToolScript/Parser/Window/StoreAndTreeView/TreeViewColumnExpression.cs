using System;
using Gtk;

namespace LPS.ToolScript.Parser
{
	public class TreeViewColumnExpression : ExpressionBase
	{
		public string ColumnType {get; private set; }
		public int StoreIndex { get; private set; }
		public EvaluatedAttributeList Params { get; private set; }

		public TreeViewColumnExpression(string ColumnType, int StoreIndex, EvaluatedAttributeList Params)
		{
			this.ColumnType = ColumnType;
			this.StoreIndex = StoreIndex;
			this.Params = Params;
		}

		public override object Eval (IExecutionContext context)
		{
			Params.Eval(context);
			return this;
		}

		public TreeViewColumn CreateColumn(WindowContext context)
		{
			string title = Params.Get<string>("title", "");

			switch(this.ColumnType)
			{
			case "text":
				return new TreeViewColumn(title, new CellRendererText(), "text", this.StoreIndex);
			case "markup":
				return new TreeViewColumn(title, new CellRendererText(), "markup", this.StoreIndex);
			default:
				throw new NotSupportedException("Sloupec typu '"+this.ColumnType+"' není podporován");
			}
		}

	}
}
