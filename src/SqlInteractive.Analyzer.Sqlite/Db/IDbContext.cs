using System.Data.Common;

namespace SqlInteractive.Analyzer.Sqlite.Db;

public interface IDbContext
{
	public DbConnection GetDbConnection();
}
