using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace LPS.Client
{
	public class BindingGroup: IEnumerable<IBinding>, IDisposable
	{
		private List<IBinding> bindings;
		public BindingInfo Info { get; protected set; }
		public Type ValType { get; set; }
		
		public BindingGroup()
		{
			bindings = new List<IBinding>();
			this.Info = new BindingInfo(null, null, true, false);
		}
		
		public BindingGroup(Type ValType)
		{
			bindings = new List<IBinding>();
			this.ValType = ValType;
			this.Info = new BindingInfo(null, null, true, false);
		}
		
		public void Add(IBinding b)
		{
			bindings.Add(b);
			b.Bindings = this;
			b.ValueChanged += HandleValueChanged;
			b.OnAdd(this);
		}
		
		public bool Remove(IBinding b)
		{
			if(bindings.Remove(b))
			{
				b.OnRemove(this);
				b.ValueChanged -= HandleValueChanged;
				b.Bindings = null;
				return true;
			}
			return false;
		}

		public object ConvertValueType(object val)
		{
			try
			{
				if(val == DBNull.Value)
					return null;
				if(val == null || ValType == null)
					return val;
				return Convert.ChangeType(val, ValType);
			}
			catch(FormatException err)
			{
				if(val is String && ((string)val == ""))
					return null;
				else if (ValType == typeof(DateTime) && val is String)
				{
					switch(((string)val).ToLower())
					{
					case "today":
					case "dnes":
						return DateTime.Today;
					case "tomorow":
					case "zítra":
					case "zitra":
						return DateTime.Today.AddDays(1.0);
					case "yesterday":
					case "včera":
					case "vcera":
						return DateTime.Today.AddDays(-1.0);
					case "now":
					case "nyní":
					case "nyni":
						return DateTime.Now;
					}
				}
				Log.Error(err);
				throw err;
			}
		}

		bool is_updating;
		private void HandleValueChanged(IBinding sender, BindingValueChangedArgs args)
		{
			if(is_updating)
				return;
			using(Log.Scope("Aktualizace hodnoty BindingGroup"))
			{
				is_updating = true;
				try
				{
					this.Info.Value = ConvertValueType(args.NewValue);
					//Log.Debug("{0} - Changed to {1}", sender, this.Info.Value);
					if(args.HasAllValues)
					{
						this.Info.OriginalValue = ConvertValueType(args.OriginalValue);
						this.Info.ReadOnly = args.ReadOnly;
						this.Info.Enabled = args.Enabled;
					}
					foreach(IBinding b in this)
					{
						if(b != sender)
							b.UpdateValue(this.Info);
					}
				}
				finally
				{
					is_updating = false;
				}
			}
		}
		
		#region IEnumerable implementation
		IEnumerator IEnumerable.GetEnumerator()
		{
			return bindings.GetEnumerator();
		}
		
		IEnumerator<IBinding> IEnumerable<IBinding>.GetEnumerator()
		{
			return bindings.GetEnumerator();
		}
		#endregion
		
		public void Clear()
		{
			for(int i = bindings.Count-1; i >= 0; i--)
			{
				IBinding b = bindings[i];
				b.Dispose();
			}
		}
		
		public virtual void Dispose()
		{
			Clear();
		}
	}
}
