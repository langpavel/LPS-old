using System;
using System.Collections;
using System.Collections.Generic;

namespace LPS.Client
{
	public class BindingGroup: IEnumerable<IBinding>
	{
		private List<IBinding> bindings;
		public object OriginalValue { get; set; }
		public object Value { get; set; }
		
		public BindingGroup()
		{
		}
		
		public void Add(IBinding b)
		{
			bindings.Add(b);
			b.Bindings = this;
			b.ValueChanged += HandleValueChanged;
			b.OnAdd(this);
		}
		
		public bool Remove(IBinding b)
		{
			if(bindings.Remove(b))
			{
				b.OnRemove(this);
				b.ValueChanged -= HandleValueChanged;
				b.Bindings = null;
				return true;
			}
			return false;
		}

		private void HandleValueChanged(IBinding sender, BindingValueChangedArgs args)
		{
			this.Value = args.NewValue;
			if(args.HasOriginalValue)
				this.OriginalValue = args.OriginalValue;
			foreach(IBinding b in this)
			{
				if(b != sender)
					b.UpdateValue(this.OriginalValue, this.Value);
			}
		}
		
		#region IEnumerable implementation
		IEnumerator IEnumerable.GetEnumerator()
		{
			return bindings.GetEnumerator();
		}
		
		IEnumerator<IBinding> IEnumerable<IBinding>.GetEnumerator()
		{
			return bindings.GetEnumerator();
		}
		#endregion
		
	}
}
