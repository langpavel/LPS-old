using System;

namespace LPS.ToolScript
{
	public class WindowContext : ExecutionContext
	{
		public Gtk.Window Window { get; set; }
		public Gtk.AccelGroup AccelGroup { get; private set; }

		private WindowContext(IExecutionContext ParentContext, IExecutionContext GlobalContext, ToolScriptParser Parser)
			: base(ParentContext, GlobalContext, Parser)
		{
		}

		public static WindowContext CreateAsChild(IExecutionContext parent)
		{
			if(parent == null)
				throw new ArgumentNullException("parent");
			return new WindowContext(parent, parent.GlobalContext, parent.Parser);
		}

		public WindowContext CloneWindowContext()
		{
			WindowContext clone = (WindowContext)base.Clone();
			clone.AccelGroup = new Gtk.AccelGroup();
			return clone;
		}

		public WindowContext Show()
		{
			this.Window.ShowAll();
			return this;
		}

		public int ShowDialog()
		{
			try
			{
				if(!(this.Window is Gtk.Dialog))
					throw new InvalidOperationException("Okno není dialog");
				return ((Gtk.Dialog)this.Window).Run();
			}
			finally
			{
				this.Window.Hide();
			}
		}

	}
}
