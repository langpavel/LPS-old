using System;
using System.Data;
using Gtk;

namespace LPSClient.Sklad
{
	public class AdresaForm : AutobindWindow
	{
		public AdresaForm ()
		{
		}
		
		public override void Load (long id)
		{
			Load("select * from adresa where id=:id", id);
		}

	}
}
