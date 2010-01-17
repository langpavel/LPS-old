using System;

namespace LPSServer
{
	[Serializable]
	public class LoginRequiredException : Exception
	{
		public LoginRequiredException()
			: base("Je nutné se přihlásit")
		{
		}

		public LoginRequiredException(string message)
			: base(message)
		{
		}
		
		public LoginRequiredException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
		
	}
}
