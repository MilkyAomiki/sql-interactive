using SqlInteractive.BLL.Models;

namespace SqlInteractive.BLL.Services;

public interface ISqlService : ISqlExecutionService
{
	IEnumerable<Statement> GetStatements(string sql, SqlDialect sqlDialect);
}
