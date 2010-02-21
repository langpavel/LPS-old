using System;
using System.IO;

namespace LPS.Util
{
	public class ChangeDirCommand : CommandBase
	{
		public ChangeDirCommand(CommandCollection Commands, string Name)
			: base(Commands, Name)
		{
		}

		public override Type[] ParamTypes
		{
			get { return new Type[] { typeof(string) }; }
		}

		public override string Help
		{
			get { return "změní pracovní adresář"; }
		}

		public override object Execute(TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			string p = Get<string>(ref Params, 0);
			if(p != "")
				System.IO.Directory.SetCurrentDirectory(p);

			string curr_dir = System.IO.Directory.GetCurrentDirectory();
			Info.WriteLine(curr_dir);
			return curr_dir;
		}
	}
}
