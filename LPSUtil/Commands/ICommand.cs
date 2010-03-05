using System;
using System.IO;
using LPS.ToolScript;

namespace LPS.Util
{
	public interface ICommand
	{
		string Name { get; }
		string Help { get; }

		object Execute(IExecutionContext context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params);
	}
}
