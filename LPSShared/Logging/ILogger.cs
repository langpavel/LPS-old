using System;

namespace LPS
{
	public interface ILogger: IDisposable
	{
		void Write(LogScope scope, Verbosity verbosity, string source, string text);
	}
}
