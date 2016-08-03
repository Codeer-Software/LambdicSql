using LambdicSql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Test
{
    /*
    [TestClass]
    public class TestDbInfoNormalType
    {
        [TestMethod]
        public void TestNoramlClass()
        {
            var query = Sql.Query(() => new Db1());
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
        public void TestNoramlClassGeneric()
        {
            var query = Sql.Query<Db1>();
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
            Sql.Log = l => Debug.Print(l);

            var query = Sql.Query(() => new
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
    }*/
}
