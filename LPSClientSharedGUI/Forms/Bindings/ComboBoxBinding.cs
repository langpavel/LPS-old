using System;
using System.Data;
using Gtk;

namespace LPS.Client
{
	public class ComboBoxBinding : LookupBindingBase
	{
		public ComboBoxBinding()
		{
		}

		public ComboBoxBinding(ILookupInfo LookupInfo, ComboBox ComboBox)
			: base(LookupInfo)
		{
			this.ComboBox = ComboBox;
		}

		private ComboBox combo;
		public ComboBox ComboBox
		{
			get { return combo; }
			set
			{
				Unbind();
				combo = value;
				UpdateValueByBindings();
				Bind();
			}
		}

		void HandleComboChanged (object sender, EventArgs e)
		{
			if(IsUpdating)
				return;
			TreeIter iter;
			if(combo.GetActiveIter(out iter))
			{
				DoValueChanged(Store.GetValue(iter, 0));
			}
			else
				DoValueChanged(null);
		}

		private void Bind()
		{
			if(combo == null)
				return;
			combo.Model = this.Store;
			combo.Clear();
			for(int i=1; i<Store.NColumns; i++)
			{
				CellRenderer r = new CellRendererText();
				combo.PackStart(r, ((i+1)==Store.NColumns));
				combo.AddAttribute(r, "text", i);
			}
			combo.Changed += HandleComboChanged;
		}

		private void Unbind()
		{
			if(combo == null)
				return;
			combo.Changed -= HandleComboChanged;
			combo.Clear();
		}

		protected override void DoUpdateValue (object orig_value, object new_value)
		{
			if(combo == null)
				return;
			if(new_value == null || new_value == DBNull.Value)
			{
				combo.SetActiveIter(TreeIter.Zero);
				return;
			}
			long id = Convert.ToInt64(new_value);
			TreeIter iter;
			if(Store.GetIterFirst(out iter))
			{
				do
				{
					if(id.Equals(Store.GetValue(iter, 0)))
					{
						combo.SetActiveIter(iter);
						return;
					}
				} while(Store.IterNext(ref iter));
			}
			else
				combo.SetActiveIter(TreeIter.Zero);
		}

		public override void Dispose ()
		{
			Unbind();
			base.Dispose();
		}

	}
}
