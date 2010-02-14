using System;

namespace LPS.Client
{
	public delegate void BindingValueChanged(IBinding sender, BindingValueChangedArgs args);

	public class BindingValueChangedArgs : EventArgs
	{
		public object NewValue { get; private set; }

		public bool HasAllValues { get; private set; }
		public object OriginalValue { get; private set; }
		public bool ReadOnly { get; private set; }
		public bool Enabled { get; private set; }

		public BindingValueChangedArgs(object NewValue)
		{
			this.NewValue = NewValue;
			this.HasAllValues = false;
		}

		public BindingValueChangedArgs(object OriginalValue, object NewValue, bool read_only, bool enabled)
		{
			this.NewValue = NewValue;
			this.HasAllValues = true;
			this.OriginalValue = OriginalValue;
			this.ReadOnly = read_only;
			this.Enabled = enabled;
		}
	}
}
