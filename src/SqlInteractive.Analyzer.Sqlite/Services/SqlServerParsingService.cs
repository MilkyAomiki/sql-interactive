using SqlInteractive.BLL.Models;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Management.SqlParser.Parser;
using Microsoft.SqlServer.Management.SqlParser.Common;
using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;
using SqlInteractive.BLL.SqlAnalysis.Modules;

namespace SqlInteractive.SqlExecution;

public class SqlServerParsingService
{
	private readonly ILogger<SqlServerParsingService> logger;

	public SqlServerParsingService(ILogger<SqlServerParsingService> logger)
	{
		this.logger = logger;
	}

	public IEnumerable<Statement> GetStatements(string sql)
	{
		logger.LogInformation($"Parsing sql: {sql}");

		var ast = Parser.Parse(sql, new ParseOptions() { TransactSqlVersion = TransactSqlVersion.Version160 }).Script;

		var visitor = new DocsVisitor(logger);

		ast.Accept(visitor);
		logger.LogInformation($"Found statements: {visitor.Statements.Aggregate(string.Empty, (res, next) => string.Join(", ", res, next.ToString()))}");
		return visitor.Statements;
	}
}

internal class DocsVisitor : SqlCodeObjectRecursiveVisitor
{
	private readonly ILogger logger;

	public ISet<Statement> Statements { get; set; } = new HashSet<Statement>();

	public DocsVisitor(ILogger logger)
	{
		this.logger = logger;
	}

	public override void Visit(SqlSelectStatement statement)
	{
		Statements.Add(Statement.Select);

		base.Visit(statement);
	}

	public override void Visit(SqlCreateTableStatement statement)
	{
		Statements.Add(Statement.CreateTable);

		base.Visit(statement);
	}

	public override void Visit(SqlInsertStatement statement)
	{
		Statements.Add(Statement.InsertInto);

		base.Visit(statement);
	}

	public override void Visit(SqlDropTableStatement statement)
	{
		Statements.Add(Statement.DropTable);

		base.Visit(statement);
	}

	public override void Visit(SqlUpdateStatement codeObject)
	{
		Statements.Add(Statement.UpdateTable);

		base.Visit(codeObject);
	}
}
