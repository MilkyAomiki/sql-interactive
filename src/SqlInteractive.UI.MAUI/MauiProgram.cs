using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using SqlInteractive.UI.Services;
using SqlInteractive.UI.MAUI.Services;
using SqlInteractive.AppConfiguration;

namespace SqlInteractive.UI.MAUI;

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

		builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		CommonConfiguration.AddServices(builder.Services);
		builder.Services.AddScoped<IStaticFilesService, StaticFilesService>();

		return builder.Build();
	}
}
