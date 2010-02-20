using System;
using System.IO;
using System.Text;

namespace LPS.Util
{
	public abstract class CommandBase : ICommand
	{
		private string name;
		protected CommandBase(CommandCollection Commands, string Name)
		{
			this.name = Name;
			this.Commands = Consumer;
			this.Commands.Commands.Add(this);
		}

		public string Name { get { return this.name; } }
		public virtual string Help
		{
			get { return "nedokumentováno";}
		}

		public TextWriter Out { get; set; }
		public TextWriter Info { get; set; }
		public TextWriter Err { get; set; }
		public CommandCollection Commands { get; set; }

		public virtual Type[] ParamTypes
		{
			get { return new Type[] { }; }
		}

		protected T Get<T>(ref object[] parameters, int index)
		{
			if(typeof(T) != ParamTypes[index])
				throw new InvalidOperationException("typová chyba");
			object val = parameters[index];
			if(val == null || val is T)
			   return (T)val;
			val = Convert.ChangeType(val, typeof(T));
			parameters[index] = val;
			return (T)val;
		}

		public abstract object Execute(TextWriter Out, TextWriter Info, TextWriter Err, object[] Params);

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Name).Append("(");
			for(int i=0; i<ParamTypes.Length; i++)
			{
				sb.Append(t.Name);
				if(i < ParamTypes.Length-1)
					sb.Append(", ");
			}
			sb.Append(")");
			return sb.ToString();
		}
	}
}
