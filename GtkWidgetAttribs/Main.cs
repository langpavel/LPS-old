using System;
using System.Reflection;
using Gtk;
using System.Text;

public class GtkWidgetAttribs {
	private static TreeStore store = null;
	private static Dialog dialog = null;
	private static Label dialog_label = null;
		
	public GtkWidgetAttribs ()
	{
	}

	public void Run(string[] args)
	{
		Application.Init ();
		PopulateStore ();
		store.SetSortColumnId(2, SortType.Ascending);

		Window win = new Window ("Gtk Widget Attributes");
		win.DeleteEvent += new DeleteEventHandler (DeleteCB);
		win.SetDefaultSize (640,480);

		ScrolledWindow sw = new ScrolledWindow ();
		win.Add (sw);

		TreeView tv = new TreeView (store);
		tv.HeadersVisible = true;

		tv.AppendColumn ("Name", new CellRendererText (), "markup", 0);
		tv.AppendColumn ("Type", new CellRendererText (), "text", 1);

		foreach(TreeViewColumn col in tv.Columns)
			col.Resizable = true;

		tv.SearchColumn = 2;

		sw.Add (tv);

		dialog.Destroy ();
		dialog = null;

		win.ShowAll ();

		Application.Run ();
	}

	private static string NiceTypeName(Type t)
	{
		string typename = t.FullName;
		if(typename.StartsWith("Gtk."))
			typename = typename.Substring(4);
		else if(typename.StartsWith("System."))
		{
			typename = typename.Substring(7);
			if(typename == "Int32") typename = "int";
			else if(typename == "UInt32") typename = "unsigned int";
			else if(typename == "Boolean") typename = "bool";
			else if(typename == "Int16") typename = "short";
			else if(typename == "UInt16") typename = "unsigned short";
			else if(typename == "Int64") typename = "long";
			else if(typename == "UInt64") typename = "unsigned long";
			else if(typename == "String") typename = "string";
			else if(typename == "String[]") typename = "string[]";
		}
		return typename;
	}

	private static string Inherits(Type t)
	{
		StringBuilder sb = new StringBuilder();
		do
		{
			t = t.BaseType;
			if(t != null && t != typeof(System.Object))
				sb.AppendFormat(": {0}", NiceTypeName(t));
		}
		while(t.BaseType != null
			&& t.BaseType != typeof(object)
			&& t.BaseType != typeof(GLib.Object)
			&& t.BaseType != typeof(Gtk.Object));
		return sb.ToString();
	}

	private static void ProcessType (TreeIter parent, System.Type t)
	{
		bool iterset = false;
		TreeIter iter = TreeIter.Zero;
		//if(!(t==typeof(Gtk.Widget) || t.IsSubclassOf(typeof(Gtk.Widget))))
		//	return;
		foreach (MemberInfo mi in t.GetMembers ())
		{
			object[] attrs = mi.GetCustomAttributes(typeof(GLib.PropertyAttribute), false);
			if(attrs != null && attrs.Length > 0)
			{
				PropertyInfo prop = (PropertyInfo)mi;
				if(prop.DeclaringType != t)
					continue;
				foreach(GLib.PropertyAttribute attr in attrs)
				{
					if(!iterset)
					{
						iterset = true;
						iter = store.AppendValues (parent, "<tt><b>"+NiceTypeName(t)+"</b></tt><small>" + Inherits(t)+"</small>", t.ToString()+" : "+t.BaseType.ToString(), t.Name);
					}
					string rw = ((prop.CanRead)?"r":"") + ((prop.CanWrite)?"w":"");
					store.AppendValues (iter,
						String.Format("({0})\t<tt><b>{1}</b></tt><small>: {2}</small>",
							rw, attr.Name, NiceTypeName(prop.PropertyType)), prop.PropertyType.ToString(), attr.Name);
				}
			}
		}
		if(iterset)
		{
			bool implicit_constructor = false;
			bool no_constructor = true;
			foreach (MemberInfo mi in t.GetMembers ())
			{
				if(mi is ConstructorInfo)
				{
					ConstructorInfo constructor = (ConstructorInfo)mi;
					ParameterInfo[] p = constructor.GetParameters();
					if(p.Length == 0)
						implicit_constructor = true;
					if(p.Length == 1 && p[0].ParameterType == typeof(IntPtr))
						continue;
					no_constructor = false;
					string[] ptext = new string[p.Length];
					for(int i=0; i<p.Length; i++)
					{
						ptext[i] = "<small>" + NiceTypeName(p[i].ParameterType) + "</small> <b>" + p[i].Name + "</b>";
					}
					store.AppendValues (iter, "new\t<tt>(" + String.Join(", ", ptext) + ")</tt>", "konstruktor", "");
				}
			}
			if(implicit_constructor)
				store.SetValues(iter, "<tt><b><span foreground=\"#770000\">"+NiceTypeName(t)+"</span></b></tt><small>" + Inherits(t)+"</small>", t.ToString()+" : "+t.BaseType.ToString(), t.Name);
			else if(no_constructor)
				store.SetValues(iter, "<tt><b><span foreground=\"#999999\">"+NiceTypeName(t)+"</span></b></tt><small>" + Inherits(t)+"</small>", t.ToString()+" : "+t.BaseType.ToString(), t.Name);
		}
	}

	private static void ProcessAssembly (TreeIter parent, Assembly asm)
	{
		string asm_name = asm.GetName ().Name;

		foreach (System.Type t in asm.GetTypes ())
		{
			UpdateDialog ("Loading from {0}:\n{1}", asm_name, t.ToString ());
			ProcessType (parent, t);
		}
	}

	private static void PopulateStore ()
	{
		if (store != null)
			return;

		store = new TreeStore (typeof (string), typeof (string), typeof(string));

		foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies ())
		{
			string asmname = asm.GetName().Name;
			if(asmname == "System" || asmname == "mscorlib" || asmname == "glib-sharp" || asm == Assembly.GetExecutingAssembly())
				continue;
			UpdateDialog ("Loading {0}", asm.GetName ().Name);

			TreeIter iter = store.AppendValues (asm.GetName ().Name, "Assembly", "");
			ProcessAssembly (iter, asm);
		}
	}

	public static void Main (string[] args)
	{
		(new GtkWidgetAttribs()).Run(args);
	}

	private static void DeleteCB (System.Object o, DeleteEventArgs args)
	{
		Application.Quit ();
	}

	private static void UpdateDialog (string format, params object[] args)
	{
		string text = String.Format (format, args);

		if (dialog == null)
		{
			dialog = new Dialog ();
			dialog.Title = "Loading data from assemblies...";
			dialog.AddButton (Stock.Cancel, 1);
			dialog.Response += new ResponseHandler (ResponseCB);
			dialog.SetDefaultSize (480, 100);
					
			VBox vbox = dialog.VBox;
			HBox hbox = new HBox (false, 4);
			vbox.PackStart (hbox, true, true, 0);
				
			Gtk.Image icon = new Gtk.Image (Stock.DialogInfo, IconSize.Dialog);
			hbox.PackStart (icon, false, false, 0);
			dialog_label = new Label (text);
			hbox.PackStart (dialog_label, false, false, 0);
			dialog.ShowAll ();
		} else {
			dialog_label.Text = text;
			while (Application.EventsPending ())
				Application.RunIteration ();
 		}
	}

	private static void ResponseCB (object obj, ResponseArgs args)
	{
		Application.Quit ();
		System.Environment.Exit (0);
	}
}