using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace LPS.ToolScript.Parser
{
	public class MethodWraper : IFunction
	{
		public MethodInfo[] Methods { get; private set; }
		public object Instance { get; private set; }

		public MethodWraper(MethodInfo[] methods, object instance)
		{
			this.Methods = methods;
			this.Instance = instance;
		}

		public object Execute (NamedArgumentList arguments)
		{
			//List<MethodInfo> try_again = new List<MethodInfo>(Methods.Length);
			foreach(MethodInfo method in Methods)
			{
				ParameterInfo[] aparams = method.GetParameters();
				if(aparams.Length != arguments.Count)
					continue;
				object[] vals = new object[aparams.Length];
				for(int i = 0; i < aparams.Length; i++)
				{
					object val = arguments.GetValue(aparams[i].Name, i);
					vals[i] = val;
					Type m_type = aparams[i].ParameterType;
					if(val != null)
					{
						Type p_type = val.GetType();
						if(!(m_type == p_type || p_type.IsSubclassOf(m_type)))
							goto NextMethod;
					}
					else
					{
						if(m_type.IsValueType)
							goto NextMethod;
					}
				}
				using(Log.Scope("Trying invoke {0}", method))
				{
					try
					{
						return method.Invoke(Instance, vals);
					}
					catch(TargetInvocationException err)
					{
						// unwrap exception
						if(err.InnerException != null)
							throw err.InnerException;
						else
							throw err;
					}
				}
NextMethod:		;
			}
			StringBuilder sb = new StringBuilder("Patřičná metoda nebyla nalezena. Varianty ");
			sb.AppendLine(Methods.Length.ToString());
			foreach(MethodInfo method in Methods)
				sb.AppendLine(method.ToString());
			throw new InvalidOperationException(sb.ToString());
		}

		public Delegate GetEventHandler (Type EventHandlerType, IExecutionContext CustomContext)
		{
			if(Methods.Length != 1 || Methods[0].GetParameters().Length != 2)
				throw new Exception("Metoda nelze použít jako event handler");
			return Delegate.CreateDelegate(EventHandlerType, this, typeof(MethodWraper).GetMethod("AsEventHandler"));
		}

		public void AsEventHandler(object sender, EventArgs args)
		{
			Methods[0].Invoke(Instance, new object[] {sender, args});
		}
	}
}
