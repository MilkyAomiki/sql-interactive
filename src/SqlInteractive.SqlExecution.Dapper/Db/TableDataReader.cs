using System.Data.Common;
using SqlInteractive.BLL.Models;

namespace SqlInteractive.SqlExecution.Dapper.Db;

public class TableDataReader : IDisposable, IAsyncDisposable
{
	private readonly DbDataReader reader;

	public TableDataReader(DbDataReader reader)
	{
		this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
	}

	public async Task<ICollection<Table>> ReadTables(CancellationToken cancellationToken)
	{
		LinkedList<Table> tables = new();

		//go over result sets

		do
		{
			tables.AddLast(await ReadTable(cancellationToken));
		} while (await reader.NextResultAsync(cancellationToken));

		return tables;
	}

	private async Task<Table> ReadTable(CancellationToken cancellationToken)
	{
		var tableName = reader.GetSchemaTable().TableName;
		var columnNames = GetColumnNames();
		var columnTypes = GetColumnTypes();
		var rows = await GetRows(cancellationToken);

		return new Table(tableName, reader.FieldCount, columnNames, columnTypes, rows);
	}

	private async Task<LinkedList<ICollection<string>>> GetRows(CancellationToken cancellationToken)
	{
		LinkedList<ICollection<string>> rows = new();

		//go over rows in a result set
		while (await reader.ReadAsync(cancellationToken))
		{
			LinkedList<string> row = new();

			//go over columns in a row
			for (int i = 0; i < reader.FieldCount; i++)
			{
				row.AddLast(reader.GetValue(i).ToString());
			}

			rows.AddLast(row);
		}

		return rows;
	}

	private LinkedList<string> GetColumnTypes()
	{
		LinkedList<string> columnTypes = new();

		//go over columns and set their metadata
		for (int i = 0; i < reader.FieldCount; i++)
		{
			columnTypes.AddLast(reader.GetDataTypeName(i));
		}

		return columnTypes;
	}

	private ICollection<string> GetColumnNames()
	{
		LinkedList<string> columnNames = new();

		//go over columns and set their metadata
		for (int i = 0; i < reader.FieldCount; i++)
		{
			columnNames.AddLast(reader.GetName(i));
		}

		return columnNames;
	}

	public void Dispose() => ((IDisposable)reader).Dispose();

	public ValueTask DisposeAsync() => ((IAsyncDisposable)reader).DisposeAsync();
}
