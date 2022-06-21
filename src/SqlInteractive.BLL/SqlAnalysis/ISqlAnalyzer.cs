using SqlInteractive.BLL.Models;
using SqlInteractive.BLL.SqlAnalysis.Modules;

namespace SqlInteractive.BLL.SqlAnalysis;

/// <summary>
/// Основной интерфейс подсистем анализирования и выполнения запросов
/// </summary>
public interface ISqlAnalyzer : ISqlExecutor
{
	public IEnumerable<Statement> GetStatements(string sql);
}
