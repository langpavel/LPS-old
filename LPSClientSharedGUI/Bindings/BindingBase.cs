using System;

namespace LPS.Client
{
	public abstract class BindingBase : IBinding
	{
		private bool has_error;
		public bool HasError { get { return has_error; } }
		private string error;
		public string Error { get { return error; } }
		public virtual bool IsMaster { get { return false; } }
		protected bool IsUpdating { get; set; }
		
		public event BindingValueChanged ValueChanged;
		public BindingGroup Bindings { get; set; }
		
		public virtual void OnAdd(BindingGroup bindings)
		{
			if(IsMaster)
				DoValueChanged();
			else
				UpdateValueByBindings();
		}
		
		public virtual void OnRemove(BindingGroup bindings)
		{
			if(IsMaster)
				DoValueChanged(null, null);
			else
				UpdateValue(null, null);
		}

		protected virtual void Bind()
		{
			if(IsMaster)
				DoValueChanged();
			else
				UpdateValueByBindings();
		}

		protected virtual void Unbind()
		{
			if(IsMaster)
				DoValueChanged(null, null);
			else
				UpdateValue(null, null);
		}

		protected virtual void OnUpdateValueError(object orig_value, object new_value, Exception exception)
		{
		}

		bool is_updating;
		public virtual void UpdateValue(object orig_value, object new_value)
		{
			if(IsUpdating)
				return;
			IsUpdating = true;
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
				IsUpdating = false;
			}
		}

		/// <summary>
		/// Update others from binded value
		/// </summary>
		protected abstract void DoValueChanged();

		/// <summary>
		/// Notify others of value change - to update other values
		/// </summary>
		protected void DoValueChanged(object new_value)
		{
			if(ValueChanged != null)
				ValueChanged(this, new BindingValueChangedArgs(new_value));
		}
		
		/// <summary>
		/// Notify others of value change - to update other values
		/// </summary>
		protected void DoValueChanged(object orig_value, object new_value)
		{
			if(ValueChanged != null)
				ValueChanged(this, new BindingValueChangedArgs(orig_value, new_value));
		}

		/// <summary>
		/// Update binded object
		/// </summary>
		protected abstract void DoUpdateValue(object orig_value, object new_value);
		
		/// <summary>
		/// Update binded object with value from binding
		/// </summary>
		protected void UpdateValueByBindings()
		{
			if(Bindings != null)
				UpdateValue(Bindings.OriginalValue, Bindings.Value);
			else
				UpdateValue(null, null);
		}
		
		public virtual void Dispose()
		{
			if(Bindings != null)
				Bindings.Remove(this);
		}
	}
}
