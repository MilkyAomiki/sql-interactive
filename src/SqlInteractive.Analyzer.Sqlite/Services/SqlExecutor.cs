using Microsoft.Extensions.Logging;
using SqlInteractive.Analyzer.Sqlite.Db;
using SqlInteractive.BLL.Models;
using SqlInteractive.BLL.SqlAnalysis.Modules;
using SqlInteractive.SqlExecution.Db;

namespace SqlInteractive.SqlExecution.Services;

public class SqlExecutor : ISqlExecutor
{
	private readonly IDbContext dbContext;
	private readonly ILogger<SqlExecutor> logger;

	public SqlExecutor(IDbContext dbContext, ILogger<SqlExecutor> logger)
	{
		this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		this.logger = logger;
	}

	public async Task<QueryExecutionResult> ExecuteAsync(string sql, CancellationToken cancellationToken)
	{
		using var connection = dbContext.GetDbConnection();

		logger.LogInformation("Executing the query...");
		var tables = await connection.ExecuteAsync(sql, cancellationToken);
		logger.LogInformation("Execution of the query is completed.");

		return tables;
	}
}
