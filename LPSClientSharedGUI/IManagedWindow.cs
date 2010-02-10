using System;

namespace LPS.Client
{
	public interface IManagedWindow
	{
		string WindowName { get; }
		long Id { get; }
		TableInfo TableInfo { get; set; }
		// For menu item group category
		string Category { get; }
		// For menu item caption
		string Title { get; }
		void NewItem();
		void LoadItem(long id);
		bool DeleteItem();
		void Present();
		event EventHandler Destroyed;
	}
}
