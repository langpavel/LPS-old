using System;
using System.IO;
using System.Text;
using LPS.ToolScript.Parser;
using LPS.ToolScript;

namespace LPS.Util
{
	public abstract class CommandBase : ICommand, IFunction
	{
		private string name;
		protected CommandBase(string Name)
		{
			this.name = Name;
		}

		public string Name { get { return this.name; } }
		public virtual string Help
		{
			get { return "nedokumentováno";}
		}

		protected T Get<T>(object[] parameters, int index)
		{
			if(parameters == null || index >= parameters.Length)
				return default(T);
			object val = parameters[index];
			if(val == null || val is T)
			   return (T)val;
			return (T)Convert.ChangeType(val, typeof(T));
		}

		public abstract object Execute(Context context, TextWriter Out, TextWriter Info, TextWriter Err, object[] Params);

		public override string ToString ()
		{
			return String.Format("function {0}(...);", Name);
		}

		public virtual object Execute (NamedArgumentList argumentValues)
		{
			Context context = UtilMainWindow.Instance.ParserContext;
			TextWriter Out = (TextWriter)context.GetVariable("__STD_OUT");
			TextWriter Info = (TextWriter)context.GetVariable("__STD_INFO");
			TextWriter Err = (TextWriter)context.GetVariable("__STD_ERR");
			object[] args = new object[argumentValues.Count];
			for(int i=0; i<argumentValues.Count; i++)
				args[i] = argumentValues[i].Value;
			return Execute(context, Out, Info, Err, args);
		}
	}
}
