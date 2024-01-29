using System.Data;
using System.Data.SqlClient;

namespace Connection.Database.Dapper
{
	public class DapperConnection : IDisposable
	{
		private static SqlConnection ConnectionString(string dbName)
		{
			SqlConnection conn = new SqlConnection
			{
				// Local
				ConnectionString = @"Data Source=SAYED-MIS\MSSQLSERVER2014;Initial Catalog=" + dbName + "; PersistSecurityInfo = false; Integrated Security = false; Pooling = true; User id=sa;Password=Admin1234;"
				// ConnectionString = @"Data Source=AHNAF;Initial Catalog=ImageService; PersistSecurityInfo = false; Integrated Security = false; Pooling = true; User id=sa;Password=Admin1234;"
			};

			return conn;
		}

		protected static IDbConnection LiveConnection(string dbName)
		{
			var connection = OpenConnection(ConnectionString(dbName));
			connection.Open();
			return connection;
		}

		private static IDbConnection OpenConnection(SqlConnection conn)
		{
			return new SqlConnection(conn.ConnectionString);
		}

		protected static bool CloseConnection(IDbConnection connection)
		{
			if (connection.State != ConnectionState.Closed)
			{
				connection.Close();
				// connection.Dispose();
			}
			return true;
		}
		private static void ClearPool()
		{
			SqlConnection.ClearAllPools();
		}

		public void Dispose()
		{
			ClearPool();
		}
	}
}
