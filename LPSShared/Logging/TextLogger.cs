using System;
using System.Text;
using System.IO;

namespace LPS
{
	public class TextLogger : ILogger
	{
		private LogScope last_scope;
		private TextWriter writer;
		private Verbosity minimal_verbosity;

		public TextLogger(TextWriter writer, Verbosity minimal_verbosity)
		{
			this.writer = writer;
			this.minimal_verbosity = minimal_verbosity;
		}

		private void UpdateScope(LogScope scope)
		{
			if(last_scope == scope)
				return;
			if(scope == null)
			{
				last_scope = null;
				return;
			}
			last_scope = scope;
			while(last_scope.ParentScope != null)
				last_scope = last_scope.ParentScope;
			while(last_scope != null)
			{
				StringBuilder sb = new StringBuilder();
				for(int i=0; i<scope.Level; i++)
					sb.Append(" | ");
				writer.WriteLine("{0} {1} at {2}", sb.ToString(), scope.Text, scope.Source);
				last_scope = last_scope.ChildScope;
			}
			last_scope = scope;
		}

		public void Write (LogScope scope, Verbosity verbosity, string source, string text)
		{
			if((int)minimal_verbosity > (int)verbosity || writer == null)
				return;
			lock(writer)
			{
				UpdateScope(scope);
				int lvl = (scope != null) ? scope.Level : 0;
				StringBuilder sb = new StringBuilder();
				for(int i=0; i<lvl; i++)
					sb.Append(" | ");
				string spaces = sb.ToString();
				writer.WriteLine("{0} - {1}: {2} at {3}", spaces, verbosity, text, source);
			}
		}

		public void Dispose ()
		{
			lock(writer)
			{
				if(writer != null)
				{
					writer.Close();
					writer = null;
				}
			}
		}
	}
}
