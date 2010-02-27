using System;

namespace LPS.ToolScript.Parser
{
	public class WidgetFromExpression : WidgetBase
	{
		public IExpression Expr { get; private set; }

		private object Value;

		public WidgetFromExpression(IExpression Expr, WidgetParamList Params)
			: base(null, Params)
		{
			this.Expr = Expr;
		}

		public override object Eval (Context context)
		{
			object result = base.Eval(context);
			if(this.Expr is Variable)
			{
				string varname = ((Variable)this.Expr).Name.ToLower();
				switch(varname)
				{
				case "label":
					this.Value = typeof(Gtk.Label);
					break;
				case "edit":
				case "entry":
					this.Value = typeof(Gtk.Entry);
					break;
				case "button":
					this.Value = typeof(Gtk.Button);
					break;
				case "togglebutton":
					this.Value = typeof(Gtk.ToggleButton);
					break;
				case "checkbutton":
				case "checkbox":
					this.Value = typeof(Gtk.CheckButton);
					break;
				case "spin":
				case "spinedit":
					this.Value = typeof(Gtk.SpinButton);
					break;
				case "radiobutton":
					this.Value = typeof(Gtk.RadioButton);
					break;
				case "filebutton":
					this.Value = typeof(Gtk.FileChooserButton);
					break;
				case "colorbutton":
					this.Value = typeof(Gtk.ColorButton);
					break;
				case "fontbutton":
					this.Value = typeof(Gtk.FontButton);
					break;
				case "link":
				case "linkbutton":
					this.Value = typeof(Gtk.LinkButton);
					break;
				case "image":
					this.Value = typeof(Gtk.Image);
					break;
				case "combo":
				case "combobox":
					this.Value = typeof(Gtk.ComboBox);
					break;
				case "comboentry":
				case "comboboxentry":
					this.Value = typeof(Gtk.ComboBoxEntry);
					break;
				case "progress":
				case "progressbar":
					this.Value = typeof(Gtk.ProgressBar);
					break;
				case "status":
				case "statusbar":
					this.Value = typeof(Gtk.Statusbar);
					break;
				case "textview":
				case "textarea":
				case "text":
					this.Value = typeof(Gtk.TextView);
					break;
				case "treeview":
				case "tree":
					this.Value = typeof(Gtk.TreeView);
					break;
				case "iconview":
					this.Value = typeof(Gtk.IconView);
					break;
				case "calendar":
					this.Value = typeof(Gtk.Calendar);
					break;
				case "hseparator":
				case "horizontalseparator":
					this.Value = typeof(Gtk.HSeparator);
					break;
				case "vseparator":
				case "verticalseparator":
					this.Value = typeof(Gtk.VSeparator);
					break;
				default:
					this.Value = Expr.Eval(context);
					break;
				}
			}
			else
			{
				this.Value = Expr.Eval(context);
			}
			return result;
		}

		protected override Gtk.Widget CreateWidget ()
		{
			if(Value == null)
				return new Gtk.Label("");
			if(Value is IWidgetBuilder)
				return ((IWidgetBuilder)Value).Build();
			if(Value is Gtk.Widget)
				return (Gtk.Widget)Value;
			if(Value is Type && ((Type)Value).IsSubclassOf(typeof(Gtk.Widget)))
				return (Gtk.Widget)Activator.CreateInstance(((Type)Value));
			if(Value is String)
			{
				Gtk.Label l = new Gtk.Label();
				l.Markup = (string)Value;
				return l;
			}
			throw new NotImplementedException();
		}

		public override T GetAttribute<T> (string name, T default_value)
		{
			if(Value is IWidgetBuilder)
				return base.GetAttribute(name, ((IWidgetBuilder)Value).GetAttribute<T>(name, default_value));
			return base.GetAttribute(name, default_value);
		}
	}
}
