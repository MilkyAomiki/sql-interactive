using SqlInteractive.UI.Services;

namespace SqlInteractive.UI.MAUI.Services;

internal class StaticFilesService : IStaticFilesService
{
	public async Task<string> GetTextFromFile(string path)
	{
		var resultStream = await FileSystem.OpenAppPackageFileAsync(path);

		return (new StreamReader(resultStream)).ReadToEnd();
	}
}
