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
		
		protected override void DoUpdateValue (BindingInfo info)
		{
			if(entry != null)
			{
				entry.Sensitive = info.Enabled && !info.ReadOnly;
				if(info.ValueIsNull)
					entry.Text = "";
				else
					entry.Text = info.Value.ToString();
			}
		}

		protected override void DoValueChanged()
		{
			DoValueChanged(entry.Text);
		}

		protected void SetBackground(byte r, byte g, byte b)
		{
			if(entry == null)
				return;
			entry.ModifyBase(StateType.Normal, new Gdk.Color(r,g,b));
		}

		protected void SetBackgroundByState(bool error)
		{
			if(error)
				SetBackground(255,128,128);
			else
				SetBackground(255,255,255);
		}

		protected override void AfterValueChanged(object new_value, Exception error)
		{
			if(entry == null)
				return;
			SetBackgroundByState(error != null);
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

		void HandleEntryFocusOutEvent (object o, FocusOutEventArgs args)
		{
			UpdateValueByBindings();
			SetBackgroundByState(false);
		}

		protected override void Bind()
		{
			if(entry != null)
			{
				entry.Changed += HandleEntryChanged;
				entry.FocusOutEvent += HandleEntryFocusOutEvent;
			}
			base.Bind();
		}

		protected override void Unbind()
		{
			if(entry != null)
			{
				entry.Changed -= HandleEntryChanged;
				entry.FocusOutEvent -= HandleEntryFocusOutEvent;
			}
			base.Unbind();
		}
		
		public override void Dispose()
		{
			this.EntryWidget = null;
			base.Dispose();
		}
	}
}
