LambdicSql_α
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

## Support for combination of the text.
You can insert text to expression.
And insert expression to text.

## 2 way sql.
Do you know 2 way sql?
It's executable sql text.
And change by condition and keyword.

## Summary Code.
Standard code.
```cs
//important
using Dapper;
using LambdicSql;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using static LambdicSql.Utils;

public void TestStandard()
{
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
        Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

    //to string and params.
    var info = query.ToSqlInfo(_connection.GetType());
    Debug.Print(info.SqlText);

    //dapper
    var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
}
```
```sql
SELECT
	tbl_staff.name AS "Name",
	tbl_remuneration.payment_date AS "PaymentDate",
	tbl_remuneration.money AS "Money"
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_staff.id) = (tbl_remuneration.staff_id)
WHERE ((@p_0) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_1))
```
And building sql is freedom.
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

    var where = Sql<DB>.Create(db => 
        Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

    var orderby = Sql<DB>.Create(db => 
         OrderBy(new Asc(db.tbl_staff.name)));

    var query = select.Concat(from).Concat(where).Concat(orderby);
}
```
```cs
public void TestSqlExpression()
{
    //make sql.
    var expMoneyAdd = Sql<DB>.Create(db => db.tbl_remuneration.money + 100);
    var expWhereMin = Sql<DB>.Create(db => 3000 < db.tbl_remuneration.money);
    var expWhereMax = Sql<DB>.Create(db => db.tbl_remuneration.money < 4000);

    var query = Sql< DB>.Create(db =>
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
```cs
public void TestSubQueryAtFrom()
{
    //make sql.
    var subQuery = Sql<DB>.Create(db => 
        Select(new
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
            Name = subQuery.Cast().name_sub
        }).
        From(subQuery.Cast()));
}
```
```cs
//You can use text.
public void TestFormatText()
{
    //make sql.
    var query = Sql<DB>.Create(db =>
        Select(new
        {
            name = db.tbl_staff.name,
            payment_date = db.tbl_remuneration.payment_date,
            money = Text<decimal>("{0} + 1000", db.tbl_remuneration.money),
        }).
        From(db.tbl_remuneration).
            Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
        Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

    //to string and params.
    var info = query.ToSqlInfo(_connection.GetType());
    Debug.Print(info.SqlText);

    //dapper
    var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
}
```
```cs
//2 way sql
public void TestFormat2WaySql()
{
    TestFormat2WaySql(true, true, 1000);
    TestFormat2WaySql(true, false, 2000);
    TestFormat2WaySql(false, true, 3000);
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
    
    var addMoney = Sql<DB>.Create(db => bonus);

    var where = Sql<DB>.Create(db =>
        Where(
            Condition(false, 3000 < db.tbl_remuneration.money) &&
            Condition(false, db.tbl_remuneration.money < 4000)));

    var query = TwoWaySql.Format(sql, addMoney, where);

    //to string and params.
    var info = query.ToSqlInfo(_connection.GetType());
    Debug.Print(info.SqlText);

    //dapper
    var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
}
```
## Samples
Look for how to use from the samples.<br>
https://github.com/Codeer-Software/LambdicSql/blob/master/Project/TestCheck35/Samples.cs