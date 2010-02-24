using System;
using System.IO;

namespace LPS.Util
{
	public class ChangeDirCommand : CommandBase
	{
		public ChangeDirCommand(string Name)
			: base(Name)
		{
		}

		public override string Help
		{
			get { return "změní pracovní adresář"; }
		}

		public override object Execute(LPS.ToolScript.Context context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			string p = Get<string>(Params, 0);
			if(!String.IsNullOrEmpty(p))
				System.IO.Directory.SetCurrentDirectory(p);

			string curr_dir = System.IO.Directory.GetCurrentDirectory();
			Info.WriteLine(curr_dir);
			return curr_dir;
		}
	}
}
