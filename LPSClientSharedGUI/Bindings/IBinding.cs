using System;

namespace LPS.Client
{
	public interface IBinding : IDisposable
	{
		BindingGroup Bindings { get; set; }
		bool IsMaster { get; }

		void OnAdd(BindingGroup bindings);
		void OnRemove(BindingGroup bindings);

		void UpdateValue(BindingInfo info);
		event BindingValueChanged ValueChanged;
	}
}
