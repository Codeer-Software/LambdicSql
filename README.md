LambdicSql_α
======================

## Features ...
#### Write SQL using lambda.
## Getting Started
LambdicSql from NuGet

    PM> Install-Package LambdicSql
https://www.nuget.org/packages/LambdicSql/
## Supported Keywords.
- SELECT
- FROM
	- JOIN
	- LEFT JOIN
	- RIGHT JOIN
	- CROSS JOIN
- WHERE
- ORDER BY
	- ASC
	- DESC
- GROUP BY
- HAVING
- UPDATE
	- SET
- DELETE
- INSERT INTO
	- VALUES
- DISTINCT
- ALL
- LIKE
- BETWEEN
- IN
- CASE
	- WHEN
	- THEN
	- ELSE
	- END

## Supported Functions
- SUM
- AVG
- COUNT
- MIN
- MAX

## Supported Functions
- AVG
- LAG
	- OVER
	- PARTITION BY
	- ORDER BY
	- ROWS
		- BETWEEN
		- PRECEDING
		- AND
		- FOLLOWING
		
Look for how to use from the samples.
https://github.com/Codeer-Software/LambdicSql/blob/master/Project/TestCheck35/Samples.cs

## Supported DataType
- string
- bool
- bool?
- byte
- byte?
- short
- short?
- int
- int?
- long
- long?
- float
- float?
- double
- double?
- decimal
- decimal?
- DateTime
- DateTime?

## Supported database
|DataBase type|Support|
|:--|:--|
|SQL Server|○|
|SQLite|○|
|PostgreSQL|○|
|Oracle|Comming soon|
|MySQL|Comming soon|
|DB2|Comming soon|

## Building query.
It can be combined parts.
Query, Sub query, Expression.
You can write DRY code.

## Samples
Look for how to use from the samples.
https://github.com/Codeer-Software/LambdicSql/blob/master/Project/TestCheck35/Samples.cs