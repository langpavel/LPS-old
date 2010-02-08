using System;
using Gtk;

namespace LPS.Client
{
	public class EntryBinding : BindingBase, IDisposable
	{
		public EntryBinding()
		{
		}
		
		private Entry entry;
		public Entry EntryWidget
		{
			get	{ return entry;	}
			set { Unbind(); entry = value; UpdateValueByBindings(); Bind(); }
		}
		
		protected override void DoUpdateValue (object orig_value, object new_value)
		{
			entry.Text = (new_value ?? "").ToString();
		}
		
		private void HandleEntryChanged (object sender, EventArgs e)
		{
			if(IsUpdating)
				return;
			DoValueChanged(entry.Text);
		}
		
		private void Bind()
		{
			if(entry != null)
				entry.Changed += HandleEntryChanged;
		}

		private void Unbind()
		{
			if(entry != null)
				entry.Changed -= HandleEntryChanged;
		}
		
		public virtual void Dispose()
		{
			this.EntryWidget = null;
		}
	}
}
