using System;
using System.Data;
using Gtk;

namespace LPSClient
{
	public class TextRowBinding : IDisposable
	{
		public TextRowBinding()
		{
		}

		public TextRowBinding(DataRow row, string TextMask)
		{
			this.Row = row;
			this.TextMask = TextMask;
			Bind();
		}
		
		public TextRowBinding(DataRow row, Label label)
		{
			this.Row = row;
			this.TextMask = label.Text;
			this.Label = label;
			Bind();
		}
		
		public Label Label { get; set; }
		public DataRow Row { get; set; }
		
		public string TextMask { get; set; }
		public event TextUpdatingHandler TextUpdating;

		public void DoTextUpdating(string newText)
		{
			TextUpdatingArgs args = new TextUpdatingArgs(newText);
			if(TextUpdating != null)
				TextUpdating(this, args);
			if(this.Label != null)
				this.Label.Text = args.Text;
		}
		
		public virtual void Bind()
		{
			this.Row.Table.RowChanged += HandleRowChanged;
			UpdateText();
		}

		public virtual void Unbind()
		{
			this.Row.Table.RowChanged -= HandleRowChanged;
		}

		public void UpdateText()
		{
			string result = this.TextMask;
			string val;
			foreach(DataColumn col in this.Row.Table.Columns)
			{
				object o = Row[col.Ordinal];
				if(o == null || o is DBNull)
					val = "";
				else
					val = o.ToString();
				result = result.Replace("{" + col.ColumnName + "}", val);
			}
			string shortstate;
			switch(Row.RowState)
			{
			case DataRowState.Added:
				val = "Nový";
				shortstate = "(+++)";
				break;
			case DataRowState.Deleted:
				val = "Smazáno";
				shortstate = "(Smazáno)";
				break;
			case DataRowState.Modified: 
				val = "Změněno";
				shortstate = "(*)";
				break;
			case DataRowState.Unchanged:
				val = "Nezměněno";
				shortstate = "";
				break;
			default:
				val = Row.RowState.ToString();
				shortstate = "(???)";
				break;
			}
			result = result.Replace("{_???_}", val);
			result = result.Replace("{_?_}", shortstate);
			result = result.Replace("   ", " ");
			DoTextUpdating(result.Trim());
		}
		
		void HandleRowChanged (object sender, DataRowChangeEventArgs e)
		{
			if(e.Row == this.Row)
			{
				UpdateText();
			}
		}
		
		public virtual void Dispose()
		{
			Unbind();
		}
	}

	public delegate void TextUpdatingHandler(object sender, TextUpdatingArgs args);
	
	public class TextUpdatingArgs : EventArgs
	{
		public TextUpdatingArgs(string text)
		{
			this.Text = text;
		}
		
		public String Text { get; set; }
	}

}
