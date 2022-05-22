using System.Data;
using System.Data.Common;
using SqlInteractive.BLL.Models;

namespace SqlInteractive.SqlExecution.Dapper.Db;

public static class SqliteConnectionExtensions
{
	/// <summary>
	/// Выполнить SQL запрос в базе данных
	/// </summary>
	/// <returns>Коллекция множеств возвращенных базой данных</returns>
	public static async Task<ICollection<Table>> ExecuteAsync(this DbConnection connection, string sql, CancellationToken cancellationToken = default)
	{
		await connection.OpenIfClosedAsync(cancellationToken);

		using var command = connection.CreateCommand();
		command.CommandText = sql;

		using var reader = new TableDataReader(await command.ExecuteReaderAsync(cancellationToken));

		var tables = await reader.ReadTables(cancellationToken);

		return tables;
	}

	public static async Task CreateUserIfNotExists(this DbConnection connection, Session session, CancellationToken cancellationToken = default)
	{
		await connection.OpenIfClosedAsync(cancellationToken);

		if (await DoesDbExists(connection, $"d_{session.Id:N}", cancellationToken)) return;

		await connection.CreateDefaultUserDatabase(session, cancellationToken);

		using var command = connection.CreateCommand();
		command.CommandText = $@"
USE d_{session.Id:N};
CREATE LOGIN l_{session.Id:N} WITH PASSWORD = '{session.Id:N}';
CREATE USER u_{session.Id:N} FOR LOGIN l_{session.Id:N} WITH DEFAULT_SCHEMA = s_{session.Id:N};
GRANT CREATE TABLE TO u_{session.Id:N};
";
		await command.ExecuteNonQueryAsync(cancellationToken);
		await connection.CreateSchemaForUser(session, cancellationToken);
	}

	public static async Task CreateSchemaForUser(this DbConnection connection, Session session, CancellationToken cancellationToken = default)
	{
		await connection.OpenIfClosedAsync(cancellationToken);

		using var command = connection.CreateCommand();
		command.CommandText = $@"CREATE SCHEMA s_{session.Id:N} AUTHORIZATION u_{session.Id:N};";

		await command.ExecuteNonQueryAsync(cancellationToken);
	}

	public static Task OpenIfClosedAsync(this DbConnection connection, CancellationToken cancellationToken = default)
	{
		if (connection.State is ConnectionState.Closed)
			return connection.OpenAsync(cancellationToken);

		return Task.CompletedTask;
	}

	private static async Task<bool> DoesUserExists(this DbConnection connection, string userName, CancellationToken cancellationToken = default)
	{
		await connection.OpenIfClosedAsync(cancellationToken);

		using var command = connection.CreateCommand();
		command.CommandText = $"SELECT [name] FROM [sys].[database_principals] WHERE name = N'{userName}'";
		var resultSchemaId = await command.ExecuteScalarAsync(cancellationToken);

		return resultSchemaId is not null;
	}

	private static async Task CreateDefaultUserDatabase(this DbConnection connection, Session session, CancellationToken cancellationToken = default)
	{
		await connection.OpenIfClosedAsync(cancellationToken);

		using var command = connection.CreateCommand();
		command.CommandText = @$"
CREATE DATABASE d_{session.Id:N};
";

		await command.ExecuteNonQueryAsync(cancellationToken);
	}

	private static async Task<bool> DoesDbExists(this DbConnection connection, string dbName, CancellationToken cancellationToken = default)
	{
		await connection.OpenIfClosedAsync(cancellationToken);

		using var command = connection.CreateCommand();
		command.CommandText = $"SELECT [name] FROM [sys].[databases] WHERE name = N'{dbName}'";
		var resultDbName = await command.ExecuteScalarAsync(cancellationToken);

		return resultDbName is not null;
	}
}
