using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Tokens
{
	public sealed class TypeLiteral : LiteralBase, IConstantValue
	{
		public string Name { get; private set; }
		public Type Type { get; private set; }

		public TypeLiteral(Type type)
		{
			this.Type = type;
		}

		public TypeLiteral(string name)
		{
			this.Name = name;
		}

		public override object Eval(Context context)
		{
			return this.Type;
		}

		public override bool EvalAsBool(Context context)
		{
			throw new Exception("Nelze vyhodnotit hodnotu typu typ jako boolean");
		}

	}
}
