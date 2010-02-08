using System;

namespace LPS.Client
{
	public abstract class BindingBase : IBinding
	{
		private bool is_updating;
		public bool IsUpdating { get { return is_updating; } }
		private bool has_error;
		public bool HasError { get { return has_error; } }
		private string error;
		public string Error { get { return error; } }
		
		public event BindingValueChanged ValueChanged;
		public BindingGroup Bindings { get; set; }
		
		public virtual void OnAdd(BindingGroup bindings)
		{
		}
		
		public virtual void OnRemove(BindingGroup bindings)
		{
		}

		protected virtual void OnUpdateValueError(object orig_value, object new_value, Exception exception)
		{
		}
		
		public virtual void UpdateValue(object orig_value, object new_value)
		{
			is_updating = true;
			try
			{
				DoUpdateValue(orig_value, new_value);
				has_error = false;
				error = null;
			}
			catch(Exception err)
			{
				has_error = true;
				error = err.Message;
				OnUpdateValueError(orig_value, new_value, err);
			}
			finally
			{
				is_updating = false;
			}
		}
		
		protected void DoValueChanged(object new_value)
		{
			if(ValueChanged != null)
				ValueChanged(this, new BindingValueChangedArgs(new_value));
		}
		
		protected void DoValueChanged(object orig_value, object new_value)
		{
			if(ValueChanged != null)
				ValueChanged(this, new BindingValueChangedArgs(orig_value, new_value));
		}
		
		protected abstract void DoUpdateValue(object orig_value, object new_value);
		
		protected void UpdateValueByBindings()
		{
			if(Bindings != null)
				DoUpdateValue(Bindings.OriginalValue, Bindings.Value);
			else
				DoUpdateValue(null, null);
		}
	}
}
