LambdicSql
======================

## Features ...
#### Write SQL using lambda.
## Getting Started
LambdicSql from NuGet

    PM> Install-Package LambdicSql
https://www.nuget.org/packages/LambdicSql/

write only lambda.
```cs  
public void LambdaOnly()
{
    //log for debug.
    Sql.Log = l => Debug.Print(l);

    //make sql.
    var query = Sql.Using(() => new
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
    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();

    foreach (var e in datas)
    {
        Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
    }
}

```
```sql
SELECT 
	tbl_staff.name AS name,
	tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money AS money

FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)

WHERE
	(('3000') < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < ('4000'))

ORDER BY 
	tbl_staff.name ASC
```
use normal class.
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
public void StandardNoramlType()
{
    //log for debug.
    Sql.Log = l => Debug.Print(l);

    //make sql.
    var query = Sql.Using(() => new DB()).
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
    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();

    foreach (var e in datas)
    {
        Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
    }
}
```
```sql
SELECT 
	tbl_staff.name AS name,
	tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money AS money

FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)

WHERE
	(('3000') < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < ('4000'))

ORDER BY 
	tbl_staff.name ASC
```
Avoid select.
```cs  
public void AvoidSelect()
{
    //make sql.
    var query = Sql.Using(() => new DB()).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000).
        OrderBy().ASC(db => db.tbl_staff.name);

    //execute.
    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();

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

FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)

WHERE
	(('3000') < (tbl_remuneration.money)) AND ((tbl_remuneration.money) < ('4000'))

ORDER BY 
	tbl_staff.name ASC
```
If table count is 1 and want to get all, avoid where.
```cs  
public void AvoidWhere()
{
    //make sql.
    var query = Sql.Using(() => new
    {
        tbl_staff = new Staff()
    });

    //execute.
    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();

    foreach (var e in datas)
    {
        Debug.Print("{0}, {1}", e.tbl_staff.id, e.tbl_staff.name);
    }
}
```
```sql
SELECT 
	tbl_staff.id,
	tbl_staff.name

FROM tbl_staff
```
Group by.
```cs  
public void GroupBy()
{
    var query = Sql.Using(() => new DB()).
    Select((db, function) => new
    {
        name = db.tbl_staff.name,
        count = function.Count(db.tbl_remuneration.money),
        total = function.Sum(db.tbl_remuneration.money),
        average = function.Avg(db.tbl_remuneration.money),
        minimum = function.Min(db.tbl_remuneration.money),
        maximum = function.Max(db.tbl_remuneration.money),
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name);

    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();
    foreach (var e in datas)
    {
        Debug.Print("{0}, {1}, {2}, {3}, {4}, {5}", e.name, e.count, e.total, e.average, e.minimum, e.maximum);
    }
}
```
```sql
SELECT 
	tbl_staff.name AS name,
	Count(tbl_remuneration.money) AS count,
	Sum(tbl_remuneration.money) AS total,
	Avg(tbl_remuneration.money) AS average,
	Min(tbl_remuneration.money) AS minimum,
	Max(tbl_remuneration.money) AS maximum

FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)

GROUP BY 
	tbl_staff.id,
	tbl_staff.name
```
Having
```cs  
public void Having()
{
    var query = Sql.Using(() => new DB()).
    Select((db, function) => new
    {
        name = db.tbl_staff.name,
        total = function.Sum(db.tbl_remuneration.money)
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name).
    Having((db, function) => 10000 < function.Sum(db.tbl_remuneration.money));

    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();
}
```
```sql
SELECT 
	tbl_staff.name AS name,
	Sum(tbl_remuneration.money) AS total

FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)

GROUP BY 
	tbl_staff.id,
	tbl_staff.name

HAVING
	('10000') < (Sum(tbl_remuneration.money))
```
You can write sequencial AND OR.
```cs  
public void WhereAndOr()
{
    var query = Sql.Using(() => new DB()).
        From(db => db.tbl_remuneration).
            Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).Where();

    //sequencial write!
    query = query.And(db => 3000 < db.tbl_remuneration.money).And(db => db.tbl_remuneration.money < 4000).Or(db => db.tbl_staff.id == 1);
    
    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();

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

FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)

WHERE
	('3000') < (tbl_remuneration.money)
	AND (tbl_remuneration.money) < ('4000')
	OR (tbl_staff.id) = ('1')
```
Like, In, Between
```cs  
public void Like()
{
    var query = Sql.Using(() => new { tbl_staff = new Staff() }).
                Where().Like(db => db.tbl_staff.name, "%son%");
    
    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();
}
```
```sql
SELECT 
	tbl_staff.id,
	tbl_staff.name

FROM tbl_staff

WHERE
	tbl_staff.name LIKE '%son%'
```
```cs
public void In()
{
    var query = Sql.Using(() => new { tbl_staff = new Staff() }).
                Where().In(db => db.tbl_staff.id, 1, 3);

    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();
}
```
```sql
SELECT 
	tbl_staff.id,
	tbl_staff.name

FROM tbl_staff

WHERE
	tbl_staff.id IN('1', '3')
```
```cs
public void Between()
{
    var query = Sql.Using(() => new { tbl_staff = new Staff() }).
    Where().Between(db => db.tbl_staff.id, 1, 3);

    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();
}

```
```sql
SELECT 
	tbl_staff.id,
	tbl_staff.name

FROM tbl_staff

WHERE
	tbl_staff.id BETWEEN '1' AND '3'
```
