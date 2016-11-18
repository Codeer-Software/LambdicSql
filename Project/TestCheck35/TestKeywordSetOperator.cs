using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using static LambdicSql.Utils;
using System.Collections.Generic;
using LambdicSql.SqlBase;
using System.Data.SqlClient;
using System.Linq.Expressions;
using static TestCheck35.TestSynatax;


namespace TestCheck35
{
    class TestKeywordSetOperator
    {
        public IDbConnection _connection;

        public void TestInitialize(string testName, IDbConnection connection)
        {
            _connection = connection;
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
            public DateTime PaymentDate { get; set; }
            public decimal Money { get; set; }
        }

        public void Test_Union()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Union().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
UNION
SELECT *
FROM tbl_staff");
        }

        public void Test_Union_All()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Union(true).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
UNION ALL
SELECT *
FROM tbl_staff");
        }

        public void Test_Union_All_False()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Union(false).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
UNION
SELECT *
FROM tbl_staff");
        }

        public void Test_Intersect()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Intersect().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
INTERSECT
SELECT *
FROM tbl_staff");
        }

        public void Test_Except()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Except().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
EXCEPT
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)");
        }

        public void Test_Except_All()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Except(true).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
EXCEPT ALL
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)");
        }

        public void Test_Except_All_False()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Except(false).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
EXCEPT
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)");
        }

        public void Test_Minus()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Minus().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
MINUS
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)");
        }

    }
}
