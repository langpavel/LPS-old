using System;
using System.Data;
using Gtk;

namespace LPS.Client.Sklad
{
	public class AdresaForm : AutobindWindow
	{
		public AdresaForm ()
		{
		}
		
		public override void Load (long id)
		{
			Load("adresa", id);
		}

		protected override void OnNewRow (DataRow row)
		{
			row["id"] = Connection.NextSeqValue("adresa_id_seq");
		}
	}
}
