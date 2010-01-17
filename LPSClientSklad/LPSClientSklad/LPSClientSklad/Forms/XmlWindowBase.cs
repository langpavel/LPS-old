using System;
using Gtk;

namespace LPSClientSklad
{
	public class XmlWindowBase: IXmlFormHandler, IDisposable
	{
		public Glade.XML GladeXML { get; set; }
		public Window Window { get; set; }
		
		public XmlWindowBase ()
		{
		}
		
		public virtual void OnCreate()
		{
		}

		public virtual void Destroy()
		{
			if(this.Window != null)
			{
				this.Window.Destroy();
				this.Window = null;
				GladeXML.Dispose();
				GladeXML = null;
			}
		}
		
		public virtual void Dispose()
		{
			Destroy();
		}

		public virtual void Show()
		{
			this.Window.Show();
		}
		
		public virtual void ShowAll()
		{
			this.Window.ShowAll();
		}
		
	}
}
