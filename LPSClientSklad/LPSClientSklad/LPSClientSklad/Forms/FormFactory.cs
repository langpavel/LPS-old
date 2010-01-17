using System;
using System.Collections.Generic;

namespace LPSClientSklad
{
	public class FormFactory
	{
		Dictionary<string, FormInfo> forms;
		
		#region Singleton
		private FormFactory ()
		{
			forms = new Dictionary<string, FormInfo>();
		}
		
		private static FormFactory _Instance = null;
		public static FormFactory Instance
		{
			get
			{
				return _Instance ?? (_Instance = new FormFactory());
			}
		}
		#endregion
		
		public static void Register(FormInfo formInfo)
		{
			Instance.forms[formInfo.Id] = formInfo;
		}
		
		public FormInfo GetFormInfo(string id)
		{
			return forms[id];
		}
		
		public static T Create<T>(string id)
		{
			FormInfo fi = Instance.GetFormInfo(id);
			return (T) fi.CreateObject();
		}
	}
}
