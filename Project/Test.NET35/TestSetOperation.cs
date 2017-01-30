﻿using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;

namespace Test
{
    [TestClass]
    public class TestSetOperation
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
            var query = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Union(query);

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
            var query = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Union(All(), query);

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
        public void Test_Intersect()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Intersect(query);

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

            var query = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            var queryAfter = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Except(queryAfter);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
EXCEPT
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_All()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            var queryAfter = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Except(All(), queryAfter);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
EXCEPT ALL
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Minus()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            var queryAfter = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Minus(queryAfter);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
MINUS
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_Exp()
        {
            var query = Db<DB>.Sql(db => "SELECT * FROM tbl_staff".ToSql());
            query = query.Union(query);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
UNION
SELECT * FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_All_Exp()
        {
            var query = Db<DB>.Sql(db => "SELECT * FROM tbl_staff".ToSql());
            query = query.Union(All(), query);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
UNION ALL
SELECT * FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Intersect_Exp()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db => "SELECT * FROM tbl_staff".ToSql());
            query = query.Intersect(query);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
INTERSECT
SELECT * FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_Exp()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Db<DB>.Sql(db => "SELECT * FROM tbl_staff".ToSql());
            var queryAfter = Db<DB>.Sql(db => "SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)".ToSql());
            query = query.Except(queryAfter);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
EXCEPT
SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_All_Exp()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Db<DB>.Sql(db => "SELECT * FROM tbl_staff".ToSql());
            var queryAfter = Db<DB>.Sql(db => "SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)".ToSql());
            query = query.Except(All(), queryAfter);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
EXCEPT ALL
SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Minus_Exp()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Db<DB>.Sql(db => "SELECT * FROM tbl_staff".ToSql());
            var queryAfter = Db<DB>.Sql(db => "SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)".ToSql());
            query = query.Minus(queryAfter);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
MINUS
SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Concat()
        {
            var select = Db<DB>.Sql(db => Select(new { name = db.tbl_staff.name}));
            var from = Db<DB>.Sql(db => From(db.tbl_staff));
            var query = select + from;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.name AS name
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Concat_Exp()
        {
            var select = Db<DB>.Sql(db => "SELECT tbl_staff.name AS name".ToSql());
            var from = Db<DB>.Sql(db => "FROM tbl_staff".ToSql());
            var query = select + from;

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT tbl_staff.name AS name
FROM tbl_staff");
        }
    }
}