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
			set
			{
				Unbind();
				entry = value;
				Bind();
			}
		}
		
		protected override void DoUpdateValue (object orig_value, object new_value)
		{
			if(entry != null)
				entry.Text = (new_value ?? "").ToString();
		}

		protected override void DoValueChanged()
		{
			DoValueChanged(entry.Text);
		}

		protected override void AfterValueChanged(object new_value, Exception error)
		{
			if(entry == null)
				return;
			if(error != null)
				entry.ModifyBase(StateType.Normal, new Gdk.Color(255,128,128));
			else
				entry.ModifyBase(StateType.Normal, new Gdk.Color(255,255,255));
		}

		bool is_updating;
		private void HandleEntryChanged (object sender, EventArgs e)
		{
			if(is_updating || IsUpdating)
				return;
			is_updating = true;
			try
			{
				DoValueChanged();
			}
			finally
			{
				is_updating = false;
			}
		}
		
		protected override void Bind()
		{
			if(entry != null)
				entry.Changed += HandleEntryChanged;
			base.Bind();
		}

		protected override void Unbind()
		{
			if(entry != null)
				entry.Changed -= HandleEntryChanged;
			base.Unbind();
		}
		
		public override void Dispose()
		{
			this.EntryWidget = null;
			base.Dispose();
		}
	}
}
