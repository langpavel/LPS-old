using System;
using System.Collections.Generic;
using Gtk;

namespace LPS.ToolScript.Parser
{
	public enum MenuExpressionKind
	{
		MenuBar,
		Menu,
		MenuItem,
		ItemSeparator
	}

	public class MenuExpression : WidgetBase
	{
		public MenuExpressionKind Kind { get; set; }
		public List<MenuExpression> MenuItems { get; private set; }

		public MenuExpression(string Name, MenuExpressionKind Kind, EvaluatedAttributeList Params, List<MenuExpression> MenuItems)
			: base(Name, Params)
		{
			this.Kind = Kind;
			this.MenuItems = MenuItems;
		}

		public override object Eval (IExecutionContext context)
		{
			object result = base.Eval(context);
			if(this.MenuItems != null)
				foreach(MenuExpression child in MenuItems)
					child.Eval(context);
			return result;
		}

		protected void AppendItems(MenuShell shell, WindowContext context)
		{
			if(this.MenuItems != null)
				foreach(MenuExpression expr in this.MenuItems)
					shell.Append(expr.Build(context));
		}

		private MenuItem CreateMenuItem(WindowContext context)
		{
			if(HasAttribute("stock"))
			{
				ImageMenuItem imi = new ImageMenuItem(this.GetAttribute<string>("stock"), context.AccelGroup);
				//IconSet icons = IconFactory.LookupDefault();
				//imi.Image = new Image(icons.RenderIcon(imi.Style, TextDirection.Ltr, StateType.Normal, IconSize.Menu, imi, null));
				return imi;
			}
			return new MenuItem(GetAttribute<string>("title",""));
		}

		protected override Widget CreateWidget(WindowContext context)
		{
			switch(this.Kind)
			{
			case MenuExpressionKind.MenuBar:
				MenuBar bar = new MenuBar();
				AppendItems(bar, context);
				return bar;
			case MenuExpressionKind.Menu:
				MenuItem item = CreateMenuItem(context);
				Menu menu = new Menu();
				item.Submenu = menu;
				AppendItems(menu, context);
				return item;
			case MenuExpressionKind.MenuItem:
				return CreateMenuItem(context);
			case MenuExpressionKind.ItemSeparator:
				return new SeparatorMenuItem();
			default:
				throw new NotImplementedException();
			}
		}
	}
}
