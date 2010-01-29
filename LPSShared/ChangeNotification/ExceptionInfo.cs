using System;
using System.Xml.Serialization;

namespace LPS
{
	[XmlRoot("exception")]
	public class ExceptionInfo
	{
		public ExceptionInfo()
		{
		}

		public ExceptionInfo(Exception err)
		{
			Name = err.GetType().FullName;
			Message = err.Message;
			StackTrace = err.StackTrace;
			if(err.InnerException != null)
				InnerException = new ExceptionInfo(err.InnerException);
		}

		[XmlAttribute("name")]
		public String Name { get; set; }
		public String Message { get; set; }
		public String StackTrace { get; set; }
		
		public ExceptionInfo InnerException { get; set; }
	}
}
