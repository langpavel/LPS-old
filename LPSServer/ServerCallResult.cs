using System;
using System.Data;
using System.Xml.Serialization;

namespace LPS
{
	[Serializable]
	[XmlRoot("result", Namespace="http://lpsoft.org/server/")]
	public class ServerCallResult
	{
		/// <summary>
		/// Implicit constructor
		/// </summary>
		public ServerCallResult()
		{
		}
		
		/// <summary>
		/// Constructor for errors
		/// </summary>
		public ServerCallResult(Exception err)
		{
			SetException(err);
		}
		
		/// <summary>
		/// Set current exception info
		/// </summary>
		public void SetException(Exception err)
		{
			Exception = new ExceptionInfo(err);
		}
		
		/// <summary>
		/// Changes dataset se vraci null pokud nebyly zmeny na serveru
		/// </summary>
		public DataSet Changes { get; set; }

		/// <summary>
		/// Readonly - if has changes
		/// </summary>
		public bool HasChanges
		{
			get
			{
				return Changes != null && Changes.Tables.Count > 0;
			}
		}
		
		/// <summary>
		/// Exception info if there is an exception or null
		/// </summary>
		public ExceptionInfo Exception { get; set; }
	}
}
