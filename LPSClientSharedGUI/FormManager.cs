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
		
		private Dictionary<string, AutobindWindow> windows;

		private FormManager()
		{
			windows = new Dictionary<string, AutobindWindow>();
		}

		public AutobindWindow GetWindow(string detail_name, ModulesTreeInfo module, long id)
		{
			string win_id = String.Format("{0}[{1}]", detail_name, id);
			AutobindWindow win;
			if(windows.TryGetValue(win_id, out win))
			{
				win.Window.Present();
				return win;
			}
			else
			{
				FormInfo fi = FormFactory.Instance.GetFormInfo(detail_name);
				win = fi.CreateObject() as AutobindWindow;
				win.TableInfo = ServerConnection.Instance.Resources.GetTableInfo(module.Table);
				win.Load(id);
				windows.Add(win_id, win);
				win.Window.Destroyed += delegate {
					windows.Remove(win_id);
				};
				win.ShowAll();
				return win;
			}
		}
	}
}
