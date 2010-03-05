using System;
using System.Collections.Generic;
using System.IO;
using LPS.ToolScript;

namespace LPS.Util
{
	public class HelpCommand : CommandBase
	{
		public HelpCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "vypíše tento seznam na výstup"; }
		}

		public override object Execute(LPS.ToolScript.IExecutionContext context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			foreach(object o in context.LocalVariables.Values)
			{
				ICommand cmd;
				if((cmd = o as ICommand) != null)
				{
					Out.WriteLine("{0}", cmd.ToString());
					Out.WriteLine("\t{0}", cmd.Help);
				}
			}
			return null;
		}
	}
}
