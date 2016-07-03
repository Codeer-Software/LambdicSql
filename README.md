LambdicSql_ƒ¿
======================

## Features ...
#### Write SQL using lambda.
## Getting Started
LambdicSql from NuGet

    PM> Install-Package LambdicSql
https://www.nuget.org/packages/LambdicSql/
## Implemented Keywords.
- SELECT
- FROM
- WHERE
	- JOIN
	- LEFT JOIN
	- RIGHT JOIN
	- CROSS JOIN
- ORDER BY
	- ASC
	- DESC
- GROUP BY
- HAVING
- DISTINCT
- UPDATE
	- SET
- DELETE
- INSERT INTO
	- VALUES
- DISTINCT
Look for how to use from the samples.
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
    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();

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
	((3000) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (4000))
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
    var query = Sql.Query(() => new DB()).
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
    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();

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
	((3000) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (4000))
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
    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();

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
	((3000) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (4000))
ORDER BY 
	tbl_staff.name ASC;
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
        count = Sql.Func.Count(db.tbl_remuneration.money),
        total = Sql.Func.Sum(db.tbl_remuneration.money),
        average = Sql.Func.Avg(db.tbl_remuneration.money),
        minimum = Sql.Func.Min(db.tbl_remuneration.money),
        maximum = Sql.Func.Max(db.tbl_remuneration.money),
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name);

    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
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
        total = Sql.Func.Sum(db.tbl_remuneration.money)
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name).
    Having(db => 10000 < Sql.Func.Sum(db.tbl_remuneration.money));

    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
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
	(10000) < (Sum(tbl_remuneration.money));
```
You can write sequencial AND OR.
```cs
[TestMethod]
public void WhereAndOr()
{
    var query = Sql.Query(() => new DB()).
        Select().
        From(db => db.tbl_remuneration).
            Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).Where();

    //sequencial write!
    query = query.And(db => 3000 < db.tbl_remuneration.money).And(db => db.tbl_remuneration.money < 4000).Or(db => db.tbl_staff.id == 1);

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
	(3000) < (tbl_remuneration.money)
	AND (tbl_remuneration.money) < (4000)
	OR (tbl_staff.id) = (1);
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
        Where().Like(db => db.tbl_staff.name, "%son%");

    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
}
```
```sql
SELECT
	tbl_staff.id,
	tbl_staff.name
FROM
	tbl_staff
WHERE
	tbl_staff.name LIKE '%son%';
```
```cs
[TestMethod]
public void In()
{
    var query = Sql.Query(() => new { tbl_staff = new Staff() }).
        Select().
        From(db => db.tbl_staff).
        Where().In(db => db.tbl_staff.id, 1, 3);

    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
}
```
```sql
SELECT
	tbl_staff.id,
	tbl_staff.name
FROM
	tbl_staff
WHERE
	tbl_staff.id IN(1, 3);
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

    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
}
```
```sql
SELECT
	tbl_staff.id,
	tbl_staff.name
FROM
	tbl_staff
WHERE
	tbl_staff.id BETWEEN 1 AND 3;
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
        Where().In(db => db.tbl_staff.id, db => sub.Cast<int>()).//sub query.
        ToExecutor(TestEnvironment.Adapter).Read();
}
```
```sql
SELECT
	tbl_staff.name AS "name"
FROM
	tbl_staff
WHERE
	tbl_staff.id IN((SELECT tbl_remuneration.staff_id AS "total" FROM tbl_remuneration));
```
```cs
[TestMethod]
public void SelectSubQuery()
{
    var define = Sql.Query<DB>();

    var sub = define.
        Select(db => new { total = Sql.Func.Sum(db.tbl_remuneration.money) }).
        From(db => db.tbl_remuneration);

    var datas = define.
        Select(db => new
        {
            name = db.tbl_staff.name,
            total = sub.Cast<decimal>()//sub query.
        }).
        From(db => db.tbl_staff).
        ToExecutor(TestEnvironment.Adapter).Read();
}
```
```sql
SELECT
	tbl_staff.name AS "name",
	(SELECT Sum(tbl_remuneration.money) AS "total" FROM tbl_remuneration) AS "total"
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

    var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
}
```
```sql
SELECT
	tbl_sub.name AS "name"
FROM
	(SELECT tbl_staff.name AS "name", tbl_remuneration.payment_date AS "payment_date", tbl_remuneration.money AS "money" FROM tbl_remuneration JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id) WHERE ((3000) < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < (4000))) AS tbl_sub;
```
