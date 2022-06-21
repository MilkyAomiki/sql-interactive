using SqlInteractive.UI.Services;

namespace SqlInteractive.UI.WASM.Services;

public class StaticFilesService : IStaticFilesService
{
	private readonly HttpClient client;

	public StaticFilesService(HttpClient client)
	{
		this.client = client;
	}

	public string GetPathTo(string file) => $"_content/SqlInteractive.UI/{file}";

	public Task<string> GetTextFromFile(string path)
	{
		return client.GetStringAsync(GetPathTo(path));
	}
}
