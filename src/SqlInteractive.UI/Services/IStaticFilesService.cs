namespace SqlInteractive.UI.Services;

public interface IStaticFilesService
{
	Task<string> GetTextFromFile(string filepath);
}
