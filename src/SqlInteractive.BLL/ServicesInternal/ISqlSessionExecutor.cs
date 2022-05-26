using SqlInteractive.BLL.Models;

namespace SqlInteractive.BLL.ServicesInternal;

public interface ISqlSessionExecutor
{
	Task<QueryExecutionResult> ExecuteAsync(string sql, Session session, CancellationToken cancellationToken = default);
}
