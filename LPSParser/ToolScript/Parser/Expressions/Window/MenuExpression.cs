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

		public MenuExpression(string Name, WidgetParamList Params, MenuExpressionKind Kind, List<MenuExpression> MenuItems)
			: base(Name, Params)
		{
			this.MenuItems = MenuItems;
		}

		protected void AppendItems(MenuShell shell)
		{
			if(this.MenuItems != null)
				foreach(MenuExpression expr in MenuItems)
					shell.Append(expr.Build());
		}

		protected override Widget CreateWidget()
		{
			switch(this.Kind)
			{
			case MenuExpressionKind.MenuBar:
				Log.Debug("MenuBar");
				MenuBar bar = new MenuBar();
				AppendItems(bar);
				return bar;
			case MenuExpressionKind.Menu:
				Log.Debug("Menu");
				Menu menu = new Menu();
				AppendItems(menu);
				MenuItem item = new MenuItem(GetAttribute<string>("title",""));
				item.Submenu = menu;
				return item;
			case MenuExpressionKind.MenuItem:
				Log.Debug("MenuItem");
				MenuItem mi = new MenuItem(GetAttribute<string>("title",""));
				mi.Activated += delegate {
					Log.Debug("Item activated");
				};
				return mi;
			case MenuExpressionKind.ItemSeparator:
				return new SeparatorMenuItem();
			default:
				throw new NotImplementedException();
			}
		}
	}
}
