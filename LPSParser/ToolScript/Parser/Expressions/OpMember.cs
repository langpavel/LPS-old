using System;
using System.Reflection;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class OpMember : ExpressionBase, IAssignable
	{
		public IExpression Expr1 { get; private set; }
		public IExpression Expr2 { get; private set; }

		public OpMember(IExpression Expr1, IExpression Expr2)
		{
			this.Expr1 = Expr1;
			this.Expr2 = Expr2;
		}

		protected MemberInfo[] FindMembers(object obj, string membername, MemberTypes membertypes)
		{
			Type t = obj.GetType();
			MemberInfo[] members = t.FindMembers(membertypes,
				BindingFlags.Public | BindingFlags.Static| BindingFlags.Instance,
				Type.FilterName, membername);
			if(members.Length == 0)
				throw new Exception("Člen objektu " + membername + " nebyl nalezen");
			return members;
		}

		public override object Eval (Context context)
		{
			object obj = Expr1.Eval(context);
			if(Expr2 is Variable)
			{
				string membername = ((Variable)Expr2).Name;
				MemberInfo[] members = FindMembers(obj, membername, MemberTypes.Field | MemberTypes.Property | MemberTypes.Method);
				if(members.Length == 1)
				{
					switch(members[0].MemberType)
					{
					case MemberTypes.Property:
						if(!((PropertyInfo)members[0]).CanRead)
							throw new Exception("Property nemůže být čtena");
						MethodInfo getter = ((PropertyInfo)members[0]).GetGetMethod();
						return getter.Invoke(obj, new object[] {});
					case MemberTypes.Field:
						return ((FieldInfo)members[0]).GetValue(obj);
					case MemberTypes.Method:
						return new MethodWraper(new MethodInfo[] {(MethodInfo)(members[0])}, obj);
					}
				}
				else
				{
					List<MethodInfo> list = new List<MethodInfo>();
					foreach(MemberInfo member in members)
						if(member.MemberType != MemberTypes.Method)
							throw new Exception("Ne všechny members jsou metody!");
						else
							list.Add((MethodInfo)member);
					return new MethodWraper(list.ToArray(), obj);
				}
			}

			throw new System.NotImplementedException ();
		}

		public void AssignValue (Context context, object val)
		{
			object obj = Expr1.Eval(context);
			if(Expr2 is Variable)
			{
				MemberInfo[] members = FindMembers(obj, ((Variable)Expr2).Name, MemberTypes.Field | MemberTypes.Property);
				if(members.Length == 1)
				{
					switch(members[0].MemberType)
					{
					case MemberTypes.Property:
						if(!((PropertyInfo)members[0]).CanWrite)
							throw new Exception("Property nemůže být zapisována");
						MethodInfo setter = ((PropertyInfo)members[0]).GetSetMethod();
						setter.Invoke(obj, new object[] { val });
						return;
					case MemberTypes.Field:
						((FieldInfo)members[0]).SetValue(obj, val);
						return;
					}
				}
			}

			throw new System.NotImplementedException ();
		}

		public override string ToString ()
		{
			return string.Format("{0}.{1}", Expr1, Expr2.GetType().Name);
		}

	}
}
