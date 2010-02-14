using System;

namespace LPS.Client
{
	public class BindingInfo
	{
		public BindingInfo(object orig_val, object new_val, bool read_only, bool enabled)
		{
			this.Value = new_val;
			this.OriginalValue = orig_val;
			this.ReadOnly = read_only;
			this.Enabled = enabled;
		}

		public object Value { get; set; }
		public bool ValueIsNull { get { return this.Value == null || this.Value == DBNull.Value; } }
		public object OriginalValue { get; set; }
		public bool OriginalValueIsNull { get { return this.OriginalValue == null || this.OriginalValue == DBNull.Value; } }
		public bool ReadOnly { get; set; }
		public bool Enabled { get; set; }
	}
}
