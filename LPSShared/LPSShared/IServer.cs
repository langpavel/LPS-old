using System;
using System.Data;
using System.Collections.Generic;

namespace LPSServer
{
	public interface IServer
	{
		string Ping(string data);
		long Login(string login, string password);
		string GetLoggedUser();
		bool ChangePassword(string old_password, string new_password);
		void Logout();
		int ExecuteNonquerySimple(string sql);
		int ExecuteNonquery(string sql, params object[] parameters);
		object ExecuteScalarSimple(string sql);
		object ExecuteScalar(string sql, params object[] parameters);
		Int64 NextSeqValue(string generator);
		DataSet GetDataSetSimple(string sql);
		DataSet GetDataSet(string sql, object[] parameters);
		string GetTextResource(string path);
		int SaveDataSet(DataSet changes, bool updateUserInfo, string selectSql, object[] parameters);
		void DisposeDataSet(int server_id);
	}
}
