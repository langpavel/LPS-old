using System;
using System.Xml.Serialization;
using LPS.ToolScript;

namespace LPS.Util
{
	public class SerializeCommand : CommandBase
	{
		public SerializeCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "Serializuje objekt jako XML"; }
		}

		public override object Execute (LPS.ToolScript.IExecutionContext context, System.IO.TextWriter Out, System.IO.TextWriter Info, System.IO.TextWriter Err, object[] Params)
		{
			object o = Params[0];
			XmlSerializer xser = new XmlSerializer(o.GetType());
			xser.Serialize(Out, o);
			return SpecialValue.Void;
		}

	}
}
