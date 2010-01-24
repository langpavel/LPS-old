using System;
using System.Data;
using Gtk;

namespace LPSClient
{
	public class TextRowBinding
	{
		public TextRowBinding()
		{
		}

		public TextRowBinding(DataRow row, string TextMask)
		{
		}
	}

	public class TextRowBindingArgs : EventArgs
	{
		private string _text;
		public TextRowBindingArgs(string text)
		{
			_text = text;
		}
		
		public String Text
		{
			get { return _text; }
		}
	}

}
