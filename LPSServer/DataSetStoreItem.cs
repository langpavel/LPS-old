using System;
using System.Data;
using Npgsql;

namespace LPSServer
{
	public class DataSetStoreItem: IDisposable
	{
		public DataSetStoreItem()
		{
		}
		
		public DataSet DataSet { get; set; }
		public NpgsqlCommand Command { get; set; }
		public NpgsqlCommandBuilder CommandBuilder { get; set; }
		public NpgsqlDataAdapter DataAdapter { get; set; }
		
		public void Dispose()
		{
			if(this.DataSet != null)
			{
				this.DataSet.Dispose();
				this.DataSet = null;
			}
		}
		
		public void DisposeWithoutDataSet()
		{
			if(CommandBuilder != null)
			{
				CommandBuilder.Dispose();
				CommandBuilder = null;
			}
			if(DataAdapter != null)
			{
				DataAdapter.Dispose();
				DataAdapter = null;
			}
			if(Command != null)
			{
				Command.Dispose();
				Command = null;
			}
		}
	}
}
