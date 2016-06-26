﻿using LambdicSql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

            //make sql by lambda.
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
                mony = db.tbl_remuneration.money,
            }).
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
            Where(db => 3000 < db.tbl_remuneration.money).
                OrderBy().ASC(db => db.tbl_staff.name);

            //execute.
            var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.mony);
            }

            Assert.AreEqual(3, datas.Count());
            var first = datas.First();
            Assert.AreEqual("Jackson", first.name);
            Assert.AreEqual(new DateTime(2016, 1, 1), first.payment_date);
            Assert.AreEqual(3500, first.mony);
        }

        [TestMethod]
        public void GroupBy()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql by lambda.
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

            Assert.AreEqual(4, datas.Count());
            var first = datas.First();
            Assert.AreEqual("Emma", first.name);
            Assert.AreEqual(6000, first.total);
        }

        [TestMethod]
        public void WhereAnd()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql by lambda.
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
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
                
            Where();

            query = query.And(db => 3000 < db.tbl_remuneration.money).And(db => db.tbl_remuneration.money < 4000);

            var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();
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

            //make sql by lambda.
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
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
                
            Where().And().Like(db => db.tbl_staff.name, "%son%");

            var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}", e.tbl_staff.name, e.tbl_remuneration.money);
            }
        }

        [TestMethod]
        public void WhereIn()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql by lambda.
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
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).

            Where().And().In(db => db.tbl_staff.id, 1, 3);

            var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}", e.tbl_staff.name, e.tbl_remuneration.money);
            }
        }

        [TestMethod]
        public void WhereBetween()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql by lambda.
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
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).

            //TODO あー、何にでもLikeとかってかけて、Andがないから駄目よ。とかの方がいいか。
            Where().And().Between(db => db.tbl_staff.id, 1, 3);

            var datas = query.ToExecutor(TestEnvironment.ConnectionString).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}", e.tbl_staff.name, e.tbl_remuneration.money);
            }
        }
    }
}
