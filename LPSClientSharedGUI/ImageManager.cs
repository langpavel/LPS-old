using System;
using Gtk;

namespace LPS.Client
{
	public static class ImageManager
	{
		public static Image GetImage(string resource_name)
		{
			return new Image(null, resource_name);
		}
	}
}
