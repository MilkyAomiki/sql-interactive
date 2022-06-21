using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.FileProviders;
using SqlInteractive.UI;
using SqlInteractive.UI.Services;
using SqlInteractive.UI.WASM.Services;
using SqlInteractive.AppConfiguration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var assembly = typeof(Program).GetTypeInfo().Assembly;
builder.Configuration.AddJsonFile(new EmbeddedFileProvider(assembly), "appsettings.json", optional: false, false);

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IStaticFilesService, StaticFilesService>();
CommonConfiguration.AddServices(builder.Services);

var app = builder.Build();

await app.RunAsync();