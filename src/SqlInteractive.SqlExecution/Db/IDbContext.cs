using System.Data.Common;

namespace SqlInteractive.SqlExecution.Db;

public interface IDbContext
{
	public DbConnection GetDbConnection();
}
