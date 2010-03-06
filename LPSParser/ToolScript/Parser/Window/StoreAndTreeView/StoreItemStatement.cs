using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class StoreItemStatement : StatementBase
	{
		public List<IExpression> Expressions { get; private set; }
		public List<StoreItemStatement> Childs { get; private set; }

		public object[] Evaluated {get; private set; }

		public StoreItemStatement(List<IExpression> Expressions, List<StoreItemStatement> Childs)
		{
			this.Expressions = Expressions;
			this.Childs = Childs;
		}

		public override void Run (IExecutionContext context)
		{
			Evaluated = new object[Expressions.Count];
			for(int i=0; i < Expressions.Count; i++)
				Evaluated[i] = Expressions[i].Eval(context);
			if(Childs != null)
				foreach(StoreItemStatement child in Childs)
					child.Run(context);
		}

		private Type UpdateType(Type old, Type current)
		{
			if(old == null)
				return current;
			if(current == null)
				return old;
			while(!(current == null || current == old || old.IsSubclassOf(current)))
				current = current.BaseType;
			return current;
		}

		internal void UpdateTypes(List<Type> types)
		{
			for(int i = 0; i < this.Evaluated.Length; i++)
			{
				Type current = this.Evaluated[i] == null ? null : this.Evaluated[i].GetType();
				if(i >= types.Count)
					types.Add(current);
				else
					types[i] = UpdateType(types[i], current);
			}
			foreach(StoreItemStatement child in Childs)
				child.UpdateTypes(types);
		}
	}
}
