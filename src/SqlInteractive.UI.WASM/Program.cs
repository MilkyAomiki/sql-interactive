using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.FileProviders;
using SqlInteractive.BLL.Services;
using SqlInteractive.BLL.ServicesImpls;
using SqlInteractive.BLL.ServicesInternal;
using SqlInteractive.UI.MAUI.Data;
using SqlInteractive.UI.WASM;
using SqlInteractive.UI;
using SqlInteractive.SqlExecution.Configuration;
using SqlInteractive.SqlExecution.Db;
using SqlInteractive.SqlExecution.Db.DbContexts;
using SqlInteractive.SqlExecution.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var assembly = typeof(Program).GetTypeInfo().Assembly;
builder.Configuration.AddJsonFile(new EmbeddedFileProvider(assembly), "appsettings.json", optional: false, false);

builder.Services.AddOptions<DbOptions>().BindConfiguration("Db");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<ISqlExecutor, SqlExecutor>();
builder.Services.AddSingleton<ISqlSessionExecutor, SqlExecutorWithUsers>();
builder.Services.AddSingleton<IDbContext, DbContextSqlite>();
builder.Services.AddSingleton<ISqlService, SqlService>();
builder.Services.AddSingleton<SqlExecutionService>();

await builder.Build().RunAsync();
