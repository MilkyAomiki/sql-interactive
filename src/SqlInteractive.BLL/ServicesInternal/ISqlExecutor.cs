using SqlInteractive.BLL.Models;

namespace SqlInteractive.BLL.ServicesInternal;

public interface ISqlExecutor
{
	Task<QueryExecutionResult> ExecuteAsync(string sql, CancellationToken cancellationToken = default);
}
