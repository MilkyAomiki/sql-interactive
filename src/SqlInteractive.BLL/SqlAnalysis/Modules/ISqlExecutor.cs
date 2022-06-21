using SqlInteractive.BLL.Models;

namespace SqlInteractive.BLL.SqlAnalysis.Modules;

public interface ISqlExecutor
{
    Task<QueryExecutionResult> ExecuteAsync(string sql, CancellationToken cancellationToken = default);
}
