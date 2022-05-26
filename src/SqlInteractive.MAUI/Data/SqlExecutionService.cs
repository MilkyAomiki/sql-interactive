using SqlInteractive.BLL.Models;
using SqlInteractive.BLL.Services;

namespace SqlInteractive.MAUI.Data;

public class SqlExecutionService
{
	private readonly ISqlService sqlService;

	public SqlExecutionService(ISqlService sqlService)
	{
		this.sqlService = sqlService;
	}

	public async Task<QueryExecutionResult> ExecuteSql(string sql)
	{
		var executionResult = await sqlService.ExecuteAsync(sql);

		return executionResult;
	}
}
