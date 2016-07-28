using LambdicSql;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace TestCore
{
    public class Samples
    {
        public IDbConnection _connection;

        public void TestInitialize(string testName, IDbConnection connection)
        {
            _connection = connection;

            Sql.Log = null;
            Delete();
            Sql.Log = l =>
            {
                Debug.Print("");
                Debug.Print(testName);
                Debug.Print("```sql");
                Debug.Print(l);
                Debug.Print("```");
            };
        }

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
            var datas = query.ToExecutor(_connection).Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }
        }

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
            var datas = query.ToExecutor(_connection).Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }
        }

        //Select all.
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
            var datas = query.ToExecutor(_connection).Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.tbl_staff.name, e.tbl_remuneration.payment_date, e.tbl_remuneration.money);
            }
        }

        //Select one table.
        public void SelectFrom()
        {
            //make sql.
            var query = Sql.Query(() => new DB()).
            SelectFrom(db => db.tbl_staff);

            //execute.
            var datas = query.ToExecutor(_connection).Read();
        }

        //Group by.
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

            var datas = query.ToExecutor(_connection).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}, {3}, {4}, {5}", e.name, e.count, e.total, e.average, e.minimum, e.maximum);
            }
        }

        //Group by using Distinct.
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

        //Group by using All.
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

        //Having
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

            var datas = query.ToExecutor(_connection).Read();
        }

        //You can write sequencial AND OR.
        public void WhereAndOr()
        {
            WhereAndOr(new ValueY() { Value = new ValueX() { Value = 3000 } });
            WhereAndOr(new ValueY() { Value = new ValueX() { Value = 3000 } });
        }

        public class ValueX
        {
            public Decimal Value { get; set; }
        }
        public class ValueY
        {
            public ValueX Value { get; set; }
        }
        public void WhereAndOr(ValueY x)
        {
            var query = Sql.Query(() => new DB()).
                Select().
                From(db => db.tbl_remuneration).
                    Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).Where();
        
            //sequencial write!
            query = query.And(db => x.Value.Value < db.tbl_remuneration.money).And(db => db.tbl_remuneration.money < 4000).Or(db => db.tbl_staff.id == 1);

            var datas = query.ToExecutor(_connection).Read();
        }

        //Like, In, Between
        public void Like()
        {
            var query =
                Sql.Query(() => new { tbl_staff = new Staff() }).
                Select().
                From(db => db.tbl_staff).
                Where(db => Sql.Words.Like(db.tbl_staff.name, "%son%"));

            var datas = query.ToExecutor(_connection).Read();
        }
        
        public void In()
        {
            var query = Sql.Query(() => new { tbl_staff = new Staff() }).
                Select().
                From(db => db.tbl_staff).
                Where(db => Sql.Words.In(db.tbl_staff.id, 1, 3));

            var datas = query.ToExecutor(_connection).Read();
        }
        
        public void Between()
        {
            var query =
                Sql.Query(() => new { tbl_staff = new Staff() }).
                Select().
                From(db => db.tbl_staff).
                Where(db => Sql.Words.Between(db.tbl_staff.id, 1, 3));

            var datas = query.ToExecutor(_connection).Read();
        }
        
        //You can use sub query.
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
                ToExecutor(_connection).Read();
        }

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
                ToExecutor(_connection).Read();
        }
        
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

            var datas = query.ToExecutor(_connection).Read();
        }

        //Distinct
        //```cs
        public void SelectPredicateDistinct()
        {
            var datas = Sql.Query<DB>().
                Select(AggregatePredicate.Distinct, db => new
                {
                    id = db.tbl_remuneration.staff_id
                }).
                From(db => db.tbl_remuneration).
                ToExecutor(_connection).Read();
        }
        //```

        //All
        //```cs
        public void SelectPredicateAll()
        {
            var datas = Sql.Query<DB>().
                Select(AggregatePredicate.All, db => new
                {
                    id = db.tbl_remuneration.staff_id
                }).
                From(db => db.tbl_remuneration).
                ToExecutor(_connection).Read();
        }
        //```

        //Delete all.
        //```cs
        public void Delete()
        {
            var count = Sql.Query<DBData>().
                Delete().
                From(db => db.tbl_data).
                ToExecutor(_connection).Write();
        }
        //```

        //Delete condition matching row. 
        //```cs
        public void DeleteWhere()
        {
            var count = Sql.Query<DBData>().
                Delete().
                From(db => db.tbl_data).
                Where(db => db.tbl_data.id == 3).
                ToExecutor(_connection).Write();
        }
        //```

        //Insert.
        //```cs
        public void Insert()
        {
            var count = 
                Sql.Query<DBData>().
                InsertInto(db => db.tbl_data).
                Values(new Data() { id = 1, val1 = 10, val2 = "a" }).
                ToExecutor(_connection).Write();
        }
        //```

        //Insert selected data.
        //```cs
        public void InsertSelectedData()
        {
            var count = 
                Sql.Query<DBData>().
                InsertInto(db => db.tbl_data, tbl => tbl.id, tbl => tbl.val1).
                Values(new Data() { id = 1, val1 = 10 }).
                ToExecutor(_connection).Write();
        }
        //```

        //Insert using anonymous type.
        //```cs
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
        //```

        //Update
        //```cs
        public void Update()
        {
            var count = Sql.Query<DBData>().
                Update(db => db.tbl_data).
                Set().
                Assign(tbl => tbl.val1, 100).
                Assign(tbl => tbl.val2, "200").
                Where(db => db.tbl_data.id == 1).
                ToExecutor(_connection).Write();
        }
        //```

        //Update using table value.
        //```cs
        public void UpdateUsingTableValue()
        {
            var count = Sql.Query<DBData>().
                Update(db => db.tbl_data).
                Set().
                Assign(tbl => tbl.val1, tbl => tbl.val1 * 2).
                Where(db => db.tbl_data.id == 1).
                ToExecutor(_connection).Write();
        }
        //```
        
        public class Data
        {
            public int id { get; set; }
            public int val1 { get; set; }
            public string val2 { get; set; }
        }

        public class DBData
        {
            public Data tbl_data { get; set; }
        }
        
        public void IsNull()
        {
            decimal? val = null;
            var query = Sql.Query<DB>().
                Select(db => new SelectData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db => db.tbl_remuneration).
                    Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
               Where(db => db.tbl_staff.name == null || db.tbl_remuneration.money == val);

            var datas = query.ToExecutor(_connection).Read();
        }
        
        public void IsNotNull()
        {
            decimal? val = null;
            var query = Sql.Query<DB>().
                Select(db => new SelectData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db => db.tbl_remuneration).
                    Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
               Where(db => db.tbl_staff.name != null || db.tbl_remuneration.money != val);

            var datas = query.ToExecutor(_connection).Read();
        }

        public class RemunerationNullable
        {
            public int id { get; set; }
            public int? staff_id { get; set; }
            public DateTime? payment_date { get; set; }
            public decimal? money { get; set; }
        }
        public class DBNullable
        {
            public Staff tbl_staff { get; set; }
            public RemunerationNullable tbl_remuneration { get; set; }
        }

        public class SelectDataNullable
        {
            public string name { get; set; }
            public DateTime? payment_date { get; set; }
            public decimal? money { get; set; }
        }
        
        public void Nullable()
        {
            var query = Sql.Query<DBNullable>().
                Select(db => new SelectDataNullable()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db => db.tbl_remuneration).
                    Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
                    Where(db=>db.tbl_remuneration.money != null);

            var datas = query.ToExecutor(_connection).Read();
        }
        
        public void StringCalc()
        {
            var query = Sql.Query<DB>().
            Select(db => new SelectData()
            {
                name = db.tbl_staff.name + "x"
            }).
            From(db => db.tbl_staff);

            //execute.
            var datas = query.ToExecutor(_connection).Read();
        }

        public void WhereEx()
        {
            WhereEx(true, true);
            WhereEx(true, false);
            WhereEx(false, true);
            WhereEx(false, false);
        }

        public void WhereEx(bool minCondition, bool maxCondition)
        {
            var datas =
                Sql.Query<DB>().SelectFrom(db=>db.tbl_remuneration).Where().
                And(minCondition, db => 3000 < db.tbl_remuneration.money).
                And(maxCondition, db => db.tbl_remuneration.money < 4000).
                ToExecutor(_connection).Read();
        }
    }
}
