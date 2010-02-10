using System;
using System.Data;
using System.Collections.Generic;
//using System.Threading.IntelParallel;

namespace LPS.Server
{
	public static class ServerChangeSink
	{
		private static List<ServerChangeListener> listeners;
		
		static ServerChangeSink()
		{
			listeners = new List<ServerChangeListener>();
		}
		
		public static ServerChangeListener RegisterNewListener()
		{
			ServerChangeListener result = new ServerChangeListener();
			lock(listeners)
			{
				for(int i=0; i<listeners.Count; i++)
				{
					if(listeners[i] == null)
					{
						listeners[i] = result;
						result.SinkIndex = i;
						return result;
					}
				}
				listeners.Add(result);
				result.SinkIndex = listeners.IndexOf(result);
				return result;
			}
		}
		
		public static ServerChangeListener GetListener(int index, int security)
		{
			lock(listeners)
			{
				ServerChangeListener l = listeners[index];
				if(l == null)
					throw new ArgumentException("listener byl odstraněn", "index");
				if(l.Security != security)
					throw new ArgumentException("security klíč listeneru neplatí", "security");
				return l;
			}
		}

		public static void AddNewData(string table, DateTime dt, bool del)
		{
			lock(listeners)
			{
				foreach(ServerChangeListener listener in listeners)
				{
					if(listener != null)
						listener.AddNewData(table, dt, del);
				}
			}
		}
		
		public static void RemoveListener(int index, int security)
		{
			ServerChangeListener listener = null;
			lock(listeners)
			{
				listener = listeners[index];
				if(listener == null)
					throw new ArgumentException("listener již byl odstraněn", "index");
				if(listener.Security != security)
					throw new ArgumentException("security klíč listeneru neplatí", "security");
				listeners[index] = null;
			}
			if(listener != null)
				listener.Dispose();
		}
		
		public static int RemoveOldListeners(double minutes)
		{
			int result = 0;
			DateTime max_dt = DateTime.Now.AddMinutes(- minutes);
			lock(listeners)
			{
				for(int i = 0; i < listeners.Count; i++)
				{
					ServerChangeListener listener = listeners[i];
					if(listener != null && listener.last_access < max_dt)
					{
						listeners[i] = null;
						listener.Dispose();
						result++;
					}
				}
			}
			return result;
		}
		
		public static ServerCallResult GetResult(int idx, int security)
		{
			if(idx < 0)
				return null;
			ServerChangeListener l = ServerChangeSink.GetListener(idx, security);
			ServerCallResult result = new ServerCallResult();
			result.Changes = l.GetChangesAndClear();
			return result;
		}
	}
}
