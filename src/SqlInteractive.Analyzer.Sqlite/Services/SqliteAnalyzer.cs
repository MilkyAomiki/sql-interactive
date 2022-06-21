using Microsoft.Extensions.Logging;
using SqlInteractive.Analyzer.Sqlite.Db;
using SqlInteractive.BLL.Models;
using SqlInteractive.BLL.SqlAnalysis;
using SqlInteractive.SqlExecution.Db;

namespace SqlInteractive.SqlExecution.Services;

public class SqliteAnalyzer : SqlServerParsingService, ISqlAnalyzer
{
	private readonly IDbContext dbContext;
	private readonly ILogger<SqliteAnalyzer> logger;

	public SqliteAnalyzer(IDbContext dbContext, ILogger<SqliteAnalyzer> logger, ILogger<SqlServerParsingService> logger2) : base(logger2)
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
