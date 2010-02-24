using System;
using LPS.Client;
using System.IO;

namespace LPS.Util
{
	public class RefreshCommand : CommandBase
	{
		public RefreshCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "FlushModulesInfo v connection"; }
		}

		public override object Execute(LPS.ToolScript.Context context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			UtilMainWindow.Instance.InitCmds();
			Out.WriteLine("Inicializováno");
			return true;
		}
	}
}
