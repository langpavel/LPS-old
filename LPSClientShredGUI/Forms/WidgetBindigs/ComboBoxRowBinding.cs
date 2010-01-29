using System;
using System.Data;
using Gtk;

namespace LPSClient
{

	public class ComboBoxRowBinding : WidgetRowBinding
	{
		public ComboBox ComboBox
		{
			get { return this.Widget as ComboBox; }
			set { this.Widget = value; }
		}
		public ColumnInfo ColInfo { get; set; }
		
		public ComboBoxRowBinding()
		{
		}
		
		public ComboBoxRowBinding(ComboBox cb, DataColumn column, DataRow row, ColumnInfo colinfo)
		{
			this.ComboBox = cb;
			this.Column = column;
			this.ColInfo = colinfo;
			this.Row = row;
			Bind();
			UptadeComboValue(row[Column]);
		}
		
		private DataTable table;
		private ListStore store;
		public override void Bind ()
		{
			string table = ColInfo.FkReferenceTable;
			string refCols = ColInfo.FkComboReplaceColumns ?? ColInfo.FkReplaceColumns;
			string sql = String.Format("select id, {1} from {0}", table, refCols);
			this.ComboBox.Clear();
			this.table = ServerConnection.Instance.GetCachedDataSet(sql).Tables[0];
			int cnt = this.table.Columns.Count;
			Type[] col_types = new Type[cnt];
			for(int i = 0; i<cnt; i++)
			{
				col_types[i] = this.table.Columns[i].DataType;	
				CellRendererText renderer = new CellRendererText();
				if(i > 0)
				{
					this.ComboBox.PackStart(renderer, (i == (cnt - 1)));
					this.ComboBox.AddAttribute(renderer, "text", i);
				}
			}
			this.store = new ListStore(col_types);
			foreach(DataRow r in this.table.Rows)
			{
				this.store.AppendValues(r.ItemArray);
			}
			this.ComboBox.Model = this.store;
			this.ComboBox.Changed += HandleComboBoxChanged;
			this.Row.Table.ColumnChanged += HandleRowTableColumnChanged;
		}

		public override void Unbind ()
		{
			this.ComboBox.Changed -= HandleComboBoxChanged;
			this.Row.Table.ColumnChanged -= HandleRowTableColumnChanged;
			this.ComboBox.Clear();
			this.store.Clear();
			this.store.Dispose();
		}

		void HandleRowTableColumnChanged (object sender, DataColumnChangeEventArgs e)
		{
			if(e.Column == this.Column && e.Row == this.Row)
			{
				UptadeComboValue(e.ProposedValue);
			}
		}

		void UptadeComboValue(object newVal)
		{
			TreeIter iter;
			if(!this.store.GetIterFirst(out iter))
				return;
			while(true)
			{
				long val = Convert.ToInt64(this.store.GetValue(iter, 0));
				if(Convert.ToInt64(newVal) == val)
				{
					this.ComboBox.SetActiveIter(iter);
					return;
				}
				if(!this.store.IterNext(ref iter))
					return;
			}
		}
		
		void HandleComboBoxChanged(object sender, EventArgs e)
		{
			TreeIter iter;
			if(this.ComboBox.GetActiveIter(out iter))
			{
				Type dbtype = this.Column.DataType;
				object val = Convert.ChangeType(this.store.GetValue(iter, 0), dbtype);
				if(!Convert.ChangeType(this.Row[this.Column], dbtype).Equals(val))
				{
					Console.WriteLine("{0} <==\t'{1}'", Column.ColumnName, val);
					this.Row[this.Column] = val;
				}
			}
			else
			{
				Console.WriteLine("{0} <==\tnull", Column.ColumnName);
				this.Row[this.Column] = DBNull.Value;
			}
		}

	}
}
