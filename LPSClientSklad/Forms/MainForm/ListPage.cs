using System;
using System.Data;
using Gtk;

namespace LPS.Client.Sklad
{
	public class ListPage : ScrolledWindow
	{
		private MainForm mainform;
		private Notebook notebook;
		private ModulesTreeInfo module;
		private HBox headerbox;
		private Label headerlabel;
		private TreeView treeview;
		
		public ListPage (MainForm mainform, Notebook notebook, ModulesTreeInfo module)
		{
			this.mainform = mainform;
			this.notebook = notebook;
			this.module = module;

			headerbox = new HBox();
			headerlabel = new Label(module.Text);
			headerbox.PackStart(headerlabel);
			Image img = new Image("gtk-close", IconSize.Menu);
			Button btnCloseTab = new Button(img);
			btnCloseTab.BorderWidth = 0;
			btnCloseTab.Relief = ReliefStyle.None;
			btnCloseTab.WidthRequest = 19;
			btnCloseTab.HeightRequest = 19;
			btnCloseTab.Clicked += delegate { this.Dispose(); };
			headerbox.PackStart(btnCloseTab);
			headerbox.ShowAll();
			
			treeview = new TreeView();
			treeview.RowActivated += ListRowActivated;
			this.Add(treeview);
			this.ShowAll();
			
			notebook.AppendPage(this, headerbox);
			notebook.SetTabReorderable(this, true);
		}

		public bool IsCurrent
		{ 
			get { return notebook.GetNthPage(notebook.CurrentPage) == this; }
		}
		
		public int PageIndex
		{
			get { return notebook.PageNum(this); }
		}
		
		public void SetCurrent()
		{
			notebook.CurrentPage = this.PageIndex;
			//mainform.
		}

		public void ListRowActivated(object sender, RowActivatedArgs args)
		{
			//OpenDetailForm(view, args.Path);
		}

		public override void Dispose ()
		{
			notebook.RemovePage(this.PageIndex);
			
			base.Dispose ();
		}
	}
}
