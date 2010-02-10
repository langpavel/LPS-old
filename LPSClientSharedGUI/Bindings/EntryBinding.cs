using System;
using Gtk;

namespace LPS.Client
{
	public class EntryBinding : BindingBase
	{
		public EntryBinding()
		{
		}
		
		public EntryBinding(Entry entry)
		{
			EntryWidget = entry;
		}
		
		private Entry entry;
		public Entry EntryWidget
		{
			get	{ return entry;	}
			set { Unbind(); entry = value; UpdateValueByBindings(); Bind(); }
		}
		
		protected override void DoUpdateValue (object orig_value, object new_value)
		{
			if(entry != null)
				entry.Text = (new_value ?? "").ToString();
		}

		protected override void DoValueChanged()
		{
			if(IsUpdating)
				return;
			DoValueChanged(entry.Text);
		}

		private void HandleEntryChanged (object sender, EventArgs e)
		{
			DoValueChanged();
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
		
		public override void Dispose()
		{
			this.EntryWidget = null;
			base.Dispose();
		}
	}
}
