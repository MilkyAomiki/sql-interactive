using Microsoft.AspNetCore.Mvc;
using SqlInteractive.BLL.Models;
using SqlInteractive.BLL.ServicesInternal;

namespace SqlInteractive.WebAPI.Controllers;

[ApiController]
[Route("sql-execution")]
public class SqlExecutionController : ApiController
{
	private readonly ILogger<AppInfoController> logger;
	private readonly ISqlSessionExecutor sqlExecutor;

	public SqlExecutionController(ILogger<AppInfoController> logger, ISqlSessionExecutor sqlExecutor)
	{
		this.logger = logger;
		this.sqlExecutor = sqlExecutor;
	}

	[HttpPost("execute")]
	public async Task<IActionResult> ExecuteSql([FromBody]string sql, CancellationToken cancellationToken)
	{
		var session = GetSession();
		logger.LogInformation("Session id: {sessionId}", session.Id);
		ICollection<Table> result = await sqlExecutor.ExecuteAsync(sql, session, cancellationToken);
		return Ok(result);
	}
}
