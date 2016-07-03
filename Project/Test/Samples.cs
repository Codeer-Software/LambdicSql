using LambdicSql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Test
{
    [TestClass]
    public class Samples
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
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
    }
}
