using SqlInteractive.BLL.Models;

namespace SqlInteractive.BLL.ServicesInternal;

public interface ISqlExecutor
{
	Task<ICollection<Table>> ExecuteAsync(string sql, Session session, CancellationToken cancellationToken = default);
}
