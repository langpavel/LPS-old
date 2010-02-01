using System;
using System.Data;

namespace LPS.Client
{
	public class ServerException: ApplicationException
	{
		public string ExceptionName { get; set; }
		public LPSClientShared.LPSServer.ExceptionInfo ExceptionInfo { get; set; }
		
		public ServerException ()
			:base()
		{
		}

		public ServerException (string message)
			:base(message)
		{
		}

		public ServerException (string message, Exception innerException)
			:base(message, innerException)
		{
		}

		public static ServerException Create(LPSClientShared.LPSServer.ExceptionInfo exceptionInfo)
		{
			ServerException result;
			if(exceptionInfo.InnerException != null)
				result = new ServerException(exceptionInfo.Message, ServerException.Create(exceptionInfo.InnerException));
			else
				result = new ServerException(exceptionInfo.Message);
			result.ExceptionInfo = exceptionInfo;
			return result;
		}
	}
}
