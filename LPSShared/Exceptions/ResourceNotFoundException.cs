using System;

namespace LPS
{
	[Serializable]
	public class ResourceNotFoundException : ApplicationException
	{
		public ResourceNotFoundException ()
			: base("Resource nenalezeno")
		{
		}
		
		public ResourceNotFoundException(string message)
			: base(message)
		{
		}

		public ResourceNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
