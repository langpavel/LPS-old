using System;
using System.IO;
using System.Reflection;
using Gtk;

namespace LPS.Client
{
	public static class GtkTheme
	{
		public static bool LoadGtkResourceFile(string filename)
		{
			try
			{
				if(!File.Exists(filename))
				{
					Log.Error("Gtk resource {0} not exists", filename);
					return false;
				}
     			Gtk.Rc.AddDefaultFile(filename);
     			Gtk.Rc.Parse(filename);
				Log.Info("Gtk resource {0} loaded", filename);
				return true;
			}
			catch(Exception ex)
			{
				Log.Error("Gtk resource {0} failed to load: {1}", filename, ex);
				return false;
			}
		}

		public static void LoadGtkResourceFile()
		{
			string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			path = Path.Combine(path, "..");
			path = Path.Combine(path, "usr");
			path = Path.Combine(path, "theme");
			if(!LoadGtkResourceFile(Path.Combine(path, "gtkrc")))
			{
				string path2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				path2 = Path.Combine(path2, "..");
				path2 = Path.Combine(path2, "usr");
				path2 = Path.Combine(path2, "theme");
				if(path != path2)
					LoadGtkResourceFile(Path.Combine(path2, "gtkrc"));
			}
		}
	}
}
