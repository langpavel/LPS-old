using System;
using System.IO;
using LPS.ToolScript;
using System.Collections.Generic;

namespace LPS.Util
{
	public class VarsCommand : CommandBase
	{
		public VarsCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "vypíše proměnné na výstup"; }
		}

		public override object Execute(IExecutionContext context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			foreach(KeyValuePair<string, object> p in context.LocalVariables)
				if(!p.Key.StartsWith("__"))
					Out.WriteLine("{0}:\t{1}", p.Key, p.Value);
			return SpecialValue.Void;
		}
	}
}
