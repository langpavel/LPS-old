using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LPS
{
	public enum Verbosity : int
	{
		Fatal = 4,
		Error = 3,
		Warning = 2,
		Info = 1,
		Debug = 0
	}

	public static class Log
	{
		private static List<ILogger> loggers;
		[ThreadStatic]
		private static LogScope current_scope;

		public static object SyncRoot { get; private set; }

		static Log()
		{
			SyncRoot = new Object();
			loggers = new List<ILogger>();
		}

		public static void Add(ILogger logger)
		{
			lock(SyncRoot)
			{
				loggers.Add(logger);
			}
		}

		public static bool Remove(ILogger logger)
		{
			lock(SyncRoot)
			{
				return loggers.Remove(logger);
			}
		}

		public static void DisposeLoggers()
		{
			ILogger[] copy;
			lock(SyncRoot)
			{
				copy = loggers.ToArray();
			}
			foreach(ILogger logger in copy)
				logger.Dispose();
			lock(SyncRoot)
			{
				loggers.Clear();
			}
		}

		private static void ScopeDisposed (object sender, EventArgs e)
		{
			LogScope scope = (LogScope)sender;
			current_scope = scope.ParentScope;
		}

		public static string GetCurrentLocation(int skip_frames)
		{
			StackFrame sf = new StackFrame(skip_frames + 1, true);
			return sf.ToString();
		}

		public static string GetCurrentLocationShort(int skip_frames)
		{
			StackFrame sf = new StackFrame(skip_frames + 1, true);
			return String.Format("{0}", sf.GetMethod().ToString());
		}

		public static LogScope ScopeSource(string source, string text)
		{
			current_scope = new LogScope(current_scope, source, text);
			current_scope.Disposed += ScopeDisposed;
			return current_scope;
		}

		public static LogScope Scope()
		{
			return ScopeSource(GetCurrentLocation(1), GetCurrentLocationShort(1));
		}

		public static LogScope Scope(string text)
		{
			return ScopeSource(GetCurrentLocation(1), text);
		}

		public static LogScope Scope(string text, params object[] args)
		{
			return ScopeSource(GetCurrentLocation(1), String.Format(text, args));
		}

		public static void Write(Verbosity verbosity, string source, string text)
		{
			lock(SyncRoot)
			{
				foreach(ILogger logger in loggers)
					logger.Write(current_scope, verbosity, source, text);
			}
		}

		public static void Write(Verbosity verbosity, string source, string text, params object[] args)
		{
			Write(verbosity, source, String.Format(text, args));
		}

		public static void Write(Verbosity verbosity, string text, params object[] args)
		{
			Write(verbosity, GetCurrentLocation(1), String.Format(text, args));
		}

		public static void Debug(string text, params object[] args)
		{
			Write(Verbosity.Debug, GetCurrentLocation(1), String.Format(text, args));
		}

		public static void Info(string text, params object[] args)
		{
			Write(Verbosity.Info, GetCurrentLocation(1), String.Format(text, args));
		}

		public static void Warning(string text, params object[] args)
		{
			Write(Verbosity.Warning, GetCurrentLocation(1), String.Format(text, args));
		}

		public static void Error(string text, params object[] args)
		{
			Write(Verbosity.Error, GetCurrentLocation(1), String.Format(text, args));
		}

		public static void Error(Exception exception)
		{
			Write(Verbosity.Error, GetCurrentLocation(1), exception.ToString());
		}

		public static void Fatal(string text, params object[] args)
		{
			Write(Verbosity.Fatal, GetCurrentLocation(1), String.Format(text, args));
		}

		public static void Fatal(Exception exception)
		{
			Write(Verbosity.Fatal, GetCurrentLocation(1), exception.ToString());
		}

	}
}
