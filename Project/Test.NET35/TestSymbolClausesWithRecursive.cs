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
    public class TestSymbolClausesWithRecursive
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

        class SelectedData
        {
            public int id { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_With()
        {
            if (!_connection.IsTarget(TargetDB.Oracle, TargetDB.Postgre, TargetDB.SQLite, TargetDB.DB2)) return;

            var sub1 = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff));

            var sub2 = Db<DB>.Sql(db =>
                Select(Asterisk(sub1.Body)).
                From(sub1));

            var sub3 = Db<DB>.Sql(db => sub2);
            
            var sql = Db<DB>.Sql(db =>
                With(sub1, sub2).
                Select(new SelectedData
                {
                    id = sub3.Body.id
                }).
                From(sub3)
            );

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"WITH
	sub1 AS
		(SELECT *
		FROM tbl_staff),
	sub2 AS
		(SELECT *
		FROM sub1)
SELECT
	sub3.id AS id
FROM sub2 sub3");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Recursive_1()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.SQLite)) return;

            var rec = Db<DB>.Sql(db => Recursive(new { val = 0 }));

            var select = Db<DB>.Sql(db =>
                Select(new object[] { 1 }).
                Union(All()).
                Select(new object[] { rec.Body.val + 1 }).
                From(rec).
                Where(rec.Body.val + 1 <= 5)
                );

            var sql = Db<DB>.Sql(db =>
                With(rec, select).
                Select(rec.Body.val).
                From(rec)
                );

            var datas = _connection.Query<SelectedData>(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"WITH
	rec(val) AS
		(SELECT
			@p_0
		UNION ALL
		SELECT
			(rec.val) + (@p_1)
		FROM rec
		WHERE ((rec.val) + (@p_2)) <= (@p_3))
SELECT
	rec.val
FROM rec", 1, 1, 1, 5);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Recursive_2()
        {
            var name = _connection.GetType().Name;
            if (name != "OracleConnection") return;

            var rec = Db<DB>.Sql(db => Recursive(new { val = 0 }));

            var select = Db<DB>.Sql(db =>
                Select(new object[] { 1 }).
                From(Dual).
                Union(All()).
                Select(new object[] { rec.Body.val + 1 }).
                From(rec).
                Where(rec.Body.val + 1 <= 5)
                );

            var sql = Db<DB>.Sql(db =>
                With(rec, select).
                Select(rec.Body.val).
                From(rec)
                );

            var datas = _connection.Query<SelectedData>(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"WITH
	rec(val) AS
		(SELECT
			:p_0
		FROM DUAL
		UNION ALL
		SELECT
			(rec.val) + (:p_1)
		FROM rec
		WHERE ((rec.val) + (:p_2)) <= (:p_3))
SELECT
	rec.val
FROM rec", 1, 1, 1, 5);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Recursive_3()
        {
            if (!_connection.IsTarget(TargetDB.Postgre)) return;

            var rec = Db<DB>.Sql(db => Recursive(new { val = 0 }));

            var select = Db<DB>.Sql(db =>
                Select(new object[] { 1 }).
                Union(All()).
                Select(new object[] { rec.Body.val + 1 }).
                From(rec).
                Where(rec.Body.val + 1 <= 5)
                );

            var sql = Db<DB>.Sql(db =>
                With(rec, select).
                Select(rec.Body.val).
                From(rec)
                );

            var datas = _connection.Query<SelectedData>(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"WITH
	RECURSIVE rec(val) AS
		(SELECT
			@p_0
		UNION ALL
		SELECT
			(rec.val) + (@p_1)
		FROM rec
		WHERE ((rec.val) + (@p_2)) <= (@p_3))
SELECT
	rec.val
FROM rec", 1, 1, 1, 5);
        }
    }
}
