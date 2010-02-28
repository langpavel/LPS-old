using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class DBColumnAttributes : List<DBColumnAttribute>, IExpression
	{
		public DBColumnAttributes()
		{
		}

		public void Run (Context context)
		{
			Eval(context);
		}

		public object Eval (Context context)
		{
			foreach(DBColumnAttribute attr in this)
				attr.Eval(context);
		}
	}
}
