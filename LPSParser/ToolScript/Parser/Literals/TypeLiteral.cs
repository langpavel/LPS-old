using System;
using com.calitha.goldparser;

namespace LPS.ToolScript.Parser
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

		public override object Eval(IExecutionContext context)
		{
			return this.Type;
		}

		public override bool EvalAsBool(IExecutionContext context)
		{
			throw new Exception("Nelze vyhodnotit hodnotu typu typ jako boolean");
		}

	}
}
