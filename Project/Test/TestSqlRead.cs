using LambdicSql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Test
{
    [TestClass]
    public class TestSqlRead
    {
        [TestMethod]
        public void Standard()
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
            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }

            Assert.AreEqual(1, datas.Count());
            var first = datas.First();
            Assert.AreEqual("Jackson", first.name);
            Assert.AreEqual(new DateTime(2016, 1, 1), first.payment_date);
            Assert.AreEqual(3500, first.money);
        }

        //noraml type.
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
        public class Data
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
            var query = Sql.Using(() => new Data()).
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

            Assert.AreEqual(1, datas.Count());
            var first = datas.First();
            Assert.AreEqual("Jackson", first.name);
            Assert.AreEqual(new DateTime(2016, 1, 1), first.payment_date);
            Assert.AreEqual(3500, first.money);
        }

        [TestMethod]
        public void StandardNoramlTypeGeneric()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Using<Data>().
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

            Assert.AreEqual(1, datas.Count());
            var first = datas.First();
            Assert.AreEqual("Jackson", first.name);
            Assert.AreEqual(new DateTime(2016, 1, 1), first.payment_date);
            Assert.AreEqual(3500, first.money);
        }

        [TestMethod]
        public void TestTableOne()
        {
            Sql.Log = l => Debug.Print(l);
            var query = Sql.Using(() => new
            {
                tbl_staff = new Staff()
            });
            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}", e.tbl_staff.name);
            }
        }

        [TestMethod]
        public void AvoidSelect()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Using(() => new Data()).
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

            Assert.AreEqual(1, datas.Count());
            var first = datas.First();
            Assert.AreEqual("Jackson", first.tbl_staff.name);
            Assert.AreEqual(new DateTime(2016, 1, 1), first.tbl_remuneration.payment_date);
            Assert.AreEqual(3500, first.tbl_remuneration.money);
        }

        [TestMethod]
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
            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}", e.tbl_staff.id, e.tbl_staff.name);
            }

            Assert.AreEqual(4, datas.Count());
            var first = datas.First();
            Assert.AreEqual(1, first.tbl_staff.id);
            Assert.AreEqual("Emma", first.tbl_staff.name);
        }

        [TestMethod]
        public void GroupBy()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Using(() => new Data()).
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

            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}, {3}, {4}, {5}", e.name, e.count, e.total, e.average, e.minimum, e.maximum);
            }

            Assert.AreEqual(4, datas.Count());
            var first = datas.First();
            Assert.AreEqual("Emma", first.name);
            Assert.AreEqual(6000, first.total);
        }

        [TestMethod]
        public void Having()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Using(() => new Data()).
            Select((db, function) => new
            {
                name = db.tbl_staff.name,
                total = function.Sum(db.tbl_remuneration.money)
            }).
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
            GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name).Having((db, function) => 10000 < function.Sum(db.tbl_remuneration.money));

            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}, {3}, {4}, {5}", e.name, e.total);
            }
        }

        [TestMethod]
        public void WhereAndOr()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            var query = Sql.Using(() => new Data()).
                From(db => db.tbl_remuneration).
                    Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).Where();

            //sequencial write!
            query = query.And(db => 3000 < db.tbl_remuneration.money).And(db => db.tbl_remuneration.money < 4000).Or(db => db.tbl_staff.id == 1);

            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}", e.tbl_staff.name, e.tbl_remuneration.money);
            }
        }

        [TestMethod]
        public void WhereLike()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Using(() => new { tbl_staff = new Staff() }).
            Where().Like(db => db.tbl_staff.name, "%son%");

            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}", e.tbl_staff.name);
            }
        }

        [TestMethod]
        public void WhereIn()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Using(() => new { tbl_staff = new Staff() }).
            Where().In(db => db.tbl_staff.id, 1, 3);

            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}", e.tbl_staff.name);
            }
        }

        [TestMethod]
        public void WhereBetween()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Using(() => new { tbl_staff = new Staff() }).
            Where().Between(db => db.tbl_staff.id, 1, 3);

            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}", e.tbl_staff.name);
            }
        }

        [TestMethod]
        public void TestShort()
        {
            Sql.Log = l => Debug.Print(l);
            ReadData1();
            ReadData2();
            ReadData3();
        }

        public IEnumerable<SelectData> ReadData1() =>
            Sql.Using<Data>().
            Select(db => new SelectData()
            {
                name = db.tbl_staff.name,
                payment_date = db.tbl_remuneration.payment_date,
                money = db.tbl_remuneration.money,
            }).
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
            Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000).
              OrderBy().ASC(db => db.tbl_staff.name).
            ToExecutor(TestEnvironment.Adapter).Read();


        public IEnumerable<Data> ReadData2() =>
            Sql.Using<Data>().
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
            Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000).
            OrderBy().ASC(db => db.tbl_staff.name).
            ToExecutor(TestEnvironment.Adapter).Read();
        
        public IEnumerable<Staff> ReadData3() =>
            Sql.Using(() => new { tbl_staff = new Staff() }).
            ToExecutor(TestEnvironment.Adapter).Read().Select(e=>e.tbl_staff);

        [TestMethod]
        public void SelectSubQuery()
        {
            Sql.Log = l => Debug.Print(l);
            var define = Sql.Using<Data>();

            var sub = define.
                Select((db, func) => new { total = func.Sum(db.tbl_remuneration.money) }).
                From(db => db.tbl_remuneration);

            var datas = define.
                    Select(db => new
                    {
                        name = db.tbl_staff.name,
                        total = sub.ToSubQuery<decimal>()
                    }).
                    From(db => db.tbl_staff).
                    ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}", e.name, e.total);
            }
        }

        [TestMethod]
        public void WhereInSubQuery()
        {
            Sql.Log = l => Debug.Print(l);
            var define = Sql.Using<Data>();

            var sub = define.
                Select((db, func) => new { total = db.tbl_remuneration.staff_id }).
                From(db => db.tbl_remuneration);

            var datas = define.
                Select(db=>new { name = db.tbl_staff.name }).
                From(db => db.tbl_staff).
                Where().In(db=>db.tbl_staff.id, db=>sub.ToSubQuery<int>()).
                ToExecutor(TestEnvironment.Adapter).Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}", e.name);
            }
        }
    }
}
