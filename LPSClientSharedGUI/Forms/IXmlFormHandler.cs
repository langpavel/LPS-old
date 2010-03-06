using System;
using Gtk;
using LPS;

namespace LPS.Client
{
	public interface IXmlFormHandler
	{
		Glade.XML GladeXML { get; set; }
		Window Window { get; set; }
		ITableInfo TableInfo { get; set; }
		void OnCreate();
	}
}
