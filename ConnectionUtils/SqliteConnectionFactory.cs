using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

namespace ConnectionUtils
{
	public class SqliteConnectionFactory : ConnectionFactory
	{
		public override IDbConnection CreateConnection(IDictionary<string,string> props)
		{
			String connectionString = props["ConnectionString"];
			Console.WriteLine("SQLite --- Connecting to  ... {0}", connectionString);
			return new SqliteConnection(connectionString);
		}
	}
}
