using System;
using Gtk;
using Gdk;

namespace LPS.ToolScript.Parser
{
	public class ImageExpression : WidgetBase
	{
		public ImageExpression(string Name, EvaluatedAttributeList Params)
			:base(Name, Params)
		{
		}

		private bool SetStock(Gtk.Image image, string atrname, IconSize size)
		{
			if(this.HasAttribute(atrname))
			{
				IconSet icons = IconFactory.LookupDefault(this.GetAttribute<string>(atrname));
				image.Pixbuf = icons.RenderIcon(image.Style, TextDirection.Ltr, StateType.Normal, size, image, null);
				return true;
			}
			return false;
		}

		public static Pixbuf CreatePixbuf(string stockname, Widget widget, IconSize size)
		{
			IconSet icons = IconFactory.LookupDefault(stockname);
			return icons.RenderIcon(widget.Style, TextDirection.Ltr, StateType.Normal, size, widget, null);
		}

		public static Gtk.Image CreateImage(string stockname, IconSize size)
		{
			Gtk.Image image = new Gtk.Image();
			image.Pixbuf = CreatePixbuf(stockname, image, size);
			return image;
		}

		protected override Gtk.Widget CreateWidget()
		{
			if(this.HasAttribute("icon"))
			{
				return Gtk.Image.NewFromIconName(
					this.GetAttribute<string>("icon"),
					(IconSize)this.GetAttribute<int>("iconsize"));
			}
			Gtk.Image image = new Gtk.Image();
			if(SetStock(image, "dialog_stock", IconSize.Dialog)) { }
			else if(SetStock(image, "small_stock", IconSize.Menu)) { }
			else if(SetStock(image, "button_stock", IconSize.Button)) { }
			else if(SetStock(image, "smalltool_stock", IconSize.SmallToolbar)) { }
			else if(SetStock(image, "bigtool_stock", IconSize.LargeToolbar)) { }
			return image;
		}
	}
}
