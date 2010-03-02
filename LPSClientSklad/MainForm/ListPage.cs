using System;
using System.Data;
using Gtk;

namespace LPS.Client.Sklad
{
	public class ListPage : ScrolledWindow
	{
		private Notebook notebook;
		private ModulesTreeInfo module;
		public ModulesTreeInfo Module { get {return module; } }
		private HBox headerbox;
		private Label headerlabel;
		private DataTableView tableview;
		public DataTableView TableView { get {return tableview; } }
		private Image close_img;
		private Button btnCloseTab;
		
		public ListPage (Notebook notebook, ModulesTreeInfo module)
		{
			this.notebook = notebook;
			this.module = module;

			headerbox = new HBox();
			headerlabel = new Label(module.Text);
			headerbox.PackStart(headerlabel);
			//Image img = new Image("gtk-close", IconSize.Menu);
			close_img = ImageManager.GetImage("Images.close-button.png");
			//close_img = new Image("gtk-close", IconSize.Menu);
			btnCloseTab = new Button(close_img);
			btnCloseTab.BorderWidth = 0;
			btnCloseTab.Relief = ReliefStyle.None;
			//btnCloseTab.WidthRequest = 19;
			//btnCloseTab.HeightRequest = 19;
			btnCloseTab.Clicked += delegate { this.Dispose(); };
			headerbox.PackStart(btnCloseTab);
			headerbox.ShowAll();
			
			tableview = new DataTableView(module);
			this.Add(tableview);
			this.ShowAll();
			
			notebook.AppendPage(this, headerbox);
			notebook.SetTabReorderable(this, true);
		}

		public override void Dispose ()
		{
			tableview.SaveConfiguration("__current");
			notebook.RemovePage(this.PageIndex);
			close_img.Destroy();
			btnCloseTab.Destroy();
			headerlabel.Destroy();
			headerbox.Destroy();
			tableview.Dispose();
			this.Destroy();
			base.Dispose();
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
			//TODO: assign column list
		}

	}
}
