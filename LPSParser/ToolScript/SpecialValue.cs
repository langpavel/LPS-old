using System;

namespace LPS.ToolScript
{
	public sealed class SpecialValue
	{
		/// <summary>
		/// Variable without value
		/// </summary>
		public static SpecialValue VariableNotSet { get; private set; }
		/// <summary>
		/// Returned result is void
		/// </summary>
		public static SpecialValue Void { get; private set; }

		private string message;
		private int code;
		private SpecialValue(string message, int code)
		{
			this.message = message;
			this.code = code;
		}

		static SpecialValue()
		{
			SpecialValue.VariableNotSet = new SpecialValue("Variable without value", 10001);
			SpecialValue.Void = new SpecialValue("Returned result is void", 10002);
		}

		public override bool Equals(object obj)
		{
			return SpecialValue.ReferenceEquals(this, obj);
		}

		public override int GetHashCode ()
		{
			return code;
		}

		public override string ToString ()
		{
			return message;
		}
	}
}
