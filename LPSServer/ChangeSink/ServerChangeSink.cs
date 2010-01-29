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
		
		public static long RegisterNewListener()
		{
			ServerChangeListener listener = new ServerChangeListener();
			lock(listeners)
			{
				for(int i=0; i<listeners.Count; i++)
				{
					if(listeners[i] == null)
					{
						listeners[i] = listener;
						listener.SinkIndex = i;
						return i;
					}
				}
				listener.SinkIndex = listeners.Add(listener);
				return listener.SinkIndex;
			}
		}
		
		public static void AddNewData(string table, long id, char type, DataRow row)
		{
			lock(listeners)
			{
				ParallelFor f = new ParallelFor(listeners.Count);
				f.Run(delegate(int i)
				{
					listeners[i].AddNewData(table, id, type, row);
				});
			}
		}
	}
}
