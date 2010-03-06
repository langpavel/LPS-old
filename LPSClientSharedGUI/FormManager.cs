using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public class FormManager
	{
		private static FormManager instance;
		public static FormManager Instance { get { return instance ?? (instance = new FormManager()); } }
		
		private List<IManagedWindow> windows;

		private FormManager()
		{
			windows = new List<IManagedWindow>();
		}

		public IManagedWindow GetWindow(string window_name, long id, IListInfo module)
		{
			ITableInfo tableinfo = null;
			if(module != null)
				tableinfo = ServerConnection.Instance.Resources.GetTableInfo(module.TableName);
			if(id != 0)
			{
				foreach(IManagedWindow window in windows)
				{
					if(window.WindowName == window_name && window.Id == id && window.TableInfo == tableinfo)
					{
						window.Present();
						return window;
					}
				}
			}
			IManagedWindow win = (IManagedWindow)FormFactory.Create(window_name);
			win.TableInfo = tableinfo;
			win.LoadItem(id);
			windows.Add(win);
			win.Destroyed += delegate {
				windows.Remove(win);
			};
			win.Present();
			return win;
		}
	}
}
