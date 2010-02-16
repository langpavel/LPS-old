using System;
using System.Threading;

namespace LPS
{
	public sealed class LogScope: IDisposable
	{
		public LogScope ParentScope { get; private set; }
		public LogScope ChildScope { get; private set; }
		public string Source { get; private set; }
		public string Text { get; private set; }
		public int Level { get; private set; }
		public bool IsRootScope { get { return this.Level == 0; } }
		public Thread Thread { get; private set; }
		public DateTime CreateDateTime { get; private set; }
		public event EventHandler Disposed;

		public LogScope(LogScope ParentScope, string Source, string Text)
		{
			this.CreateDateTime = DateTime.Now;
			this.ParentScope = ParentScope;
			this.Source = Source;
			this.Text = Text;
			this.Thread = Thread.CurrentThread;

			if(this.ParentScope != null)
			{
				if(this.ParentScope.ChildScope != null)
					throw new ApplicationException("Log scope error");
				this.ParentScope.ChildScope = this;
				this.Level = this.ParentScope.Level + 1;
			}
			else
			{
				this.Level = 0;
			}
		}

		public void Dispose()
		{
			if(this.ChildScope != null)
				this.ChildScope.Dispose();
			if(Disposed != null)
				Disposed(this, EventArgs.Empty);
			if(this.ParentScope != null)
				this.ParentScope.ChildScope = null;
		}
	}
}
