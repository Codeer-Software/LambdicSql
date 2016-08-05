using LambdicSql;
using LambdicSql.QueryBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace Test
{
    //TODO must to be detail testing.
    [TestClass]
    public class TestSqlRead
    {
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

        class Data2
        {
            public Staff tbl_staff { get; set; }
            public SelectData tbl_sub { get; set; }
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

        public IDbConnection _connection;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection("SQLServer");
            _connection.Open();
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();
#if xxxxx
        [TestMethod]
        public void LambdaOnlySelectFrom()
        {
            var data = Sql.Query(() => new
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
            }).SelectFrom(db => db.tbl_staff).ToExecutor(_connection).Read();
        }

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
            var datas = query.ToExecutor(_connection).Read();

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
            Select(db => new
            {
                name = db.tbl_sub.name
            }).
            From(db => db.tbl_sub);

            query1.ToExecutor(_connection).Read();

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

            query2.ToExecutor(_connection).Read();
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
            var datas = query.ToExecutor(_connection).Read();

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
            var datas = query.ToExecutor(_connection).Read();

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
            var datas = queryJoin.ToExecutor(_connection).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }
            datas = queryLeftJoin.ToExecutor(_connection).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }
            datas = queryRightJoin.ToExecutor(_connection).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }
            datas = queryCorssJoin.ToExecutor(_connection).Read();
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
            Select(AggregatePredicate.Distinct, db => new
            {
                id = db.tbl_remuneration.staff_id
            }).
            From(db => db.tbl_remuneration);

            //execute.
            var datas = query.ToExecutor(_connection).Read();
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
            var query = Sql.Query(() => new { tbl_staff = new Staff() }).
            Free("/* comment */").
            Select().
            From(db => db.tbl_staff);

            //execute.
            query.ToExecutor(_connection).Read();

        }

        [TestMethod]
        public void TestTableOne()
        {
            Sql.Log = l => Debug.Print(l);
            var query = Sql.Query(() => new
            {
                tbl_staff = new Staff()
            });
            var datas = query.ToExecutor(_connection).Read();

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
            var datas = query.ToExecutor(_connection).Read();

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
            var datas = query.Select().From(db => db.tbl_staff).ToExecutor(_connection).Read();

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
                total = Sql.Funcs.Sum(db.tbl_remuneration.money)
            }).
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
            GroupBy(db => db.tbl_staff.id, db => db.tbl_staff.name).Having(db => 10000 < Sql.Funcs.Sum(db.tbl_remuneration.money));

            var datas = query.ToExecutor(_connection).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}, {3}, {4}, {5}", e.name, e.total);
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
            Where(db => Sql.Words.Like(db.tbl_staff.name, "%son%"));

            var datas = query.ToExecutor(_connection).Read();
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
            Where(db => Sql.Words.In(db.tbl_staff.id, 1, 3));

            var datas = query.ToExecutor(_connection).Read();
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
            Where(db => Sql.Words.Between(db.tbl_staff.id, 1, 3));

            var datas = query.ToExecutor(_connection).Read();
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
            ToExecutor(_connection).Read();


        public IEnumerable<Data> ReadData2() =>
            Sql.Query<Data>().
            Select().
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
            Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000).
            OrderBy().ASC(db => db.tbl_staff.name).
            ToExecutor(_connection).Read();

        public IEnumerable<Staff> ReadData3() =>
            Sql.Query(() => new { tbl_staff = new Staff() }).
            Select().
            From(db => db.tbl_staff).
            ToExecutor(_connection).Read().Select(e => e.tbl_staff);

        [TestMethod]
        public void SelectSubQuery()
        {
            Sql.Log = l => Debug.Print(l);
            var define = Sql.Query<Data>();

            var sub = define.
                Select(db => new { total = Sql.Funcs.Sum(db.tbl_remuneration.money) }).
                From(db => db.tbl_remuneration);

            var datas = define.
                    Select(db => new
                    {
                        name = db.tbl_staff.name,
                        total = sub.Cast<decimal>()
                    }).
                    From(db => db.tbl_staff).
                    ToExecutor(_connection).Read();
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
                Select(db => new { name = db.tbl_staff.name }).
                From(db => db.tbl_staff).
                Where(db => Sql.Words.In(db.tbl_staff.id, sub.Cast<int>())).
                ToExecutor(_connection).Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}", e.name);
            }
        }

        [TestMethod]
        public void Insert()
        {
            Sql.Log = l => Debug.Print(l);

            var data = new tbl_data() { id = 1, val1 = 10, val2 = "a" };

            Delete();
            var count1 = Sql.Query<DataChangeTest>().
                InsertInto(db => db.tbl_data).
                Values(data).
                ToExecutor(_connection).Write();

            Delete();
            var count2 = Sql.Query<DataChangeTest>().
                InsertInto(db => db.tbl_data, tbl => tbl.id, tbl => tbl.val1).
                Values(data).
                ToExecutor(_connection).Write();
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
                Where(db => db.tbl_data.id == 1).
                ToExecutor(_connection).Write();

            var count2 = Sql.Query<DataChangeTest>().
                Update(db => db.tbl_data).
                Set().
                Assign(tbl => tbl.val1, tbl => tbl.val1 * 2).
                Where(db => db.tbl_data.id == 1).
                ToExecutor(_connection).Write();
        }

        public class DB
        {
            public Staff tbl_staff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
        }

        [TestMethod]
        public void MultiDB()
        {
            var query = Sql.Query<DB>().
            Select(db => new SelectData()
            {
                name = db.tbl_staff.name,
                payment_date = db.tbl_remuneration.payment_date,
                money = db.tbl_remuneration.money,
            }).
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id);

            var data1 = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
            var data2 = query.ToExecutor(new NpgsqlConnection(TestEnvironment.PostgresConnectionString)).Read();
            var data3 = query.ToExecutor(new SQLiteConnection("Data Source=" + TestEnvironment.SQLiteTest1Path)).Read();
        }

        //Distinct
        //```cs
        [TestMethod]
        public void DistinctCount()
        {
            Sql.Log = l => Debug.Print(l);
            var datas = Sql.Query<DB>().
                Select(db => new
                {
                    id = Sql.Funcs.Count(AggregatePredicate.Distinct, db.tbl_remuneration.staff_id)
                }).
                From(db => db.tbl_remuneration).GroupBy(db => db.tbl_remuneration.id).
                ToExecutor(_connection).Read();
        }
#endif
        /////////////////////////////////////////////////////////////////////
        //Insert Into とValues が弱くなるなー

        [TestMethod]
        public void Delete()
        {
            SqlOption.Log = l => Debug.Print(l);

            var count = Sql<DataChangeTest>.Create((db, x) => x.
                Delete().
                From(db.tbl_data)).
                ToExecutor(_connection).Write();

        }

        [TestMethod]
        public void DeleteWhere()
        {
            SqlOption.Log = l => Debug.Print(l);

            var count = Sql<DataChangeTest>.Create((db, x) => x.
                Delete().
                From(db.tbl_data).
                Where(db.tbl_data.id == 3)).
                ToExecutor(_connection).Write();
        }

        [TestMethod]
        public void SelectEx()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<Data>.Create((db, x) =>
                x.
                Select(new
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void GroupByEx()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<Data>.Create((db, x) =>
                x.
                Select(new
                {
                    name = db.tbl_staff.name,
                    count = x.Func().Count(db.tbl_remuneration.money),
                    total = x.Func().Sum(db.tbl_remuneration.money),
                    average = x.Func().Avg(db.tbl_remuneration.money),
                    minimum = x.Func().Min(db.tbl_remuneration.money),
                    maximum = x.Func().Max(db.tbl_remuneration.money),
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            var datas = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void OrderByEx()
        {
            SqlOption.Log = l => Debug.Print(l);

            var query = Sql<Data>.Create((db, x) => x.
                Select(new
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000).
                OrderBy().ASC(db.tbl_staff.id).DESC(db.tbl_remuneration.id));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void SubEx()
        {
            SqlOption.Log = l => Debug.Print(l);

            var q = Sql<Data>.Create((db, x) => x.
                Select(new
                {
                    id = db.tbl_staff.id,
                    rid = db.tbl_remuneration.id,
                    type = x.Case().
                                When(db.tbl_remuneration.money < 1000).Then("poverty").
                                When(4000 < db.tbl_remuneration.money).Then("rich").
                                Else("normal").
                            End().Cast<string>(),
                    total = x.Select(new { total = x.Func().Sum(db.tbl_remuneration.money) }).
                            From(db.tbl_remuneration).Cast<decimal>()
                }).
                From(db.tbl_staff).
                    Join(db.tbl_remuneration, db.tbl_staff.id == db.tbl_remuneration.staff_id).
                Where(100 < db.tbl_remuneration.money && db.tbl_remuneration.money < 500).
                OrderBy().ASC(db.tbl_remuneration.money));

            var datas = q.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void SubEx2()
        {
            SqlOption.Log = l => Debug.Print(l);

            var caseExp = Sql<Data>.Create((db, x) => x.
                Case().
                    When(db.tbl_remuneration.money < 1000).Then("poverty").
                    When(4000 < db.tbl_remuneration.money).Then("rich").
                    Else("normal").
                End());

            var subQuery = Sql<Data>.Create((db, x) => x.
                Select(new { total = x.Func().Sum(db.tbl_remuneration.money) }).
                            From(db.tbl_remuneration));

            var condition = Sql<Data>.Create((db, x) =>
                 100 < db.tbl_remuneration.money && db.tbl_remuneration.money < 500);

            var query = Sql<Data>.Create((db, x) => x.
                Select(new
                {
                    id = db.tbl_staff.id,
                    rid = db.tbl_remuneration.id,
                    type = caseExp.Cast<string>(),
                    total = subQuery.Cast<decimal>()
                }).
                From(db.tbl_staff).
                    Join(db.tbl_remuneration, db.tbl_staff.id == db.tbl_remuneration.staff_id).
                Where(condition.Cast<bool>()).
                OrderBy().ASC(db.tbl_remuneration.money));

            var datas = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void InsertEx()
        {
            SqlOption.Log = l => Debug.Print(l);

            var data = new tbl_data() { id = 1, val1 = 10, val2 = "a" };

            Delete();
            var query = Sql<DataChangeTest>.Create((db, x) =>
                x.
                InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val1, db.tbl_data.val2).
                Values(data.id, data.val1, data.val2));//TODO change style.

            query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Write();
        }

        [TestMethod]
        public void UpdateEx()
        {
            SqlOption.Log = l => Debug.Print(l);
            var count1 = Sql<DataChangeTest>.Create((db, x) => x.
                Update(db.tbl_data).
                Set().
                    Assign(db.tbl_data.val1, 100).
                    Assign(db.tbl_data.val2, "200").
                    Where(db.tbl_data.id == 1)).
                ToExecutor(_connection).Write();

            var count2 = Sql<DataChangeTest>.Create((db, x) => x.
                Update(db.tbl_data).
                Set().
                    Assign(db.tbl_data.val1, db.tbl_data.val1 * 2).
                    Where(db.tbl_data.id == 1)).
                ToExecutor(_connection).Write();
            //TODO change style to under.
            /*
            var query = Sql<DataChangeTest>.Create((db, x) => x.
            Update(db.tbl_data).
            Set(new tbl_data()
            {
                val1 = 100,
                val2 = "200"
            })).
            ToExecutor(_connection).Write();
            */
        }

        [TestMethod]
        public void Window()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<Data>.Create((db, x) => x.
                Select(new
                {
                    x = x.Window().AvgOver(db.tbl_remuneration.money).
                            PartitionBy(db.tbl_staff.name, db.tbl_remuneration.payment_date).
                            OrderBy().Asc(db.tbl_remuneration.money).Desc(db.tbl_remuneration.payment_date).
                            Rows(1, 1).Cast<decimal>(),
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void Lag()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<Data>.Create((db, x) =>
                x.
                Select(new
                {
                    x = x.Window().LagOver(db.tbl_remuneration.money, 1, 0).
                            PartitionBy(db.tbl_staff.name, db.tbl_remuneration.payment_date).
                            OrderBy().Asc(db.tbl_remuneration.money).Desc(db.tbl_remuneration.payment_date).Cast<decimal>(),
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();

        }

        int b = 0;
        [TestMethod]
        public void TestMeta()
        {
            int a = 0;
            var exp = GetExp();
            var query = Sql<Data>.Create((db, x) => a == 1 && a == b && exp.Cast<bool>());
            var info = query.ToSqlInfo(typeof(SqlConnection));

            Debug.Print(info.SqlText);
        }

        public ISqlExpression GetExp()
        {
            int a = 0;
            return Sql<Data>.Create((db, x) => a == 2 && a == b);
        }

        [TestMethod]
        public void TestMetaMethod()
        {
            var info1 = Sql<Data>.Create((db, x) => GetStatic1()).ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info1.SqlText);
            var info2 = Sql<Data>.Create((db, x) => GetInstance2()).ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info2.SqlText);

            //check cahe.
            info1 = Sql<Data>.Create((db, x) => GetStatic1()).ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info1.SqlText);
            info2 = Sql<Data>.Create((db, x) => GetInstance2()).ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info2.SqlText);
        }

        public static int GetStatic1() => 1;
        public int GetInstance2() => 2;
    }
}
