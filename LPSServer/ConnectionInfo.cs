using System;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using Npgsql;

namespace LPSServer
{
	
	public class ConnectionInfo : IDisposable
	{
		public long Id { get; set; }
		public string UserName { get; set; }
		public NpgsqlConnection Connection {get; set; }
		private string passwdhash;

		private ConnectionInfo()
		{
		}

		public static string GetSHA1String(string data, string salt)
		{
			SHA1 sha1 = new SHA1Managed();
			byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data + salt));
			return Convert.ToBase64String(hash);
		}
		
		public static ConnectionInfo Create(string login, string password)
		{
			NpgsqlConnection connection = new NpgsqlConnection();
			connection.ConnectionString = String.Format(
				"Server={0};Port={1};" +
				"Database={2};Userid={3};Password={4};" +
				"Protocol=3;Pooling=true;MinPoolSize=1;MaxPoolSize=20;ConnectionLifeTime=15;",
				"127.0.0.1", "5432",
				"filmarena_sklad", "filmarena", "filmArena3095");
			connection.Open();

			ConnectionInfo result = new ConnectionInfo();
			result.Connection = connection;
			result.UserName = login;
			result.passwdhash = GetSHA1String(password, login);
			object id = result.ExecuteScalar(
				"select id from users where username=:username and passwd=:passwd",
				new NpgsqlParameter("username", login),
				new NpgsqlParameter("passwd", result.passwdhash));
			if(id == null || id is DBNull)
				throw new BadPasswordException();
			result.Id = Convert.ToInt64(id);
			return result;
		}
		
		public bool Verify(string login, string password)
		{
			return (login == UserName) && 
				(passwdhash == GetSHA1String(password, login)) &&
				TestConnection();
		}
		
		public void Dispose()
		{
			if(Connection != null)
			{
				Connection.Close();
				//Connection.Dispose();
				Connection = null;
			}
		}

		#region SQL helpers
		
		public bool TestConnection()
		{
			return ("OK" == (string)ExecuteScalar("select 'OK';"));
		}
		
		public int ExecuteNonquery(string sql)
		{
			using(NpgsqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = sql;
				return cmd.ExecuteNonQuery();
			}
		}
		
		public int ExecuteNonquery(string sql, params NpgsqlParameter[] parameters)
		{
			using(NpgsqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = sql;
				foreach(NpgsqlParameter param in parameters)
					cmd.Parameters.Add(param);
				return cmd.ExecuteNonQuery();
			}
		}
		
		public object ExecuteScalar(string sql)
		{
			using(NpgsqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = sql;
				return cmd.ExecuteScalar();
			}
		}
		
		public object ExecuteScalar(string sql, params NpgsqlParameter[] parameters)
		{
			using(NpgsqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = sql;
				foreach(NpgsqlParameter param in parameters)
					cmd.Parameters.Add(param);
				return cmd.ExecuteScalar();
			}
		}
		
		public NpgsqlCommand CreateCommand(string sql)
		{
			NpgsqlCommand cmd = this.Connection.CreateCommand();
			cmd.CommandText = sql;
			return cmd;
		}
		
		public NpgsqlCommand CreateCommand(string sql, params NpgsqlParameter[] parameters)
		{
			NpgsqlCommand cmd = this.Connection.CreateCommand();
			cmd.CommandText = sql;
			foreach(NpgsqlParameter param in parameters)
				cmd.Parameters.Add(param);
			return cmd;
		}
		
		#endregion
	}
}
