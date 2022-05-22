using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SqlInteractive.SqlExecution.Dapper.Configuration;

namespace SqlInteractive.SqlExecution.Dapper.Db;

public class DbContext
{
	private readonly DbOptions options;

	public DbContext(IOptions<DbOptions> options)
	{
		this.options = options.Value;
	}

	public DbConnection GetDbConnection() => new SqlConnection(options.ConnectionString);
}
