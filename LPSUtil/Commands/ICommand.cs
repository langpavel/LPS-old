using System;
using System.IO;

namespace LPS.Util
{
	public interface ICommand
	{
		void Execute(string cmd_name, string argline, TextWriter output);
		string GetHelp();
	}
}
