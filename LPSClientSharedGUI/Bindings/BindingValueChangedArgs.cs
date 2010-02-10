using System;

namespace LPS.Client
{
	public delegate void BindingValueChanged(IBinding sender, BindingValueChangedArgs args);

	public class BindingValueChangedArgs : EventArgs
	{
		private object newValue;
		public object NewValue { get { return newValue; } }
		
		private object originalValue;
		public object OriginalValue { get { return originalValue; } }
		
		private bool hasOriginalValue;
		public bool HasOriginalValue { get { return hasOriginalValue; } }
		
		public BindingValueChangedArgs(object NewValue)
		{
			this.newValue = NewValue;
			this.hasOriginalValue = false;
			this.originalValue = null;
		}

		public BindingValueChangedArgs(object OriginalValue, object NewValue)
		{
			this.newValue = NewValue;
			this.hasOriginalValue = true;
			this.originalValue = OriginalValue;
		}
	}
}
