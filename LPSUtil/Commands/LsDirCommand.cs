using System;
using System.IO;
using System.Collections.Generic;

namespace LPS.Util
{
	public class LsDirCommand : CommandBase
	{
		public LsDirCommand(CommandCollection Commands, string Name)
			: base(Commands, Name)
		{
		}

		public override Type[] ParamTypes
		{
			get { return new Type[] { typeof(string) }; }
		}

		public override string Help
		{
			get { return "vypíše obsah adresáře"; }
		}

		public override object Execute(TextWriter Out, TextWriter Info, TextWriter Err, object[] Params)
		{
			string p = Get<string>(ref Params, 0);
			if(p == "")
				p = "*";
			string dirname = Directory.GetCurrentDirectory();
			Info.WriteLine("Výpis adresáře {0}", dirname);
			DirectoryInfo info = new DirectoryInfo(dirname);
			List<string> names = new List<string>();
			foreach(DirectoryInfo dir in info.GetDirectories(p))
			{
				names.Add(String.Format("{0}/", dir.Name));
				Out.WriteLine("{0}/", dir.Name);
			}
			foreach(FileInfo file in info.GetFiles(p))
			{
				names.Add(file.Name);
				Out.WriteLine("{0}", file.Name);
			}
			return names.ToArray();
		}
	}
}
