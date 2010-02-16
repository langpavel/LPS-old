using System;

namespace LPS
{
	public interface IConfiguration
	{
		void Load(string data);
		//void Merge(IConfiguration conf);
		string Save();
	}
}
