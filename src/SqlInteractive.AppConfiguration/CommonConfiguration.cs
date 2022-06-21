using Microsoft.Extensions.DependencyInjection;
using SqlInteractive.BLL.Services;
using SqlInteractive.BLL.ServicesImpls;
using SqlInteractive.BLL.SqlAnalysis.Modules;
using SqlInteractive.Analyzer.Sqlite.Db;
using SqlInteractive.SqlExecution.Services;
using SqlInteractive.SqlExecution.Db.DbContexts;
using SqlInteractive.SqlExecution.Configuration;
using SqlInteractive.BLL.SqlAnalysis;

namespace SqlInteractive.AppConfiguration;

public static class CommonConfiguration
{
	public static void AddServices(IServiceCollection services)
    {
		services.AddSingleton<ISqlExecutor, SqlExecutor>();
		services.AddSingleton<ISqlSessionExecutor, SqlExecutorWithUsers>();
		services.AddSingleton<IDbContext, DbContextSqlite>();
		services.AddSingleton<ISqlService, SqlService>();
		services.AddSingleton<ISqlAnalyzerSelector, SqlAnalyzerSelector>();
		services.AddSingleton<SqliteAnalyzer>();

		services.AddOptions<DbOptions>().BindConfiguration("Db");
	}
}
