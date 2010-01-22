using System;
using System.Collections;
using System.Collections.Generic;

namespace LPS
{
	public sealed class DeepEnumerator: IEnumerator
	{
		private IEnumerator root;
		private Stack<IEnumerator> stack;
		private IEnumerator curr;
		
		public DeepEnumerator(IEnumerator baseEnumerator)
		{
			stack = new Stack<IEnumerator>();
			stack.Push(baseEnumerator);
			root = baseEnumerator;
			curr = root;
		}
		
		public bool MoveNext()
		{
			if(stack.Count == 0)
				return false;
			while(!stack.Peek().MoveNext())
			{
				stack.Pop();
				if(stack.Count == 0)
					return false;
			}
			curr = stack.Peek();
			IEnumerable e = curr.Current as IEnumerable;
			if(e != null && !(e is string))
				stack.Push(e.GetEnumerator());
			return true;
		}

		public void Reset()
		{
			stack.Clear();
			root.Reset();
			stack.Push(root);
		}

		public object Current 
		{ 
			get { return curr.Current; }
		}
		
	}
}
