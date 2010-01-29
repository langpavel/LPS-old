using System;
using System.Data;

namespace LPS
{
	[Serializable]
	public class ServerCallResult
	{
		public ServerCallResult()
		{
		}
		
		public bool ListenerRegistered { get; set; }
		/// <summary>
		/// Changes dataset se vraci null pokud nebyly zmeny na serveru
		/// </summary>
		public DataSet Changes { get; set; }
		public bool HasChanges
		{
			get
			{
				return Changes != null && Changes.Tables.Count > 0;
			}
		}
	}
}
