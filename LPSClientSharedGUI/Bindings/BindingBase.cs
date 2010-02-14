using System;

namespace LPS.Client
{
	public abstract class BindingBase : IBinding
	{
		public virtual bool IsMaster { get { return false; } }
		protected bool IsUpdating { get; private set; }
		
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
				DoValueChanged(null, null, true, false);
			else
				UpdateValue(new BindingInfo(null, null, true, false));
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
				DoValueChanged(null, null, true, false);
			else
				UpdateValue(new BindingInfo(null, null, true, false));
		}

		public virtual void UpdateValue(BindingInfo info)
		{
			if(IsUpdating)
				return;
			IsUpdating = true;
			try
			{
				DoUpdateValue(info);
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
		/// handles state after value changed
		/// </summary>
		protected virtual void AfterValueChanged(object new_value, Exception error)
		{
		}

		/// <summary>
		/// Notify others of value change - to update other values
		/// </summary>
		protected void DoValueChanged(object new_value)
		{
			try
			{
				if(ValueChanged != null)
				{
					ValueChanged(this, new BindingValueChangedArgs(new_value));
					AfterValueChanged(new_value, null);
				}
			}
			catch(Exception err)
			{
				AfterValueChanged(new_value, err);
			}
		}
		
		/// <summary>
		/// Notify others of value change - to update other values
		/// </summary>
		protected void DoValueChanged(object orig_value, object new_value, bool read_only, bool enabled)
		{
			try
			{
				if(ValueChanged != null)
				{
					ValueChanged(this, new BindingValueChangedArgs(orig_value, new_value, read_only, enabled));
					AfterValueChanged(new_value, null);
				}
			}
			catch(Exception err)
			{
				AfterValueChanged(new_value, err);
			}
		}

		/// <summary>
		/// Update binded object
		/// </summary>
		protected abstract void DoUpdateValue(BindingInfo info);
		
		/// <summary>
		/// Update binded object with value from binding
		/// </summary>
		protected void UpdateValueByBindings()
		{
			if(Bindings != null)
				UpdateValue(Bindings.Info);
			else
				UpdateValue(new BindingInfo(null, null, true, false));
		}
		
		public virtual void Dispose()
		{
			if(Bindings != null)
				Bindings.Remove(this);
		}
	}
}
