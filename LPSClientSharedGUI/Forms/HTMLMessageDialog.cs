/*
using System;
using Gtk;
using Mono.WebBrowser;

namespace LPS.Client
{

	public class HTMLMessageDialog : XmlDialogBase
	{
		[Glade.Widget] ScrolledWindow scrolledwindow;
		IWebBrowser browser;
		
		public HTMLMessageDialog ()
		{
		}
		
		public override void OnCreate ()
		{
			base.OnCreate ();
			try
			{
				browser = Mono.WebBrowser.Manager.GetNewInstance(Platform.Gtk);
				if(!browser.Initialized)
				{
					throw new ApplicationException("gluezilla needed");
				}
				browser.Load(scrolledwindow.Handle, 640,480);
				browser.
				scrolledwindow.ShowAll();
			}
			catch
			{		
				this.Window.Destroy();
				throw;
			}
		}

		public string Title
		{
			get { return this.Window.Title; }
			set { this.Window.Title = value; }
		}
		
		/ *
		private string GetContentOf(string tag, string str)
		{
			int start = str.ToLower().IndexOf("<"+tag.ToLower()+">");
			int end = str.ToLower().IndexOf("</"+tag.ToLower()+">");
			if(start >= 0 && end >= 0)
				return str.Substring(start + tag.Length + 2, end - start - tag.Length - 2);
			else
				return null;
		}
		* /
		
		public string Html
		{
			set 
			{
				this.browser.Render(value);
				this.Window.Title = this.browser.Document.Title;
			}
		}

		public static void Show(string htmltext)
		{
			HTMLMessageDialog dlg = FormFactory.Create<HTMLMessageDialog>("html-message");
			try
			{
				dlg.Html = htmltext;
				dlg.Run();
			}
			finally
			{
				dlg.Destroy();
				dlg.Dispose();
			}
		}
		
	}
}
*/