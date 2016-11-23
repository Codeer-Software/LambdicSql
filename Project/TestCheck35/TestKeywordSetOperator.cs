using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Test.Helper.DBProviderInfo;

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
    [TestClass]
    class TestKeywordSetOperator
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext);
            _connection.Open();
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Union()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Create(db =>
                Union().Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
UNION
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Union_All()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Create(db =>
                Union(true).Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
UNION ALL
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Union_All_False()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Create(db =>
                Union(false).Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
UNION
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Intersect()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Create(db =>
                Intersect().Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
INTERSECT
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Except()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Create(db =>
                Except().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Concat(target);

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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Except_All()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Create(db =>
                Except(true).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Concat(target);

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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Except_All_False()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Create(db =>
                Except(false).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Concat(target);

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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Minus()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Create(db =>
                Minus().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Concat(target);

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
