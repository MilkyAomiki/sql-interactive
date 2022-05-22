namespace SqlInteractive.BLL.Models;

public record Table(
	string Name,
	int ColumnCount,
	ICollection<string> ColumnNames,
	ICollection<string> ColumnTypes,
	ICollection<ICollection<string>> Rows);
