using System;
using System.Data;
using System.Collections.Generic;
using Npgsql;

namespace LPSServer
{
	public interface IServer
	{
		bool Ping();
		bool Login(string login, string password);
		string GetLoggedUser();
		bool ChangePassword(string old_password, string new_password);
		void Logout();
		int ExecuteNonquerySimple(string sql);
		int ExecuteNonquery(string sql, params object[] parameters);
		object ExecuteScalarSimple(string sql);
		object ExecuteScalar(string sql, params object[] parameters);
		Int64 NextSeqValue(string generator);
		DataSet GetDataSetSimple(string sql);
		DataSet GetDataSet(string sql, bool for_edit, object[] parameters, out int server_id);
		string GetTextResource(string path);
		int SaveDataSet(DataSet changes, int server_id);
		void DisposeDataSet(int server_id);
	}
}
