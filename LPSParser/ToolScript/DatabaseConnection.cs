using System;
using LPS.ToolScript.Parser;
using LPS.Client;

namespace LPS.ToolScript
{
	public class DatabaseConnection : IDisposable
	{
		public IDatabaseSchema Schema { get; private set; }
		public ServerConnection Server { get; private set; }

		public DatabaseConnection(IDatabaseSchema Schema, string url)
		{
			this.Schema = Schema;
			this.Server = new ServerConnection(url);
		}

		public string SchemaDiff()
		{
			throw new NotImplementedException();
		}

		public virtual void Dispose ()
		{
			this.Server.Dispose();
		}

	}
}
