using System;

namespace LPS.ToolScript.Parser
{
	public class DBColumnVarchar : DBColumnBase
	{
		public int MaxLength { get; set; }
		public bool UnlimitedLength
		{
			get { return this.MaxLength == int.MaxValue; }
			set { if(value) this.MaxLength = int.MaxValue; }
		}

		public DBColumnVarchar(int MaxLength)
			: base(typeof(String))
		{
			this.MaxLength = MaxLength;
		}

		public DBColumnVarchar()
			: base(typeof(String))
		{
			this.UnlimitedLength = true;
		}

		protected override string GetDBTypeName ()
		{
			if(UnlimitedLength)
				return "text";
			else
				return String.Format("varchar({0})", MaxLength);
		}

	}
}
