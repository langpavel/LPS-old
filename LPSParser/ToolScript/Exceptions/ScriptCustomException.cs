using System;
using System.Runtime.Serialization;

namespace LPS.ToolScript
{
	[Serializable]
	public class ScriptCustomException : Exception
	{
		public object CustomObject { get; private set; }

		public ScriptCustomException(object CustomObject, string message, Exception inner)
			: base(message, inner)
		{
			this.CustomObject = CustomObject;
		}
	}
}
