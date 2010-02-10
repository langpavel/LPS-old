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
			set { Unbind(); textview = value; UpdateValueByBindings(); Bind(); }
		}
		
		protected override void DoUpdateValue (object orig_value, object new_value)
		{
			if(textview == null)
				return;
			textview.Buffer.Text = (new_value ?? "").ToString();
		}
		
		private void HandleTextViewChanged (object sender, EventArgs e)
		{
			if(IsUpdating)
				return;
			DoValueChanged(textview.Buffer.Text);
		}
		
		private void Bind()
		{
			if(textview != null)
				textview.Buffer.Changed += HandleTextViewChanged;
		}

		private void Unbind()
		{
			if(textview != null)
				textview.Buffer.Changed -= HandleTextViewChanged;
		}
		
		public override void Dispose()
		{
			this.TextViewWidget = null;
			base.Dispose();
		}
	}
}
