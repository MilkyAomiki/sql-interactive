using Microsoft.Extensions.Logging;
using SqlInteractive.BLL.Models;
using SqlInteractive.BLL.ServicesInternal;
using SqlInteractive.SqlExecution.Dapper.Db;

namespace SqlInteractive.SqlExecution.Dapper.Services;

public class SqlExecutor: ISqlExecutor
{
	private readonly DbContext dbContext;
	private readonly ILogger<SqlExecutor> logger;

	public SqlExecutor(DbContext dbContext, ILogger<SqlExecutor> logger)
	{
		this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		this.logger = logger;
	}

	public async Task<ICollection<Table>> ExecuteAsync(string sql, Session session, CancellationToken cancellationToken)
	{
		using var connection = dbContext.GetDbConnection();

		logger.LogInformation("Creating schema for the session {sessionId}", session.Id);
		await connection.CreateUserIfNotExists(session, cancellationToken);
		logger.LogInformation("Schema created");
		sql = sql.Trim();
		if (!sql.EndsWith(";"))
		{
			sql += ";";
		}

		sql = $" USE d_{session.Id:N}; EXECUTE AS USER = 'u_{session.Id:N}'; "
		+ sql
		+"REVERT;";

		logger.LogInformation("Executing the query...");
		var tables = await connection.ExecuteAsync(sql, cancellationToken);
		logger.LogInformation("Execution of the query is completed.");

		return tables;
	}
}
