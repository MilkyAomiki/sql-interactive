using SqlInteractive.BLL.Models;
using SqlInteractive.BLL.Services;
using SqlInteractive.BLL.ServicesInternal;

namespace SqlInteractive.BLL.ServicesImpls
{
	public class SqlService : ISqlService
	{
		private readonly ISqlExecutor sqlExecutor;
		private readonly ISqlSessionExecutor sqlSessionExecutor;

		public SqlService(ISqlExecutor sqlExecutor, ISqlSessionExecutor sqlSessionExecutor)
		{
			this.sqlExecutor = sqlExecutor;
			this.sqlSessionExecutor = sqlSessionExecutor;
		}

		public Task<QueryExecutionResult> ExecuteAsync(string sql, Session session, CancellationToken cancellationToken)
		{
			return sqlSessionExecutor.ExecuteAsync(sql, session, cancellationToken);
		}

		public Task<QueryExecutionResult> ExecuteAsync(string sql, CancellationToken cancellationToken = default)
		{
			return sqlExecutor.ExecuteAsync(sql, cancellationToken);
		}
	}
}
