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
    public class TestSymbolClausesLimit
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
        public void Test_Limit()
        {
            if (!_connection.IsTarget(TargetDB.SQLite, TargetDB.MySQL, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(Asc(db.tbl_remuneration.id)).
                 Limit(1, 3)
                 );
            
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0, @p_1",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Limit_Start()
        {
            if (!_connection.IsTarget(TargetDB.SQLite, TargetDB.MySQL, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(Asc(db.tbl_remuneration.id)).
                 Limit(1, 3)
                 );

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0, @p_1",
1, 3);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Limit_Offset()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.SQLite, TargetDB.MySQL, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(Asc(db.tbl_remuneration.id)).
                 Limit(1).
                 Offset(3)
                 );

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0
OFFSET @p_1",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Limit_Offset_Start()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.SQLite, TargetDB.MySQL, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(Asc(db.tbl_remuneration.id)) + 
                 Limit(1) +
                 Offset(3)
                 );

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0
OFFSET @p_1",
1, 3);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OffsetRows_FetchNext()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(Asc(db.tbl_remuneration.id)).
                 OffsetRows(1).
                 FetchNextRowsOnly(3)
                 );

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
OFFSET @p_0 ROWS
FETCH NEXT @p_1 ROWS ONLY",
1, 3);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OffsetRows_FetchNext_Start()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(Asc(db.tbl_remuneration.id)) +
                 OffsetRows(1) +
                 FetchNextRowsOnly(3)
                 );

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
OFFSET @p_0 ROWS
FETCH NEXT @p_1 ROWS ONLY",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_RowNum()
        {
            if (!_connection.IsTarget(TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 Where(Between(RowNum(), 1, 3)).
                 OrderBy(Asc(db.tbl_remuneration.id))
                 );

            var datas = _connection.Query(sql).ToList();
            Assert.AreEqual(3, datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT *
FROM tbl_remuneration
WHERE ROWNUM BETWEEN :p_0 AND :p_1
ORDER BY
	tbl_remuneration.id ASC",
1, 3);
        }
    }
}
