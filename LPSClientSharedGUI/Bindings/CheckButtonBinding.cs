using System;
using Gtk;

namespace LPS.Client
{
	public class CheckButtonBinding : BindingBase
	{
		public CheckButtonBinding()
		{
			ThreeState = false;
		}
		
		public CheckButtonBinding(CheckButton CheckBtn)
		{
			ThreeState = false;
			this.CheckBtn = CheckBtn;
		}
		
		public CheckButtonBinding(CheckButton CheckBtn, bool AllowNull)
		{
			ThreeState = AllowNull;
			this.CheckBtn = CheckBtn;
		}
		
		public bool ThreeState { get; set; }

		private CheckButton check;
		public CheckButton CheckBtn
		{
			get	{ return check;	}
			set
			{
				Unbind();
				check = value;
				Bind();
			}
		}
		
		protected override void DoUpdateValue (object orig_value, object new_value)
		{
			if(IsUpdating || check == null)
				return;
			Console.WriteLine("CheckBox DoUpdateValue: {0}",
				(new_value==null || new_value == DBNull.Value)?"NULL":new_value.ToString());
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

		protected override void DoValueChanged ()
		{
			if(IsUpdating || check == null)
				return;
			IsUpdating = true;
			try
			{
				if(!check.Inconsistent && check.Active && ThreeState)
				{
					check.Inconsistent = true;
					check.Active = false;
				}
				else if(check.Inconsistent)
				{
					check.Inconsistent = false;
					check.Active = true;
				}
	
				if(check.Inconsistent)
					DoValueChanged(null);
				else
					DoValueChanged(check.Active);
			}
			finally
			{
				IsUpdating = false;
			}
		}

		private void HandleCheckToggled (object sender, EventArgs e)
		{
			DoValueChanged();
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
