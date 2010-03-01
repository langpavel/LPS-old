using System;

namespace LPS.ToolScript
{
	[Serializable]
	public class VariableException : ApplicationException
	{
		public VariableException ()
			: base ("Chyba s proměnnou")
		{
		}

		public VariableException (string message)
			: base (message)
		{
		}

		public VariableException (string message, Exception inner)
			: base (message, inner)
		{
		}
	}
}
