using System;

namespace LPS.ToolScript
{
	public sealed class SpecialValue
	{
		public static SpecialValue VariableNotSet { get; private set; }

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
