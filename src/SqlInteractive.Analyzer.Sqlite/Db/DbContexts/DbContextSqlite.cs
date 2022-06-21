using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlInteractive.Analyzer.Sqlite.Db;
using SqlInteractive.SqlExecution.Configuration;
using System.Data.Common;

namespace SqlInteractive.SqlExecution.Db.DbContexts;

public class DbContextSqlite : IDbContext
{
	private readonly DbOptions options;
    private readonly ILogger<DbOptions> logger;

    public DbContextSqlite(IOptions<DbOptions> options, ILogger<DbOptions> logger)
	{
		SQLitePCL.Batteries_V2.Init();
		this.options = options.Value;
        this.logger = logger;
    }

	public DbConnection GetDbConnection()
	{
		var connString = options.ParseConnectionString();
		connString["Data Source"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), (string)connString["Data Source"]);
		
		logger.LogDebug($"Connection string is {connString.ConnectionString}");

		return new SqliteConnection(connString.ConnectionString);
	}
}
