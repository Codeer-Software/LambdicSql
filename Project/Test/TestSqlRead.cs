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
        public void StandardNoramlTypeInit()
        {
            Sql.Query(() => new Data()
            {
                tbl_staff = new Staff()
                {
                    id = 10
                }
            });
        }


        [TestMethod]
        public void SubQueryUsing()
        {
            var query = Sql.Query(() => new Data()).
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

            Sql.Query(() => new
            {
                tbl_staff = new Staff(),
                tbl_sub = query.Cast()
            });
        }

        class Data2
        {
            public Staff tbl_staff { get; set; }
            public SelectData tbl_sub { get; set; }
        }

        [TestMethod]
        public void SubQueryUsingType()
        {
            Sql.Log = l => Debug.Print(l);

            var subQuery = Sql.Query(() => new Data()).
                Select(db => new SelectData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db => db.tbl_remuneration).
                    Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000);

            var query1 = Sql.Query(() => new Data2
            {
                tbl_staff = new Staff(),
                tbl_sub = subQuery.Cast()
            }).
            Select(db=>new
            {
                name = db.tbl_sub.name
            }).
            From(db=>db.tbl_sub);

            query1.ToExecutor(TestEnvironment.Adapter).Read();

            var query2 = Sql.Query(() => new Data2
            {
                tbl_staff = new Staff(),
                tbl_sub = subQuery.Cast()
            }).
            Select(db => new
            {
                name = db.tbl_sub.name
            }).
            From(db => db.tbl_staff).Join(db => db.tbl_sub, db => db.tbl_sub.name == db.tbl_staff.name);

            query2.ToExecutor(TestEnvironment.Adapter).Read();
        }

        [TestMethod]
        public void StandardNoramlType()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Query(() => new Data()).
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
            var query = Sql.Query<Data>().
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
        public void Join()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Query<Data>().
            Select(db => new SelectData()
            {
                name = db.tbl_staff.name,
                payment_date = db.tbl_remuneration.payment_date,
                money = db.tbl_remuneration.money,
            }).
            From(db => db.tbl_remuneration);

            var queryJoin = query.Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id);
            var queryLeftJoin = query.LeftJoin(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id);
            var queryRightJoin = query.RightJoin(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id);
            var queryCorssJoin = query.CrossJoin(db => db.tbl_staff);

            //execute.
            var datas = queryJoin.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }
            datas = queryLeftJoin.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }
            datas = queryRightJoin.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }
            datas = queryCorssJoin.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }
        }

        [TestMethod]
        public void Distinct()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Query<Data>().
            Select(db => new
            {
                id = Sql.Word.Distinct(db.tbl_remuneration.staff_id)
            }).
            From(db => db.tbl_remuneration);

            //execute.
            var datas = query.ToExecutor(TestEnvironment.Adapter).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}", e.id);
            }
        }

        [TestMethod]
        public void TestFree()
        {            
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var query = Sql.Query(()=>new { tbl_staff = new Staff() }).
            Free("/* comment */").
            Select().
            From(db=>db.tbl_staff);

            //execute.
            query.ToExecutor(TestEnvironment.Adapter).Read();

        }

        [TestMethod]
        public void TestTableOne()
        {
            Sql.Log = l => Debug.Print(l);
            var query = Sql.Query(() => new
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
            var query = Sql.Query(() => new Data()).
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
            var query = Sql.Query(() => new
            {
                tbl_staff = new Staff()
            });

            //execute.
            var datas = query.Select().From(db => db.tbl_staff).ToExecutor(TestEnvironment.Adapter).Read();

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
            var query = Sql.Query(() => new Data()).
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
            var query = Sql.Query(() => new Data()).
            Select(db => new
            {
                name = db.tbl_staff.name,
                total = Sql.Func.Sum(db.tbl_remuneration.money)
            }).
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
            GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name).Having(db => 10000 < Sql.Func.Sum(db.tbl_remuneration.money));

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

            var query = Sql.Query(() => new Data()).
                Select().
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
            var query = Sql.Query(() => new { tbl_staff = new Staff() }).
            Select().
            From(db => db.tbl_staff).
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
            var query = Sql.Query(() => new { tbl_staff = new Staff() }).
            Select().
            From(db => db.tbl_staff).
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
            var query = Sql.Query(() => new { tbl_staff = new Staff() }).
            Select().
            From(db => db.tbl_staff).
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
            Sql.Query<Data>().
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
            Sql.Query<Data>().
            Select().
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
            Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000).
            OrderBy().ASC(db => db.tbl_staff.name).
            ToExecutor(TestEnvironment.Adapter).Read();
        
        public IEnumerable<Staff> ReadData3() =>
            Sql.Query(() => new { tbl_staff = new Staff() }).
            Select().
            From(db => db.tbl_staff).
            ToExecutor(TestEnvironment.Adapter).Read().Select(e=>e.tbl_staff);

        [TestMethod]
        public void SelectSubQuery()
        {
            Sql.Log = l => Debug.Print(l);
            var define = Sql.Query<Data>();

            var sub = define.
                Select(db => new { total = Sql.Func.Sum(db.tbl_remuneration.money) }).
                From(db => db.tbl_remuneration);

            var datas = define.
                    Select(db => new
                    {
                        name = db.tbl_staff.name,
                        total = sub.Cast<decimal>()
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
            var define = Sql.Query<Data>();

            var sub = define.
                Select(db => new { total = db.tbl_remuneration.staff_id }).
                From(db => db.tbl_remuneration);

            var datas = define.
                Select(db=>new { name = db.tbl_staff.name }).
                From(db => db.tbl_staff).
                Where().In(db=>db.tbl_staff.id, db=>sub.Cast<int>()).
                ToExecutor(TestEnvironment.Adapter).Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}", e.name);
            }
        }

        public class tbl_data
        {
            public int id { get; set; }
            public int val1 { get; set; }
            public string val2 { get; set; }
        }

        public class DataChangeTest
        {
            public Staff tbl_staff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
            public tbl_data tbl_data { get; set; }
        }

        [TestMethod]
        public void Delete()
        {
            Sql.Log = l => Debug.Print(l);


            var count = Sql.Query<DataChangeTest>().
                Delete().
                From(db => db.tbl_data).
                ToExecutor(TestEnvironment.Adapter).Write();

        }

        [TestMethod]
        public void DeleteWhere()
        {
            Sql.Log = l => Debug.Print(l);


            var count = Sql.Query<DataChangeTest>().
                Delete().
                From(db => db.tbl_data).
                Where(db=>db.tbl_data.id == 3).
                ToExecutor(TestEnvironment.Adapter).Write();
        }


        [TestMethod]
        public void Insert()
        {
            Sql.Log = l => Debug.Print(l);

            tbl_data[] datas = new[]
            {
                new tbl_data() { id = 1, val1 = 10, val2 = "a" },
                new tbl_data() { id = 2, val1 = 20, val2 = "b" }
            };

            Delete();
            var count1 = Sql.Query<DataChangeTest>().
                InsertInto(db => db.tbl_data).
                Values(datas).
                ToExecutor(TestEnvironment.Adapter).Write();
            
            Delete();
            var count2 = Sql.Query<DataChangeTest>().
                InsertInto(db => db.tbl_data, tbl => tbl.id, tbl => tbl.val1).
                Values(datas).
                ToExecutor(TestEnvironment.Adapter).Write();
             
            Delete();
            var datasX = datas.Select(e => new
            {
                id = e.id,
                val1 = e.val1,
                val2 = e.val2
            }).ToArray();
            var count3 = Sql.Query(() => new
            {
                tbl_data = datasX.FirstOrDefault()
            }).
            InsertInto(db=>db.tbl_data).
            Values(datasX).
            ToExecutor(TestEnvironment.Adapter).Write();
        }

        [TestMethod]
        public void Update()
        {
            Sql.Log = l => Debug.Print(l);

            var count1 = Sql.Query<DataChangeTest>().
                Update(db => db.tbl_data).
                Set().
                Assign(tbl => tbl.val1, 100).
                Assign(tbl => tbl.val2, "200").
                Where(db=>db.tbl_data.id == 1).
                ToExecutor(TestEnvironment.Adapter).Write();

            var count2 = Sql.Query<DataChangeTest>().
                Update(db => db.tbl_data).
                Set().
                Assign(tbl => tbl.val1, tbl => tbl.val1 * 2).
                Where(db => db.tbl_data.id == 1).
                ToExecutor(TestEnvironment.Adapter).Write();
        }
    }
}
