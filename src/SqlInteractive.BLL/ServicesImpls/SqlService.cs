using SqlInteractive.BLL.Models;
using SqlInteractive.BLL.Services;
using SqlInteractive.BLL.SqlAnalysis;
using SqlInteractive.BLL.SqlAnalysis.Modules;

namespace SqlInteractive.BLL.ServicesImpls;

/// <summary>
///	Медиатор подсистемы работы с SQL
/// </summary>
public class SqlService : ISqlService
{
    private readonly ISqlAnalyzerSelector analyzerSelector;

    public SqlService(
		ISqlAnalyzerSelector analyzerSelector)
	{
        this.analyzerSelector = analyzerSelector;
    }

	public Task<QueryExecutionResult> ExecuteAsync(string sql, SqlDialect sqlDialect, CancellationToken cancellationToken = default)
	{
		return analyzerSelector.GetAnalyzer(sqlDialect).ExecuteAsync(sql, cancellationToken);
	}

	public IEnumerable<Statement> GetStatements(string sql, SqlDialect sqlDialect)
	{
		return analyzerSelector.GetAnalyzer(sqlDialect).GetStatements(sql);
	}
}
