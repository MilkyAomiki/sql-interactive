using SqlInteractive.BLL.Models;

namespace SqlInteractive.BLL.Services;

public interface ISqlExecutionService
{

	/// <summary>
	/// Выполнить данный запрос в базе данных для данной сессии
	/// </summary>
	/// <returns>
	/// Возвращенные базой данных множества значений.
	/// </returns>
	Task<QueryExecutionResult> ExecuteAsync(string sql, SqlDialect sqlDialect, CancellationToken cancellationToken = default);
}
