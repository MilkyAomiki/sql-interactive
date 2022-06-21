# ALTER statement

The `ALTER TABLE` statement is used to add, delete, or modify columns in an existing table.

The `ALTER TABLE` statement is also used to add and drop various constraints on an existing table.

## Add column

To add a column in a table, use the following syntax:

```sql
ALTER TABLE table_name
ADD column_name datatype;
```

The following SQL adds an "Email" column to the "Customers" table:

```sql
ALTER TABLE Customers
ADD Email varchar(255);
```

## Drop column

To delete a column in a table, use the following syntax (notice that some database systems don't allow deleting a column):

```sql
ALTER TABLE table_name
DROP COLUMN column_name;
```

The following SQL deletes the `Email` column from the `Customers` table:

```sql
ALTER TABLE Customers
DROP COLUMN Email;
```

## Modify column

To change the data type of a column in a table, use the following syntax:

**SQL Server / MS Access:**

```sql
ALTER TABLE table_name
ALTER COLUMN column_name datatype;
```

**My SQL / Oracle (prior version 10G):**

```sql
ALTER TABLE table_name
MODIFY COLUMN column_name datatype;
```

**Oracle 10G and later:**

```sql
ALTER TABLE table_name
MODIFY column_name datatype;
```
