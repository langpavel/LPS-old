using System;
using System.Collections.Generic;
using Gtk;

namespace LPSClient
{
	public class XmlWindowBase: IXmlFormHandler, IDisposable
	{
		public Glade.XML GladeXML { get; set; }
		public Window Window { get; set; }
		public virtual ModulesTreeInfo ListInfo { get; set; }
		//public bool DestroyOnDelete { get; set; }
		private List<IDisposable> _OwnedComponents;
		public List<IDisposable> OwnedComponents
		{ 
			get { return _OwnedComponents ?? (_OwnedComponents = new List<IDisposable>()); } 
		}

		public ServerConnection Connection { get { return ServerConnection.Instance; } }
		
		public XmlWindowBase ()
		{
			//DestroyOnDelete = true;
		}
		
		public virtual void OnCreate()
		{
			// to neni ta udalost! ;-(
			//this.Window.DeleteEvent += delegate {
			//	if(DestroyOnDelete)
			//		Destroy();
			//};
		}

		public virtual void Destroy()
		{
			if(this._OwnedComponents != null)
			{
				_OwnedComponents.Reverse();
				foreach(IDisposable obj in _OwnedComponents)
				{
					try
					{
						obj.Dispose();
					}
					catch(Exception ex)
					{
						if(obj == null)
							Console.WriteLine("Destroy err - OwnedComponents null: {0}", ex);
						else
							Console.WriteLine("Destroy err - _OwnedComponents: {0} {1}", obj.GetType(), ex);
					}
				}
				_OwnedComponents = null;
			}
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
		
		public void ShowMessage(MessageType msgType, string caption, string text, params object[] args)
		{
			string txt = String.Format(text, args);
			txt = txt.Replace("&","&amp;");
			txt = txt.Replace("<","&lt;");
			
			using(Gtk.MessageDialog d = 
				new Gtk.MessageDialog(
					this.Window, 
					DialogFlags.DestroyWithParent | DialogFlags.Modal,
					msgType, ButtonsType.Ok, txt))
			{
				d.Title = caption;
				d.Run();
				d.Destroy();
			}
		}

	}
}
