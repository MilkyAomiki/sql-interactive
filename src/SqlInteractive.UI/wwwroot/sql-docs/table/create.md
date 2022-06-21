# DELETE statement

The `CREATE TABLE` statement is used to create a new table in a database.

```sql
CREATE TABLE table_name (
    column1 datatype,
    column2 datatype,
    column3 datatype,
   ....
);
```

The `column` parameters specify the names of the columns of the table.

The `datatype` parameter specifies the type of data the column can hold (e.g. `varchar`, `integer`, `date`, etc.).

The following example creates a table called "Persons" that contains five columns: `PersonID`, `LastName`, `FirstName`, `Address`, and `City`:

```sql
CREATE TABLE Persons (
    PersonID int,
    LastName varchar(255),
    FirstName varchar(255),
    Address varchar(255),
    City varchar(255)
);
```

The `PersonID` column is of type int and will hold an integer.

The `LastName`, `FirstName`, `Address`, and `City` columns are of type `varchar` and will hold characters, and the maximum length for these fields is 255 characters.

The empty `Persons` table can now be filled with data with the `SQL INSERT INTO` statement.
