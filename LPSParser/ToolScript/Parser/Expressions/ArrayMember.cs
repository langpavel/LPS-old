using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

namespace LPS.ToolScript.Parser
{
	public class ArrayMember : ExpressionBase, IAssignable
	{
		public IExpression Expr1 { get; private set; }
		public IExpression Expr2 { get; private set; }

		public ArrayMember(IExpression Expr1, IExpression Expr2)
		{
			this.Expr1 = Expr1;
			this.Expr2 = Expr2;
		}

		public override object Eval (Context context)
		{
			object obj = Expr1.Eval(context);

			if(obj == null)
			{
				throw new InvalidOperationException();
			}
			else if(obj is Hashtable)
			{
				return ((Hashtable)obj)[Expr2.Eval(context)];
			}
			else if(obj is Array)
			{
				object index = Expr2.Eval(context);
				if(!IsInteger(index))
					throw new Exception("Index pole musí být celočíselný");
				return ((Array)obj).GetValue(Convert.ToInt64(index));
			}
			else if(obj is String)
			{
				object index = Expr2.Eval(context);
				if(!IsInteger(index))
					throw new Exception("Index pole musí být celočíselný");
				return ((String)obj)[Convert.ToInt32(index)];
			}
			else
			{
				object index = Expr2.Eval(context);
				Type t = obj.GetType();
				// find indexer //
				PropertyInfo prop = t.GetProperty("Item");
				//Console.WriteLine(prop);
				//Console.WriteLine(prop.GetGetMethod());
				return prop.GetGetMethod().Invoke(obj, new object[] {index});
			}
			//throw new System.NotImplementedException ();
		}

		public void AssignValue (Context context, object val)
		{
			object obj = Expr1.Eval(context);

			if(obj is Hashtable)
			{
				((Hashtable)obj)[Expr2.Eval(context)] = val;
				return;
			}
			else if(obj is Array)
			{
				object index = Expr2.Eval(context);
				if(!IsInteger(index))
					throw new Exception("Index pole musí být celočíselný");
				((Array)obj).SetValue(val, Convert.ToInt64(index));
				return;
			}
			else
			{
				object index = Expr2.Eval(context);
				Type t = obj.GetType();
				// find indexer //
				PropertyInfo prop = t.GetProperty("Item");
				prop.GetSetMethod().Invoke(obj, new object[] {index, val});
			}
			//throw new System.NotImplementedException ();
		}

		public override string ToString ()
		{
			return string.Format("{0}[{1}]", Expr1, Expr2.GetType().Name);
		}

	}
}
