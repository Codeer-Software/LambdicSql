using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestSymbolEtc
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
        public void Test_Asterisk_1()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk()).
                From(db.tbl_staff));

            var datas = _connection.Query<Staff>(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Asterisk_2()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Asterisk_3()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk<Staff>()).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Asc_Desc()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                OrderBy(Asc(db.tbl_staff.id), Desc(db.tbl_staff.name)));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
ORDER BY
	tbl_staff.id ASC,
	tbl_staff.name DESC");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Top()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer)) return;

            var sql = Db<DB>.Sql(db =>
                Select(Top(1), Asterisk(db.tbl_staff)).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT TOP 1 *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_All()
        {
            var sql = Db<DB>.Sql(db =>
                Select(All(), Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT ALL *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Distinct()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Distinct(), Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT DISTINCT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_RowNum()
        {
            if (!_connection.IsTarget(TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                 Select(RowNum()).
                 From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	ROWNUM
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentDate_1()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Date()
                }));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_DATE AS Val");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentDate_2()
        {
            if (!_connection.IsTarget(TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Date()
                }).From(Dual));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_DATE AS Val
FROM DUAL");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentDate_3()
        {
            if (!_connection.IsTarget(TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Date()
                }).From(SysIBM.SysDummy1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT DATE AS Val
FROM SYSIBM.SYSDUMMY1");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTime_1()
        {
            if (!_connection.IsTarget(TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Time()
                }));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_TIME AS Val");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTime_2()
        {
            if (!_connection.IsTarget(TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Time()
                }).
                From(SysIBM.SysDummy1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT TIME AS Val
FROM SYSIBM.SYSDUMMY1");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTimeStamp_1()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
            Select(new
            {
                Val = Current_TimeStamp()
            }));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_TIMESTAMP AS Val");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTimeStamp_2()
        {
            if (!_connection.IsTarget(TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_TimeStamp()
                }).
                From(Dual));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_TIMESTAMP AS Val
FROM DUAL");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTimeStamp_3()
        {
            if (!_connection.IsTarget(TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_TimeStamp()
                }).
                From(SysIBM.SysDummy1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT TIMESTAMP AS Val
FROM SYSIBM.SYSDUMMY1");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Dual()
        {
            if (!_connection.IsTarget(TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Date()
                }).From(Dual));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_DATE AS Val
FROM DUAL");
        }
    }
}
