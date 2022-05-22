using SqlInteractive.BLL.Models;

namespace SqlInteractive.BLL.Services;

public interface ISqlService
{
	/// <summary>
	/// Выполнить данный запрос в базе данных
	/// </summary>
	/// <returns>
	/// Возвращенные базой данных множества значений.
	/// </returns>
	Task<ICollection<Table>> ExecuteAsync(string sql, Session session, CancellationToken cancellationToken = default);
}
