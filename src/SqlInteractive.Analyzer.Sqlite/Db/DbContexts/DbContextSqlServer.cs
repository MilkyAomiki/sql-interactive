using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SqlInteractive.Analyzer.Sqlite.Db;
using SqlInteractive.SqlExecution.Configuration;

namespace SqlInteractive.SqlExecution.Db.DbContexts;

public class DbContextSqlServer : IDbContext
{
	private readonly DbOptions options;

	public DbContextSqlServer(IOptions<DbOptions> options)
	{
		this.options = options.Value;
	}

	public DbConnection GetDbConnection() => new SqlConnection(options.ConnectionString);
}
