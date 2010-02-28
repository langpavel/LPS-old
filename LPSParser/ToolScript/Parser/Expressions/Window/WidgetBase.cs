using System;
using System.Reflection;

namespace LPS.ToolScript.Parser
{
	public abstract class WidgetBase : ExpressionBase, IWidgetBuilder
	{
		public string Name { get; set; }
		public EvaluatedAttributeList Params { get; private set; }

		public WidgetBase(string Name, EvaluatedAttributeList Params)
		{
			this.Name = Name;
			this.Params = Params;
		}

		public override object Eval (Context context)
		{
			Params.Eval(context);
			if(this.Name != null)
				context.InitVariable(this.Name, this);
			return this;
		}

		public virtual T GetAttribute<T>(string name, T default_value)
		{
			return Params.Get<T>(name, default_value);
		}

		public virtual bool TryGetAttribute<T>(string name, out T value)
		{
			return Params.TryGet<T>(name, out value);
		}

		public virtual bool TryGetAttribute(Type type, string name, out object value)
		{
			return Params.TryGet(type, name, out value);
		}

		public Gtk.Widget Build()
		{
			Gtk.Widget widget = CreateWidget();
			SetWidgetAttributes(widget);
			return widget;
		}

		protected abstract Gtk.Widget CreateWidget();

		public virtual void SetWidgetAttributes(Gtk.Widget widget)
		{
			Type t = widget.GetType();
			t.FindMembers(
				MemberTypes.Property,
				BindingFlags.Public | BindingFlags.Instance,
				SetGtkPropertyValue, widget);
		}

		private bool SetGtkPropertyValue(MemberInfo member, object widget)
		{
			PropertyInfo prop = (PropertyInfo)member;
			if(!prop.CanWrite)
				return false;
			bool gtkprop = false;
			object val;
			foreach(GLib.PropertyAttribute propname in member.GetCustomAttributes(typeof(GLib.PropertyAttribute), true))
			{
				gtkprop = true;
				if(TryGetAttribute(prop.PropertyType, propname.Name.Replace('-','_'), out val))
				{
					prop.SetValue(widget, val, null);
					return true;
				}
				else if(!String.IsNullOrEmpty(propname.Nickname) && TryGetAttribute(prop.PropertyType, propname.Nickname.Replace('-','_'), out val))
				{
					prop.SetValue(widget, val, null);
					return true;
				}
			}
			if(TryGetAttribute(prop.PropertyType, prop.Name, out val))
			{
				prop.SetValue(widget, val, null);
				return true; //??
			}
			return gtkprop;
		}

		public override bool EvalAsBool (Context context)
		{
			throw new InvalidOperationException("Widget nelze přetypovat na boolean");
		}
	}
}
