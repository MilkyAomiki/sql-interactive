namespace SqlInteractive.BLL.Models;

public enum Statement
{
	/// <summary>
	/// SELECT
	/// </summary>
	Select = 1,

	/// <summary>
	/// CREATE TABLE
	/// </summary>
	CreateTable = 2,

	/// <summary>
	/// INSERT INTO
	/// </summary>
	InsertInto = 3,

	/// <summary>
	/// DROP TABLE
	/// </summary>
	DropTable = 4,

	/// <summary>
	/// ALTER TABLE
	/// </summary>
	AlterTable = 5,

	/// <summary>
	/// UPDATE
	/// </summary>
	UpdateTable = 6
}
