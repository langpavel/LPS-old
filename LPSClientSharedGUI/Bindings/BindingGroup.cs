using System;
using System.Collections;
using System.Collections.Generic;

namespace LPS.Client
{
	public class BindingGroup: IEnumerable<IBinding>, IDisposable
	{
		private List<IBinding> bindings;
		public object OriginalValue { get; set; }
		public object Value { get; set; }
		public Type ValType { get; set; }
		
		public BindingGroup()
		{
			bindings = new List<IBinding>();
		}
		
		public BindingGroup(Type ValType)
		{
			bindings = new List<IBinding>();
			this.ValType = ValType;
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

		public object ConvertValueType(object val)
		{
			try
			{
				if(val == DBNull.Value)
					return null;
				if(val == null || ValType == null)
					return val;
				return Convert.ChangeType(val, ValType);
			}
			catch(FormatException)
			{
				if(val is String && ((string)val == ""))
					return null;
				else
					throw;
			}
		}

		bool is_updating;
		private void HandleValueChanged(IBinding sender, BindingValueChangedArgs args)
		{
			if(is_updating)
				return;
			is_updating = true;
			try
			{
				this.Value = ConvertValueType(args.NewValue);
				Console.WriteLine("{0} - Changed to {1}", sender, this.Value);
				if(args.HasOriginalValue)
					this.OriginalValue = ConvertValueType(args.OriginalValue);
				foreach(IBinding b in this)
				{
					if(b != sender)
						b.UpdateValue(this.OriginalValue, this.Value);
				}
			}
			finally
			{
				is_updating = false;
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
		
		public void Clear()
		{
			for(int i = bindings.Count-1; i >= 0; i--)
			{
				IBinding b = bindings[i];
				b.Dispose();
			}
		}
		
		public virtual void Dispose()
		{
			Clear();
		}
	}
}
