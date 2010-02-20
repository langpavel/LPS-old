using System;
using System.Collections.Generic;
using System.IO;

namespace LPS.Util
{
	public class HelpCommand : CommandBase
	{
		public HelpCommand(CommandCollection Commands, string Name)
			: base(Commands, Name)
		{
		}

		public override Type[] ParamTypes
		{
			get { return new Type[] { }; }
		}

		public override string Help
		{
			get { return "vypíše tento seznam na výstup"; }
		}

		public override object Execute(TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			foreach(ICommand cmd in this.Commands.Commands)
			{
				Out.WriteLine("{0}", cmd.ToString());
				Out.WriteLine("\t{0}", cmd.Help);
			}
			return null;
		}
	}
}
