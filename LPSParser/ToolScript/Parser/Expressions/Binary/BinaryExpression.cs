using System;
using System.Reflection;

namespace LPS.ToolScript.Parser
{
	public abstract class BinaryExpression : ExpressionBase
	{
		public IExpression Expr1 { get; private set; }
		public IExpression Expr2 { get; private set; }

		public BinaryExpression(IExpression Expr1, IExpression Expr2)
		{
			this.Expr1 = Expr1;
			this.Expr2 = Expr2;
		}

		public override object Eval (IExecutionContext context)
		{
			return Eval(context, Expr1.Eval(context), Expr2.Eval(context));
		}

		public override bool EvalAsBool(IExecutionContext context)
		{
			return EvalAsBool(context, Expr1.Eval(context), Expr2.Eval(context));
		}

		public abstract object Eval(IExecutionContext context, object val1, object val2);
		public abstract bool EvalAsBool(IExecutionContext context, object val1, object val2);

		public static bool InvokeOperator(string name, object val1, object val2, out object result)
		{
			result = null;
			if(val1 == null || val2 == null)
				return false;
			Type val1t = val1.GetType();
			Type val2t = val2.GetType();
			MemberInfo[] members = val1t.FindMembers(
				MemberTypes.Method,
				BindingFlags.Public | BindingFlags.Static,
				Type.FilterName, name);
			foreach(MethodInfo method in members)
			{
				ParameterInfo[] pi = method.GetParameters();
				if(pi.Length != 2)
					continue;
				if(pi[1].ParameterType == val2t || val2t.IsSubclassOf(pi[1].ParameterType))
				{
					result = method.Invoke(null, new object[] {val1, val2});
					return true;
				}
			}
			return false;
		}
	}
}
