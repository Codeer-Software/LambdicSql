using LambdicSql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace Test
{
    [TestClass]
    public class Samples
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Sql.Log = null;
            Delete();
            Sql.Log = l =>
            {
                Debug.Print("");
                Debug.Print(TestContext.TestName);
                Debug.Print("```sql");
                Debug.Print(l);
                Debug.Print("```");
            };
        }
        
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

        //Select all.
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
        
        //Group by.
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

        //Having
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

        //You can write sequencial AND OR.
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

        //Like, In, Between
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

        [TestMethod]
        public void In()
        {
            var query = Sql.Query(() => new { tbl_staff = new Staff() }).
                Select().
                From(db => db.tbl_staff).
                Where().In(db => db.tbl_staff.id, 1, 3);

            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
        }

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
        
        //You can use sub query.
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

        //Distinct
        //```cs
        [TestMethod]
        public void Distinct()
        {
            var datas = Sql.Query<DB>().
                Select(db => new
                {
                    id = Sql.Word.Distinct(db.tbl_remuneration.staff_id)
                }).
                From(db => db.tbl_remuneration).
                ToExecutor(TestEnvironment.Adapter).Read();
        }
        //```

        //Delete all.
        //```cs
        [TestMethod]
        public void Delete()
        {
            var count = Sql.Query<DBData>().
                Delete().
                From(db => db.tbl_data).
                ToExecutor(TestEnvironment.Adapter).Write();
        }
        //```

        //Delete condition matching row. 
        //```cs
        [TestMethod]
        public void DeleteWhere()
        {
            var count = Sql.Query<DBData>().
                Delete().
                From(db => db.tbl_data).
                Where(db => db.tbl_data.id == 3).
                ToExecutor(TestEnvironment.Adapter).Write();
        }
        //```

        //Insert.
        //```cs
        [TestMethod]
        public void Insert()
        {
            Data[] datas = new[]
            {
                new Data() { id = 1, val1 = 10, val2 = "a" },
                new Data() { id = 2, val1 = 20, val2 = "b" }
            };
            
            var count = 
                Sql.Query<DBData>().
                InsertInto(db => db.tbl_data).
                Values(datas).
                ToExecutor(TestEnvironment.Adapter).Write();
        }
        //```

        //Insert selected data.
        //```cs
        [TestMethod]
        public void InsertSelectedData()
        {
            Data[] datas = new[]
            {
                new Data() { id = 1, val1 = 10, val2 = "a" },
                new Data() { id = 2, val1 = 20, val2 = "b" }
            };
            
            var count = 
                Sql.Query<DBData>().
                InsertInto(db => db.tbl_data, tbl => tbl.id, tbl => tbl.val1).
                Values(datas).
                ToExecutor(TestEnvironment.Adapter).Write();
        }
        //```

        //Insert using anonymous type.
        //```cs
        [TestMethod]
        public void InsertUsingAnonymousType()
        {
            var datas = Enumerable.Range(1, 5).Select(x => new
            {
                id = x,
                val1 = x,
                val2 = x.ToString()
            }).ToArray();

            var count =
                Sql.Query(() => new
                {
                    tbl_data = datas.FirstOrDefault()
                }).
                InsertInto(db => db.tbl_data).
                Values(datas).
                ToExecutor(TestEnvironment.Adapter).Write();
        }
        //```

        //Update
        //```cs
        [TestMethod]
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
        //```

        //Update using table value.
        //```cs
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
    }
}
