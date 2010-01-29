using System;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public abstract class WidgetRowBinding: IDisposable
	{
		public Widget Widget { get; set; }
		public DataRow Row { get; set; }
		public DataColumn Column { get; set; }

		public WidgetRowBinding()
		{
		}
		
		public abstract void Bind();
		public abstract void Unbind();
		
		public virtual void Dispose()
		{
			Unbind();
		}
	}
}
