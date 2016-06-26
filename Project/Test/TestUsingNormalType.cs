using LambdicSql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace Test
{
    [TestClass]
    public class TestUsingNormalType
    {
        [TestMethod]
        public void TestNoramlClass()
        {
            var query = Sql.Using(() => new Db1());
            var data = new TestResult();
            data["table1@col1"] = "abc";
            data["table1@col2"] = 100;
            data["table2@col3"] = "def";
            data["table2@col4"] = 200;
            var obj = data.Create(query);
            Assert.AreEqual(obj.table1.col1, "abc");
            Assert.AreEqual(obj.table1.col2, 100);
            Assert.AreEqual(obj.table2.col3, "def");
            Assert.AreEqual(obj.table2.col4, 200);
        }

        [TestMethod]
        public void TestNoramlAndLambda()
        {
            var query = Sql.Using(() => new
            {
                table1 = new Tbl1(),
                table2 = new Tbl2()
            });
            var data = new TestResult();
            data["table1@col1"] = "abc";
            data["table1@col2"] = 100;
            data["table2@col3"] = "def";
            data["table2@col4"] = 200;
            var obj = data.Create(query);
            Assert.AreEqual(obj.table1.col1, "abc");
            Assert.AreEqual(obj.table1.col2, 100);
            Assert.AreEqual(obj.table2.col3, "def");
            Assert.AreEqual(obj.table2.col4, 200);
        }
        
        [TestMethod]
        public void UsingAndSelect()
        {
            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql by lambda.
            var query = Sql.Using(() => new DB()).
            Select(db => new SelectData()
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


        public class Tbl1
        {
            public string col1 { get; set; }
            public int col2 { get; set; }
        }
        public class Tbl2
        {
            public string col3 { get; set; }
            public int col4 { get; set; }
        }
        public class Db1
        {
            public Tbl1 table1 { get; set; }
            public Tbl2 table2 { get; set; }
        }

        
        public class Staff
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class Remuneration
        {
            public int id { get; set; }
            public string name { get; set; }
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
            public decimal mony { get; set; }
        }
    }
}
