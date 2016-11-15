using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using LambdicSql.SqlBase;
using System.Data.SqlClient;
using System.Linq.Expressions;
using static TestCheck35.TestKeywordSelect.TestUtility;

namespace TestCheck35
{
    public class TestKeywordCase
    {
        public IDbConnection _connection;

        public void TestInitialize(string testName, IDbConnection connection)
        {
            _connection = connection;
        }

        [SqlSyntax]
        public static class TestUtility
        {
            public static IQuery<Non> Empty() => null;
            static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods) => string.Empty;
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

        public class Data
        {
            public int id { get; set; }
            public int val1 { get; set; }
            public string val2 { get; set; }
        }

        public class DB
        {
            public Staff tbl_staff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
            public Data tbl_data { get; set; }
        }

        public class SelectData
        {
            public string Type { get; set; }
        }

        public void Test_Case1()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Type = Case().
                                When(db.tbl_staff.id == 3).Then("x").
                                When(db.tbl_staff.id == 4).Then("y").
                                Else("z").
                            End()
                }).
                From(db.tbl_staff));
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(datas.Any(e => e.Type == "x"));
            Assert.IsTrue(datas.Any(e => e.Type == "y"));
            Assert.IsTrue(datas.Any(e => e.Type == "z"));
        }

        public void Test_Case2()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Type = Case().
                                When(db.tbl_staff.id == 3).Then("x").
                                When(db.tbl_staff.id == 4).Then("y").
                            End()
                }).
                From(db.tbl_staff));
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(datas.Any(e => e.Type == "x"));
            Assert.IsTrue(datas.Any(e => e.Type == "y"));
            Assert.IsFalse(datas.Any(e => e.Type == "z"));
        }

        public void Test_Case3()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Type = Case(db.tbl_staff.id).
                                When(3).Then("x").
                                When(4).Then("y").
                                Else("z").
                            End()
                }).
                From(db.tbl_staff));
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(datas.Any(e => e.Type == "x"));
            Assert.IsTrue(datas.Any(e => e.Type == "y"));
            Assert.IsTrue(datas.Any(e => e.Type == "z"));
        }
    }
}
