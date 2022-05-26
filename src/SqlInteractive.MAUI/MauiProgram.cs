using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using SqlInteractive.BLL.Services;
using SqlInteractive.BLL.ServicesImpls;
using SqlInteractive.BLL.ServicesInternal;
using SqlInteractive.MAUI.Data;
using SqlInteractive.SqlExecution.Configuration;
using SqlInteractive.SqlExecution.Db;
using SqlInteractive.SqlExecution.Db.DbContexts;
using SqlInteractive.SqlExecution.Services;
using System.Reflection;

namespace SqlInteractive.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		var assembly = typeof(App).GetTypeInfo().Assembly;
		builder.Configuration.AddJsonFile(new EmbeddedFileProvider(assembly), "appsettings.json", optional: false, false);
		using var stream = typeof(App).Assembly.GetManifestResourceStream("appsettings.json");

		builder.Services.AddOptions<DbOptions>().BindConfiguration("Db");

		builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		builder.Services.AddScoped<ISqlExecutor, SqlExecutor>();
		builder.Services.AddScoped<ISqlSessionExecutor, SqlExecutorWithUsers>();
		builder.Services.AddScoped<IDbContext, DbContextSqlite>();
		builder.Services.AddScoped<ISqlService, SqlService>();
		builder.Services.AddSingleton<SqlExecutionService>();

		return builder.Build();
	}
}
