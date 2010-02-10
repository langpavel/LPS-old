using System;

namespace LPS.Client
{
	public interface IBinding : IDisposable
	{
		BindingGroup Bindings { get; set; }
		void OnAdd(BindingGroup bindings);
		void OnRemove(BindingGroup bindings);

		void UpdateValue(object orig_value, object new_value);
		event BindingValueChanged ValueChanged;
	}
}
