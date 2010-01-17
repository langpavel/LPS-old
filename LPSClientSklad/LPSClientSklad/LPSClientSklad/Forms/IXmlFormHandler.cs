using System;
using Gtk;

namespace LPSClientSklad
{
	public interface IXmlFormHandler
	{
		Glade.XML GladeXML { get; set; }
		Window Window { get; set; }
		void OnCreate();
	}
}
