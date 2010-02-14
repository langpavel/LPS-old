using System;
using Gtk;

namespace LPS.Client
{
	public class TextViewBinding : BindingBase
	{
		public TextViewBinding()
		{
		}
		
		public TextViewBinding(TextView textview)
		{
			TextViewWidget = textview;
		}
		
		private TextView textview;
		public TextView TextViewWidget
		{
			get	{ return textview;	}
			set { Unbind(); textview = value; Bind(); }
		}
		
		protected override void DoUpdateValue (BindingInfo info)
		{
			if(textview == null)
				return;
			textview.Sensitive = info.Enabled && !info.ReadOnly;
			if(info.ValueIsNull)
				textview.Buffer.Text = "";
			else
				textview.Buffer.Text = info.Value.ToString();
		}

		protected override void DoValueChanged ()
		{
			DoValueChanged(textview.Buffer.Text);
		}

		bool is_updating;
		private void HandleTextViewChanged (object sender, EventArgs e)
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
			if(textview != null)
				textview.Buffer.Changed += HandleTextViewChanged;
			base.Bind();
		}

		protected override void Unbind()
		{
			if(textview != null)
				textview.Buffer.Changed -= HandleTextViewChanged;
			base.Unbind();
		}
		
		public override void Dispose()
		{
			this.TextViewWidget = null;
			base.Dispose();
		}
	}
}
