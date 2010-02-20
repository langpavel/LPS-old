using System;

namespace LPS.ToolScript
{
	public sealed class Context
	{
		public Context GlobalContext { get; private set; }
		public Context ParentContext { get; private set; }

		private Context(Context ParentContext, Context GlobalContext)
		{
			this.GlobalContext = GlobalContext;
			this.ParentContext = ParentContext;
		}

		public static Context CreateRootContext()
		{
			return new Context(null, new Context(null, null));
		}

		public Context CreateChildContext()
		{
			return new Context(this, this.GlobalContext);
		}
	}
}
