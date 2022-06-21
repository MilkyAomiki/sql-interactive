using SqlInteractive.BLL.Models;

namespace SqlInteractive.BLL.SqlAnalysis;

public interface ISqlAnalyzerSelector
{
    ISqlAnalyzer GetAnalyzer(SqlDialect sqlDialect);
}
