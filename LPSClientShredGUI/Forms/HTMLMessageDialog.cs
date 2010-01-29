/*
using System;
using Gtk;
using Gecko;

namespace LPSClient
{

	public class HTMLMessageDialog : XmlDialogBase
	{
		[Glade.Widget] ScrolledWindow scrolledwindow;
		Gecko.WebControl browser;
		
		public HTMLMessageDialog ()
		{
		}
		
		public override void OnCreate ()
		{
			base.OnCreate ();
			try
			{
				browser = new WebControl();
				scrolledwindow.AddWithViewport(browser);
				browser.ShowAll();
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
		
		private string GetContentOf(string tag, string str)
		{
			int start = str.ToLower().IndexOf("<"+tag.ToLower()+">");
			int end = str.ToLower().IndexOf("</"+tag.ToLower()+">");
			if(start >= 0 && end >= 0)
				return str.Substring(start + tag.Length + 2, end - start - tag.Length - 2);
			else
				return null;
		}
		
		public string Html
		{
			set 
			{
				this.browser.RenderData(value, "http://localhost/", "text/html");
				this.Window.Title = this.browser.Title;
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