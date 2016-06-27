LambdicSql
======================

## Features ...
#### Write SQL using lambda.

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
Avoid select.
```cs  
public void AvoidSelect()
{
    //log for debug.
    Sql.Log = l => Debug.Print(l);

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
If table count is 1 and want to get all, avoid where.
```cs  
public void AvoidWhere()
{
    //log for debug.
    Sql.Log = l => Debug.Print(l);

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
Group by and Sum.
```cs  
public void GroupBy()
{
    var query = Sql.Using(() => new DB()).
    Select((db, function) => new
    {
        name = db.tbl_staff.name,
        total = function.Sum(db.tbl_remuneration.money)
    }).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name);
            
    var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();
    foreach (var e in datas)
    {
        Debug.Print("{0}, {1}", e.name, e.total);
    }
}
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
}
```
Like, In, Between
```cs  
public void Like()
{
    var query = Sql.Using(() => new DB()).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    Where().Like(db => db.tbl_staff.name, "%son%");
}

public void In()
{
    var query = Sql.Using(() => new DB()).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    Where().In(db => db.tbl_staff.id, 1, 3);
}

public void Between()
{
    var query = Sql.Using(() => new DB()).
    From(db => db.tbl_remuneration).
        Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
    Where().Between(db => db.tbl_staff.id, 1, 3);
}
```
