using Microsoft.Extensions.DependencyInjection;
using SqlInteractive.BLL.Models;
using SqlInteractive.BLL.SqlAnalysis;
using SqlInteractive.SqlExecution.Services;

namespace SqlInteractive.AppConfiguration;

internal class SqlAnalyzerSelector : ISqlAnalyzerSelector
{
    private readonly IServiceProvider services;

    public SqlAnalyzerSelector(IServiceProvider services)
    {
        this.services = services;
    }

    public ISqlAnalyzer GetAnalyzer(SqlDialect sqlDialect) => sqlDialect switch
    {
        SqlDialect.SQLite => services.GetRequiredService<SqliteAnalyzer>(),
        _ => throw new NotImplementedException($"Analyzer not implemented for dialect: {sqlDialect}")
    };
}
