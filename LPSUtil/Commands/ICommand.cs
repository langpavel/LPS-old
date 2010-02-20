using System;
using System.IO;

namespace LPS.Util
{
	public interface ICommand
	{
		string Name { get; }
		string Help { get; }
		CommandCollection Commands { get; }
		Type[] ParamTypes { get; }

		object Execute(TextWriter Out, TextWriter Info, TextWriter Err, object[] Params);
	}
}
