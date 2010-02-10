using System;

namespace LPS.Server
{
	[Serializable]
	public class ChangeInfo
	{
		public ChangeInfo()
		{
		}

		public ChangeInfo(string TableName, DateTime ModifyDateTime, bool HasDeletedRows)
		{
			this.TableName = TableName;
			this.ModifyDateTime = ModifyDateTime;
			this.HasDeletedRows = HasDeletedRows;
		}

		public string TableName { get; set; }
		public DateTime ModifyDateTime { get; set; }
		public bool HasDeletedRows { get; set; }
	}
}
