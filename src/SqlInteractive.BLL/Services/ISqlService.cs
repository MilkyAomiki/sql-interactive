using SqlInteractive.BLL.Models;

namespace SqlInteractive.BLL.Services;

public interface ISqlService
{
	/// <summary>
	/// Выполнить данный запрос в базе данных для данной сессии
	/// </summary>
	/// <returns>
	/// Возвращенные базой данных множества значений.
	/// </returns>
	Task<QueryExecutionResult> ExecuteAsync(string sql, Session session, CancellationToken cancellationToken = default);

	/// <summary>
	/// Выполнить данный запрос в базе данных для данной сессии
	/// </summary>
	/// <returns>
	/// Возвращенные базой данных множества значений.
	/// </returns>
	Task<QueryExecutionResult> ExecuteAsync(string sql, CancellationToken cancellationToken = default);
}
