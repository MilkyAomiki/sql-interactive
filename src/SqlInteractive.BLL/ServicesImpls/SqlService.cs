using SqlInteractive.BLL.Models;
using SqlInteractive.BLL.Services;
using SqlInteractive.BLL.ServicesInternal;

namespace SqlInteractive.BLL.ServicesImpls
{
	public class SqlService : ISqlService
	{
		private readonly ISqlExecutor sqlExecutor;

		public SqlService(ISqlExecutor sqlExecutor)
		{
			this.sqlExecutor = sqlExecutor;
		}

		public Task<ICollection<Table>> ExecuteAsync(string sql, Session session, CancellationToken cancellationToken)
		{
			return sqlExecutor.ExecuteAsync(sql, session, cancellationToken);
		}
	}
}
