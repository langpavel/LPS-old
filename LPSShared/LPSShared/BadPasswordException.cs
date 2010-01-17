using System;

namespace LPSServer
{
	[Serializable]
	public class BadPasswordException : Exception
	{
		public BadPasswordException()
			: base("Neplatné jméno nebo heslo")
		{
		}

		public BadPasswordException(string message)
			: base(message)
		{
		}
		
		public BadPasswordException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
		
	}
}
