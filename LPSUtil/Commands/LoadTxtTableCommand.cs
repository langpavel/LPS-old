using System;
using System.IO;

namespace LPS.Util
{
	public class LoadTxtTableCommand : CommandBase
	{
		public LoadTxtTableCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "načte tabulku z textového souboru"; }
		}

		public override object Execute(LPS.ToolScript.IExecutionContext context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			return null;
		}
	}
}
