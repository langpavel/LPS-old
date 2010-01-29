using System;
using System.Data;
using System.Collections;
using System.Threading.IntelParallel;

namespace LPS.Server
{
	public static class ServerChangeSink
	{
		private static ArrayList listeners;
		
		static ServerChangeSink()
		{
			listeners = new ArrayList();
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
				result.SinkIndex = listeners.Add(result);
				return result;
			}
		}
		
		public static ServerChangeListener GetListener(int index)
		{
			lock(listeners)
			{
				return (ServerChangeListener)listeners[index];
			}
		}

		public static void AddNewData(string table, long id, char type, DataRow row)
		{
			ArrayList l2;
			lock(listeners)
			{
				l2 = new ArrayList(listeners.Count);
				for(int i = 0; i < listeners.Count; i++)
				{
					object o = listeners[i];
					if(o != null)
						l2.Add(o);
				}
			}
			
			ParallelFor f = new ParallelFor(l2.Count);
			f.Run(delegate(int i)
			{
				ServerChangeListener listener = (ServerChangeListener)l2[i];
					listener.AddNewData(table, id, type, row);
			});
		}
		
		public static void RemoveListener(int index)
		{
			ServerChangeListener listener = null;
			lock(listeners)
			{
				listener = (ServerChangeListener)listeners[index];
				listeners[index] = null;
			}
			if(listener != null)
				listener.Dispose();
		}
	}
}
