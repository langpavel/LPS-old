using System;
using System.Xml.Serialization;
using Npgsql;

namespace LPS
{
	[XmlRoot("exception", Namespace="http://lpsoft.org/server/")]
	public class ExceptionInfo
	{
		public ExceptionInfo()
		{
		}

		public ExceptionInfo(Exception err)
		{
			Name = err.GetType().FullName;
			Message = err.Message;
			NpgsqlException pgerr = err as NpgsqlException;
			if(pgerr != null)
			{
				Message += "\nSQL: " + pgerr.ErrorSql + "\n" + pgerr.Detail;
			}
			StackTrace = err.StackTrace;
			if(err.InnerException != null)
				InnerException = new ExceptionInfo(err.InnerException);
		}

		public String Name { get; set; }
		public String Message { get; set; }
		public String StackTrace { get; set; }
		
		public int ErrCode { get; set; }
		public String ErrCodeName { get; set; }
		
		public object[] Data { get; set; }
		
		public ExceptionInfo InnerException { get; set; }
	}
}
