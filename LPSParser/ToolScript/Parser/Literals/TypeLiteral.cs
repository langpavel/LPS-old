using System;
using com.calitha.goldparser;
using System.Reflection;

namespace LPS.ToolScript.Parser
{
	public sealed class TypeLiteral : LiteralBase, IConstantValue
	{
		public QualifiedName Name { get; private set; }
		public Type Type { get; private set; }

		public TypeLiteral(Type type)
		{
			this.Type = type;
		}

		public TypeLiteral(QualifiedName Name)
		{
			this.Name = Name;
		}

		public static Type FindType(QualifiedName name)
		{
			Type t = null;
			string typename = name.ToString();
			foreach(Assembly a in AppDomain.CurrentDomain.GetAssemblies())
			{
				t = a.GetType(typename, false);
				if(t != null)
					return t;
			}
			throw new Exception("Typ '" + typename + "' nebyl nalezen");
		}

		public override object Eval(IExecutionContext context)
		{
			if(this.Type != null)
				return this.Type;
			if(this.Name != null)
				return this.Type = FindType(this.Name);
			throw new InvalidProgramException();
		}

		public override bool EvalAsBool(IExecutionContext context)
		{
			throw new Exception("Nelze vyhodnotit hodnotu typu typ jako boolean");
		}

	}
}
