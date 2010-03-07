using System;
using Gtk;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class TreeViewExpression : WidgetBase
	{
		public List<TreeViewColumnExpression> Columns { get; private set; }

		public TreeViewExpression(string Name, EvaluatedAttributeList Params, List<TreeViewColumnExpression> Columns)
			: base(Name, Params)
		{
			this.Columns = Columns;
		}

		public override object Eval(IExecutionContext context)
		{
			base.Eval(context);
			foreach(TreeViewColumnExpression col in Columns)
				col.Run(context);
			return this;
		}

		private void CreateColumns(WindowContext context, TreeView view)
		{
			foreach(TreeViewColumnExpression col in this.Columns)
				view.AppendColumn(col.CreateColumn(context));
		}

		protected override Widget CreateWidget(WindowContext context)
		{
			TreeView view = new TreeView();
			CreateColumns(context, view);
			return view;
		}

	}
}
