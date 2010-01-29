using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.IntelParallel;

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

		public static void AddNewData(string table, long id, char type, DataRow row)
		{
			List<ServerChangeListener> l2;
			lock(listeners)
			{
				l2 = new List<ServerChangeListener>(listeners.Count);
				for(int i = 0; i < listeners.Count; i++)
				{
					ServerChangeListener o = listeners[i];
					if(o != null)
						l2.Add(o);
				}
			}
			
			ParallelFor f = new ParallelFor(l2.Count);
			f.Run(delegate(int i)
			{
				l2[i].AddNewData(table, id, type, row);
			});
		}
		
		public static void RemoveListener(int index, int security)
		{
			ServerChangeListener listener = null;
			lock(listeners)
			{
				listener = listeners[index];
				if(listener == null)
					throw new ArgumentException("listener byl odstraněn", "index");
				if(listener.Security != security)
					throw new ArgumentException("security klíč listeneru neplatí", "security");
				listeners[index] = null;
			}
			if(listener != null)
				listener.Dispose();
		}
	}
}
