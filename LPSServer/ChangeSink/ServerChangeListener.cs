using System;
using System.Data;
using System.Collections.Generic;

namespace LPS.Server
{
	public class ServerChangeListener: IDisposable
	{
		private Dictionary<string, ChangeInfo> changes;
		internal DateTime last_access;

		public ServerChangeListener ()
		{
			last_access = DateTime.Now;
			changes = new Dictionary<string, ChangeInfo>();
		}
		
		public int SinkIndex { get; set; }
		public int Security
		{
			get	{ return this.GetHashCode(); }
		}
		
		public void AddNewData(string table_name, DateTime dt, bool del)
		{
			lock(this)
			{
				ChangeInfo ch;
				if(changes.TryGetValue(table_name, out ch))
				{
					if(del)
						ch.HasDeletedRows = true;
				}
				else
				{
					changes[table_name] = new ChangeInfo(table_name, dt, del);
				}
			}
		}
		
		public ChangeInfo[] GetChangesAndClear()
		{
			lock(this)
			{
				last_access = DateTime.Now;
				ChangeInfo[] result = new ChangeInfo[changes.Count];
				int i = 0;
				foreach(ChangeInfo ch in changes.Values)
					result[i++] = ch;
				changes = new Dictionary<string, ChangeInfo>();
				return result;
			}
		}
		
		public void Dispose()
		{
		}
	}
}
