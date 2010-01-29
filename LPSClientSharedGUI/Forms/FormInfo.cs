using System;
using System.Reflection;
using Gtk;

namespace LPS.Client
{
	public abstract class FormInfo
	{
		public string Id { get; set; }

		public FormInfo ()
		{
		}

		public abstract object CreateObject();
		
		public T Create<T>()
		{
			return (T)CreateObject();
		}
	}
	
	public class FormInfo<T> : FormInfo, ICloneable where T: new() 
	{
		protected virtual void CallInterfaces(object o, Glade.XML xml, Window window)
		{
			IXmlFormHandler handler = o as IXmlFormHandler;
			if(handler != null)
			{
				handler.GladeXML = xml;
				handler.Window = window;
			}
			if(handler != null)
				handler.OnCreate();
		}

		public override object CreateObject()
		{
			return this.Create();
		}

		public virtual T Create ()
		{
			T result = new T();
			CallInterfaces(result, null, null);
			return result;
		}

		public virtual object Clone()
		{
			return this.MemberwiseClone();
		}
	}
	
	public class FormXmlResourceInfo<T> : FormInfo<T>, ICloneable where T: new() 
	{
		public Assembly ResourceAssembly { get; set; }
		public string ResourceName { get; set; }
		public string RootId { get; set; }
		public string XmlDomain { get; set; }
		
		public FormXmlResourceInfo(string Id, Assembly ResourceAssembly, string ResourceName, string RootId, string XmlDomain)
		{
			this.Id = Id;
			this.ResourceAssembly = ResourceAssembly;
			this.ResourceName = ResourceName;
			this.RootId = RootId;
			this.XmlDomain = XmlDomain;
		}

		public FormXmlResourceInfo(string Id, Assembly ResourceAssembly, string ResourceName, string RootId)
			: this(Id, ResourceAssembly, ResourceName, RootId, null)
		{
		}
		
		public FormXmlResourceInfo(string Id, string ResourceName, string RootId)
			: this(Id, null, ResourceName, RootId, null)
		{
			this.ResourceAssembly = Assembly.GetAssembly(typeof(T));
		}
		
		public FormXmlResourceInfo(string Id, string ResourceName)
			: this(Id, null, ResourceName, null, null)
		{
			this.ResourceAssembly = Assembly.GetAssembly(typeof(T));
		}
		
		public override T Create ()
		{
			T result = new T();
			Glade.XML gxml = new Glade.XML(ResourceAssembly, ResourceName, RootId ?? Id, XmlDomain);
			gxml.Autoconnect(result);
			Window wnd = gxml.GetWidget(RootId ?? Id) as Window;
			CallInterfaces(result, gxml, wnd);
			return result;
		}
		
		public override object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}
