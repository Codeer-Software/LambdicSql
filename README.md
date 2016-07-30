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
	
Look for how to use from the samples.

## Supported Functions
- SUM
- AVG
- COUNT
- MIN
- MAX

And you can add function very easy.

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

## Simple sample
write only lambda.
```cs
[TestMethod]
public void LambdaOnly()
{
    //log for debug.
    Sql.Log = l => Debug.Print(l);

    //make sql.
    var query = Sql.Query(() => new
    {
        tbl_staff = new
        {
            id = 0,
            name = ""
        },
        tbl_remuneration = new
        {
            id = 0,
            staff_id = 0,
            payment_date = default(DateTime),
            money = default(decimal)
        }
    }).
    Select(db => new
    {
        name = db.tbl_staff.name,
        payment_date = db.tbl_remuneration.payment_date,
        money = db.tbl_remuneration.money,
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000).
    OrderBy().ASC(db => db.tbl_staff.name);

    //execute.
    var datas = query.ToExecutor(new SqlConnection(ConnectionString)).Read();

    foreach (var e in datas)
    {
        Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
    }
}
```
```sql
SELECT
	tbl_staff.name AS "name",
	tbl_remuneration.payment_date AS "payment_date",
	tbl_remuneration.money AS "money"
FROM
	tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
WHERE
	(((@p_0) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_1)))
ORDER BY 
	tbl_staff.name ASC;
```
Using normal type.
```cs
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
public class SelectData
{
    public string name { get; set; }
    public DateTime payment_date { get; set; }
    public decimal money { get; set; }
}

[TestMethod]
public void StandardNoramlType()
{
    //log for debug.
    Sql.Log = l => Debug.Print(l);

    //make sql.
	var query = Sql.Query<DB>().
    Select(db => new SelectData()
    {
        name = db.tbl_staff.name,
        payment_date = db.tbl_remuneration.payment_date,
        money = db.tbl_remuneration.money,
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000).
    OrderBy().ASC(db => db.tbl_staff.name);

    //execute.
    var datas = query.ToExecutor(new SqlConnection(ConnectionString)).Read();

    foreach (var e in datas)
    {
        Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
    }
}
```
```sql
SELECT
	tbl_staff.name AS "name",
	tbl_remuneration.payment_date AS "payment_date",
	tbl_remuneration.money AS "money"
FROM
	tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
WHERE
	(((@p_0) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_1)))
ORDER BY 
	tbl_staff.name ASC;
```
Select all.
```cs
[TestMethod]
public void SelectAll()
{
    //make sql.
    var query = Sql.Query(() => new DB()).
    Select().
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000).
        OrderBy().ASC(db => db.tbl_staff.name);

    //execute.
    var datas = query.ToExecutor(new SqlConnection(ConnectionString)).Read();

    foreach (var e in datas)
    {
        Debug.Print("{0}, {1}, {2}", e.tbl_staff.name, e.tbl_remuneration.payment_date, e.tbl_remuneration.money);
    }
}
```
```sql
SELECT
	tbl_staff.id,
	tbl_staff.name,
	tbl_remuneration.id,
	tbl_remuneration.staff_id,
	tbl_remuneration.payment_date,
	tbl_remuneration.money
FROM
	tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
WHERE
	(((@p_0) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_1)))
ORDER BY 
	tbl_staff.name ASC;
```
Select one table.
```cs
[TestMethod]
public void SelectFrom()
{
    //make sql.
    var query = Sql.Query(() => new DB()).
    SelectFrom(db => db.tbl_staff);

    //execute.
    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
}
```
```sql
SELECT
	id,
	name
FROM
	tbl_staff;
```

Group by.
```cs
[TestMethod]
public void GroupBy()
{
    var query = Sql.Query(() => new DB()).
    Select(db => new
    {
        name = db.tbl_staff.name,
        count = Sql.Funcs.Count(db.tbl_remuneration.money),
        total = Sql.Funcs.Sum(db.tbl_remuneration.money),
        average = Sql.Funcs.Avg(db.tbl_remuneration.money),
        minimum = Sql.Funcs.Min(db.tbl_remuneration.money),
        maximum = Sql.Funcs.Max(db.tbl_remuneration.money),
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name);

    var datas = query.ToExecutor(new SqlConnection(ConnectionString)).Read();
    foreach (var e in datas)
    {
        Debug.Print("{0}, {1}, {2}, {3}, {4}, {5}", e.name, e.count, e.total, e.average, e.minimum, e.maximum);
    }
}
```
```sql
SELECT
	tbl_staff.name AS "name",
	Count(tbl_remuneration.money) AS "count",
	Sum(tbl_remuneration.money) AS "total",
	Avg(tbl_remuneration.money) AS "average",
	Min(tbl_remuneration.money) AS "minimum",
	Max(tbl_remuneration.money) AS "maximum"
FROM
	tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY 
	tbl_staff.id,
	tbl_staff.name;
```
Having.
```cs
[TestMethod]
public void Having()
{
    var query = Sql.Query(() => new DB()).
    Select(db => new
    {
        name = db.tbl_staff.name,
        total = Sql.Funcs.Sum(db.tbl_remuneration.money)
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name).
    Having(db => 10000 < Sql.Funcs.Sum(db.tbl_remuneration.money));

    var datas = query.ToExecutor(new SqlConnection(ConnectionString)).Read();
}
```
```sql
SELECT
	tbl_staff.name AS "name",
	Sum(tbl_remuneration.money) AS "total"
FROM
	tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY 
	tbl_staff.id,
	tbl_staff.name
HAVING
	((@p_0) < (Sum(tbl_remuneration.money)));
```
You can write sequencial AND OR.
```cs
[TestMethod]
public void WhereAndOr()
{
    var exp = Sql.Query<Data>().
        ConditionBuilder().
        Continue((db, x) => 3000 < db.tbl_remuneration.money).
        Continue((db, x) => x && db.tbl_remuneration.money < 4000).
        Continue((db, x) => x || db.tbl_staff.id == 1);

    var query = Sql.Query<Data>().
        Select().
        From(db => db.tbl_remuneration).
            Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
        Where(db => exp.Cast<bool>());

    var datas = query.ToExecutor(_connection).Read();
}
```
```sql
SELECT
	tbl_staff.id,
	tbl_staff.name,
	tbl_remuneration.id,
	tbl_remuneration.staff_id,
	tbl_remuneration.payment_date,
	tbl_remuneration.money
FROM
	tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
WHERE
	(((@p_2) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_1))) OR ((tbl_staff.id) = (@p_0));
```
Like, In, Between
```cs
[TestMethod]
public void Like()
{
    var query =
        Sql.Query(() => new { tbl_staff = new Staff() }).
        Select().
        From(db=>db.tbl_staff).
        Where(db => Sql.Words.Like(db.tbl_staff.name, "%son%"));

    var datas = query.ToExecutor(new SqlConnection(ConnectionString)).Read();
}
```
```sql
SELECT
	tbl_staff.id,
	tbl_staff.name
FROM
	tbl_staff
WHERE
	(tbl_staff.name LIKE @p_0);
```
```cs
[TestMethod]
public void In()
{
    var query = Sql.Query(() => new { tbl_staff = new Staff() }).
        Select().
        From(db => db.tbl_staff).
        Where(db => Sql.Words.In(db.tbl_staff.id, 1, 3));

    var datas = query.ToExecutor(new SqlConnection(ConnectionString)).Read();
}
```
```sql
SELECT
	tbl_staff.id,
	tbl_staff.name
FROM
	tbl_staff
WHERE
	(tbl_staff.id IN(@p_0, @p_1));
```
```cs
[TestMethod]
public void Between()
{
    var query =
        Sql.Query(() => new { tbl_staff = new Staff() }).
        Select().
        From(db => db.tbl_staff).
        Where().Between(db => db.tbl_staff.id, 1, 3);

    var datas = query.ToExecutor(new SqlConnection(ConnectionString)).Read();
}
```
```sql
SELECT
	tbl_staff.id,
	tbl_staff.name
FROM
	tbl_staff
WHERE
	(tbl_staff.id BETWEEN @p_0 AND @p_1);
```
You can use sub query.
```cs
[TestMethod]
public void WhereInSubQuery()
{
    var define = Sql.Query<DB>();

    var sub = define.
        Select(db => new { total = db.tbl_remuneration.staff_id }).
        From(db => db.tbl_remuneration);

    var datas = define.
        Select(db => new { name = db.tbl_staff.name }).
        From(db => db.tbl_staff).
        Where(db => Sql.Words.In(db.tbl_staff.id, sub.Cast<int>())).//sub query.
        ToExecutor(new SqlConnection(ConnectionString)).Read();
}
```
```sql
SELECT
	tbl_staff.name AS "name"
FROM
	tbl_staff
WHERE
	(tbl_staff.id IN(
	(SELECT
		tbl_remuneration.staff_id AS "total"
	FROM
		tbl_remuneration)));
```
```cs
[TestMethod]
public void SelectSubQuery()
{
    var define = Sql.Query<DB>();

    var sub = define.
        Select(db => new { total = Sql.Funcs.Sum(db.tbl_remuneration.money) }).
        From(db => db.tbl_remuneration);

    var datas = define.
        Select(db => new
        {
            name = db.tbl_staff.name,
            total = sub.Cast<decimal>()//sub query.
        }).
        From(db => db.tbl_staff).
        ToExecutor(new SqlConnection(ConnectionString)).Read();
}
```
```sql
SELECT
	tbl_staff.name AS "name",
	(SELECT
		Sum(tbl_remuneration.money) AS "total"
	FROM
		tbl_remuneration) AS "total"
FROM
	tbl_staff;
```
```cs
[TestMethod]
public void FromSubQuery()
{
    var subQuery = Sql.Query(() => new DB()).
        Select(db => new SelectData()
        {
            name = db.tbl_staff.name,
            payment_date = db.tbl_remuneration.payment_date,
            money = db.tbl_remuneration.money,
        }).
        From(db => db.tbl_remuneration).
            Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
        Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000);

    var query = Sql.Query(() => new
    {
        tbl_staff = new Staff(),
        tbl_sub = subQuery.Cast() //sub query.
    }).
    Select(db => new
    {
        name = db.tbl_sub.name
    }).
    From(db => db.tbl_sub);

    var datas = query.ToExecutor(new SqlConnection(ConnectionString)).Read();
}
```
```sql
SELECT
	tbl_sub.name AS "name"
FROM
	(SELECT
		tbl_staff.name AS "name",
		tbl_remuneration.payment_date AS "payment_date",
		tbl_remuneration.money AS "money"
	FROM
		tbl_remuneration
		JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
	WHERE
		(((@p_0) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (@p_1)))) AS tbl_sub;
```
Distinct
```cs
[TestMethod]
public void Distinct()
{
    var datas = Sql.Query<DB>().
        Select(AggregatePredicate.Distinct, db => new
        {
            id = db.tbl_remuneration.staff_id
        }).
        From(db => db.tbl_remuneration).
        ToExecutor(new SqlConnection(ConnectionString)).Read();
}
```
```sql
SELECT
	DISTINCT tbl_remuneration.staff_id AS "id"
FROM
	tbl_remuneration;
```
```cs
[TestMethod]
public void GroupByPredicateDistinct()
{
    var query = Sql.Query(() => new DB()).
    Select(db => new
    {
        name = db.tbl_staff.name,
        count = Sql.Funcs.Count(AggregatePredicate.Distinct, db.tbl_remuneration.money),
        total = Sql.Funcs.Sum(AggregatePredicate.Distinct, db.tbl_remuneration.money)
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name);

    var datas = query.ToExecutor(_connection).Read();
}

```
```sql
SELECT
	tbl_staff.name AS "name",
	Count(DISTINCT tbl_remuneration.money) AS "count",
	Sum(DISTINCT tbl_remuneration.money) AS "total"
FROM
	tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY 
	tbl_staff.id,
	tbl_staff.name;
```
All
```cs
[TestMethod]
public void All()
{
    var datas = Sql.Query<DB>().
        Select(AggregatePredicate.All, db => new
        {
            id = db.tbl_remuneration.staff_id
        }).
        From(db => db.tbl_remuneration).
        ToExecutor(new SqlConnection(ConnectionString)).Read();
}
```
```sql
SELECT
	ALL tbl_remuneration.staff_id AS "id"
FROM
	tbl_remuneration;
```
```cs
[TestMethod]
public void GroupByPredicateAll()
{
    var query = Sql.Query(() => new DB()).
    Select(db => new
    {
        name = db.tbl_staff.name,
        count = Sql.Funcs.Count(AggregatePredicate.All, db.tbl_remuneration.money),
        total = Sql.Funcs.Sum(AggregatePredicate.All, db.tbl_remuneration.money)
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name);

    var datas = query.ToExecutor(_connection).Read();
}

```
```sql
SELECT
	tbl_staff.name AS "name",
	Count(ALL tbl_remuneration.money) AS "count",
	Sum(ALL tbl_remuneration.money) AS "total"
FROM
	tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY 
	tbl_staff.id,
	tbl_staff.name;
```
Delete all.
```cs
[TestMethod]
public void Delete()
{
    var count = Sql.Query<DBData>().
        Delete().
        From(db => db.tbl_data).
        ToExecutor(new SqlConnection(ConnectionString)).Write();
}
```
```sql
DELETE
FROM
	tbl_data;
```
Delete condition matching row. 
```cs
[TestMethod]
public void DeleteWhere()
{
    var count = Sql.Query<DBData>().
        Delete().
        From(db => db.tbl_data).
        Where(db => db.tbl_data.id == 3).
        ToExecutor(new SqlConnection(ConnectionString)).Write();
}
```
```sql
DELETE
FROM
	tbl_data
WHERE
	((tbl_data.id) = (@p_0));
```
Insert.
```cs
[TestMethod]
public void Insert()
{
    var count = 
        Sql.Query<DBData>().
        InsertInto(db => db.tbl_data).
        Values(new Data() { id = 1, val1 = 10, val2 = "a" }).
        ToExecutor(_connection).Write();
}
```
```sql
INSERT INTO tbl_data (id, val1, val2)
VALUES(@p_0, @p_1, @p_2);
```
Insert selected data.
```cs
[TestMethod]
public void InsertSelectedData()
{
    var count = 
        Sql.Query<DBData>().
        InsertInto(db => db.tbl_data, tbl => tbl.id, tbl => tbl.val1).
        Values(new Data() { id = 1, val1 = 10 }).
        ToExecutor(_connection).Write();
}
```
```sql
INSERT INTO tbl_data (id, val1)
VALUES(@p_0, @p_1);
```
Insert using anonymous type.
```cs
[TestMethod]
public void InsertUsingAnonymousType()
{
    var data = new
    {
        id = 1,
        val1 = 1,
        val2 = 1.ToString()
    };

    var count =
        Sql.Query(() => new
        {
            tbl_data = data
        }).
        InsertInto(db => db.tbl_data).
        Values(data).
        ToExecutor(_connection).Write();
}
```
```sql
INSERT INTO tbl_data (id, val1, val2)
VALUES(@p_0, @p_1, @p_2);
```
Update
```cs
public void Update()
{
    var count = Sql.Query<DBData>().
        Update(db => db.tbl_data).
        Set().
        Assign(tbl => tbl.val1, 100).
        Assign(tbl => tbl.val2, "200").
        Where(db => db.tbl_data.id == 1).
        ToExecutor(TestEnvironment.Adapter).Write();
}
```
```sql
UPDATE tbl_data
SET
	val1 = @p_0,
	val2 = @p_1
WHERE
	((tbl_data.id) = (@p_2));
```
Update using table value.
```cs
[TestMethod]
public void UpdateUsingTableValue()
{
    var count = Sql.Query<DBData>().
        Update(db => db.tbl_data).
        Set().
        Assign(tbl => tbl.val1, tbl => tbl.val1 * 2).
        Where(db => db.tbl_data.id == 1).
        ToExecutor(TestEnvironment.Adapter).Write();
}
```
```sql
UPDATE tbl_data
SET
	val1 = (val1) * (@p_0)
WHERE
	((tbl_data.id) = (@p_1));
```
IS NULL


```cs
[TestMethod]
public void IsNull()
{
    decimal? val = null;
    var query = Sql.Query<DB>().
        Select().
        From(db => db.tbl_remuneration).
            Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
       Where(db => db.tbl_staff.name == null || db.tbl_remuneration.money == val);

    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
}
```
```sql
SELECT
	tbl_staff.id,
	tbl_staff.name,
	tbl_remuneration.id,
	tbl_remuneration.staff_id,
	tbl_remuneration.payment_date,
	tbl_remuneration.money
FROM
	tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
WHERE
	(((tbl_staff.name) IS NULL) OR ((tbl_remuneration.money) IS NULL));
```
```cs
[TestMethod]
public void IsNotNull()
{
    decimal? val = null;
    var query = Sql.Query<DB>().
        Select().
        From(db => db.tbl_remuneration).
            Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
       Where(db => db.tbl_staff.name != null || db.tbl_remuneration.money != val);

    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
}
```
```sql
SELECT
	tbl_staff.id,
	tbl_staff.name,
	tbl_remuneration.id,
	tbl_remuneration.staff_id,
	tbl_remuneration.payment_date,
	tbl_remuneration.money
FROM
	tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
WHERE
	(((tbl_staff.name) IS NOT NULL) OR ((tbl_remuneration.money) IS NOT NULL));
```
Case
```cs
[TestMethod]
public void Case1()
{
    var caseQuery = Sql.Query<DB>().Case().
        WhenThen(db => db.tbl_staff.id == 3, "x").
        WhenThen(db => db.tbl_staff.id == 4, "y").
        Else("z");

    var datas = Sql.Query<DB>().
        Select(db => new
        {
            type = caseQuery.Cast<string>()
        }).
        From(db => db.tbl_staff).
        ToExecutor(_connection).Read();
}
```
```sql
SELECT
	CASE 
		WHEN (tbl_staff.id) = (@p_0) THEN @p_1
		WHEN (tbl_staff.id) = (@p_2) THEN @p_3
		ELSE @p_4
	END AS "type"
FROM
	tbl_staff;
```
```cs
public void Case2()
{
    var caseQuery = Sql.Query<DB>().Case(db => db.tbl_staff.id).
        WhenThen(3, "x").
        WhenThen(4, "y").
        Else("z");

    var datas = Sql.Query<DB>().
        Select(db => new
        {
            type = caseQuery.Cast<string>()
        }).
        From(db => db.tbl_staff).
        ToExecutor(_connection).Read();
}
```
```sql
SELECT
	CASE tbl_staff.id
		WHEN @p_0 THEN @p_1
		WHEN @p_2 THEN @p_3
		ELSE @p_4
	END AS "type"
FROM
	tbl_staff;
```
