using System;
using System.IO;

namespace LPS.Util
{
	public class LoadTxtTableCommand : CommandBase
	{
		public LoadTxtTableCommand(CommandCollection Commands, string Name)
			: base(Commands, Name)
		{
		}

		public override Type[] ParamTypes
		{
			get { return new Type[] { typeof(string) }; }
		}

		public override string Help
		{
			get { return "načte tabulku z textového souboru"; }
		}

		public override object Execute(TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			return null;
		}
	}
}
