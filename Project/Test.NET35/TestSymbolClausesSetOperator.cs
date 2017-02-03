using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using static Test.Helper.DBProviderInfo;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class TestSymbolClausesSetOperator
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
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Union().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
UNION
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_Start()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff)
                + Union() +
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
UNION
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_All()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Union(All()).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
UNION ALL
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_All_Start()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff)
                + Union(All()) +
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
UNION ALL
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Intersect()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.SQLite, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Intersect().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
INTERSECT
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Intersect_Start()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.SQLite, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff)
                 + Intersect() +
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
INTERSECT
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.SQLite, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Except().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
EXCEPT
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_Start()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.SQLite, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff)
                 + Except() +
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Except(All()).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
EXCEPT ALL
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_All_Start()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff)
                + Except(All()) +
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Minus().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
MINUS
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Minus_Start()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff)
                + Minus() +
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
MINUS
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }
    }
}
