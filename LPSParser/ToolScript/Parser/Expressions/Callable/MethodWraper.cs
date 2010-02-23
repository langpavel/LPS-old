using System;
using System.Reflection;
using System.Collections.Generic;

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

		public object Execute (Context context, NamedArgumentList arguments)
		{
			//List<MethodInfo> try_again = new List<MethodInfo>(Methods.Length);
			foreach(MethodInfo method in Methods)
			{
				ParameterInfo[] aparams = method.GetParameters();
				if(aparams.Length != arguments.Count)
					continue;
				object[] paramvals = new object[aparams.Length];
				for(int i = 0; i < aparams.Length; i++)
				{
					paramvals[i] = arguments.GetValue(aparams[i].Name, i);
					if(paramvals[i] != null)
					{
						if(!paramvals[i].GetType().IsSubclassOf(aparams[i].ParameterType))
							continue;
					}
				}
				return method.Invoke(Instance, paramvals);
			}
			throw new InvalidOperationException("Patřičná metoda nebyla nalezena");
		}
	}
}
