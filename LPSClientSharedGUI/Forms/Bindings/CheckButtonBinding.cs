using System;
using Gtk;

namespace LPS.Client
{
	public class CheckButtonBinding : BindingBase
	{
		public CheckButtonBinding()
		{
		}
		
		public CheckButtonBinding(CheckButton CheckBtn)
		{
			this.CheckBtn = CheckBtn;
		}
		
		private CheckButton check;
		public CheckButton CheckBtn
		{
			get	{ return check;	}
			set { Unbind(); check = value; UpdateValueByBindings(); Bind(); }
		}
		
		protected override void DoUpdateValue (object orig_value, object new_value)
		{
			if(check == null)
				return;
			if(new_value == null || new_value is DBNull)
			{
				check.Active = false;
				check.Inconsistent = true;
			}
			else if(Convert.ToBoolean(new_value))
			{
				check.Inconsistent = false;
				check.Active = true;
			}
			else
			{
				check.Inconsistent = false;
				check.Active = false;
			}
		}
		
		private void HandleCheckToggled (object sender, EventArgs e)
		{
			if(IsUpdating)
				return;
			if(check.Inconsistent)
				DoValueChanged(null);
			else
				DoValueChanged(check.Active);
		}
		
		private void Bind()
		{
			if(check != null)
				check.Toggled += HandleCheckToggled;
		}

		private void Unbind()
		{
			if(check != null)
				check.Toggled -= HandleCheckToggled;
		}
		
		public override void Dispose()
		{
			this.CheckBtn = null;
			base.Dispose();
		}
	}
}
