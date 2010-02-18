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
				Bind();
			}
		}

		protected override void DoValueChanged()
		{
			TreeIter iter;
			if(combo.GetActiveIter(out iter))
			{
				DoValueChanged(GetIdFromIter(iter));
			}
			else
				DoValueChanged(null);
		}

		bool is_updating;
		void HandleComboChanged (object sender, EventArgs e)
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
			if(combo != null)
			{
				combo.Model = this.Store;
				combo.Clear();
				for(int i=2; i<Store.NColumns; i++)
				{
					CellRendererText r = new CellRendererText();
					r.Alignment = Pango.Alignment.Left;
					combo.PackStart(r, ((i+2)==Store.NColumns));
					combo.AddAttribute(r, "text", i);
				}
				combo.Changed += HandleComboChanged;
			}
			base.Bind();
		}

		protected override void Unbind()
		{
			if(combo != null)
			{
				combo.Changed -= HandleComboChanged;
				combo.Clear();
				combo.Model = null;
			}
			base.Unbind();
		}

		protected override void DoUpdateValue (BindingInfo info)
		{
			if(combo == null || combo.Model == null)
				return;
			if(info.ValueIsNull)
			{
				combo.Active = -1;
				return;
			}
			combo.Sensitive = info.Enabled && !info.ReadOnly;
			long id = Convert.ToInt64(info.Value);
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

		protected override void UpdateListStoreRow (TreeIter iter, DataRow r)
		{
			base.UpdateListStoreRow (iter, r);
		}

		public override void Dispose ()
		{
			Unbind();
			base.Dispose();
		}

	}
}
