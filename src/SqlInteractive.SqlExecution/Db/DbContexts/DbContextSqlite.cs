using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using SqlInteractive.SqlExecution.Configuration;
using System.Data.Common;

namespace SqlInteractive.SqlExecution.Db.DbContexts;

public class DbContextSqlite : IDbContext
{
	private readonly DbOptions options;

	public DbContextSqlite(IOptions<DbOptions> options)
	{
		SQLitePCL.Batteries_V2.Init();
		this.options = options.Value;
	}

	public DbConnection GetDbConnection()
	{
		var connString = options.ParseConnectionString();
		connString["Data Source"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), (string)connString["Data Source"]);
		Console.WriteLine($"Connection string is {connString.ConnectionString}");

		return new SqliteConnection(connString.ConnectionString);
	}
}
