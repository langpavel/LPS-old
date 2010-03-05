using System;
using System.Reflection;

namespace LPS.ToolScript.Parser
{
	public class CatchStatement
	{
		public QualifiedName ExceptionType { get; private set; }
		public string ID { get; private set; }
		public IStatement Handler { get; private set; }

		public CatchStatement(QualifiedName ExceptionType, string ID, IStatement Handler)
		{
			this.ExceptionType = ExceptionType;
			this.ID = ID;
			this.Handler = Handler;
		}

		public bool Handle(IExecutionContext context, object errObject)
		{
			if(errObject is TargetInvocationException)
				errObject = ((TargetInvocationException)errObject).InnerException;

			if(errObject is ScriptCustomException)
				errObject = ((ScriptCustomException)errObject).CustomObject;

			if(ExceptionType != null)
			{
				if(errObject != null)
					return false;
				Type catchType = TypeLiteral.FindType(ExceptionType);
				Type errType = errObject.GetType();
				if(errType != catchType && !errType.IsSubclassOf(catchType))
					return false;
			}

			if(ID != null)
				context.InitVariable(ID, errObject);

			if(Handler != null)
				Handler.Run(context);

			return true;
		}
	}
}
