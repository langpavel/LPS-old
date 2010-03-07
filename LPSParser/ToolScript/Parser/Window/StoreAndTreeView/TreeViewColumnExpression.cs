using System;
using Gtk;
using System.Reflection;

namespace LPS.ToolScript.Parser
{
	public class TreeViewColumnExpression : ExpressionBase
	{
		public string ColumnType {get; private set; }
		public int StoreIndex { get; private set; }
		public EvaluatedAttributeList Params { get; private set; }

		public TreeViewColumnExpression(string ColumnType, int StoreIndex, EvaluatedAttributeList Params)
		{
			this.ColumnType = ColumnType;
			this.StoreIndex = StoreIndex;
			this.Params = Params;
		}

		public override object Eval (IExecutionContext context)
		{
			Params.Eval(context);
			return this;
		}

		private void SetColumnAttributes(TreeViewColumn column, WindowContext context)
		{
			Type t = column.GetType();
			t.FindMembers(
				MemberTypes.Property | MemberTypes.Event,
				BindingFlags.Public | BindingFlags.Instance,
				SetGtkPropertyValue, new object[] { column, context });
		}

		private bool SetGtkPropertyValue(MemberInfo member, object attrs)
		{
			object column = ((object[])attrs)[0];
			WindowContext context = (WindowContext)(((object[])attrs)[1]);
			if(member is PropertyInfo)
			{
				PropertyInfo prop = (PropertyInfo)member;
				if(!prop.CanWrite)
					return false;
				bool gtkprop = false;
				object val;
				foreach(GLib.PropertyAttribute propname in member.GetCustomAttributes(typeof(GLib.PropertyAttribute), true))
				{
					gtkprop = true;
					if(Params.TryGet(prop.PropertyType, propname.Name.Replace('-','_'), out val))
					{
						prop.SetValue(column, val, null);
						return true;
					}
					else if(!String.IsNullOrEmpty(propname.Nickname) && Params.TryGet(prop.PropertyType, propname.Nickname.Replace('-','_'), out val))
					{
						prop.SetValue(column, val, null);
						return true;
					}
				}
				if(Params.TryGet(prop.PropertyType, prop.Name, out val))
				{
					prop.SetValue(column, val, null);
					return true; //??
				}
				return gtkprop;
			}
			else if(member is EventInfo)
			{
				EventInfo ev = (EventInfo)member;
				foreach(GLib.SignalAttribute signal in ev.GetCustomAttributes(typeof(GLib.SignalAttribute), true))
				{
					object handler;
					if(Params.TryGet(typeof(object), signal.CName.Replace('-','_'), out handler))
					{
						if(handler is Delegate)
							ev.AddEventHandler(column, (Delegate)handler);
						else if(handler is IFunction)
							ev.AddEventHandler(column, ((IFunction)handler).GetEventHandler(ev.EventHandlerType, context));
						else
							throw new NotSupportedException();
						return true;
					}
				}
				return false;
			}
			else
				throw new NotSupportedException();
		}

		public TreeViewColumn CreateColumn(WindowContext context)
		{
			string title = Params.Get<string>("title", "");
			TreeViewColumn column;

			switch(this.ColumnType)
			{
			case "text":
				column = new TreeViewColumn(title, new CellRendererText(), "text", this.StoreIndex);
				break;
			case "markup":
				column = new TreeViewColumn(title, new CellRendererText(), "markup", this.StoreIndex);
				break;
			default:
				throw new NotSupportedException("Sloupec typu '"+this.ColumnType+"' není podporován");
			}
			SetColumnAttributes(column, context);
			return column;
		}

	}
}
