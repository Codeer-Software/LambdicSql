LambdicSql_β 0.15.0
======================

## Features ...
#### LambdicSql's purpose is to generate sql text and parameters from lambda.
## Getting Started
LambdicSql from NuGet

    PM> Install-Package LambdicSql

## Featuring Dapper
Generate sql text and parameters by LambdicSql.<br>
And execut and map to object, recommend using dapper.

    PM> Install-Package Dapper

https://www.nuget.org/packages/LambdicSql/

## Summary Code.
Standard code.
```cs
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;

//tables.
public class Staff
{
    public int id { get; set; }
    public string name { get; set; }
}

public class Remuneration
{
    public int id { get; set; }
    public int staff_id { get; set; }
    public DateTime payment_date { get; set; }
    public decimal money { get; set; }
}

public class DB
{
    public Staff tbl_staff { get; set; }
    public Remuneration tbl_remuneration { get; set; }
}

public void TestStandard()
{
	var min = 3000;
	
    //make sql.
    var query = Sql<DB>.Create(db =>
        Select(new SelectData()
        {
            Name = db.tbl_staff.name,
            PaymentDate = db.tbl_remuneration.payment_date,
            Money = db.tbl_remuneration.money,
        }).
        From(db.tbl_remuneration).
            Join(db.tbl_staff, db.tbl_staff.id == db.tbl_remuneration.staff_id).
        Where(min < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

    //to string and params.
    var info = query.ToSqlInfo(_connection.GetType());
    Debug.Print(info.SqlText);

    //if you installed dapper, use this extension.
    var datas = _connection.Query(query).ToList();
}
```
```sql
SELECT
	tbl_staff.name AS Name,
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_staff.id) = (tbl_remuneration.staff_id)
WHERE ((@min) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_1))
```

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
	- ROLLUP
	- WITH ROLLUP
	- CUBE
	- GROUPING SETS
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
- EXISTS
- CASE
	- WHEN
	- THEN
	- ELSE
	- END
- UNION
- INTERSECT
- EXCEPT
- MINUS
- LIMIT
- OFFSET
- ROWNUM
- OFFSET ROWS
- FETCH NEXT ROWS ONLY
- TOP
- ROWNUM

## Supported Functions
- SUM
- AVG
- COUNT
- MIN
- MAX
- CONCAT
- LENGTH
- LEN
- LOWER
- UPPER
- REPLACE
- SUBSTRING
- CURRENT_DATE
- CURRENT DATE
- CURRENT_TIME
- CURRENT TIME
- CURRENT_TIMESTAMP
- CURRENT TIMESTAMP
- CAST
- COALESCE
- ISNULL
- NVL

## Supported Window Functions
- AVG
- SUN
- COUNT
- MAX
- MIN
- RANK
- DENSE_RANK
- PERCENT_RANK
- CAM_DIST
- NTIL
- FIRST_VALUE
- LAST_VALUE
- LAG
	- OVER
	- PARTITION BY
	- ORDER BY
	- ROWS
		- BETWEEN
		- PRECEDING
		- AND
		- FOLLOWING
		
Look for how to use from the samples.<br>
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
- DateTimeOffset
- DateTimeOffset?
- TimeSpan
- TimeSpan?

## Supported database
|DataBase type|Support|
|:--|:--|
|SQL Server|○|
|SQLite|○|
|PostgreSQL|○|
|Oracle|○|
|MySQL|○|
|DB2|○|

## Building query.
It can be combined parts.
Query, Sub query, Expression.
You can write DRY code.
```cs
public void TestQueryConcat()
{
    //make sql.
    var select = Sql<DB>.Create(db => 
        Select(new SelectData1()
        {
            Name = db.tbl_staff.name,
            PaymentDate = db.tbl_remuneration.payment_date,
            Money = db.tbl_remuneration.money,
        }));

    var from = Sql<DB>.Create(db => 
         From(db.tbl_remuneration).
        Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

	var min = 3000;
    var where = Sql<DB>.Create(db => 
        Where(min < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

    var orderby = Sql<DB>.Create(db => 
         OrderBy(new Asc(db.tbl_staff.name)));

    var query = select.Concat(from).Concat(where).Concat(orderby);
}
```
```sql
SELECT
	tbl_staff.name AS Name,
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
WHERE ((@min) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_1))
ORDER BY
	tbl_staff.name ASC
```
Build expressions.
```cs
public void TestSqlExpression()
{
    //make sql.
    var expMoneyAdd = Sql<DB>.Create(db => db.tbl_remuneration.money + 100);
    var expWhereMin = Sql<DB>.Create(db => 3000 < db.tbl_remuneration.money);
    var expWhereMax = Sql<DB>.Create(db => db.tbl_remuneration.money < 4000);

    var query = Sql<DB>.Create(db =>
        Select(new SelectData1()
        {
            Name = db.tbl_staff.name,
            PaymentDate = db.tbl_remuneration.payment_date,
            Money = expMoneyAdd,
        }).
        From(db.tbl_remuneration).
            Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
        Where(expWhereMin && expWhereMax).
        OrderBy(new Asc(db.tbl_staff.name)));
}
```
```sql
SELECT
	tbl_staff.name AS Name,
	tbl_remuneration.payment_date AS PaymentDate,
	(tbl_remuneration.money) + (@p_0) AS Money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
WHERE ((@p_1) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_2))
ORDER BY
	tbl_staff.name ASC
```
Sub query.
```cs
public void TestSubQueryAtFrom()
{
    //make sql.
    var subQuery = Sql<DB>.Create(db => 
        Select(new SelectedData
        {
            name_sub = db.tbl_staff.name,
            PaymentDate = db.tbl_remuneration.payment_date,
            Money = db.tbl_remuneration.money,
        }).
        From(db.tbl_remuneration).
            Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
        Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));
    
    var query = Sql<DB>.Create(db => 
        Select(new SelectData6
        {
            Name = subQuery.Body.name_sub
        }).
        From(subQuery));
}
```
```sql
SELECT
	subQuery.name_sub AS Name
FROM 
	(SELECT
		tbl_staff.name AS name_sub,
		tbl_remuneration.payment_date AS PaymentDate,
		tbl_remuneration.money AS Money
	FROM tbl_remuneration
		JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
	WHERE ((@p_2) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_3))) AS subQuery
```
## Support for combination of the text.
You can insert text to expression.
And insert expression to text.
```cs
//You can use text.
public void TestFormatText()
{
    //make sql.
    var query = Sql<DB>.Create(db =>
        Select(new SelectedData
        {
            name = db.tbl_staff.name,
            payment_date = db.tbl_remuneration.payment_date,
            money = (decimal)"{0} + 1000".ToSql(db.tbl_remuneration.money),
        }).
        From(db.tbl_remuneration).
            Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
        Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

    //to string and params.
    var info = query.ToSqlInfo(_connection.GetType());
    Debug.Print(info.SqlText);

    //if you installed dapper, use this extension.
    var datas = _connection.Query(query).ToList();
}
```
```sql
SELECT
	tbl_staff.name AS name,
	tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + 1000 AS money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
WHERE ((@p_0) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_1))
```
## 2 way sql.
Do you know 2 way sql?
It's executable sql text.
And change by condition and keyword.
```cs
//2 way sql
public void TestFormat2WaySql()
{
    TestFormat2WaySql(true, true, 1000);
    TestFormat2WaySql(false, false, 4000);
}
public void TestFormat2WaySql(bool minCondition, bool maxCondition, decimal bonus)
{
    //make sql.
    //replace /*no*/---/**/.
    var sql = @"
SELECT
tbl_staff.name AS name,
tbl_remuneration.payment_date AS payment_date,
tbl_remuneration.money + /*0*/1000/**/ AS money
FROM tbl_remuneration 
JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*1*/WHERE tbl_remuneration.money = 100/**/";
    
    var query = Sql<DB>.Create(db => sql.TwoWaySql(
        bonus,
        Where(
            Condition(minCondition, 3000 < db.tbl_remuneration.money) &&
            Condition(maxCondition, db.tbl_remuneration.money < 4000))
        ));
    var info = query.ToSqlInfo(_connection.GetType());
    Debug.Print(info.SqlText);

    //if you installed dapper, use this extension.
    var datas = _connection.Query<SelectedData>(query).ToList();
}
```
min max enable.
```sql
SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + @bonus AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
WHERE ((@p_0) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_1))
```
min, max disable, vanish where clause.
```sql
SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + @bonus AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
```

## Samples
Look for how to use from the samples.<br>
https://github.com/Codeer-Software/LambdicSql/blob/master/Project/TestCheck35/Samples.cs
